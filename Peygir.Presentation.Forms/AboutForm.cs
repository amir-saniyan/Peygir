using Peygir.Presentation.Forms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peygir.Presentation.Forms
{
    partial class AboutForm : Form
    {
        public MessageBoxOptions FormMessageBoxOptions
        {
            get
            {
                MessageBoxOptions options = (MessageBoxOptions)0;
                if (RightToLeft == System.Windows.Forms.RightToLeft.Yes)
                {
                    options = (MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
                return options;
            }
        }

        public AboutForm()
        {
            InitializeComponent();

            versionLabel.Text = string.Format(Resources.String_Version, PeygirApplication.AssemblyVersion);
        }

        private void OpenLink()
        {
            try
            {
                string address = string.Format("mailto:{0}", Settings.Default.ProgrammerEmail);
                Process.Start(address);
            }
            catch (Exception exception)
            {
                MessageBox.Show
                (
                    exception.Message,
                    Resources.String_Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    FormMessageBoxOptions

                );
            }
            return;
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink();
            return;
        }
    }
}
