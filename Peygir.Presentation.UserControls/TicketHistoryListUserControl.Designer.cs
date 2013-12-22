namespace Peygir.Presentation.UserControls
{
    partial class TicketHistoryListUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TicketHistoryListUserControl));
            this.ticketHistoryListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // ticketHistoryListView
            // 
            resources.ApplyResources(this.ticketHistoryListView, "ticketHistoryListView");
            this.ticketHistoryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.ticketHistoryListView.FullRowSelect = true;
            this.ticketHistoryListView.HideSelection = false;
            this.ticketHistoryListView.Name = "ticketHistoryListView";
            this.ticketHistoryListView.UseCompatibleStateImageBehavior = false;
            this.ticketHistoryListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // TicketHistoryListUserControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ticketHistoryListView);
            this.Name = "TicketHistoryListUserControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ticketHistoryListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
