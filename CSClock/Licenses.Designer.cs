/*
CSClock - a program which keeps track of your computer time
Copyright (C) 2017  Viktor J

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

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
            this.btn_CustomSettingsProvider = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.btn_Squirrel_Windows = new System.Windows.Forms.Button();
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
            // btn_CustomSettingsProvider
            // 
            this.btn_CustomSettingsProvider.Location = new System.Drawing.Point(153, 81);
            this.btn_CustomSettingsProvider.Name = "btn_CustomSettingsProvider";
            this.btn_CustomSettingsProvider.Size = new System.Drawing.Size(201, 23);
            this.btn_CustomSettingsProvider.TabIndex = 7;
            this.btn_CustomSettingsProvider.Text = "CustomSettingsProvider by CodeChimp";
            this.btn_CustomSettingsProvider.UseVisualStyleBackColor = true;
            this.btn_CustomSettingsProvider.Click += new System.EventHandler(this.button_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Location = new System.Drawing.Point(12, 146);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(586, 276);
            this.webBrowser1.TabIndex = 8;
            this.webBrowser1.Visible = false;
            // 
            // btn_Squirrel_Windows
            // 
            this.btn_Squirrel_Windows.Location = new System.Drawing.Point(360, 81);
            this.btn_Squirrel_Windows.Name = "btn_Squirrel_Windows";
            this.btn_Squirrel_Windows.Size = new System.Drawing.Size(157, 23);
            this.btn_Squirrel_Windows.TabIndex = 9;
            this.btn_Squirrel_Windows.Text = "Squirrel.Windows by GitHub";
            this.btn_Squirrel_Windows.UseVisualStyleBackColor = true;
            this.btn_Squirrel_Windows.Click += new System.EventHandler(this.button_Click);
            // 
            // Licenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 434);
            this.Controls.Add(this.btn_Squirrel_Windows);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.btn_CustomSettingsProvider);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.l_lcOf);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_JsonNET);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_CSClock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Licenses";
            this.Text = "Licenses - CSClock";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Licenses_FormClosing);
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
        private System.Windows.Forms.Button btn_CustomSettingsProvider;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btn_Squirrel_Windows;
    }
}