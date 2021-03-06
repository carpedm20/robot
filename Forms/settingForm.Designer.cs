﻿namespace robot
{
    partial class SettingForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.alarmLabel = new System.Windows.Forms.Label();
            this.alarmSwitch = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.autoLoginLabel = new System.Windows.Forms.Label();
            this.autoLoginSwitch = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.saySwitch = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.sayLabel = new System.Windows.Forms.Label();
            this.autoLoginTip = new System.Windows.Forms.ToolTip(this.components);
            this.alarmTip = new System.Windows.Forms.ToolTip(this.components);
            this.sayTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.facebookSwitch = new DevComponents.DotNetBar.Controls.SwitchButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // alarmLabel
            // 
            this.alarmLabel.AutoSize = true;
            this.alarmLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.alarmLabel.ForeColor = System.Drawing.Color.Black;
            this.alarmLabel.Location = new System.Drawing.Point(12, 18);
            this.alarmLabel.Name = "alarmLabel";
            this.alarmLabel.Size = new System.Drawing.Size(73, 12);
            this.alarmLabel.TabIndex = 2;
            this.alarmLabel.Text = "새 글 알리미";
            this.alarmLabel.Visible = false;
            // 
            // alarmSwitch
            // 
            this.alarmSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.alarmSwitch.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.alarmSwitch.ForeColor = System.Drawing.Color.Black;
            this.alarmSwitch.Location = new System.Drawing.Point(149, 12);
            this.alarmSwitch.Name = "alarmSwitch";
            this.alarmSwitch.OffText = "끔";
            this.alarmSwitch.OnText = "켬";
            this.alarmSwitch.Size = new System.Drawing.Size(66, 22);
            this.alarmSwitch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.alarmSwitch.TabIndex = 3;
            this.alarmSwitch.Value = true;
            this.alarmSwitch.ValueObject = "Y";
            this.alarmSwitch.Visible = false;
            this.alarmSwitch.ValueChanged += new System.EventHandler(this.alarmSwitch_ValueChanged);
            // 
            // autoLoginLabel
            // 
            this.autoLoginLabel.AutoSize = true;
            this.autoLoginLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLoginLabel.ForeColor = System.Drawing.Color.Black;
            this.autoLoginLabel.Location = new System.Drawing.Point(12, 18);
            this.autoLoginLabel.Name = "autoLoginLabel";
            this.autoLoginLabel.Size = new System.Drawing.Size(69, 12);
            this.autoLoginLabel.TabIndex = 4;
            this.autoLoginLabel.Text = "자동 로그인";
            // 
            // autoLoginSwitch
            // 
            this.autoLoginSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.autoLoginSwitch.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.autoLoginSwitch.ForeColor = System.Drawing.Color.Black;
            this.autoLoginSwitch.Location = new System.Drawing.Point(149, 13);
            this.autoLoginSwitch.Name = "autoLoginSwitch";
            this.autoLoginSwitch.OffText = "끔";
            this.autoLoginSwitch.OnText = "켬";
            this.autoLoginSwitch.Size = new System.Drawing.Size(66, 22);
            this.autoLoginSwitch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.autoLoginSwitch.TabIndex = 5;
            this.autoLoginSwitch.ValueChanged += new System.EventHandler(this.autoLoginSwitch_ValueChanged);
            // 
            // saySwitch
            // 
            this.saySwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.saySwitch.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.saySwitch.ForeColor = System.Drawing.Color.Black;
            this.saySwitch.Location = new System.Drawing.Point(149, 53);
            this.saySwitch.Name = "saySwitch";
            this.saySwitch.OffText = "끔";
            this.saySwitch.OnText = "켬";
            this.saySwitch.Size = new System.Drawing.Size(66, 22);
            this.saySwitch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.saySwitch.TabIndex = 6;
            this.saySwitch.Value = true;
            this.saySwitch.ValueObject = "Y";
            this.saySwitch.ValueChanged += new System.EventHandler(this.saySwitch_ValueChanged);
            // 
            // sayLabel
            // 
            this.sayLabel.AutoSize = true;
            this.sayLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.sayLabel.ForeColor = System.Drawing.Color.Black;
            this.sayLabel.Location = new System.Drawing.Point(12, 57);
            this.sayLabel.Name = "sayLabel";
            this.sayLabel.Size = new System.Drawing.Size(97, 12);
            this.sayLabel.TabIndex = 7;
            this.sayLabel.Text = "랜덤 명언 보이기";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pictureBox1.ForeColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(14, 119);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(201, 84);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.label5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(76, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "version 1.0.0";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "페이스북 항상 위";
            // 
            // facebookSwitch
            // 
            this.facebookSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.facebookSwitch.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.facebookSwitch.ForeColor = System.Drawing.Color.Black;
            this.facebookSwitch.Location = new System.Drawing.Point(149, 91);
            this.facebookSwitch.Name = "facebookSwitch";
            this.facebookSwitch.OffText = "끔";
            this.facebookSwitch.OnText = "켬";
            this.facebookSwitch.Size = new System.Drawing.Size(66, 22);
            this.facebookSwitch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.facebookSwitch.TabIndex = 11;
            this.facebookSwitch.Value = true;
            this.facebookSwitch.ValueObject = "Y";
            this.facebookSwitch.ValueChanged += new System.EventHandler(this.facebookSwitch_ValueChanged);
            // 
            // SettingForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(233, 226);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.facebookSwitch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.sayLabel);
            this.Controls.Add(this.saySwitch);
            this.Controls.Add(this.autoLoginSwitch);
            this.Controls.Add(this.autoLoginLabel);
            this.Controls.Add(this.alarmSwitch);
            this.Controls.Add(this.alarmLabel);
            this.Font = new System.Drawing.Font("Gulim", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "설정";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingForm_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.SettingForm_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label alarmLabel;
        private DevComponents.DotNetBar.Controls.SwitchButton alarmSwitch;
        private System.Windows.Forms.Label autoLoginLabel;
        private DevComponents.DotNetBar.Controls.SwitchButton autoLoginSwitch;
        private DevComponents.DotNetBar.Controls.SwitchButton saySwitch;
        private System.Windows.Forms.Label sayLabel;
        private System.Windows.Forms.ToolTip autoLoginTip;
        private System.Windows.Forms.ToolTip alarmTip;
        private System.Windows.Forms.ToolTip sayTip;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.SwitchButton facebookSwitch;
    }
}