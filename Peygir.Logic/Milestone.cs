using Peygir.Data;
using Peygir.Data.PeygirDatabaseDataSetTableAdapters;
using Peygir.Logic.Properties;
using System;
using System.Collections.Generic;

namespace Peygir.Logic
{
    public class Milestone : DBObject
    {
        public static Milestone[] GetMilestones()
        {
            MilestonesTableAdapter tableAdapter = Database.MilestonesTableAdapter;

            PeygirDatabaseDataSet.MilestonesDataTable rows = tableAdapter.GetData();

            // Create list.
            List<Milestone> milestones = new List<Milestone>();
            foreach (var row in rows)
            {
                // Add.
                Milestone milestone = new Milestone(row);
                milestones.Add(milestone);
            }

            return milestones.ToArray();
        }

        public static Milestone[] GetMilestones(int projectID)
        {
            MilestonesTableAdapter tableAdapter = Database.MilestonesTableAdapter;

            PeygirDatabaseDataSet.MilestonesDataTable rows = tableAdapter.GetDataByProjectID(projectID);

            // Create list.
            List<Milestone> milestones = new List<Milestone>();
            foreach (var row in rows)
            {
                // Add.
                Milestone milestone = new Milestone(row);
                milestones.Add(milestone);
            }

            return milestones.ToArray();
        }

        public static Milestone GetMilestone(int id)
        {
            MilestonesTableAdapter tableAdapter = Database.MilestonesTableAdapter;

            PeygirDatabaseDataSet.MilestonesDataTable rows = tableAdapter.GetDataByID(id);

            if (rows.Count == 1)
            {
                // Found.
                Milestone milestone = new Milestone(rows[0]);
                return milestone;
            }

            // Not found.
            return null;
        }

        public int ProjectID
        {
            get { return projectID; }
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

        public MilestoneState State
        {
            get { return state; }
            set { state = value; }
        }

        public int DisplayOrder
        {
            get { return displayOrder; }
            set { displayOrder = value; }
        }

        public Milestone(int projectID)
        {
            if (projectID == InvalidID)
            {
                string message = Resources.String_InvalidProjectID;
                throw new ArgumentException(message, "projectID");
            }

            this.projectID = projectID;
            name = string.Empty;
            description = string.Empty;
            state = (MilestoneState)(-1);
            displayOrder = -1;

            // Find max display order.
            MilestonesTableAdapter tableAdapter = Database.MilestonesTableAdapter;
            int? maxDisplayOrder = tableAdapter.GetMaxDisplayOrder(projectID);
            if (maxDisplayOrder.HasValue)
            {
                displayOrder = maxDisplayOrder.Value + 1;
            }
            else
            {
                displayOrder = 1;
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

            MilestonesTableAdapter tableAdapter = Database.MilestonesTableAdapter;

            tableAdapter.Insert(projectID, name, description, (int)state, displayOrder);

            // Find ID.
            ID = tableAdapter.GetID(projectID, name, description, (int)state, displayOrder).Value;

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

            MilestonesTableAdapter tableAdapter = Database.MilestonesTableAdapter;

            tableAdapter.UpdateByID(projectID, name, description, (int)state, displayOrder, ID);

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

            MilestonesTableAdapter tableAdapter = Database.MilestonesTableAdapter;

            tableAdapter.DeleteByID(ID);

            ID = InvalidID;

            return;
        }

        public Project GetProject()
        {
            return Project.GetProject(projectID);
        }

        public Ticket[] GetTickets()
        {
            if (ID == InvalidID)
            {
                string message = Resources.String_CurrentObjectDoesNotExistInTheDatabase;
                throw new InvalidOperationException(message);
            }

            return Ticket.GetTickets(ID);
        }

        public Ticket NewTicket()
        {
            if (ID == InvalidID)
            {
                string message = Resources.String_CurrentObjectDoesNotExistInTheDatabase;
                throw new InvalidOperationException(message);
            }

            return new Ticket(ProjectID, ID);
        }

        public override string ToString()
        {
            return name;
        }

        internal Milestone(PeygirDatabaseDataSet.MilestonesRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            ID = row.ID;
            projectID = row.ProjectID;
            name = row.Name;
            description = row.Description;
            state = (MilestoneState)row.State;
            displayOrder = row.DisplayOrder;
        }

        private int projectID;
        private string name;
        private string description;
        private MilestoneState state;
        private int displayOrder;
    }
}
