using Peygir.Presentation.Forms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peygir.Presentation.Forms
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();

            LoadSettings();
        }

        private static readonly string[] Calendars = new string[]
        {
            "ChineseLunisolarCalendar",
            "GregorianCalendar",
            "HebrewCalendar",
            "HijriCalendar",
            "JapaneseCalendar",
            "JapaneseLunisolarCalendar",
            "JulianCalendar",
            "KoreanCalendar",
            "KoreanLunisolarCalendar",
            "PersianCalendar",
            "TaiwanCalendar",
            "TaiwanLunisolarCalendar",
            "ThaiBuddhistCalendar",
            "UmAlQuraCalendar"
        };

        private void UpdateButtonsEnabledProperty()
        {
            formatDateTimePanel.Enabled = formatDateTimeCheckBox.Checked;
            return;
        }

        private void LoadSettings()
        {
            formatDateTimeCheckBox.Checked = Settings.Default.FormatDateTime;

            calendarComboBox.SelectedIndex = -1;
            for (int i = 0; i < Calendars.Length; i++)
            {
                if (Settings.Default.Calendar == Calendars[i])
                {
                    calendarComboBox.SelectedIndex = i;
                    break;
                }
            }

            dateTimePatternTextBox.Text = Settings.Default.DateTimePattern;

            UpdateButtonsEnabledProperty();

            return;
        }

        private void SaveSettings()
        {
            Settings.Default.FormatDateTime = formatDateTimeCheckBox.Checked;

            if (calendarComboBox.SelectedIndex >= 0)
            {
                Settings.Default.Calendar = Calendars[calendarComboBox.SelectedIndex];
            }
            else
            {
                Settings.Default.Calendar = string.Empty;
            }

            Settings.Default.DateTimePattern = dateTimePatternTextBox.Text;

            Settings.Default.Save();

            return;
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                SaveSettings();
            }
            return;
        }

        private void formatDateTimeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtonsEnabledProperty();
            return;
        }
    }
}
