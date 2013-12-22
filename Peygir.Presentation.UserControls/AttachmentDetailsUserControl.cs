using Peygir.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peygir.Presentation.UserControls
{
    public partial class AttachmentDetailsUserControl : UserControl
    {
        private bool readOnly;

        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
                UpdateReadOnlyState();
            }
        }

        public AttachmentDetailsUserControl()
        {
            InitializeComponent();

            contents = new byte[0];

            ReadOnly = false;
        }

        public void ShowAttachment(Attachment attachment)
        {
            if (attachment == null)
            {
                throw new ArgumentNullException("attachment");
            }

            nameTextBox.Text = attachment.Name;
            sizeTextBox.Text = string.Format("{0}", attachment.Size);
            contents = attachment.GetContents();

            return;
        }

        public void RetrieveAttachment(Attachment attachment)
        {
            if (attachment == null)
            {
                throw new ArgumentNullException("attachment");
            }

            attachment.Name = nameTextBox.Text;
            attachment.SetContents(contents);

            return;
        }

        private byte[] contents;

        private void UpdateReadOnlyState()
        {
            openButton.Enabled = !readOnly;

            return;
        }

        private void OpenFile()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                contents = File.ReadAllBytes(fileName);
                nameTextBox.Text = fileName;
            }
            return;
        }

        private void SaveFile()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;

                File.WriteAllBytes(fileName, contents);
            }
            return;
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFile();
            return;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFile();
            return;
        }
    }
}
