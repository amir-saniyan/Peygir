using Peygir.Data;
using Peygir.Data.PeygirDatabaseDataSetTableAdapters;
using Peygir.Logic.Properties;
using System;
using System.Collections.Generic;

namespace Peygir.Logic
{
    public class Ticket : DBObject
    {
        public static string[] GetReporters()
        {
            TicketReportersTableAdapter tableAdapter = Database.TicketReportersTableAdapter;

            PeygirDatabaseDataSet.TicketReportersDataTable rows = tableAdapter.GetData();

            // Create list.
            List<string> reporters = new List<string>();
            foreach (var row in rows)
            {
                // Add.
                string reporter = row.Reporter;
                reporters.Add(reporter);
            }

            return reporters.ToArray();
        }

        public static string[] GetAssignees()
        {
            TicketAssigneesTableAdapter tableAdapter = Database.TicketAssigneesTableAdapter;

            PeygirDatabaseDataSet.TicketAssigneesDataTable rows = tableAdapter.GetData();

            // Create list.
            List<string> assignees = new List<string>();
            foreach (var row in rows)
            {
                // Add.
                string assignee = row.Assignee;
                assignees.Add(assignee);
            }

            return assignees.ToArray();
        }

        public static Ticket[] GetTickets(int milestoneID)
        {
            TicketsTableAdapter tableAdapter = Database.TicketsTableAdapter;

            PeygirDatabaseDataSet.TicketsDataTable rows = tableAdapter.GetDataByMilestoneID(milestoneID);

            // Create list.
            List<Ticket> tickets = new List<Ticket>();
            foreach (var row in rows)
            {
                // Add.
                Ticket ticket = new Ticket(row);
                tickets.Add(ticket);
            }

            return tickets.ToArray();
        }

        public static Ticket GetTicket(int id)
        {
            TicketsTableAdapter tableAdapter = Database.TicketsTableAdapter;

            PeygirDatabaseDataSet.TicketsDataTable rows = tableAdapter.GetDataByID(id);

            if (rows.Count == 1)
            {
                // Found.
                Ticket ticket = new Ticket(rows[0]);
                return ticket;
            }

            // Not found.
            return null;
        }

        public int MilestoneID
        {
            get { return milestoneID; }
            set
            {
                if (value == InvalidID)
                {
                    string message = Resources.String_InvalidMilestoneID;
                    throw new ArgumentException(message, "value");
                }

                milestoneID = value;
            }
        }

        public int TicketNumber
        {
            get { return ticketNumber; }
            set { ticketNumber = value; }
        }

        public string Summary
        {
            get { return summary; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                summary = value;
            }
        }

        public string ReportedBy
        {
            get { return reportedBy; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                reportedBy = value;
            }
        }

        public TicketType Type
        {
            get { return type; }
            set { type = value; }
        }

        public TicketSeverity Severity
        {
            get { return severity; }
            set { severity = value; }
        }

        public TicketState State
        {
            get { return state; }
            set { state = value; }
        }

        public string AssignedTo
        {
            get { return assignedTo; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                assignedTo = value;
            }
        }

        public TicketPriority Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                description = value;
            }
        }

        public Ticket(int projectID, int milestoneID)
        {
            if (projectID == InvalidID)
            {
                string message = Resources.String_InvalidProjectID;
                throw new ArgumentException(message, "projectID");
            }
            if (milestoneID == InvalidID)
            {
                string message = Resources.String_InvalidMilestoneID;
                throw new ArgumentException(message, "milestoneID");
            }

            this.milestoneID = milestoneID;
            ticketNumber = 0;
            summary = string.Empty;
            reportedBy = string.Empty;
            type = (TicketType)(-1);
            severity = (TicketSeverity)(-1);
            state = (TicketState)(-1);
            assignedTo = string.Empty;
            priority = (TicketPriority)(-1);
            description = string.Empty;

            // Find max ticket number.
            TicketsTableAdapter tableAdapter = Database.TicketsTableAdapter;
            int? maxTicketNumber = tableAdapter.GetMaxTicketNumber(projectID);
            if (maxTicketNumber.HasValue)
            {
                ticketNumber = maxTicketNumber.Value + 1;
            }
            else
            {
                ticketNumber = 1;
            }
        }

        public override void Add()
        {
            if (ID != InvalidID)
            {
                string message = Resources.String_CurrentObjectAlreadyAdded;
                throw new InvalidOperationException(message);
            }

            // Add.

            TicketsTableAdapter tableAdapter = Database.TicketsTableAdapter;

            tableAdapter.Insert
            (
                milestoneID,
                ticketNumber,
                summary,
                reportedBy,
                (int)type,
                (int)severity,
                (int)state,
                assignedTo,
                (int)priority,
                description
            );

            // Find ID.
            ID = tableAdapter.GetID
            (
                milestoneID,
                ticketNumber,
                summary,
                reportedBy,
                (int)type,
                (int)severity,
                (int)state,
                assignedTo,
                (int)priority,
                description
            ).Value;

            return;
        }

        public override void Update()
        {
            if (ID == InvalidID)
            {
                string message = Resources.String_CurrentObjectDoesNotExistInTheDatabase;
                throw new InvalidOperationException(message);
            }

            // Update.

            TicketsTableAdapter tableAdapter = Database.TicketsTableAdapter;

            tableAdapter.UpdateByID
            (
                            milestoneID,
                ticketNumber,
                summary,
                reportedBy,
                (int)type,
                (int)severity,
                (int)state,
                assignedTo,
                (int)priority,
                description,
                ID
            );

            return;
        }

        public override void Delete()
        {
            if (ID == InvalidID)
            {
                string message = Resources.String_CurrentObjectDoesNotExistInTheDatabase;
                throw new InvalidOperationException(message);
            }

            // Delete.

            TicketsTableAdapter tableAdapter = Database.TicketsTableAdapter;

            tableAdapter.DeleteByID(ID);

            ID = InvalidID;

            return;
        }

        public Milestone GetMilestone()
        {
            return Milestone.GetMilestone(milestoneID);
        }

        public TicketHistory[] GetHistory()
        {
            if (ID == InvalidID)
            {
                string message = Resources.String_CurrentObjectDoesNotExistInTheDatabase;
                throw new InvalidOperationException(message);
            }

            return TicketHistory.GetTicketHistoryByTicketID(ID);
        }

        public TicketHistory NewHistory()
        {
            if (ID == InvalidID)
            {
                string message = Resources.String_CurrentObjectDoesNotExistInTheDatabase;
                throw new InvalidOperationException(message);
            }

            return new TicketHistory(ID);
        }

        public Attachment[] GetAttachments()
        {
            if (ID == InvalidID)
            {
                string message = Resources.String_CurrentObjectDoesNotExistInTheDatabase;
                throw new InvalidOperationException(message);
            }

            return Attachment.GetAttachments(ID);
        }

        public Attachment[] GetAttachmentsWithoutContents()
        {
            if (ID == InvalidID)
            {
                string message = Resources.String_CurrentObjectDoesNotExistInTheDatabase;
                throw new InvalidOperationException(message);
            }

            return Attachment.GetAttachmentsWithoutContents(ID);
        }

        public Attachment NewAttachment()
        {
            if (ID == InvalidID)
            {
                string message = Resources.String_CurrentObjectDoesNotExistInTheDatabase;
                throw new InvalidOperationException(message);
            }

            return new Attachment(ID);
        }

        public override string ToString()
        {
            return summary;
        }

        internal Ticket(PeygirDatabaseDataSet.TicketsRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            ID = row.ID;
            milestoneID = row.MilestoneID;
            ticketNumber = row.TicketNumber;
            summary = row.Summary;
            reportedBy = row.ReportedBy;
            type = (TicketType)row.Type;
            severity = (TicketSeverity)row.Severity;
            state = (TicketState)row.State;
            assignedTo = row.AssignedTo;
            priority = (TicketPriority)row.Priority;
            description = row.Description;
        }

        private int milestoneID;
        private int ticketNumber;
        private string summary;
        private string reportedBy;
        private TicketType type;
        private TicketSeverity severity;
        private TicketState state;
        private string assignedTo;
        private TicketPriority priority;
        private string description;
    }
}
