using Peygir.Data;
using Peygir.Data.PeygirDatabaseDataSetTableAdapters;
using System;
using System.IO;

namespace Peygir.Logic
{
    public static class Database
    {
        private static string currentnDatabasePath;
        public static string CurrentnDatabasePath
        {
            get { return currentnDatabasePath; }
        }

        private static bool isOpen;
        public static bool IsOpen
        {
            get { return isOpen; }
        }

        private static ProjectsTableAdapter projectsTableAdapter;
        internal static ProjectsTableAdapter ProjectsTableAdapter
        {
            get { return projectsTableAdapter; }
        }

        private static MilestonesTableAdapter milestonesTableAdapter;
        internal static MilestonesTableAdapter MilestonesTableAdapter
        {
            get { return milestonesTableAdapter; }
        }

        private static TicketReportersTableAdapter ticketReportersTableAdapter;
        internal static TicketReportersTableAdapter TicketReportersTableAdapter
        {
            get { return ticketReportersTableAdapter; }
        }

        private static TicketAssigneesTableAdapter ticketAssigneesTableAdapter;
        internal static TicketAssigneesTableAdapter TicketAssigneesTableAdapter
        {
            get { return ticketAssigneesTableAdapter; }
        }

        private static TicketsTableAdapter ticketsTableAdapter;
        internal static TicketsTableAdapter TicketsTableAdapter
        {
            get { return ticketsTableAdapter; }
        }

        private static AttachmentsWithoutContentsTableAdapter attachmentsWithoutContentsTableAdapter;
        internal static AttachmentsWithoutContentsTableAdapter AttachmentsWithoutContentsTableAdapter
        {
            get { return attachmentsWithoutContentsTableAdapter; }
        }

        private static AttachmentsTableAdapter attachmentsTableAdapter;
        internal static AttachmentsTableAdapter AttachmentsTableAdapter
        {
            get { return attachmentsTableAdapter; }
        }

        private static TicketsHistoryTableAdapter ticketsHistoryTableAdapter;
        internal static TicketsHistoryTableAdapter TicketsHistoryTableAdapter
        {
            get { return ticketsHistoryTableAdapter; }
        }

        static Database()
        {
            currentnDatabasePath = null;
            isOpen = false;

            projectsTableAdapter = null;
            milestonesTableAdapter = null;
            ticketReportersTableAdapter = null;
            ticketAssigneesTableAdapter = null;
            ticketsTableAdapter = null;
            attachmentsWithoutContentsTableAdapter = null;
            attachmentsTableAdapter = null;
            ticketsHistoryTableAdapter = null;
        }

        public static void CreateAndOpen(string databasePath)
        {
            if (databasePath == null)
            {
                throw new ArgumentNullException("databasePath");
            }

            // Close.
            if (isOpen)
            {
                Close();
            }

            // Copy default database to new location.
            string defaultDatabasePath = PeygirDatabaseDataSet.DefaultDatabaseFileName;
            File.Copy(defaultDatabasePath, databasePath, true);

            // Open database.
            Open(databasePath);

            return;
        }

        public static void Open(string databasePath)
        {
            if (databasePath == null)
            {
                throw new ArgumentNullException("databasePath");
            }

            // Close.
            if (isOpen)
            {
                Close();
            }

            PeygirDatabaseDataSet.ChangeDatabasePath(databasePath);
            
            projectsTableAdapter = new ProjectsTableAdapter();
            milestonesTableAdapter = new MilestonesTableAdapter();
            ticketReportersTableAdapter = new TicketReportersTableAdapter();
            ticketAssigneesTableAdapter = new TicketAssigneesTableAdapter();
            ticketsTableAdapter = new TicketsTableAdapter();
            attachmentsWithoutContentsTableAdapter = new AttachmentsWithoutContentsTableAdapter();
            attachmentsTableAdapter = new AttachmentsTableAdapter();
            ticketsHistoryTableAdapter = new TicketsHistoryTableAdapter();

            projectsTableAdapter.Connection.Open();
            milestonesTableAdapter.Connection.Open();
            ticketReportersTableAdapter.Connection.Open();
            ticketAssigneesTableAdapter.Connection.Open();
            ticketsTableAdapter.Connection.Open();
            attachmentsWithoutContentsTableAdapter.Connection.Open();
            attachmentsTableAdapter.Connection.Open();
            ticketsHistoryTableAdapter.Connection.Open();

            currentnDatabasePath = databasePath;
            isOpen = true;

            return;
        }

        public static void Close()
        {
            if (!isOpen)
            {
                return;
            }

            projectsTableAdapter.Connection.Close();
            milestonesTableAdapter.Connection.Close();
            ticketReportersTableAdapter.Connection.Close();
            ticketAssigneesTableAdapter.Connection.Close();
            ticketsTableAdapter.Connection.Close();
            attachmentsWithoutContentsTableAdapter.Connection.Close();
            attachmentsTableAdapter.Connection.Close();
            ticketsHistoryTableAdapter.Connection.Close();

            projectsTableAdapter = null;
            milestonesTableAdapter = null;
            ticketReportersTableAdapter = null;
            ticketAssigneesTableAdapter = null;
            ticketsTableAdapter = null;
            attachmentsWithoutContentsTableAdapter = null;
            attachmentsTableAdapter = null;
            ticketsHistoryTableAdapter = null;

            currentnDatabasePath = null;
            isOpen = false;

            return;
        }

        public static void Flush()
        {
            if (!isOpen)
            {
                throw new InvalidOperationException();
            }

            string databasePath = Database.CurrentnDatabasePath;

            // Reconnect.
            Database.Close();
            Database.Open(databasePath);

            return;
        }
    }
}
