namespace StockPriceApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtTicker;
        private System.Windows.Forms.Button btnGetPrice;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.ListBox lstPrices;
        private System.Windows.Forms.Button btnThrowException;
        private System.Windows.Forms.Button btnInefficient;
        private System.Windows.Forms.ListBox lstDbRows;
        private System.Windows.Forms.Button btnGetFromPostgres;
        private System.Windows.Forms.ListBox lstSymbolPrices;
        private System.Windows.Forms.Button btnGetBySymbol;
        private System.Windows.Forms.TextBox txtSymbolQuery;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtTicker = new System.Windows.Forms.TextBox();
            this.btnGetPrice = new System.Windows.Forms.Button();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lstPrices = new System.Windows.Forms.ListBox();
            this.btnThrowException = new System.Windows.Forms.Button();
            this.btnInefficient = new System.Windows.Forms.Button();
            this.lstDbRows = new System.Windows.Forms.ListBox();
            this.btnGetFromPostgres = new System.Windows.Forms.Button();
            this.lstSymbolPrices = new System.Windows.Forms.ListBox();
            this.btnGetBySymbol = new System.Windows.Forms.Button();
            this.txtSymbolQuery = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtTicker
            // 
            this.txtTicker.Location = new System.Drawing.Point(12, 12);
            this.txtTicker.Name = "txtTicker";
            this.txtTicker.Size = new System.Drawing.Size(100, 23);
            this.txtTicker.TabIndex = 0;
            // 
            // btnGetPrice
            // 
            this.btnGetPrice.Location = new System.Drawing.Point(118, 12);
            this.btnGetPrice.Name = "btnGetPrice";
            this.btnGetPrice.Size = new System.Drawing.Size(250, 40); // increased size for display text
            this.btnGetPrice.TabIndex = 1;
            this.btnGetPrice.Text = "Get Price and save to Postgres";
            this.btnGetPrice.UseVisualStyleBackColor = true;
            this.btnGetPrice.Click += new System.EventHandler(this.btnGetPrice_Click);
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(12, 50);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(38, 15);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "Price: ";
            // 
            // lstPrices
            // 
            this.lstPrices.Location = new System.Drawing.Point(12, 80);
            this.lstPrices.Name = "lstPrices";
            this.lstPrices.Size = new System.Drawing.Size(1200, 600);
            this.lstPrices.TabIndex = 3;
            this.lstPrices.HorizontalScrollbar = true;
            this.lstPrices.ScrollAlwaysVisible = true;
            this.lstPrices.IntegralHeight = false;
            // 
            // btnThrowException
            // 
            this.btnThrowException.Location = new System.Drawing.Point(12, 700);
            this.btnThrowException.Name = "btnThrowException";
            this.btnThrowException.Size = new System.Drawing.Size(200, 40);
            this.btnThrowException.TabIndex = 4;
            this.btnThrowException.Text = "Throw Exception";
            this.btnThrowException.UseVisualStyleBackColor = true;
            this.btnThrowException.Click += new System.EventHandler(this.btnThrowException_Click);
            // 
            // btnInefficient
            // 
            this.btnInefficient.Location = new System.Drawing.Point(230, 700);
            this.btnInefficient.Name = "btnInefficient";
            this.btnInefficient.Size = new System.Drawing.Size(250, 40);
            this.btnInefficient.TabIndex = 5;
            this.btnInefficient.Text = "Run Inefficient Code (5s)";
            this.btnInefficient.UseVisualStyleBackColor = true;
            this.btnInefficient.Click += new System.EventHandler(this.btnInefficient_Click);
            // 
            // lstDbRows
            // 
            this.lstDbRows.Location = new System.Drawing.Point(12, 760);
            this.lstDbRows.Name = "lstDbRows";
            this.lstDbRows.Size = new System.Drawing.Size(1200, 600); // match lstPrices size
            this.lstDbRows.TabIndex = 6;
            this.lstDbRows.HorizontalScrollbar = true;
            this.lstDbRows.ScrollAlwaysVisible = true;
            this.lstDbRows.IntegralHeight = false;
            // 
            // btnGetFromPostgres
            // 
            this.btnGetFromPostgres.Location = new System.Drawing.Point(500, 700);
            this.btnGetFromPostgres.Name = "btnGetFromPostgres";
            this.btnGetFromPostgres.Size = new System.Drawing.Size(250, 40);
            this.btnGetFromPostgres.TabIndex = 7;
            this.btnGetFromPostgres.Text = "Get price from Postgres";
            this.btnGetFromPostgres.UseVisualStyleBackColor = true;
            this.btnGetFromPostgres.Click += new System.EventHandler(this.btnGetFromPostgres_Click);
            // 
            // txtSymbolQuery
            // 
            this.txtSymbolQuery.Location = new System.Drawing.Point(12, 1370);
            this.txtSymbolQuery.Name = "txtSymbolQuery";
            this.txtSymbolQuery.Size = new System.Drawing.Size(100, 23);
            this.txtSymbolQuery.TabIndex = 8;
            // 
            // btnGetBySymbol
            // 
            this.btnGetBySymbol.Location = new System.Drawing.Point(120, 1370);
            this.btnGetBySymbol.Name = "btnGetBySymbol";
            this.btnGetBySymbol.Size = new System.Drawing.Size(250, 40);
            this.btnGetBySymbol.TabIndex = 9;
            this.btnGetBySymbol.Text = "Get stock price by symbol";
            this.btnGetBySymbol.UseVisualStyleBackColor = true;
            this.btnGetBySymbol.Click += new System.EventHandler(this.btnGetBySymbol_Click);
            // 
            // lstSymbolPrices
            // 
            this.lstSymbolPrices.Location = new System.Drawing.Point(12, 1420);
            this.lstSymbolPrices.Name = "lstSymbolPrices";
            this.lstSymbolPrices.Size = new System.Drawing.Size(1200, 300);
            this.lstSymbolPrices.TabIndex = 10;
            this.lstSymbolPrices.HorizontalScrollbar = true;
            this.lstSymbolPrices.ScrollAlwaysVisible = true;
            this.lstSymbolPrices.IntegralHeight = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 1800); // ensure enough space
            this.Controls.Add(this.lstSymbolPrices);
            this.Controls.Add(this.btnGetBySymbol);
            this.Controls.Add(this.txtSymbolQuery);
            this.Controls.Add(this.btnGetFromPostgres);
            this.Controls.Add(this.lstDbRows);
            this.Controls.Add(this.btnInefficient);
            this.Controls.Add(this.btnThrowException);
            this.Controls.Add(this.lstPrices);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.btnGetPrice);
            this.Controls.Add(this.txtTicker);
            this.Name = "MainForm";
            this.Text = "Stock Price App";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
