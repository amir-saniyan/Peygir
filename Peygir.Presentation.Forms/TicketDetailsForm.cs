using Peygir.Presentation.UserControls;
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
    public partial class TicketDetailsForm : Form
    {
        public TicketDetailsUserControl TicketDetailsUserControl
        {
            get { return ticketDetailsUserControl; }
        }

        public TicketDetailsForm()
        {
            InitializeComponent();
        }
    }
}
