namespace CSClock
{
    partial class Licenses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Licenses));
            this.btn_CSClock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_JsonNET = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.l_lcOf = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_CSClock
            // 
            this.btn_CSClock.Location = new System.Drawing.Point(12, 29);
            this.btn_CSClock.Name = "btn_CSClock";
            this.btn_CSClock.Size = new System.Drawing.Size(75, 23);
            this.btn_CSClock.TabIndex = 0;
            this.btn_CSClock.Text = "CSClock";
            this.btn_CSClock.UseVisualStyleBackColor = true;
            this.btn_CSClock.Click += new System.EventHandler(this.button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CSClock:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "3rd party libraries:";
            // 
            // btn_JsonNET
            // 
            this.btn_JsonNET.Location = new System.Drawing.Point(12, 81);
            this.btn_JsonNET.Name = "btn_JsonNET";
            this.btn_JsonNET.Size = new System.Drawing.Size(135, 23);
            this.btn_JsonNET.TabIndex = 2;
            this.btn_JsonNET.Text = "Json.NET by Newtonsoft";
            this.btn_JsonNET.UseVisualStyleBackColor = true;
            this.btn_JsonNET.Click += new System.EventHandler(this.button_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 146);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(586, 276);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // l_lcOf
            // 
            this.l_lcOf.AutoSize = true;
            this.l_lcOf.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_lcOf.Location = new System.Drawing.Point(12, 122);
            this.l_lcOf.Name = "l_lcOf";
            this.l_lcOf.Size = new System.Drawing.Size(82, 21);
            this.l_lcOf.TabIndex = 5;
            this.l_lcOf.Text = "License of:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(124, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(474, 54);
            this.label3.TabIndex = 6;
            this.label3.Text = "Click a button to show the license of that application/library";
            // 
            // Licenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 434);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.l_lcOf);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_JsonNET);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_CSClock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Licenses";
            this.Text = "Licenses - CSClock";
            this.Load += new System.EventHandler(this.Licenses_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_CSClock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_JsonNET;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label l_lcOf;
        private System.Windows.Forms.Label label3;
    }
}