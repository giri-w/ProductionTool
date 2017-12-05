using Demcon.ProductionTool.View;
using HemicsFat;
using System;
using System.Collections.Generic;
using System.Data;

using System.IO;
using System.Linq;

using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using WinSCP;

namespace TestToolFramework.View
{
    public partial class MeasurementForm : Form
    {
        string[] folderName;
        string downloadPath = @"C:\TestFolder";
        string configPath = @"Setting\measurement.xml";
        string profilePath = @"Setting\profile.xml";
        string ftpDirectory;

        public MeasurementForm()
        {
            InitializeComponent();

            // loading source directory
            sourceList.Items.Add("FTP Directory");
            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);
            XmlNodeList elemList = doc.GetElementsByTagName("dir");
            for (int i = 0; i < elemList.Count; i++)
            {
                Console.WriteLine(elemList[i].InnerXml);
                sourceList.Items.Add(elemList[i].InnerXml);
            }
            doc.Save(configPath);

            // loading FTP directory
            doc.Load(profilePath);
            XmlNode dirFTP = doc.SelectSingleNode("/Profile/FTPDir");
            ftpDirectory = dirFTP.InnerText;
            doc.Save(profilePath);


        }

        private void exitButton_Click(object sender, EventArgs e)
        {

            // Create array of source list
            List<string> sourcefolder = new List<string>();
            int counter = 0;
            foreach (string s in sourceList.Items)
            {
                // list will be saved from the second list
                if (counter > 0)
                    sourcefolder.Add(s);

                counter++;
            }
               

            string[] sourceArray = sourcefolder.ToArray();

            // save array to xml document
            XDocument doc = new XDocument();
            doc.Add(new XElement("source", sourceArray.Select(x => new XElement("dir", x))));
            doc.Save(configPath);

            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {


            if (sourceList.SelectedIndex != 0)
            {
                string localPath = sourceList.GetItemText(sourceList.SelectedItem);

                if (!String.IsNullOrEmpty(sourceText.Text))
                    sourceList.Items.Add(sourceText.Text);
                else if (!String.IsNullOrEmpty(localPath))
                    sourceList.Items.Add(localPath);
                else
                    MessageBox.Show("Please input source address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Cannot add FTP Source", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if(sourceList.SelectedIndex != 0)
              sourceList.Items.Remove(sourceList.SelectedItem);
            else
               MessageBox.Show("Cannot delete FTP Source", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {

            if (sourceList.SelectedIndex == 0)
            {
                FtpTransfer ftp = new FtpTransfer();
                string downloadDir = downloadPath + "\\" + folderList.GetItemText(folderList.SelectedItem);
                ftp.Download(downloadDir, folderName[folderList.SelectedIndex]);

            }
            else
            {
                try
                {
                    string sourcePath = sourceText.Text;
                    DirectoryCopy(sourcePath, downloadPath, includeDir.Checked);
                    
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Permission denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            // finish download files
            MessageBox.Show("Finish copying files", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                if (file.Name != "desktop.ini")
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }

            
        }

        private void ftpExplorer(string ftpAddress)
        {
            FtpTransfer ftp = new FtpTransfer();
            SessionOptions cOptions = ftp.createConnection();
            using (Session session = new Session())
            {
                try
                {
                    // Connect
                    // session.SessionLogPath = @"C:\";
                    session.Open(cOptions);


                    RemoteDirectoryInfo directory =
                        session.ListDirectory(ftpAddress);

                    // sort by the latest time
                    IEnumerable<RemoteFileInfo> latest =
                                                        directory.Files
                                                       .Where(file => file.IsDirectory)
                                                       .OrderByDescending(file => file.FullName);



                    // initialize the list
                    List<string> sourcefolder = new List<string>();
                    sourcefolder.Add("...");
                    folderList.Items.Add("...");

                    int track = 0;

                    foreach (RemoteFileInfo fileInfo in latest)
                    {
                        track++;
                        if (track != latest.Count())
                        {
                            Console.WriteLine(
                            "{0} with size {1}, permissions {2} and last modification at {3}",
                            fileInfo.Name, fileInfo.Length, fileInfo.FilePermissions,
                            fileInfo.LastWriteTime);

                            // save full path to array
                            sourcefolder.Add(fileInfo.FullName);
                            // display folder name in folder List
                            folderList.Items.Add(fileInfo.Name);
                        }

                    }

                    folderName = sourcefolder.ToArray();
                }



                catch (Exception f)
                {
                    MessageBox.Show("Fail establishing connection to the server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("Error: {0}", f);
                }


            }

        }

        private void sourceList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            folderList.Items.Clear();
            if (sourceList.SelectedIndex == 0)
            {
                MessageBox.Show("Establishing connection to FTP Server", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ftpExplorer(ftpDirectory);
            }

            else
            {
                string dirName;
                string localPath = sourceList.GetItemText(sourceList.SelectedItem);
                sourceText.Text = localPath;
                if (!String.IsNullOrEmpty(localPath))
                {
                    try
                    {
                        folderName = Directory.GetDirectories(localPath);
                        foreach (string folder in folderName)
                        {
                            //folderList.Items.Add(folder);
                            dirName = Path.GetFileName(folder);
                            folderList.Items.Add(dirName);
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("Permission denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                else
                {
                    MessageBox.Show("Please select source from the list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            
        }

        private void folderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sourceList.SelectedIndex == 0 & folderList.SelectedIndex == 0)
                sourceText.Text = ftpDirectory;
            else
                sourceText.Text = folderName[folderList.SelectedIndex];
        }

     
        private void folderList_DoubleClick(object sender, EventArgs e)
        {
            string localPath = folderName[folderList.SelectedIndex];
            if (sourceList.SelectedIndex == 0 & folderList.SelectedIndex == 0)
            {
                folderList.Items.Clear();
                ftpExplorer(ftpDirectory);

            }
            else if (sourceList.SelectedIndex == 0 & folderList.SelectedIndex != 0)
            {
                folderList.Items.Clear();
                ftpExplorer(localPath);
            }

            else
            {
                System.Diagnostics.Process.Start("explorer.exe", localPath);
            }

                
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", downloadPath);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (InputDialog.GetInput("FTP Address", "Please input new FTP Address", ftpDirectory, out ftpDirectory) && !string.IsNullOrWhiteSpace(ftpDirectory))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(profilePath);
                XmlNode dirFTP = doc.SelectSingleNode("/Profile/FTPDir");
                dirFTP.InnerText = ftpDirectory;
                doc.Save(profilePath);
                
            }
        }

        // Button Behaviour Button Add
        private void addButton_MouseLeave(object sender, EventArgs e)
        {
            this.addButton.BackgroundImage = global::TestToolFramework.Properties.Resources.mBtnAddResource;
        }

        private void addButton_MouseEnter(object sender, EventArgs e)
        {
            this.addButton.BackgroundImage = global::TestToolFramework.Properties.Resources.mBtnAddResourceHover;
        }

        // Button Behaviour Button Delete
        private void DeleteButton_MouseLeave(object sender, EventArgs e)
        {
            this.DeleteButton.BackgroundImage = global::TestToolFramework.Properties.Resources.mBtnDelResource;
        }

        private void DeleteButton_MouseEnter(object sender, EventArgs e)
        {
            this.DeleteButton.BackgroundImage = global::TestToolFramework.Properties.Resources.mBtnDelResourceHover;
        }

        // Button Behaviour Button Download
        private void downloadButton_MouseLeave(object sender, EventArgs e)
        {
            this.downloadButton.BackgroundImage = global::TestToolFramework.Properties.Resources.mBtnDownload;
        }

        private void downloadButton_MouseEnter(object sender, EventArgs e)
        {
            this.downloadButton.BackgroundImage = global::TestToolFramework.Properties.Resources.mBtnDownloadHover;
        }

        // Button Behaviour Button Exit
        private void exitButton_MouseLeave(object sender, EventArgs e)
        {
            this.exitButton.BackgroundImage = global::TestToolFramework.Properties.Resources.mBtnExit;
        }

        private void exitButton_MouseEnter(object sender, EventArgs e)
        {
            this.exitButton.BackgroundImage = global::TestToolFramework.Properties.Resources.mBtnExitHover;
        }



    }
}
