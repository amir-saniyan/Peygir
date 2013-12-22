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
    public partial class TicketDetailsUserControl : UserControl
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

        public ComboBox MilestoneComboBox
        {
            get
            {
                return milestoneComboBox;
            }
        }

        public Milestone Milestone
        {
            get
            {
                return (Milestone)milestoneComboBox.SelectedItem;
            }
            set
            {
                if (value == null)
                {
                    milestoneComboBox.SelectedIndex = -1;
                }
                else
                {
                    milestoneComboBox.SelectedIndex = -1;
                    for (int i = 0; i < milestoneComboBox.Items.Count; i++)
                    {
                        Milestone milestone = (Milestone)milestoneComboBox.Items[i];
                        if (milestone.ID == value.ID)
                        {
                            milestoneComboBox.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        public string Summary
        {
            get { return summaryTextBox.Text; }
            set { summaryTextBox.Text = value; }
        }

        public string ReportedBy
        {
            get { return reportedByComboBox.Text; }
            set { reportedByComboBox.Text = value; }
        }

        public TicketType Type
        {
            get { return (TicketType)typeComboBox.SelectedIndex; }
            set { typeComboBox.SelectedIndex = (int)value; }
        }

        public TicketSeverity Severity
        {
            get { return (TicketSeverity)severityComboBox.SelectedIndex; }
            set { severityComboBox.SelectedIndex = (int)value; }
        }

        public TicketState State
        {
            get { return (TicketState)stateComboBox.SelectedIndex; }
            set { stateComboBox.SelectedIndex = (int)value; }
        }

        public string AssignedTo
        {
            get { return assignedToComboBox.Text; }
            set { assignedToComboBox.Text = value; }
        }

        public TicketPriority Priority
        {
            get { return (TicketPriority)priorityComboBox.SelectedIndex; }
            set { priorityComboBox.SelectedIndex = (int)value; }
        }

        public string Description
        {
            get { return descriptionTextBox.Text; }
            set { descriptionTextBox.Text = value; }
        }

        public TicketDetailsUserControl()
        {
            InitializeComponent();

            ReadOnly = false;
        }

        public void ShowMilestones(Milestone[] milestones)
        {
            if (milestones == null)
            {
                throw new ArgumentNullException("milestones");
            }

            milestoneComboBox.BeginUpdate();
            milestoneComboBox.Items.Clear();
            milestoneComboBox.Items.AddRange(milestones);
            milestoneComboBox.EndUpdate();

            reportedByComboBox.Items.Clear();
            reportedByComboBox.Items.AddRange(Ticket.GetReporters());

            assignedToComboBox.Items.Clear();
            assignedToComboBox.Items.AddRange(Ticket.GetAssignees());

            return;
        }

        public void ShowTicket(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException("ticket");
            }

            milestoneComboBox.SelectedIndex = -1;
            for (int i = 0; i < milestoneComboBox.Items.Count; i++)
            {
                Milestone milestone = (Milestone)milestoneComboBox.Items[i];
                if (milestone.ID == ticket.MilestoneID)
                {
                    milestoneComboBox.SelectedIndex = i;
                    break;
                }
            }

            ticketNumberTextBox.Text = string.Format("{0}", ticket.TicketNumber);
            summaryTextBox.Text = ticket.Summary;
            reportedByComboBox.Text = ticket.ReportedBy;
            typeComboBox.SelectedIndex = (int)ticket.Type;
            severityComboBox.SelectedIndex = (int)ticket.Severity;
            stateComboBox.SelectedIndex = (int)ticket.State;
            assignedToComboBox.Text = ticket.AssignedTo;
            priorityComboBox.SelectedIndex = (int)ticket.Priority;
            descriptionTextBox.Text = ticket.Description;

            return;
        }

        public void RetrieveTicket(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException("ticket");
            }

            Milestone milestone = (Milestone)milestoneComboBox.SelectedItem;

            ticket.MilestoneID = milestone.ID;
            ticket.Summary = summaryTextBox.Text;
            ticket.ReportedBy = reportedByComboBox.Text.Substring(0, Math.Min(255, reportedByComboBox.Text.Length)); // Max 255 characters.
            ticket.Type = (TicketType)typeComboBox.SelectedIndex;
            ticket.Severity = (TicketSeverity)severityComboBox.SelectedIndex;
            ticket.State = (TicketState)stateComboBox.SelectedIndex;
            ticket.AssignedTo = assignedToComboBox.Text.Substring(0, Math.Min(255, assignedToComboBox.Text.Length)); // Max 255 characters.
            ticket.Priority = (TicketPriority)priorityComboBox.SelectedIndex;
            ticket.Description = descriptionTextBox.Text;

            return;
        }

        private void UpdateReadOnlyState()
        {
            milestoneComboBox.Enabled = !readOnly;
            summaryTextBox.ReadOnly = readOnly;
            reportedByComboBox.Enabled = !readOnly;
            typeComboBox.Enabled = !readOnly;
            severityComboBox.Enabled = !readOnly;
            stateComboBox.Enabled = !readOnly;
            assignedToComboBox.Enabled = !readOnly;
            priorityComboBox.Enabled = !readOnly;
            descriptionTextBox.ReadOnly = readOnly;

            return;
        }
    }
}
