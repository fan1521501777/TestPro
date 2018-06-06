namespace AppiumTest
{
    partial class runDerver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStrat = new System.Windows.Forms.Button();
            this.txtAppiumPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNodePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rtxtPageSource = new System.Windows.Forms.RichTextBox();
            this.btnGetPageSource = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStrat
            // 
            this.btnStrat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStrat.Location = new System.Drawing.Point(1204, 8);
            this.btnStrat.Name = "btnStrat";
            this.btnStrat.Size = new System.Drawing.Size(75, 23);
            this.btnStrat.TabIndex = 0;
            this.btnStrat.Text = "启动服务";
            this.btnStrat.UseVisualStyleBackColor = true;
            this.btnStrat.Click += new System.EventHandler(this.btnStrat_Click);
            // 
            // txtAppiumPath
            // 
            this.txtAppiumPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppiumPath.Location = new System.Drawing.Point(95, 10);
            this.txtAppiumPath.Name = "txtAppiumPath";
            this.txtAppiumPath.Size = new System.Drawing.Size(1103, 21);
            this.txtAppiumPath.TabIndex = 1;
            this.txtAppiumPath.Text = @"C:\Users\msi\AppData\Local\Programs\appium-desktop";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "AppiumPath：";
            // 
            // txtNodePath
            // 
            this.txtNodePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNodePath.Location = new System.Drawing.Point(95, 37);
            this.txtNodePath.Name = "txtNodePath";
            this.txtNodePath.Size = new System.Drawing.Size(1103, 21);
            this.txtNodePath.TabIndex = 3;
            this.txtNodePath.Text = "C:\\Program Files\\nodejs\\node.exe";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "NodePath：";
            // 
            // rtxtPageSource
            // 
            this.rtxtPageSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtPageSource.Location = new System.Drawing.Point(12, 64);
            this.rtxtPageSource.Name = "rtxtPageSource";
            this.rtxtPageSource.Size = new System.Drawing.Size(1267, 678);
            this.rtxtPageSource.TabIndex = 5;
            this.rtxtPageSource.Text = "";
            // 
            // btnGetPageSource
            // 
            this.btnGetPageSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetPageSource.Location = new System.Drawing.Point(1204, 37);
            this.btnGetPageSource.Name = "btnGetPageSource";
            this.btnGetPageSource.Size = new System.Drawing.Size(75, 23);
            this.btnGetPageSource.TabIndex = 6;
            this.btnGetPageSource.Text = "PageSource";
            this.btnGetPageSource.UseVisualStyleBackColor = true;
            this.btnGetPageSource.Click += new System.EventHandler(this.btnGetPageSource_Click);
            // 
            // runDerver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 754);
            this.Controls.Add(this.btnGetPageSource);
            this.Controls.Add(this.rtxtPageSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNodePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAppiumPath);
            this.Controls.Add(this.btnStrat);
            this.Name = "runDerver";
            this.Text = "wzry";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStrat;
        private System.Windows.Forms.TextBox txtAppiumPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNodePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtxtPageSource;
        private System.Windows.Forms.Button btnGetPageSource;
    }
}