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
    public partial class ProjectDetailsUserControl : UserControl
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

        public string ProjectName
        {
            get { return nameTextBox.Text; }
            set { nameTextBox.Text = value; }
        }

        public int DisplayOrder
        {
            get { return (int)displayOrderNumericUpDown.Value; }
            set { displayOrderNumericUpDown.Value = Math.Min(value, displayOrderNumericUpDown.Maximum); }
        }

        public string Description
        {
            get { return descriptionTextBox.Text; }
            set { descriptionTextBox.Text = value; }
        }

        public ProjectDetailsUserControl()
        {
            InitializeComponent();

            ReadOnly = false;
        }

        public void ShowProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            nameTextBox.Text = project.Name;
            displayOrderNumericUpDown.Value = Math.Min(project.DisplayOrder, displayOrderNumericUpDown.Maximum);
            descriptionTextBox.Text = project.Description;

            return;
        }

        public void RetrieveProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            project.Name = nameTextBox.Text;
            project.DisplayOrder = (int)displayOrderNumericUpDown.Value;
            project.Description = descriptionTextBox.Text;

            return;
        }

        private void UpdateReadOnlyState()
        {
            nameTextBox.ReadOnly = readOnly;
            displayOrderNumericUpDown.ReadOnly = readOnly;
            descriptionTextBox.ReadOnly = readOnly;

            return;
        }
    }
}
