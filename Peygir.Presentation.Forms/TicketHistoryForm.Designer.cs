namespace Peygir.Presentation.Forms
{
    partial class TicketHistoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TicketHistoryForm));
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.ticketHistoryDetailsUserControl = new Peygir.Presentation.UserControls.TicketHistoryDetailsUserControl();
            this.ticketHistoryListUserControl = new Peygir.Presentation.UserControls.TicketHistoryListUserControl();
            this.okButton = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            resources.ApplyResources(this.groupBox, "groupBox");
            this.groupBox.Controls.Add(this.ticketHistoryDetailsUserControl);
            this.groupBox.Name = "groupBox";
            this.groupBox.TabStop = false;
            // 
            // ticketHistoryDetailsUserControl
            // 
            resources.ApplyResources(this.ticketHistoryDetailsUserControl, "ticketHistoryDetailsUserControl");
            this.ticketHistoryDetailsUserControl.DateTimeFormatter = null;
            this.ticketHistoryDetailsUserControl.Name = "ticketHistoryDetailsUserControl";
            this.ticketHistoryDetailsUserControl.ReadOnly = false;
            // 
            // ticketHistoryListUserControl
            // 
            resources.ApplyResources(this.ticketHistoryListUserControl, "ticketHistoryListUserControl");
            this.ticketHistoryListUserControl.DateTimeFormatter = null;
            this.ticketHistoryListUserControl.Name = "ticketHistoryListUserControl";
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Image = global::Peygir.Presentation.Forms.Properties.Resources.Image_OK;
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // TicketHistoryForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.okButton;
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.ticketHistoryListUserControl);
            this.Controls.Add(this.groupBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TicketHistoryForm";
            this.ShowInTaskbar = false;
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private UserControls.TicketHistoryListUserControl ticketHistoryListUserControl;
        private UserControls.TicketHistoryDetailsUserControl ticketHistoryDetailsUserControl;
        private System.Windows.Forms.Button okButton;
    }
}