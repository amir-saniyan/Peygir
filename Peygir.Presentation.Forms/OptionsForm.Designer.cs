namespace Peygir.Presentation.Forms
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.formatDateTimePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePatternTextBox = new System.Windows.Forms.TextBox();
            this.calendarComboBox = new System.Windows.Forms.ComboBox();
            this.formatDateTimeCheckBox = new System.Windows.Forms.CheckBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.formatDateTimePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.formatDateTimePanel);
            this.groupBox1.Controls.Add(this.formatDateTimeCheckBox);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            // 
            // formatDateTimePanel
            // 
            resources.ApplyResources(this.formatDateTimePanel, "formatDateTimePanel");
            this.formatDateTimePanel.Controls.Add(this.label1);
            this.formatDateTimePanel.Controls.Add(this.label2);
            this.formatDateTimePanel.Controls.Add(this.dateTimePatternTextBox);
            this.formatDateTimePanel.Controls.Add(this.calendarComboBox);
            this.formatDateTimePanel.Name = "formatDateTimePanel";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // dateTimePatternTextBox
            // 
            resources.ApplyResources(this.dateTimePatternTextBox, "dateTimePatternTextBox");
            this.dateTimePatternTextBox.Name = "dateTimePatternTextBox";
            // 
            // calendarComboBox
            // 
            resources.ApplyResources(this.calendarComboBox, "calendarComboBox");
            this.calendarComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.calendarComboBox.FormattingEnabled = true;
            this.calendarComboBox.Items.AddRange(new object[] {
            resources.GetString("calendarComboBox.Items"),
            resources.GetString("calendarComboBox.Items1"),
            resources.GetString("calendarComboBox.Items2"),
            resources.GetString("calendarComboBox.Items3"),
            resources.GetString("calendarComboBox.Items4"),
            resources.GetString("calendarComboBox.Items5"),
            resources.GetString("calendarComboBox.Items6"),
            resources.GetString("calendarComboBox.Items7"),
            resources.GetString("calendarComboBox.Items8"),
            resources.GetString("calendarComboBox.Items9"),
            resources.GetString("calendarComboBox.Items10"),
            resources.GetString("calendarComboBox.Items11"),
            resources.GetString("calendarComboBox.Items12"),
            resources.GetString("calendarComboBox.Items13")});
            this.calendarComboBox.Name = "calendarComboBox";
            // 
            // formatDateTimeCheckBox
            // 
            resources.ApplyResources(this.formatDateTimeCheckBox, "formatDateTimeCheckBox");
            this.formatDateTimeCheckBox.Name = "formatDateTimeCheckBox";
            this.formatDateTimeCheckBox.UseVisualStyleBackColor = true;
            this.formatDateTimeCheckBox.CheckedChanged += new System.EventHandler(this.formatDateTimeCheckBox_CheckedChanged);
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
            // OptionsForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.formatDateTimePanel.ResumeLayout(false);
            this.formatDateTimePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox formatDateTimeCheckBox;
        private System.Windows.Forms.ComboBox calendarComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel formatDateTimePanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dateTimePatternTextBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
    }
}