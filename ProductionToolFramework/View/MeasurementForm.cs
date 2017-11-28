using HemicsFat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MeasurementForm()
        {
            InitializeComponent();
            //sourceList.Items.Add(@"C:\Users");
            //sourceList.Items.Add(@"C:\Users\GWA");
            sourceList.Items.Add("FTP Directory");
            
                        XmlDocument doc = new XmlDocument();
            doc.Load(configPath);
            XmlNodeList elemList = doc.GetElementsByTagName("dir");
            for (int i = 0; i < elemList.Count; i++)
            {
                Console.WriteLine(elemList[i].InnerXml);
                sourceList.Items.Add(elemList[i].InnerXml);
            }


        }

        private void exitButton_Click(object sender, EventArgs e)
        {

            // Create array of source list
            List<string> sourcefolder = new List<string>();
            foreach (string s in sourceList.Items)
                sourcefolder.Add(s);

            string[] sourceArray = sourcefolder.ToArray();

            // save array to xml document
            XDocument doc = new XDocument();
            doc.Add(new XElement("source", sourceArray.Select(x => new XElement("dir", x))));
            doc.Save(configPath);

            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string localPath = sourceList.GetItemText(sourceList.SelectedItem);

            if (!String.IsNullOrEmpty(sourceText.Text))
                sourceList.Items.Add(sourceText.Text);
            else if (!String.IsNullOrEmpty(localPath))
                sourceList.Items.Add(localPath);
            else
                MessageBox.Show("Please input source address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            sourceList.Items.Remove(sourceList.SelectedItem);
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

        private void sourceList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            folderList.Items.Clear();
            if (sourceList.SelectedIndex == 0)
            {
                MessageBox.Show("FTP connection", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FtpTransfer ftp = new FtpTransfer();
                SessionOptions cOptions = ftp.createConnection();
                using (Session session = new Session())
                {
                    // Connect
                    session.Open(cOptions);

                    RemoteDirectoryInfo directory =
                        session.ListDirectory("/Settings/Measurement/");

                    List<string> sourcefolder = new List<string>();
                    
                    foreach (RemoteFileInfo fileInfo in directory.Files)
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

                    folderName = sourcefolder.ToArray();

                }



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
            sourceText.Text = folderName[folderList.SelectedIndex];
        }

        private void explorerButton_Click(object sender, EventArgs e)
        {
            string localPath = folderName[folderList.SelectedIndex];
            System.Diagnostics.Process.Start("explorer.exe", localPath);
        }

        private void folderList_DoubleClick(object sender, EventArgs e)
        {
            string localPath = folderName[folderList.SelectedIndex];
            System.Diagnostics.Process.Start("explorer.exe", localPath);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", downloadPath);
        }
    }
}
