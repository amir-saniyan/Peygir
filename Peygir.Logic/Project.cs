using Peygir.Data;
using Peygir.Data.PeygirDatabaseDataSetTableAdapters;
using Peygir.Logic.Properties;
using System;
using System.Collections.Generic;

namespace Peygir.Logic
{
    public class Project : DBObject
    {
        public static Project[] GetProjects()
        {
            ProjectsTableAdapter tableAdapter = Database.ProjectsTableAdapter;

            PeygirDatabaseDataSet.ProjectsDataTable rows = tableAdapter.GetData();

            // Create list.
            List<Project> projects = new List<Project>();
            foreach (var row in rows)
            {
                // Add.
                Project project = new Project(row);
                projects.Add(project);
            }

            return projects.ToArray();
        }

        public static Project GetProject(int id)
        {
            ProjectsTableAdapter tableAdapter = Database.ProjectsTableAdapter;

            PeygirDatabaseDataSet.ProjectsDataTable rows = tableAdapter.GetDataByID(id);

            if (rows.Count == 1)
            {
                // Found.
                Project project = new Project(rows[0]);
                return project;
            }

            // Not found.
            return null;
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

        public int DisplayOrder
        {
            get { return displayOrder; }
            set { displayOrder = value; }
        }

        public Project()
        {
            name = string.Empty;
            description = string.Empty;
            displayOrder = -1;

            // Find max display order.
            ProjectsTableAdapter tableAdapter = Database.ProjectsTableAdapter;
            int? maxDisplayOrder = tableAdapter.GetMaxDisplayOrder();
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

            ProjectsTableAdapter tableAdapter = Database.ProjectsTableAdapter;

            tableAdapter.Insert(name, description, displayOrder);

            // Find ID.
            ID = tableAdapter.GetID(name, description, displayOrder).Value;

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

            ProjectsTableAdapter tableAdapter = Database.ProjectsTableAdapter;

            tableAdapter.UpdateByID(name, description, displayOrder, ID);

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

            ProjectsTableAdapter tableAdapter = Database.ProjectsTableAdapter;

            tableAdapter.DeleteByID(ID);

            ID = InvalidID;

            return;
        }

        public Milestone[] GetMilestones()
        {
            if (ID == InvalidID)
            {
                string message = Resources.String_CurrentObjectDoesNotExistInTheDatabase;
                throw new InvalidOperationException(message);
            }

            return Milestone.GetMilestones(ID);
        }

        public Milestone NewMilestone()
        {
            if (ID == InvalidID)
            {
                string message = Resources.String_CurrentObjectDoesNotExistInTheDatabase;
                throw new InvalidOperationException(message);
            }

            return new Milestone(ID);
        }

        public Ticket[] GetTickets()
        {
            if (ID == InvalidID)
            {
                string message = Resources.String_CurrentObjectDoesNotExistInTheDatabase;
                throw new InvalidOperationException(message);
            }

            TicketsTableAdapter tableAdapter = new TicketsTableAdapter();

            PeygirDatabaseDataSet.TicketsDataTable rows = tableAdapter.GetDataByProjectID(ID);

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

        public override string ToString()
        {
            return name;
        }

        internal Project(PeygirDatabaseDataSet.ProjectsRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            ID = row.ID;
            name = row.Name;
            description = row.Description;
            displayOrder = row.DisplayOrder;
        }

        private string name;
        private string description;
        private int displayOrder;
    }
}
