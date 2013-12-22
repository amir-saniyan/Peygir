using Peygir.Data;
using Peygir.Data.PeygirDatabaseDataSetTableAdapters;
using Peygir.Logic.Properties;
using System;
using System.Collections.Generic;

namespace Peygir.Logic
{
    public class TicketHistory : DBObject
    {
        public static TicketHistory[] GetTicketsHistory()
        {
            TicketsHistoryTableAdapter tableAdapter = Database.TicketsHistoryTableAdapter;

            PeygirDatabaseDataSet.TicketsHistoryDataTable rows = tableAdapter.GetData();

            // Create list.
            List<TicketHistory> ticketsHistory = new List<TicketHistory>();
            foreach (var row in rows)
            {
                // Add.
                TicketHistory ticketHistory = new TicketHistory(row);
                ticketsHistory.Add(ticketHistory);
            }

            return ticketsHistory.ToArray();
        }

        public static TicketHistory[] GetTicketHistoryByTicketID(int ticketID)
        {
            TicketsHistoryTableAdapter tableAdapter = Database.TicketsHistoryTableAdapter;

            PeygirDatabaseDataSet.TicketsHistoryDataTable rows = tableAdapter.GetDataByTicketID(ticketID);

            // Create list.
            List<TicketHistory> ticketsHistory = new List<TicketHistory>();
            foreach (var row in rows)
            {
                // Add.
                TicketHistory ticketHistory = new TicketHistory(row);
                ticketsHistory.Add(ticketHistory);
            }

            return ticketsHistory.ToArray();
        }

        public static TicketHistory GetTicketHistory(int id)
        {
            TicketsHistoryTableAdapter tableAdapter = Database.TicketsHistoryTableAdapter;

            PeygirDatabaseDataSet.TicketsHistoryDataTable rows = tableAdapter.GetDataByID(id);

            if (rows.Count == 1)
            {
                // Found.
                TicketHistory ticketsHistory = new TicketHistory(rows[0]);
                return ticketsHistory;
            }

            // Not found.
            return null;
        }

        public int TicketID
        {
            get { return ticketID; }
        }

        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public string Changes
        {
            get { return changes; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                changes = value;
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                comment = value;
            }
        }

        public TicketHistory(int ticketID)
        {
            if (ticketID == InvalidID)
            {
                string message = Resources.String_InvalidTicketID;
                throw new ArgumentException(message, "ticketID");
            }

            this.ticketID = ticketID;
            timestamp = new DateTime();
            changes = string.Empty;
            comment = string.Empty;
        }

        public override void Add()
        {
            if (ID != InvalidID)
            {
                string message = Resources.String_CurrentObjectAlreadyAdded;
                throw new InvalidOperationException(message);
            }

            // Add.

            TicketsHistoryTableAdapter tableAdapter = Database.TicketsHistoryTableAdapter;

            tableAdapter.Insert(ticketID, timestamp, changes, comment);

            // Find ID.
            ID = tableAdapter.GetID(ticketID, timestamp, changes, comment).Value;

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

            TicketsHistoryTableAdapter tableAdapter = Database.TicketsHistoryTableAdapter;

            tableAdapter.UpdateByID(ticketID, timestamp, changes, comment, ID);

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

            TicketsHistoryTableAdapter tableAdapter = Database.TicketsHistoryTableAdapter;

            tableAdapter.DeleteByID(ID);

            ID = InvalidID;

            return;
        }

        public Ticket GetTicket()
        {
            return Ticket.GetTicket(ticketID);
        }

        internal TicketHistory(PeygirDatabaseDataSet.TicketsHistoryRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            ID = row.ID;
            ticketID = row.TicketID;
            timestamp = row.Timestamp;
            changes = row.Changes;
            comment = row.Comment;
        }

        private int ticketID;
        private DateTime timestamp;
        private string changes;
        private string comment;
    }
}
