using Peygir.Logic;
using Peygir.Presentation.Forms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Peygir.Presentation.Forms
{
    public partial class ProjectForm : Form
    {
        public Project Project { get; private set; }

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

        public ProjectForm(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            Project = project;

            InitializeComponent();

            milestonesListUserControl.MilestonesListView.SelectedIndexChanged += MilestonesListView_SelectedIndexChanged;
            milestonesListUserControl.MilestonesListView.DoubleClick += MilestonesListView_DoubleClick;

            ticketsListUserControl.TicketsListView.SelectedIndexChanged += TicketsListView_SelectedIndexChanged;
            ticketsListUserControl.TicketsListView.DoubleClick += TicketsListView_DoubleClick;

            ShowProjectDetails();
            ShowMilestones();
            ShowTickets();
        }

        private string TranslateTicketType(TicketType ticketType)
        {
            switch (ticketType)
            {
                case TicketType.Defect:
                    return Resources.String_Defect;

                case TicketType.FeatureRequest:
                    return Resources.String_FeatureRequest;

                case TicketType.Task:
                    return Resources.String_Task;

                default:
                    return ticketType.ToString();
            }
        }

        private string TranslateTicketSeverity(TicketSeverity ticketSeverity)
        {
            switch (ticketSeverity)
            {
                case TicketSeverity.Blocker:
                    return Resources.String_Blocker;

                case TicketSeverity.Critical:
                    return Resources.String_Critical;

                case TicketSeverity.Major:
                    return Resources.String_Major;

                case TicketSeverity.Normal:
                    return Resources.String_Normal;

                case TicketSeverity.Minor:
                    return Resources.String_Minor;

                case TicketSeverity.Trivial:
                    return Resources.String_Trivial;

                default:
                    return ticketSeverity.ToString();
            }
        }

        private string TranslateTicketState(TicketState ticketState)
        {
            switch (ticketState)
            {
                case TicketState.New:
                    return Resources.String_New;

                case TicketState.Accepted:
                    return Resources.String_Accepted;

                case TicketState.Closed:
                    return Resources.String_Closed;

                case TicketState.Completed:
                    return Resources.String_Completed;

                default:
                    return ticketState.ToString();
            }
        }

        private string TranslateTicketPriority(TicketPriority ticketPriority)
        {
            switch (ticketPriority)
            {
                case TicketPriority.Lowest:
                    return Resources.String_Lowest;

                case TicketPriority.Low:
                    return Resources.String_Low;

                case TicketPriority.Normal:
                    return Resources.String_Normal;

                case TicketPriority.High:
                    return Resources.String_High;

                case TicketPriority.Highest:
                    return Resources.String_Highest;

                default:
                    return ticketPriority.ToString();
            }
        }

        private void UpdateButtonsEnabledProperty()
        {
            int selectedMilestonesCount = milestonesListUserControl.MilestonesListView.SelectedItems.Count;
            if (selectedMilestonesCount == 0)
            {
                showMilestoneButton.Enabled = false;
                editMilestoneButton.Enabled = false;
                deleteMilestoneButton.Enabled = false;
            }
            else if (selectedMilestonesCount == 1)
            {
                showMilestoneButton.Enabled = true;
                editMilestoneButton.Enabled = true;
                deleteMilestoneButton.Enabled = true;
            }
            else
            {
                showMilestoneButton.Enabled = false;
                editMilestoneButton.Enabled = false;
                deleteMilestoneButton.Enabled = true;
            }

            int selectedTicketsCount = ticketsListUserControl.TicketsListView.SelectedItems.Count;
            if (selectedTicketsCount == 0)
            {
                showTicketButton.Enabled = false;
                editTicketButton.Enabled = false;
                showTicketHistoryButton.Enabled = false;
                showTicketAttachmentsButton.Enabled = false;
                deleteTicketButton.Enabled = false;
            }
            else if (selectedTicketsCount == 1)
            {
                showTicketButton.Enabled = true;
                editTicketButton.Enabled = true;
                showTicketHistoryButton.Enabled = true;
                showTicketAttachmentsButton.Enabled = true;
                deleteTicketButton.Enabled = true;
            }
            else
            {
                showTicketButton.Enabled = false;
                editTicketButton.Enabled = false;
                showTicketHistoryButton.Enabled = false;
                showTicketAttachmentsButton.Enabled = false;
                deleteTicketButton.Enabled = true;
            }

            return;
        }

        private void ShowProjectDetails()
        {
            projectDetailsUserControl.ShowProject(Project);
            return;
        }

        private void ChangeProjectDetails()
        {
            ProjectDetailsForm form = new ProjectDetailsForm();

            form.ProjectDetailsUserControl.ShowProject(Project);

        Again:
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Check name.
                if (string.IsNullOrWhiteSpace(form.ProjectDetailsUserControl.ProjectName))
                {
                    MessageBox.Show
                    (
                        Resources.String_TheProjectNameCannotBeBlank,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                form.ProjectDetailsUserControl.RetrieveProject(Project);

                // Update project.
                Project.Update();

                // Flush.
                Database.Flush();

                ShowProjectDetails();
            }

            return;
        }

        private void ShowMilestones()
        {
            // Save selected milestones.
            List<int> selectedMilestones = new List<int>();
            foreach (ListViewItem item in milestonesListUserControl.MilestonesListView.SelectedItems)
            {
                Milestone milestone = (Milestone)item.Tag;
                selectedMilestones.Add(milestone.ID);
            }

            Milestone[] milestones = Project.GetMilestones();

            milestonesListUserControl.ShowMilestones(milestones);

            // Reselect milestones.
            foreach (ListViewItem item in milestonesListUserControl.MilestonesListView.Items)
            {
                Milestone milestone = (Milestone)item.Tag;
                if (selectedMilestones.Contains(milestone.ID))
                {
                    item.Selected = true;
                }
            }

            UpdateButtonsEnabledProperty();

            ShowTickets();

            return;
        }

        private void AddMilestone()
        {
            Milestone milestone = Project.NewMilestone();
            MilestoneDetailsForm form = new MilestoneDetailsForm();
            form.MilestoneDetailsUserControl.ShowMilestone(milestone);
            form.MilestoneDetailsUserControl.State = MilestoneState.Active;

        Again:
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Check name.
                if (string.IsNullOrWhiteSpace(form.MilestoneDetailsUserControl.MilestoneName))
                {
                    MessageBox.Show
                    (
                        Resources.String_TheMilestoneNameCannotBeBlank,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check state.
                if (form.MilestoneDetailsUserControl.State == (MilestoneState)(-1))
                {
                    MessageBox.Show
                    (
                        Resources.String_PleaseSpecifyMilestoneState,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                form.MilestoneDetailsUserControl.RetrieveMilestone(milestone);

                // Add.
                milestone.Add();

                // Flush.
                Database.Flush();

                // Show milestones.
                ShowMilestones();

                // Select new milestone.
                milestonesListUserControl.MilestonesListView.SelectedItems.Clear();
                foreach (ListViewItem item in milestonesListUserControl.MilestonesListView.Items)
                {
                    Milestone m = (Milestone)item.Tag;
                    if (m.ID == milestone.ID)
                    {
                        item.Selected = true;
                        item.EnsureVisible();
                        break;
                    }
                }

                UpdateButtonsEnabledProperty();

                milestonesListUserControl.Focus();
            }

            return;
        }

        private void ShowMilestone()
        {
            if (milestonesListUserControl.MilestonesListView.SelectedItems.Count != 1)
            {
                return;
            }

            Milestone milestone = (Milestone)milestonesListUserControl.MilestonesListView.SelectedItems[0].Tag;

            MilestoneDetailsForm form = new MilestoneDetailsForm();

            form.MilestoneDetailsUserControl.ShowMilestone(milestone);
            form.MilestoneDetailsUserControl.ReadOnly = true;

            form.ShowDialog();

            return;
        }

        private void EditMilestone()
        {
            if (milestonesListUserControl.MilestonesListView.SelectedItems.Count != 1)
            {
                return;
            }

            Milestone milestone = (Milestone)milestonesListUserControl.MilestonesListView.SelectedItems[0].Tag;

            MilestoneDetailsForm form = new MilestoneDetailsForm();

            form.MilestoneDetailsUserControl.ShowMilestone(milestone);

        Again:
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Check name.
                if (string.IsNullOrWhiteSpace(form.MilestoneDetailsUserControl.MilestoneName))
                {
                    MessageBox.Show
                    (
                        Resources.String_TheMilestoneNameCannotBeBlank,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check state.
                if (form.MilestoneDetailsUserControl.State == (MilestoneState)(-1))
                {
                    MessageBox.Show
                    (
                        Resources.String_PleaseSpecifyMilestoneState,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                form.MilestoneDetailsUserControl.RetrieveMilestone(milestone);

                // Update milestones.
                milestone.Update();

                // Flush.
                Database.Flush();

                // Show milestones.
                ShowMilestones();
            }

            return;
        }

        private void DeleteMilestone()
        {
            if (milestonesListUserControl.MilestonesListView.SelectedItems.Count > 0)
            {
                DialogResult result =
                MessageBox.Show
                (
                    Resources.String_AreYouSureYouWantToDeleteSelectedMilestonesAndTheirTickets,
                    Resources.String_DeleteMilestones,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1,
                    FormMessageBoxOptions
                );

                if (result != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }

                // Delete milestones.
                for (int i = 0; i < milestonesListUserControl.MilestonesListView.SelectedItems.Count; i++)
                {
                    Milestone milestone = (Milestone)milestonesListUserControl.MilestonesListView.SelectedItems[i].Tag;
                    milestone.Delete();
                }

                // Flush.
                Database.Flush();

                // Show milestones.
                ShowMilestones();
            }
            return;
        }

        private void ShowTickets()
        {
            // Save selected tickets.
            List<int> selectedTickets = new List<int>();
            foreach (ListViewItem item in ticketsListUserControl.TicketsListView.SelectedItems)
            {
                Ticket ticket = (Ticket)item.Tag;
                selectedTickets.Add(ticket.ID);
            }

            Ticket[] tickets = Project.GetTickets();

            ticketsListUserControl.ShowTickets(tickets);

            // Reselect tickets.
            foreach (ListViewItem item in ticketsListUserControl.TicketsListView.Items)
            {
                Ticket ticket = (Ticket)item.Tag;
                if (selectedTickets.Contains(ticket.ID))
                {
                    item.Selected = true;
                }
            }

            UpdateButtonsEnabledProperty();

            return;
        }

        private void AddTicket()
        {
            Milestone[] milestones = Project.GetMilestones();
            if (milestones.Length == 0)
            {
                MessageBox.Show
                (
                    Resources.String_PleaseAddAMilestoneBeforeAddingATicket,
                    Resources.String_Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    FormMessageBoxOptions
                );
                tabControl.SelectedTab = milestonesTabPage;
                return;
            }

            TicketDetailsForm form = new TicketDetailsForm();

            form.TicketDetailsUserControl.ShowMilestones(milestones);

            form.TicketDetailsUserControl.Type = (TicketType)(-1);
            form.TicketDetailsUserControl.Severity = TicketSeverity.Normal;
            form.TicketDetailsUserControl.State = TicketState.New;
            form.TicketDetailsUserControl.Priority = TicketPriority.Normal;

        Again:
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Check milestone.
                if (form.TicketDetailsUserControl.Milestone == null)
                {
                    MessageBox.Show
                    (
                        Resources.String_PleaseSelectAMilestone,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check summary.
                if (string.IsNullOrWhiteSpace(form.TicketDetailsUserControl.Summary))
                {
                    MessageBox.Show
                    (
                        Resources.String_TheTicketSummaryCannotBeBlank,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check type.
                if (form.TicketDetailsUserControl.Type == (TicketType)(-1))
                {
                    MessageBox.Show
                    (
                        Resources.String_PleaseSpecifyTicketType,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check severity.
                if (form.TicketDetailsUserControl.Severity == (TicketSeverity)(-1))
                {
                    MessageBox.Show
                    (
                        Resources.String_PleaseSpecifyTicketSeverity,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check state.
                if (form.TicketDetailsUserControl.State == (TicketState)(-1))
                {
                    MessageBox.Show
                    (
                        Resources.String_PleaseSpecifyTicketState,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check priority.
                if (form.TicketDetailsUserControl.Priority == (TicketPriority)(-1))
                {
                    MessageBox.Show
                    (
                        Resources.String_PleaseSpecifyTicketPriority,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check description.
                if (string.IsNullOrWhiteSpace(form.TicketDetailsUserControl.Description))
                {
                    MessageBox.Show
                    (
                        Resources.String_TheTicketDescriptionCannotBeBlank,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                Milestone milestone = form.TicketDetailsUserControl.Milestone;
                Ticket ticket = milestone.NewTicket();
                form.TicketDetailsUserControl.RetrieveTicket(ticket);

                // Add.
                ticket.Add();

                // Flush.
                Database.Flush();

                // Create ticket history entry.
                TicketHistory ticketHistory = ticket.NewHistory();
                ticketHistory.Timestamp = DateTime.Now;
                ticketHistory.Changes = Resources.String_TicketCreated;
                ticketHistory.Comment = ticket.Description;
                ticketHistory.Add();

                // Show tickets.
                ShowTickets();

                // Select new ticket.
                ticketsListUserControl.TicketsListView.SelectedItems.Clear();
                foreach (ListViewItem item in ticketsListUserControl.TicketsListView.Items)
                {
                    Ticket t = (Ticket)item.Tag;
                    if (t.ID == ticket.ID)
                    {
                        item.Selected = true;
                        item.EnsureVisible();
                        break;
                    }
                }

                UpdateButtonsEnabledProperty();

                ticketsListUserControl.Focus();
            }

            return;
        }

        private void ShowTicket()
        {
            if (ticketsListUserControl.TicketsListView.SelectedItems.Count != 1)
            {
                return;
            }

            Ticket ticket = (Ticket)ticketsListUserControl.TicketsListView.SelectedItems[0].Tag;

            TicketDetailsForm form = new TicketDetailsForm();

            Milestone[] milestones = Project.GetMilestones();
            form.TicketDetailsUserControl.ShowMilestones(milestones);

            form.TicketDetailsUserControl.ShowTicket(ticket);
            form.TicketDetailsUserControl.ReadOnly = true;

            form.ShowDialog();

            return;
        }

        private void EditTicket()
        {
            if (ticketsListUserControl.TicketsListView.SelectedItems.Count != 1)
            {
                return;
            }

            Ticket ticket = (Ticket)ticketsListUserControl.TicketsListView.SelectedItems[0].Tag;

            TicketDetailsForm form = new TicketDetailsForm();

            Milestone[] milestones = Project.GetMilestones();
            form.TicketDetailsUserControl.ShowMilestones(milestones);

            form.TicketDetailsUserControl.ShowTicket(ticket);

        Again:
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Check milestone.
                if (form.TicketDetailsUserControl.Milestone == null)
                {
                    MessageBox.Show
                    (
                        Resources.String_PleaseSelectAMilestone,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check summary.
                if (string.IsNullOrWhiteSpace(form.TicketDetailsUserControl.Summary))
                {
                    MessageBox.Show
                    (
                        Resources.String_TheTicketSummaryCannotBeBlank,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check type.
                if (form.TicketDetailsUserControl.Type == (TicketType)(-1))
                {
                    MessageBox.Show
                    (
                        Resources.String_PleaseSpecifyTicketType,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check severity.
                if (form.TicketDetailsUserControl.Severity == (TicketSeverity)(-1))
                {
                    MessageBox.Show
                    (
                        Resources.String_PleaseSpecifyTicketSeverity,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check state.
                if (form.TicketDetailsUserControl.State == (TicketState)(-1))
                {
                    MessageBox.Show
                    (
                        Resources.String_PleaseSpecifyTicketState,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check priority.
                if (form.TicketDetailsUserControl.Priority == (TicketPriority)(-1))
                {
                    MessageBox.Show
                    (
                        Resources.String_PleaseSpecifyTicketPriority,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Check description.
                if (string.IsNullOrWhiteSpace(form.TicketDetailsUserControl.Description))
                {
                    MessageBox.Show
                    (
                        Resources.String_TheTicketDescriptionCannotBeBlank,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                // Changes.
                StringBuilder changesStringBuilder = new StringBuilder();
                // Milestone.
                if (ticket.MilestoneID != form.TicketDetailsUserControl.Milestone.ID)
                {
                    changesStringBuilder.AppendFormat(Resources.String_MilestoneChangesFromXToY, ticket.GetMilestone().Name, form.TicketDetailsUserControl.Milestone.Name);
                    changesStringBuilder.AppendLine();
                }
                // Summary.
                if (ticket.Summary != form.TicketDetailsUserControl.Summary)
                {
                    changesStringBuilder.AppendFormat(Resources.String_SummaryChangesFromXToY, ticket.Summary, form.TicketDetailsUserControl.Summary);
                    changesStringBuilder.AppendLine();
                }
                // Reported by.
                if (ticket.ReportedBy != form.TicketDetailsUserControl.ReportedBy)
                {
                    changesStringBuilder.AppendFormat(Resources.String_ReportedByChangesFromXToY, ticket.ReportedBy, form.TicketDetailsUserControl.ReportedBy);
                    changesStringBuilder.AppendLine();
                }
                // Type.
                if (ticket.Type != form.TicketDetailsUserControl.Type)
                {
                    changesStringBuilder.AppendFormat
                    (
                        Resources.String_TypeChangesFromXToY,
                        TranslateTicketType(ticket.Type),
                        TranslateTicketType(form.TicketDetailsUserControl.Type)
                    );
                    changesStringBuilder.AppendLine();
                }
                // Severity.
                if (ticket.Severity != form.TicketDetailsUserControl.Severity)
                {
                    changesStringBuilder.AppendFormat
                    (
                        Resources.String_SeverityChangesFromXToY,
                        TranslateTicketSeverity(ticket.Severity),
                        TranslateTicketSeverity(form.TicketDetailsUserControl.Severity)
                    );
                    changesStringBuilder.AppendLine();
                }
                // State.
                if (ticket.State != form.TicketDetailsUserControl.State)
                {
                    changesStringBuilder.AppendFormat
                    (
                        Resources.String_StateChangesFromXToY,
                        TranslateTicketState(ticket.State),
                        TranslateTicketState(form.TicketDetailsUserControl.State)
                    );
                    changesStringBuilder.AppendLine();
                }
                // AssignedTo.
                if (ticket.AssignedTo != form.TicketDetailsUserControl.AssignedTo)
                {
                    changesStringBuilder.AppendFormat(Resources.String_AssignedToChangesFromXToY, ticket.AssignedTo, form.TicketDetailsUserControl.AssignedTo);
                    changesStringBuilder.AppendLine();
                }
                // Priority.
                if (ticket.Priority != form.TicketDetailsUserControl.Priority)
                {
                    changesStringBuilder.AppendFormat
                    (
                        Resources.String_PriorityChangesFromXToY,
                        TranslateTicketPriority(ticket.Priority),
                        TranslateTicketPriority(form.TicketDetailsUserControl.Priority)
                    );
                    changesStringBuilder.AppendLine();
                }
                // Description.
                if (ticket.Description != form.TicketDetailsUserControl.Description)
                {
                    changesStringBuilder.AppendFormat(Resources.String_DescriptionChanges);
                    changesStringBuilder.AppendLine();
                }

                form.TicketDetailsUserControl.RetrieveTicket(ticket);

                // Update tickets.
                ticket.Update();

                // Create ticket history entry.
                string changes = changesStringBuilder.ToString();
                if (!string.IsNullOrEmpty(changes))
                {
                    TicketHistory ticketHistory = ticket.NewHistory();
                    ticketHistory.Timestamp = DateTime.Now;
                    ticketHistory.Changes = changes;
                    ticketHistory.Comment = ticket.Description;
                    ticketHistory.Add();
                }

                // Flush.
                Database.Flush();

                // Show tickets.
                ShowTickets();
            }

            return;
        }

        private void ShowTicketHistory()
        {
            if (ticketsListUserControl.TicketsListView.SelectedItems.Count != 1)
            {
                return;
            }

            Ticket ticket = (Ticket)ticketsListUserControl.TicketsListView.SelectedItems[0].Tag;

            TicketHistoryForm form = new TicketHistoryForm(ticket);

            form.ShowDialog();

            return;
        }

        private void ShowAttachments()
        {
            if (ticketsListUserControl.TicketsListView.SelectedItems.Count != 1)
            {
                return;
            }

            Ticket ticket = (Ticket)ticketsListUserControl.TicketsListView.SelectedItems[0].Tag;

            AttachmentsForm form = new AttachmentsForm(ticket);

            form.ShowDialog();

            return;
        }

        private void DeleteTicket()
        {
            if (ticketsListUserControl.TicketsListView.SelectedItems.Count > 0)
            {
                DialogResult result =
                MessageBox.Show
                (
                    Resources.String_AreYouSureYouWantToDeleteSelectedTickets,
                    Resources.String_DeleteTickets,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1,
                    FormMessageBoxOptions
                );

                if (result != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }

                // Delete tickets.
                for (int i = 0; i < ticketsListUserControl.TicketsListView.SelectedItems.Count; i++)
                {
                    Ticket ticket = (Ticket)ticketsListUserControl.TicketsListView.SelectedItems[i].Tag;
                    ticket.Delete();
                }

                // Flush.
                Database.Flush();

                // Show tickets.
                ShowTickets();
            }
            return;
        }

        private void changeProjectDetailsButton_Click(object sender, EventArgs e)
        {
            ChangeProjectDetails();
            return;
        }

        private void MilestonesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsEnabledProperty();
            return;
        }

        private void MilestonesListView_DoubleClick(object sender, EventArgs e)
        {
            ShowMilestone();
            return;
        }

        private void addMilestoneButton_Click(object sender, EventArgs e)
        {
            AddMilestone();
            return;
        }

        private void showMilestoneButton_Click(object sender, EventArgs e)
        {
            ShowMilestone();
            return;
        }

        private void editMilestoneButton_Click(object sender, EventArgs e)
        {
            EditMilestone();
            return;
        }

        private void deleteMilestoneButton_Click(object sender, EventArgs e)
        {
            DeleteMilestone();
            return;
        }

        private void TicketsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsEnabledProperty();
            return;
        }

        private void TicketsListView_DoubleClick(object sender, EventArgs e)
        {
            ShowTicket();
            return;
        }

        private void addTicketButton_Click(object sender, EventArgs e)
        {
            AddTicket();
            return;
        }

        private void showTicketButton_Click(object sender, EventArgs e)
        {
            ShowTicket();
            return;
        }

        private void editTicketButton_Click(object sender, EventArgs e)
        {
            EditTicket();
            return;
        }

        private void showTicketHistoryButton_Click(object sender, EventArgs e)
        {
            ShowTicketHistory();
            return;
        }

        private void showTicketAttachmentsButton_Click(object sender, EventArgs e)
        {
            ShowAttachments();
            return;
        }

        private void deleteTicketButton_Click(object sender, EventArgs e)
        {
            DeleteTicket();
            return;
        }
    }
}
