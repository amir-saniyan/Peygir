using Peygir.Data;
using Peygir.Data.PeygirDatabaseDataSetTableAdapters;
using Peygir.Logic.Properties;
using System;
using System.Collections.Generic;

namespace Peygir.Logic
{
    public class Attachment : DBObject
    {
        public static Attachment[] GetAttachments()
        {
            AttachmentsTableAdapter tableAdapter = Database.AttachmentsTableAdapter;

            PeygirDatabaseDataSet.AttachmentsDataTable rows = tableAdapter.GetData();

            // Create list.
            List<Attachment> attachments = new List<Attachment>();
            foreach (var row in rows)
            {
                // Add.
                Attachment attachment = new Attachment(row);
                attachments.Add(attachment);
            }

            return attachments.ToArray();
        }

        public static Attachment[] GetAttachments(int ticketID)
        {
            AttachmentsTableAdapter tableAdapter = Database.AttachmentsTableAdapter;

            PeygirDatabaseDataSet.AttachmentsDataTable rows = tableAdapter.GetDataByTicketID(ticketID);

            // Create list.
            List<Attachment> attachments = new List<Attachment>();
            foreach (var row in rows)
            {
                // Add.
                Attachment attachment = new Attachment(row);
                attachments.Add(attachment);
            }

            return attachments.ToArray();
        }

        public static Attachment[] GetAttachmentsWithoutContents()
        {
            AttachmentsWithoutContentsTableAdapter tableAdapter = Database.AttachmentsWithoutContentsTableAdapter;

            PeygirDatabaseDataSet.AttachmentsWithoutContentsDataTable rows = tableAdapter.GetData();

            // Create list.
            List<Attachment> attachments = new List<Attachment>();
            foreach (var row in rows)
            {
                // Add.
                Attachment attachment = new Attachment(row);
                attachments.Add(attachment);
            }

            return attachments.ToArray();
        }

        public static Attachment[] GetAttachmentsWithoutContents(int ticketID)
        {
            AttachmentsWithoutContentsTableAdapter tableAdapter = Database.AttachmentsWithoutContentsTableAdapter;

            PeygirDatabaseDataSet.AttachmentsWithoutContentsDataTable rows = tableAdapter.GetDataByTicketID(ticketID);

            // Create list.
            List<Attachment> attachments = new List<Attachment>();
            foreach (var row in rows)
            {
                // Add.
                Attachment attachment = new Attachment(row);
                attachments.Add(attachment);
            }

            return attachments.ToArray();
        }


        public static Attachment GetAttachment(int id)
        {
            AttachmentsTableAdapter tableAdapter = Database.AttachmentsTableAdapter;

            PeygirDatabaseDataSet.AttachmentsDataTable rows = tableAdapter.GetDataByID(id);

            if (rows.Count == 1)
            {
                // Found.
                Attachment attachment = new Attachment(rows[0]);
                return attachment;
            }

            // Not found.
            return null;
        }

        public int TicketID
        {
            get { return ticketID; }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                name = value;
            }
        }

        public int Size
        {
            get { return size; }
        }

        public Attachment(int ticketID)
        {
            if (ticketID == InvalidID)
            {
                string message = Resources.String_InvalidTicketID;
                throw new ArgumentException(message, "ticketID");
            }

            this.ticketID = ticketID;
            name = string.Empty;
            size = 0;
            contents = string.Empty;
        }

        public override void Add()
        {
            if (ID != InvalidID)
            {
                string message = Resources.String_CurrentObjectAlreadyAdded;
                throw new InvalidOperationException(message);
            }

            // Add.

            AttachmentsTableAdapter tableAdapter = Database.AttachmentsTableAdapter;

            tableAdapter.Insert(ticketID, name, size, contents);

            // Find ID.
            ID = tableAdapter.GetID(ticketID, name, size).Value;

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

            AttachmentsTableAdapter tableAdapter = Database.AttachmentsTableAdapter;

            tableAdapter.UpdateByID(ticketID, name, size, contents, ID);

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

            AttachmentsTableAdapter tableAdapter = Database.AttachmentsTableAdapter;

            tableAdapter.DeleteByID(ID);

            ID = InvalidID;

            return;
        }

        public byte[] GetContents()
        {
            return Convert.FromBase64String(contents);
        }

        public void SetContents(byte[] contents)
        {
            if (contents == null)
            {
                throw new ArgumentNullException("contents");
            }

            this.contents = Convert.ToBase64String(contents);
            this.size = contents.Length;
        }

        public Ticket GetTicket()
        {
            return Ticket.GetTicket(ticketID);
        }

        public override string ToString()
        {
            return name;
        }

        internal Attachment(PeygirDatabaseDataSet.AttachmentsRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            ID = row.ID;
            ticketID = row.TicketID;
            name = row.Name;
            size = row.Size;
            contents = row.Contents;
        }

        internal Attachment(PeygirDatabaseDataSet.AttachmentsWithoutContentsRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            ID = row.ID;
            ticketID = row.TicketID;
            name = row.Name;
            size = row.Size;
            contents = Convert.ToBase64String(new byte[0]);
        }

        private int ticketID;
        private string name;
        private int size;
        private string contents;
    }
}
