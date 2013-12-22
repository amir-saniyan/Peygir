using Peygir.Logic;
using Peygir.Presentation.Forms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Peygir.Presentation.Forms
{
    public partial class TicketHistoryForm : Form
    {
        public Ticket Ticket { get; private set; }

        public TicketHistoryForm(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException("ticket");
            }

            Ticket = ticket;

            InitializeComponent();

            if (Settings.Default.FormatDateTime)
            {
                try
                {
                    DateTimeFormatter dateTimeFormatter = new DateTimeFormatter
                    (
                        Settings.Default.DateTimePattern,
                        Settings.Default.Calendar
                    );

                    ticketHistoryListUserControl.DateTimeFormatter = dateTimeFormatter;
                    ticketHistoryDetailsUserControl.DateTimeFormatter = dateTimeFormatter;
                }
                catch (Exception)
                {
                    // Nothing.
                }
            }

            ticketHistoryListUserControl.TicketHistoryListView.SelectedIndexChanged += TicketHistoryListView_SelectedIndexChanged;

            ShowTicketHistory();

            // Select last history.
            if (ticketHistoryListUserControl.TicketHistoryListView.Items.Count > 0)
            {
                ticketHistoryListUserControl.TicketHistoryListView.SelectedIndices.Clear();
                ticketHistoryListUserControl.TicketHistoryListView.SelectedIndices.Add(0);
            }
        }

        private void ShowTicketHistory()
        {
            TicketHistory[] history = Ticket.GetHistory();

            ticketHistoryListUserControl.ShowTicketHistory(history);

            ShowTicketHistoryDetails();

            return;
        }

        private void ShowTicketHistoryDetails()
        {
            ticketHistoryDetailsUserControl.Clear();

            if (ticketHistoryListUserControl.TicketHistoryListView.SelectedItems.Count == 1)
            {
                TicketHistory history = (TicketHistory)ticketHistoryListUserControl.TicketHistoryListView.SelectedItems[0].Tag;

                ticketHistoryDetailsUserControl.ShowTicketHistory(history);

                groupBox.Enabled = true;
            }
            else
            {
                groupBox.Enabled = false;
            }

            return;
        }

        private void TicketHistoryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTicketHistoryDetails();
            return;
        }
    }
}
