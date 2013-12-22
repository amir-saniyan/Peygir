namespace Peygir.Presentation.UserControls
{
    partial class TicketHistoryDetailsUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TicketHistoryDetailsUserControl));
            this.commentTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.changesTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timestampTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // commentTextBox
            // 
            resources.ApplyResources(this.commentTextBox, "commentTextBox");
            this.commentTextBox.Name = "commentTextBox";
            this.commentTextBox.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // changesTextBox
            // 
            resources.ApplyResources(this.changesTextBox, "changesTextBox");
            this.changesTextBox.Name = "changesTextBox";
            this.changesTextBox.ReadOnly = true;
            this.changesTextBox.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // timestampTextBox
            // 
            resources.ApplyResources(this.timestampTextBox, "timestampTextBox");
            this.timestampTextBox.Name = "timestampTextBox";
            this.timestampTextBox.ReadOnly = true;
            this.timestampTextBox.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // TicketHistoryDetailsUserControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.commentTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.changesTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.timestampTextBox);
            this.Controls.Add(this.label1);
            this.Name = "TicketHistoryDetailsUserControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox commentTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox changesTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox timestampTextBox;
        private System.Windows.Forms.Label label1;
    }
}
