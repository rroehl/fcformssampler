using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
// -------------------------------------------------------------------------
// if using .NET Framework
// https://docs.microsoft.com/en-us/dotnet/api/system.web.script.serialization.javascriptserializer?view=netframework-4.8
// This requires including the reference to System.Web.Extensions in your project
//using System.Web.Script.Serialization;
// -------------------------------------------------------------------------
// if using .Net Core
// https://docs.microsoft.com/en-us/dotnet/api/system.text.json?view=net-5.0
using System.Text.Json;
using System.Text;
// ----------------------

namespace StockPriceApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void LogEvent(string eventType, string details)
        {
            string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.log");
            string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}, {eventType}, {details}";
            System.IO.File.AppendAllText(logPath, logLine + Environment.NewLine);
        }

        private async void btnGetPrice_Click(object sender, EventArgs e)
        {
            LogEvent("ButtonClick", "Get Price and save to Postgres");
            string ticker = txtTicker.Text;
            if (string.IsNullOrWhiteSpace(ticker))
            {
                LogEvent("InputError", "Ticker symbol was empty");
                MessageBox.Show("Please enter a valid ticker symbol.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                lstPrices.Items.Clear();
                var sections = await GetStockPriceSectionsAsync(ticker);
                foreach (var section in sections)
                {
                    if (!string.IsNullOrWhiteSpace(section))
                        lstPrices.Items.Add(section.Replace("\n", " ").Trim());
                }
            }
            catch (Exception ex)
            {
                      
                LogEvent("Exception", $"btnGetPrice_Click: {ex.Message}");
                MessageBox.Show($"Error fetching stock price: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<List<string>> GetStockPriceSectionsAsync(string ticker)
        {
            string appKey = System.IO.File.ReadAllText("appkey.txt").Trim();
            string url = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={ticker}&interval=5min&apikey={appKey}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json); // Print the raw JSON to the console for debugging
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    var root = doc.RootElement;
                    var sectionsList = new List<string>();
                    foreach (var section in root.EnumerateObject())
                    {
                        if (section.Name == "Meta Data")
                            continue;
                        if (section.Value.ValueKind == JsonValueKind.Object)
                        {
                            // Write all entries (all timestamps) to the database
                            foreach (var entry in section.Value.EnumerateObject())
                            {
                                var sb = new StringBuilder();
                                sb.AppendLine($"Section: {section.Name}");
                                sb.AppendLine($"  {entry.Name}:");
                                if (entry.Value.ValueKind == JsonValueKind.Object)
                                {
                                    foreach (var field in entry.Value.EnumerateObject())
                                    {
                                        sb.AppendLine($"    {field.Name}: {field.Value}");
                                    }
                                }
                                else
                                {
                                    sb.AppendLine($"    {entry.Value}");
                                }
                                sectionsList.Add(sb.ToString().TrimEnd());
                                // Insert each entry into PostgreSQL
                                await InsertStockDataToPostgres(ticker, entry.Value, entry.Name);
                            }
                        }
                        else
                        {
                            var sb = new StringBuilder();
                            sb.AppendLine($"Section: {section.Name}");
                            sb.AppendLine($"  {section.Value}");
                            sectionsList.Add(sb.ToString().TrimEnd());
                        }
                    }
                    return sectionsList;
                }
            }
        }

        private async Task InsertStockDataToPostgres(string symbol, JsonElement entry, string timestamp)
        {
            // Map Alpha Vantage keys to your table columns
            decimal? open = null, close = null, high = null, low = null, volume = null;
            foreach (var field in entry.EnumerateObject())
            {
                switch (field.Name)
                {
                    case var s when s.Contains("open"):
                        decimal.TryParse(field.Value.ToString(), out var o); open = o; break;
                    case var s when s.Contains("close"):
                        decimal.TryParse(field.Value.ToString(), out var c); close = c; break;
                    case var s when s.Contains("high"):
                        decimal.TryParse(field.Value.ToString(), out var h); high = h; break;
                    case var s when s.Contains("low"):
                        decimal.TryParse(field.Value.ToString(), out var l); low = l; break;
                    case var s when s.Contains("volume"):
                        decimal.TryParse(field.Value.ToString(), out var v); volume = v; break;
                }
            }
            // Read the database password from file
            string password = System.IO.File.ReadAllText("dbpassword.txt").Trim();
            var connString = $"Host=127.0.0.1;Port=5432;Username=postgres;Password={password};Database=datadog";
            using (var conn = new NpgsqlConnection(connString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand(@"INSERT INTO stocks (symbol, open_price, close_price, high, low_price, volume, timestamp) VALUES (@symbol, @open, @close, @high, @low, @volume, @timestamp)", conn))
                {
                    cmd.Parameters.AddWithValue("@symbol", symbol);
                    cmd.Parameters.AddWithValue("@open", (object?)open ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@close", (object?)close ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@high", (object?)high ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@low", (object?)low ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@volume", (object?)volume ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@timestamp", DateTime.Parse(timestamp));
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private void btnThrowException_Click(object sender, EventArgs e)
        {
            LogEvent("ButtonClick", "Throw Exception button pressed");
            try
            {
                throw new Exception("This is a test exception thrown by the Throw Exception button.");
            }
            catch (Exception ex)
            {
                LogEvent("Exception", $"btnThrowException_Click: {ex.Message}");
                MessageBox.Show($"Exception caught: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnInefficient_Click(object sender, EventArgs e)
        {
            LogEvent("ButtonClick", "Run Inefficient Code (5s) button pressed");
            var previousCursor = this.Cursor;
            try
            {
                this.UseWaitCursor = true; // Show hourglass for the whole form
                await Task.Run(() => {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    long sum = 0;
                    while (watch.Elapsed.TotalSeconds < 5)
                    {
                        for (int i = 0; i < 100000; i++)
                        {
                            sum += i * i;
                        }
                    }
                    watch.Stop();
                });
                MessageBox.Show("Inefficient code completed.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogEvent("Exception", $"btnInefficient_Click: {ex.Message}");
                throw;
            }
            finally
            {
                this.UseWaitCursor = false; // Restore cursor
                this.Cursor = previousCursor;
            }
        }

        private async void btnGetFromPostgres_Click(object sender, EventArgs e)
        {
            LogEvent("ButtonClick", "Get price from Postgres button pressed");
            lstDbRows.Items.Clear();
            // Read the database password from file
            string password = System.IO.File.ReadAllText("dbpassword.txt").Trim();
            var connString = $"Host=127.0.0.1;Port=5432;Username=postgres;Password={password};Database=datadog";
            using (var conn = new Npgsql.NpgsqlConnection(connString))
            {
                await conn.OpenAsync();
                using (var cmd = new Npgsql.NpgsqlCommand("SELECT id, symbol, open_price, close_price, high, low_price, volume, timestamp FROM stocks ORDER BY timestamp DESC", conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string row = $"ID: {reader[0]}, Symbol: {reader[1]}, Open: {reader[2]}, Close: {reader[3]}, High: {reader[4]}, Low: {reader[5]}, Volume: {reader[6]}, Timestamp: {reader[7]}";
                        lstDbRows.Items.Add(row);
                    }
                }
            }
        }

        private async void btnGetBySymbol_Click(object sender, EventArgs e)
        {
            LogEvent("ButtonClick", "Get stock price by symbol button pressed");
            lstSymbolPrices.Items.Clear();
            string symbol = txtSymbolQuery.Text.Trim();
            if (string.IsNullOrWhiteSpace(symbol))
            {
                LogEvent("InputError", "Symbol query was empty");
                MessageBox.Show("Please enter a symbol to search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Read the database password from file
            string password = System.IO.File.ReadAllText("dbpassword.txt").Trim();
            var connString = $"Host=127.0.0.1;Port=5432;Username=postgres;Password={password};Database=datadog";
            using (var conn = new Npgsql.NpgsqlConnection(connString))
            {
                await conn.OpenAsync();
                // Make the symbol query case-insensitive using ILIKE
                using (var cmd = new Npgsql.NpgsqlCommand("SELECT id, symbol, open_price, close_price, high, low_price, volume, timestamp FROM stocks WHERE symbol ILIKE @symbol ORDER BY timestamp DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@symbol", symbol);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string row = $"ID: {reader[0]}, Symbol: {reader[1]}, Open: {reader[2]}, Close: {reader[3]}, High: {reader[4]}, Low: {reader[5]}, Volume: {reader[6]}, Timestamp: {reader[7]}";
                            lstSymbolPrices.Items.Add(row);
                        }
                    }
                }
            }
        }
    }
}