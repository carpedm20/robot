﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace robot
{
    public partial class SettingForm : DevComponents.DotNetBar.Metro.MetroForm
    {
        public DevComponents.DotNetBar.Controls.SwitchButton loginSwitch;
        public DevComponents.DotNetBar.Controls.SwitchButton sayswitch;

        public SettingForm()
        {
            InitializeComponent();

            loginSwitch = this.autoLoginSwitch;
            sayswitch = this.saySwitch;
        }

        public void autoLoginSwitch_ValueChanged(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.Controls.SwitchButton check = (DevComponents.DotNetBar.Controls.SwitchButton)sender;

            if (check.Value == true)
            {
                DialogResult result = MessageBox.Show("개인정보가 유출될 수 있습니다.\r\n자동 로그인을 하시겠습니까? :[", "Robot의 경고", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.No)
                {
                    check.Value = false;
                    return;
                }

                Program.ini.SetIniValue("Login", "Auto", "true");
                Program.ini.SetIniValue("Login", "Save", "true");
                Program.ini.SetIniValue("Login", "Id", Program.id);
                Program.ini.SetIniValue("Login", "Password", Program.password);
            }
            if (check.Value == false)
            {
                Program.ini.SetIniValue("Login", "Auto", "false");
                Program.ini.SetIniValue("Login", "Save", "false");
                Program.ini.SetIniValue("Login", "Id", "");
                Program.ini.SetIniValue("Login", "Password", "");
            }
        }

        private void alarmSwitch_ValueChanged(object sender, EventArgs e)
        {
            if (alarmSwitch.Value == false)
            {
                MainForm.timer1.Stop();
            }
            else
            {
                MainForm.timer1.Start();
            }
        }

        private void startProgramSwitch_ValueChanged(object sender, EventArgs e)
        {
            if (startProgramSwitch.Value == true)
            {
                MainForm.SetStartup("Robot", true);
            }
            else
            {
                MainForm.SetStartup("Robot", false);
            }
        }

        private void saySwitch_ValueChanged(object sender, EventArgs e)
        {
            if (saySwitch.Value == true)
            {
                MainForm.saylabel.Visible = true;
                MainForm.timer2.Start();
            }
            else
            {
                MainForm.saylabel.Visible = false;
                MainForm.timer2.Stop();
            }
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.settingFormExist = false;
        }
    }
}
