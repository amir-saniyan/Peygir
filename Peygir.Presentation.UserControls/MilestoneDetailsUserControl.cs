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
    public partial class MilestoneDetailsUserControl : UserControl
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

        public string MilestoneName
        {
            get { return nameTextBox.Text; }
            set { nameTextBox.Text = value; }
        }

        public MilestoneState State
        {
            get { return (MilestoneState)stateComboBox.SelectedIndex; }
            set { stateComboBox.SelectedIndex = (int)value; }
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

        public MilestoneDetailsUserControl()
        {
            InitializeComponent();

            ReadOnly = false;
        }

        public void ShowMilestone(Milestone milestone)
        {
            if (milestone == null)
            {
                throw new ArgumentNullException("milestone");
            }

            nameTextBox.Text = milestone.Name;
            stateComboBox.SelectedIndex = (int)milestone.State;
            displayOrderNumericUpDown.Value = Math.Min(milestone.DisplayOrder, displayOrderNumericUpDown.Maximum);
            descriptionTextBox.Text = milestone.Description;

            return;
        }

        public void RetrieveMilestone(Milestone milestone)
        {
            if (milestone == null)
            {
                throw new ArgumentNullException("milestone");
            }

            milestone.Name = nameTextBox.Text;
            milestone.State = (MilestoneState)stateComboBox.SelectedIndex;
            milestone.DisplayOrder = (int)displayOrderNumericUpDown.Value;
            milestone.Description = descriptionTextBox.Text;

            return;
        }

        private void UpdateReadOnlyState()
        {
            nameTextBox.ReadOnly = readOnly;
            stateComboBox.Enabled = !readOnly;
            displayOrderNumericUpDown.ReadOnly = readOnly;
            descriptionTextBox.ReadOnly = readOnly;

            return;
        }
    }
}
