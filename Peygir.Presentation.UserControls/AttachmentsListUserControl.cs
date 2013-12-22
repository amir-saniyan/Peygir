using Peygir.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peygir.Presentation.UserControls
{
    public partial class AttachmentsListUserControl : UserControl
    {
        public ListView AttachmentsListView
        {
            get { return attachmentsListView; }
        }

        public AttachmentsListUserControl()
        {
            InitializeComponent();
        }

        public void ShowAttachments(Attachment[] attachments)
        {
            if (attachments == null)
            {
                throw new ArgumentNullException("attachments");
            }

            attachmentsListView.BeginUpdate();

            attachmentsListView.Items.Clear();
            foreach (var attachment in attachments)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.Text = attachment.Name;
                lvi.SubItems.Add(string.Format("{0}", attachment.Size));
                lvi.Tag = attachment;

                attachmentsListView.Items.Add(lvi);
            }

            attachmentsListView.EndUpdate();

            attachmentsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            return;
        }
    }
}
