using Peygir.Logic;
using Peygir.Presentation.Forms.Properties;
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

namespace Peygir.Presentation.Forms
{
    public partial class AttachmentsForm : Form
    {
        public Ticket Ticket { get; private set; }

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

        public AttachmentsForm(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException("ticket");
            }

            Ticket = ticket;

            InitializeComponent();

            attachmentsListUserControl.AttachmentsListView.SelectedIndexChanged += AttachmentsListView_SelectedIndexChanged;

            ShowAttachments();
        }

        private void UpdateButtonsEnabledProperty()
        {
            int count = attachmentsListUserControl.AttachmentsListView.SelectedItems.Count;

            if (count == 0)
            {
                saveButton.Enabled = false;
                deleteButton.Enabled = false;
            }
            else if (count == 1)
            {
                saveButton.Enabled = true;
                deleteButton.Enabled = true;
            }
            else
            {
                saveButton.Enabled = false;
                deleteButton.Enabled = true;
            }

            return;
        }

        private void ShowAttachments()
        {
            // Save selected attachments.
            List<int> selectedAttachments = new List<int>();
            foreach (ListViewItem item in attachmentsListUserControl.AttachmentsListView.SelectedItems)
            {
                Attachment attachment = (Attachment)item.Tag;
                selectedAttachments.Add(attachment.ID);
            }

            Attachment[] attachments = Ticket.GetAttachmentsWithoutContents();

            attachmentsListUserControl.ShowAttachments(attachments);

            // Reselect attachments.
            foreach (ListViewItem item in attachmentsListUserControl.AttachmentsListView.Items)
            {
                Attachment attachment = (Attachment)item.Tag;
                if (selectedAttachments.Contains(attachment.ID))
                {
                    item.Selected = true;
                }
            }

            UpdateButtonsEnabledProperty();

            return;
        }

        private void AddAttachment()
        {
            try
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;
                    FileInfo fi = new FileInfo(fileName);

                    Attachment attachment = Ticket.NewAttachment();

                    attachment.Name = fi.Name.Substring(0, Math.Min(255, fi.Name.Length)); // Max 255 characters.
                    attachment.SetContents(File.ReadAllBytes(fileName));

                    attachment.Add();

                    // Flush.
                    Database.Flush();

                    ShowAttachments();

                    // Select new attachment.
                    attachmentsListUserControl.AttachmentsListView.SelectedItems.Clear();
                    foreach (ListViewItem item in attachmentsListUserControl.AttachmentsListView.Items)
                    {
                        Attachment a = (Attachment)item.Tag;
                        if (a.ID == attachment.ID)
                        {
                            item.Selected = true;
                            item.EnsureVisible();
                            break;
                        }
                    }
                }
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

                ShowAttachments();
            }
            return;
        }

        private void SaveAttachment()
        {
            if (attachmentsListUserControl.AttachmentsListView.SelectedItems.Count == 1)
            {
                try
                {
                    Attachment attachmentWithoutContents = (Attachment)attachmentsListUserControl.AttachmentsListView.SelectedItems[0].Tag;

                    saveFileDialog.FileName = attachmentWithoutContents.Name;
                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string fileName = saveFileDialog.FileName;
                        Attachment attachment = Attachment.GetAttachment(attachmentWithoutContents.ID);

                        File.WriteAllBytes(fileName, attachment.GetContents());
                    }
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
            }

            return;
        }

        private void DeleteAttachment()
        {
            if (attachmentsListUserControl.AttachmentsListView.Items.Count > 0)
            {
                DialogResult result =
                MessageBox.Show
                (
                    Resources.String_AreYouSureYouWantToDeleteAttachments,
                    Resources.String_DeleteAttachments,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1,
                    FormMessageBoxOptions
                );

                if (result != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }

                // Delete attachments.
                for (int i = 0; i < attachmentsListUserControl.AttachmentsListView.SelectedItems.Count; i++)
                {
                    Attachment attachment = (Attachment)attachmentsListUserControl.AttachmentsListView.SelectedItems[i].Tag;
                    attachment.Delete();
                }

                // Flush.
                Database.Flush();

                // Show attachments.
                ShowAttachments();
            }

            return;
        }

        void AttachmentsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsEnabledProperty();
            return;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddAttachment();
            return;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveAttachment();
            return;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DeleteAttachment();
            return;
        }
    }
}
