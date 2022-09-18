namespace PdfPortfolioCreator
{
    partial class PdfPortfolioCreatorForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DocumentDisplay = new System.Windows.Forms.PictureBox();
            this.AddFiles = new System.Windows.Forms.Button();
            this.CreatePortfolio = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // DocumentDisplay
            // 
            this.DocumentDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DocumentDisplay.Location = new System.Drawing.Point(40, 55);
            this.DocumentDisplay.Name = "DocumentDisplay";
            this.DocumentDisplay.Size = new System.Drawing.Size(702, 348);
            this.DocumentDisplay.TabIndex = 0;
            this.DocumentDisplay.TabStop = false;
            // 
            // AddFiles
            // 
            this.AddFiles.Location = new System.Drawing.Point(265, 410);
            this.AddFiles.Name = "AddFiles";
            this.AddFiles.Size = new System.Drawing.Size(95, 28);
            this.AddFiles.TabIndex = 2;
            this.AddFiles.Text = "Add Files";
            this.AddFiles.UseVisualStyleBackColor = true;
            this.AddFiles.Click += new System.EventHandler(this.AddFile_Click);
            // 
            // CreatePortfolio
            // 
            this.CreatePortfolio.Location = new System.Drawing.Point(425, 410);
            this.CreatePortfolio.Name = "CreatePortfolio";
            this.CreatePortfolio.Size = new System.Drawing.Size(116, 28);
            this.CreatePortfolio.TabIndex = 3;
            this.CreatePortfolio.Text = "Create Portfolio";
            this.CreatePortfolio.UseVisualStyleBackColor = true;
            this.CreatePortfolio.Click += new System.EventHandler(this.CreatePortfolio_Click);
            // 
            // PdfPortfolioCreatorForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 421);
            this.Controls.Add(this.CreatePortfolio);
            this.Controls.Add(this.AddFiles);
            this.Controls.Add(this.DocumentDisplay);
            this.Name = "PdfPortfolioCreatorForm";
            this.Text = "PDF Portfolio Creator";
            ((System.ComponentModel.ISupportInitialize)(this.DocumentDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //Note that these properties are declared outside of the "InitializeComponent()" block of code
        public PictureBox DocumentDisplay;
        private Button AddFiles;
        private Button CreatePortfolio;
    }
}