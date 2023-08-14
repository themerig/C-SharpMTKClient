namespace MtkClientC
{
    partial class Form1
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Start = new System.Windows.Forms.Button();
            this.portList = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(9, 16);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(351, 377);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Location = new System.Drawing.Point(418, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 402);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(12, 403);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(400, 35);
            this.Start.TabIndex = 2;
            this.Start.Text = "MTK ByPass";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.start_Click);
            // 
            // portList
            // 
            this.portList.FormattingEnabled = true;
            this.portList.Location = new System.Drawing.Point(418, 12);
            this.portList.Name = "portList";
            this.portList.Size = new System.Drawing.Size(370, 21);
            this.portList.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.portList);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "C# Mtk Client";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Start;
        public System.Windows.Forms.RichTextBox richTextBox1;
        public System.Windows.Forms.ComboBox portList;
    }
}

