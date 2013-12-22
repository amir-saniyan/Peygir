namespace Peygir.Presentation.Forms
{
    partial class MilestoneDetailsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MilestoneDetailsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.milestoneDetailsUserControl = new Peygir.Presentation.UserControls.MilestoneDetailsUserControl();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.milestoneDetailsUserControl1 = new Peygir.Presentation.UserControls.MilestoneDetailsUserControl();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.milestoneDetailsUserControl);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // milestoneDetailsUserControl
            // 
            resources.ApplyResources(this.milestoneDetailsUserControl, "milestoneDetailsUserControl");
            this.milestoneDetailsUserControl.Description = "";
            this.milestoneDetailsUserControl.DisplayOrder = 0;
            this.milestoneDetailsUserControl.MilestoneName = "";
            this.milestoneDetailsUserControl.Name = "milestoneDetailsUserControl";
            this.milestoneDetailsUserControl.ReadOnly = false;
            this.milestoneDetailsUserControl.State = Peygir.Logic.MilestoneState.Active;
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Image = global::Peygir.Presentation.Forms.Properties.Resources.Image_Cancel;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Image = global::Peygir.Presentation.Forms.Properties.Resources.Image_OK;
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // milestoneDetailsUserControl1
            // 
            resources.ApplyResources(this.milestoneDetailsUserControl1, "milestoneDetailsUserControl1");
            this.milestoneDetailsUserControl1.Description = "";
            this.milestoneDetailsUserControl1.DisplayOrder = 0;
            this.milestoneDetailsUserControl1.MilestoneName = "";
            this.milestoneDetailsUserControl1.Name = "milestoneDetailsUserControl1";
            this.milestoneDetailsUserControl1.ReadOnly = false;
            // 
            // MilestoneDetailsForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MilestoneDetailsForm";
            this.ShowInTaskbar = false;
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControls.MilestoneDetailsUserControl milestoneDetailsUserControl;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private UserControls.MilestoneDetailsUserControl milestoneDetailsUserControl1;
    }
}