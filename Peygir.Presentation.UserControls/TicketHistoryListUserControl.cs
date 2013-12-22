using Peygir.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peygir.Presentation.UserControls
{
    public partial class TicketHistoryListUserControl : UserControl
    {
        public ListView TicketHistoryListView
        {
            get { return ticketHistoryListView; }
        }

        public DateTimeFormatter DateTimeFormatter { get; set; }

        public TicketHistoryListUserControl()
        {
            InitializeComponent();
        }

        public void ShowTicketHistory(TicketHistory[] ticketHistory)
        {
            if (ticketHistory == null)
            {
                throw new ArgumentNullException("ticketHistory");
            }

            ticketHistoryListView.BeginUpdate();

            ticketHistoryListView.Items.Clear();
            foreach (var th in ticketHistory)
            {
                ListViewItem lvi = new ListViewItem();

                if (DateTimeFormatter != null)
                {
                    lvi.Text = DateTimeFormatter.Format(th.Timestamp);
                }
                else
                {
                    lvi.Text = string.Format("{0}", th.Timestamp);
                }
                lvi.Tag = th;

                ticketHistoryListView.Items.Add(lvi);
            }

            ticketHistoryListView.EndUpdate();

            ticketHistoryListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            ticketHistoryListView.Columns[0].Width -= 4;

            return;
        }
    }
}
