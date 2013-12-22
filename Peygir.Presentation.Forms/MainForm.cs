using Peygir.Logic;
using Peygir.Presentation.Forms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Peygir.Presentation.Forms
{
    public partial class MainForm : Form
    {
        public MessageBoxOptions FormMessageBoxOptions
        {
            get
            {
                MessageBoxOptions options = (MessageBoxOptions)0;
                if (RightToLeft == System.Windows.Forms.RightToLeft.Yes)
                {
                    options = (MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
                return options;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            projectsListUserControl.ProjectsListView.SelectedIndexChanged += ProjectsListView_SelectedIndexChanged;
            projectsListUserControl.ProjectsListView.DoubleClick += ProjectsListView_DoubleClick;

            ShowCurrentLanguage();

            UpdateControlsEnabledProperty();
        }

        private void ShowCurrentLanguage()
        {
            englishToolStripMenuItem.Checked = false;
            persianToolStripMenuItem.Checked = false;

            switch (Settings.Default.Language)
            {
                case "en":
                    englishToolStripMenuItem.Checked = true;
                    break;

                case "fa":
                    persianToolStripMenuItem.Checked = true;
                    break;
            }

            return;
        }

        private void ChangeLanguage(string language)
        {
            if (language == null)
            {
                throw new ArgumentNullException("language");
            }

            Settings.Default.Language = language;
            Settings.Default.Save();

            ShowCurrentLanguage();

            // Don't translate this messages.
            string message = "The application should be restarted to change language."
                + Environment.NewLine
                + "Do you want to restart the application now?";

            DialogResult result =
            MessageBox.Show
            (
                message,
                "Peygir",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Restart.
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Restart();
            }

            return;
        }

        private void UpdateControlsEnabledProperty()
        {
            if (string.IsNullOrEmpty(Database.CurrentnDatabasePath))
            {
                saveDatabaseAsToolStripMenuItem.Enabled = false;
                closeDatabaseToolStripMenuItem.Enabled = false;

                addProjectButton.Enabled = false;
                editProjectButton.Enabled = false;
                deleteProjectButton.Enabled = false;

                projectsGroupBox.Enabled = false;
            }
            else
            {
                saveDatabaseAsToolStripMenuItem.Enabled = true;
                closeDatabaseToolStripMenuItem.Enabled = true;

                int selectedProjectsCount = projectsListUserControl.ProjectsListView.SelectedItems.Count;
                if (selectedProjectsCount == 0)
                {
                    addProjectButton.Enabled = true;
                    editProjectButton.Enabled = false;
                    deleteProjectButton.Enabled = false;
                }
                else if (selectedProjectsCount == 1)
                {
                    addProjectButton.Enabled = true;
                    editProjectButton.Enabled = true;
                    deleteProjectButton.Enabled = true;
                }
                else
                {
                    addProjectButton.Enabled = true;
                    editProjectButton.Enabled = false;
                    deleteProjectButton.Enabled = true;
                }

                projectsGroupBox.Enabled = true;
            }

            databaseToolStripStatusLabel.Text = string.Format
            (
                Resources.String_DatabaseX,
                string.IsNullOrEmpty(Database.CurrentnDatabasePath) ? "-" : Database.CurrentnDatabasePath
            );

            return;
        }

        private void NewDatabase()
        {
            saveFileDialog.FileName = string.Empty;
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    CloseDatabase();

                    string databasePath = saveFileDialog.FileName;

                    Environment.CurrentDirectory = Application.StartupPath;
                    Database.CreateAndOpen(databasePath);

                    ShowProjects();
                }
                catch (Exception exception)
                {
                    MessageBox.Show
                    (
                        exception.Message,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions

                    );

                    CloseDatabase();
                }
            }
            return;
        }

        private void OpenDatabase()
        {
            openFileDialog.FileName = string.Empty;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    CloseDatabase();

                    string databasePath = openFileDialog.FileName;

                    Environment.CurrentDirectory = Application.StartupPath;
                    Database.Open(databasePath);

                    ShowProjects();
                }
                catch (Exception exception)
                {
                    MessageBox.Show
                    (
                        exception.Message,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );

                    CloseDatabase();
                }
            }
            return;
        }

        private void SaveDatabaseAs()
        {
            string currentDatabasePath = Database.CurrentnDatabasePath;

            saveFileDialog.FileName = currentDatabasePath;
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    CloseDatabase();

                    string newDatabasePath = saveFileDialog.FileName;

                    // Copy.
                    File.Copy(currentDatabasePath, newDatabasePath, true);

                    Environment.CurrentDirectory = Application.StartupPath;
                    Database.Open(newDatabasePath);

                    ShowProjects();
                }
                catch (Exception exception)
                {
                    MessageBox.Show
                    (
                        exception.Message,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );

                    CloseDatabase();
                }
            }
            return;
        }

        private void CloseDatabase()
        {
            Database.Close();

            projectsListUserControl.ProjectsListView.Items.Clear();

            UpdateControlsEnabledProperty();

            return;
        }

        private void ShowOptions()
        {
            OptionsForm form = new OptionsForm();
            form.ShowDialog();
            return;
        }

        private void ShowProjects()
        {
            // Save selected projects.
            List<int> selectedProjects = new List<int>();
            foreach (ListViewItem item in projectsListUserControl.ProjectsListView.SelectedItems)
            {
                Project project = (Project)item.Tag;
                selectedProjects.Add(project.ID);
            }

            Project[] projects = Project.GetProjects();

            projectsListUserControl.ShowProjects(projects);

            // Reselect projects.
            foreach (ListViewItem item in projectsListUserControl.ProjectsListView.Items)
            {
                Project project = (Project)item.Tag;
                if (selectedProjects.Contains(project.ID))
                {
                    item.Selected = true;
                    item.EnsureVisible();
                }
            }

            UpdateControlsEnabledProperty();

            return;
        }

        private void AddProject()
        {
            Project project = new Project();
            ProjectDetailsForm form = new ProjectDetailsForm();
            form.ProjectDetailsUserControl.ShowProject(project);

        Again:
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Check name.
                if (string.IsNullOrWhiteSpace(form.ProjectDetailsUserControl.ProjectName))
                {
                    MessageBox.Show
                    (
                        Resources.String_TheProjectNameCannotBeBlank,
                        Resources.String_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        FormMessageBoxOptions
                    );
                    goto Again;
                }

                form.ProjectDetailsUserControl.RetrieveProject(project);

                // Add.
                project.Add();

                // Flush.
                Database.Flush();

                // Show projects.
                ShowProjects();

                // Select new project.
                projectsListUserControl.ProjectsListView.SelectedItems.Clear();
                foreach (ListViewItem item in projectsListUserControl.ProjectsListView.Items)
                {
                    Project p = (Project)item.Tag;
                    if (p.ID == project.ID)
                    {
                        item.Selected = true;
                        break;
                    }
                }

                UpdateControlsEnabledProperty();

                // Show edit project form.
                EditProject();

                projectsListUserControl.Focus();
            }

            return;
        }

        private void EditProject()
        {
            if (projectsListUserControl.ProjectsListView.SelectedItems.Count != 1)
            {
                return;
            }

            Project project = (Project)projectsListUserControl.ProjectsListView.SelectedItems[0].Tag;

            ProjectForm form = new ProjectForm(project);
            form.ShowDialog();

            ShowProjects();

            return;
        }

        private void DeleteProject()
        {
            if (projectsListUserControl.ProjectsListView.SelectedItems.Count > 0)
            {
                DialogResult result =
                MessageBox.Show
                (
                    Resources.String_AreYouSureYouWantToDeleteSelectedProjects,
                    Resources.String_DeleteProjects,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1,
                    FormMessageBoxOptions
                );

                if (result != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }

                // Delete projects.
                for (int i = 0; i < projectsListUserControl.ProjectsListView.SelectedItems.Count; i++)
                {
                    Project project = (Project)projectsListUserControl.ProjectsListView.SelectedItems[i].Tag;
                    project.Delete();
                }

                // Flush.
                Database.Flush();

                // Show projects.
                ShowProjects();
            }
            return;
        }

        private void ShowHelp()
        {
            try
            {
                string helpPath = string.Format(Settings.Default.HelpPath, Application.StartupPath);
                Process.Start(helpPath);
            }
            catch (Exception exception)
            {
                MessageBox.Show
                (
                    exception.Message,
                    Resources.String_Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    FormMessageBoxOptions
                );
            }
            return;
        }

        private void ShowAbout()
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
            return;
        }

        private void newDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDatabase();
            return;
        }

        private void openDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDatabase();
            return;
        }

        private void saveDatabaseAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDatabaseAs();
            return;
        }

        private void closeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseDatabase();
            return;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOptions();
            return;
        }

        private void ProjectsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControlsEnabledProperty();
            return;
        }

        private void ProjectsListView_DoubleClick(object sender, EventArgs e)
        {
            EditProject();
            return;
        }

        private void addProjectButton_Click(object sender, EventArgs e)
        {
            AddProject();
            return;
        }

        private void editProjectButton_Click(object sender, EventArgs e)
        {
            EditProject();
            return;
        }

        private void deleteProjectButton_Click(object sender, EventArgs e)
        {
            DeleteProject();
            return;
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp();
            return;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAbout();
            return;
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("en");
            return;
        }

        private void persianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("fa");
            return;
        }
    }
}
