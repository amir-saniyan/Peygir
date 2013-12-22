namespace Peygir.Presentation.UserControls
{
    partial class TicketDetailsUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TicketDetailsUserControl));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.milestoneComboBox = new System.Windows.Forms.ComboBox();
            this.priorityComboBox = new System.Windows.Forms.ComboBox();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.severityComboBox = new System.Windows.Forms.ComboBox();
            this.stateComboBox = new System.Windows.Forms.ComboBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ticketNumberTextBox = new System.Windows.Forms.TextBox();
            this.summaryTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.reportedByComboBox = new System.Windows.Forms.ComboBox();
            this.assignedToComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // milestoneComboBox
            // 
            this.milestoneComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.milestoneComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.milestoneComboBox, "milestoneComboBox");
            this.milestoneComboBox.Name = "milestoneComboBox";
            // 
            // priorityComboBox
            // 
            this.priorityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.priorityComboBox.FormattingEnabled = true;
            this.priorityComboBox.Items.AddRange(new object[] {
            resources.GetString("priorityComboBox.Items"),
            resources.GetString("priorityComboBox.Items1"),
            resources.GetString("priorityComboBox.Items2"),
            resources.GetString("priorityComboBox.Items3"),
            resources.GetString("priorityComboBox.Items4")});
            resources.ApplyResources(this.priorityComboBox, "priorityComboBox");
            this.priorityComboBox.Name = "priorityComboBox";
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            resources.GetString("typeComboBox.Items"),
            resources.GetString("typeComboBox.Items1"),
            resources.GetString("typeComboBox.Items2")});
            resources.ApplyResources(this.typeComboBox, "typeComboBox");
            this.typeComboBox.Name = "typeComboBox";
            // 
            // severityComboBox
            // 
            this.severityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.severityComboBox.FormattingEnabled = true;
            this.severityComboBox.Items.AddRange(new object[] {
            resources.GetString("severityComboBox.Items"),
            resources.GetString("severityComboBox.Items1"),
            resources.GetString("severityComboBox.Items2"),
            resources.GetString("severityComboBox.Items3"),
            resources.GetString("severityComboBox.Items4"),
            resources.GetString("severityComboBox.Items5")});
            resources.ApplyResources(this.severityComboBox, "severityComboBox");
            this.severityComboBox.Name = "severityComboBox";
            // 
            // stateComboBox
            // 
            this.stateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stateComboBox.FormattingEnabled = true;
            this.stateComboBox.Items.AddRange(new object[] {
            resources.GetString("stateComboBox.Items"),
            resources.GetString("stateComboBox.Items1"),
            resources.GetString("stateComboBox.Items2"),
            resources.GetString("stateComboBox.Items3")});
            resources.ApplyResources(this.stateComboBox, "stateComboBox");
            this.stateComboBox.Name = "stateComboBox";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.AcceptsReturn = true;
            resources.ApplyResources(this.descriptionTextBox, "descriptionTextBox");
            this.descriptionTextBox.Name = "descriptionTextBox";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // ticketNumberTextBox
            // 
            resources.ApplyResources(this.ticketNumberTextBox, "ticketNumberTextBox");
            this.ticketNumberTextBox.Name = "ticketNumberTextBox";
            this.ticketNumberTextBox.ReadOnly = true;
            this.ticketNumberTextBox.TabStop = false;
            // 
            // summaryTextBox
            // 
            resources.ApplyResources(this.summaryTextBox, "summaryTextBox");
            this.summaryTextBox.Name = "summaryTextBox";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // reportedByComboBox
            // 
            this.reportedByComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.reportedByComboBox, "reportedByComboBox");
            this.reportedByComboBox.Name = "reportedByComboBox";
            // 
            // assignedToComboBox
            // 
            this.assignedToComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.assignedToComboBox, "assignedToComboBox");
            this.assignedToComboBox.Name = "assignedToComboBox";
            // 
            // TicketDetailsUserControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.assignedToComboBox);
            this.Controls.Add(this.reportedByComboBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.milestoneComboBox);
            this.Controls.Add(this.priorityComboBox);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.severityComboBox);
            this.Controls.Add(this.stateComboBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ticketNumberTextBox);
            this.Controls.Add(this.summaryTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "TicketDetailsUserControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox milestoneComboBox;
        private System.Windows.Forms.ComboBox priorityComboBox;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.ComboBox severityComboBox;
        private System.Windows.Forms.ComboBox stateComboBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ticketNumberTextBox;
        private System.Windows.Forms.TextBox summaryTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox reportedByComboBox;
        private System.Windows.Forms.ComboBox assignedToComboBox;
    }
}
