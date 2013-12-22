using Peygir.Logic;
using Peygir.Presentation.UserControls.Properties;
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
    public partial class MilestonesListUserControl : UserControl
    {
        public ListView MilestonesListView
        {
            get { return milestonesListView; }
        }

        public MilestonesListUserControl()
        {
            InitializeComponent();
        }

        public void ShowMilestones(Milestone[] milestones)
        {
            if (milestones == null)
            {
                throw new ArgumentNullException("milestones");
            }

            milestonesListView.BeginUpdate();

            milestonesListView.Items.Clear();
            foreach (var milestone in milestones)
            {
                string milestoneState;
                switch (milestone.State)
                {
                    case MilestoneState.Active:
                        milestoneState = Resources.String_Active;
                        break;

                    case MilestoneState.Completed:
                        milestoneState = Resources.String_Completed;
                        break;

                    case MilestoneState.Cancelled:
                        milestoneState = Resources.String_Cancelled;
                        break;

                    default:
                        milestoneState = string.Empty;
                        break;
                }

                ListViewItem lvi = new ListViewItem();

                lvi.Text = milestone.Name;
                lvi.SubItems.Add(milestoneState);
                lvi.Tag = milestone;

                milestonesListView.Items.Add(lvi);
            }

            milestonesListView.EndUpdate();

            milestonesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            return;
        }
    }
}
