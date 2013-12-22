using Peygir.Logic;
using Peygir.Presentation.UserControls.Properties;
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
    public partial class TicketsListUserControl : UserControl
    {
        public ListView TicketsListView
        {
            get { return ticketsListView; }
        }

        public TicketsListUserControl()
        {
            InitializeComponent();
        }

        public void ShowTickets(Ticket[] tickets)
        {
            if (tickets == null)
            {
                throw new ArgumentNullException("tickets");
            }

            // Cache milestones.
            Dictionary<int, string> milestoneNames = new Dictionary<int, string>();
            Milestone[] milestones = Milestone.GetMilestones();
            foreach (Milestone milestone in milestones)
            {
                milestoneNames[milestone.ID] = milestone.Name;
            }

            ticketsListView.BeginUpdate();

            ticketsListView.Items.Clear();
            foreach (var ticket in tickets)
            {
                string ticketPriority;
                switch (ticket.Priority)
                {
                    case TicketPriority.Lowest:
                        ticketPriority = Resources.String_Lowest;
                        break;

                    case TicketPriority.Low:
                        ticketPriority = Resources.String_Low;
                        break;

                    case TicketPriority.Normal:
                        ticketPriority = Resources.String_Normal;
                        break;

                    case TicketPriority.High:
                        ticketPriority = Resources.String_High;
                        break;

                    case TicketPriority.Highest:
                        ticketPriority = Resources.String_Highest;
                        break;

                    default:
                        ticketPriority = string.Empty;
                        break;
                }

                string ticketType;
                switch (ticket.Type)
                {
                    case TicketType.Defect:
                        ticketType = Resources.String_Defect;
                        break;

                    case TicketType.FeatureRequest:
                        ticketType = Resources.String_FeatureRequest;
                        break;

                    case TicketType.Task:
                        ticketType = Resources.String_Task;
                        break;

                    default:
                        ticketType = string.Empty;
                        break;
                }

                string ticketSeverity;
                switch (ticket.Severity)
                {
                    case TicketSeverity.Blocker:
                        ticketSeverity = Resources.String_Blocker;
                        break;

                    case TicketSeverity.Critical:
                        ticketSeverity = Resources.String_Critical;
                        break;

                    case TicketSeverity.Major:
                        ticketSeverity = Resources.String_Major;
                        break;

                    case TicketSeverity.Normal:
                        ticketSeverity = Resources.String_Normal;
                        break;

                    case TicketSeverity.Minor:
                        ticketSeverity = Resources.String_Minor;
                        break;

                    case TicketSeverity.Trivial:
                        ticketSeverity = Resources.String_Trivial;
                        break;

                    default:
                        ticketSeverity = string.Empty;
                        break;
                }

                string ticketState;
                switch (ticket.State)
                {
                    case TicketState.New:
                        ticketState = Resources.String_New;
                        break;

                    case TicketState.Accepted:
                        ticketState = Resources.String_Accepted;
                        break;

                    case TicketState.Closed:
                        ticketState = Resources.String_Closed;
                        break;

                    case TicketState.Completed:
                        ticketState = Resources.String_Completed;
                        break;

                    default:
                        ticketState = string.Empty;
                        break;
                }

                ListViewItem lvi = new ListViewItem();

                lvi.Text = string.Format("{0}", ticket.TicketNumber);
                lvi.SubItems.Add(ticket.Summary);
                lvi.SubItems.Add(milestoneNames[ticket.MilestoneID]);
                lvi.SubItems.Add(ticketPriority);
                lvi.SubItems.Add(ticketType);
                lvi.SubItems.Add(ticketSeverity);
                lvi.SubItems.Add(ticketState);
                lvi.Tag = ticket;

                ticketsListView.Items.Add(lvi);
            }

            ticketsListView.EndUpdate();

            ticketsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            return;
        }
    }
}
