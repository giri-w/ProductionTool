using System;
using System.Linq;
using System.Windows.Forms;
using Demcon.ProductionTool.General;
using Demcon.ProductionTool.Hardware;
using Demcon.ProductionTool.Model;
using Demcon.ProductionTool.View.FatTestPages;
using HemicsFat;
using System.Threading;

namespace Demcon.ProductionTool.View
{
    public partial class MainForm : Form
    {
        private TestPage testPage;

        
        public MainForm()
        {

            try
            {
                this.HwManager = new HwManager();
                string versionString = "HandScanner Tool; " + GetSvnRevision();
                this.TestManager = new TestManager(versionString);
                InitializeComponent();
                this.Text = versionString;
                this.tabFatTests1.TestManager = this.TestManager;
                this.testPage1.TestManager = this.TestManager;

               // Set Tooltip for the user control element
                toolTip1.SetToolTip(tabFatTests1.btnTestSet1,   "Setting and Calibration of the System");
                toolTip1.SetToolTip(tabFatTests1.btnTestSet2,   "System Safety With Respect to the Lasers");
                toolTip1.SetToolTip(tabFatTests1.btnTestSet3,   "Grounding, Leakage Current, and High Voltage Tests");
                toolTip1.SetToolTip(tabFatTests1.btnTestSet4,   "System Validation Test");
                toolTip1.SetToolTip(tabFatTests1.btnReport,     "Generate Report Based on the Test Results");

                toolTip1.SetToolTip(linkStatus1, "Check Connection Status to the Machine");
            }
            catch (Exception ex)
            {
                string message = string.Empty;
                Exception e = ex;
                while (e != null)
                {
                    message += e.Message + "\n";
                    e = e.InnerException;
                }

                MessageBox.Show(message);
            }
        }

        public HwManager HwManager
        {
            get;
            private set;
        }

        public TestManager TestManager
        {
            get;
            private set;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.TestManager.TestSequenceRequestedEvent += this.StartTestSequence;
        }

        private string GetSvnRevision()
        {
            string retVal = "Unknown version ";
            string[] svnPathParts = Demcon.ProductionTool.Model.SvnInformation.Path.Split(new char[] { '/', '\\' });
            int svnPathPartsCount = svnPathParts.Length;
            if (svnPathPartsCount > 3)
            {
                if (svnPathParts.Contains("trunk"))
                {
                    retVal = "Trunk version ";
                }
                else
                {
                    if (svnPathParts.Contains("tags"))
                    {
                        retVal = "Release ";
                    }
                    else if (svnPathParts.Contains("branches"))
                    {
                        retVal = "Branch ";
                    }

                    retVal += svnPathParts.Last(); // branch / tag name
                }
            }

            retVal += " R" +  Demcon.ProductionTool.Model.SvnInformation.Revision;
            if (Demcon.ProductionTool.Model.SvnInformation.Modified)
            {
                retVal += "M";
            }

            return retVal;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.TestManager.TestSequenceRequestedEvent -= this.StartTestSequence;
            this.TestManager.Dispose();
            this.HwManager.Dispose();
        }

        private void StartTestSequence(object sender, EventArgs<string> e)
        {
            if (this.TestManager.CurrentSequence == null)
            {
                this.tabFatTests1.BringToFront();
                this.testPage1.SendToBack();
                
            }
            else
            {
                this.testPage1.BringToFront();
                this.tabFatTests1.SendToBack();
               
            }
        }

        private void tabFatTests1_Load(object sender, EventArgs e)
        {

        }

        delegate void SetTextCallback(string text, int color);

        private void SetText(string text, int color)
        {
            if (this.statusText1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text,color });
            }
            else
            {
                this.statusText1.Text = text;
                if (color == 1)
                    this.statusText1.ForeColor = System.Drawing.Color.Green;
                else if (color == 2)
                    this.statusText1.ForeColor = System.Drawing.Color.Red;
            }
        }


        private void linkStatus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Console.WriteLine("Hello, world");
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                FtpTransfer ftp = new FtpTransfer();
                while (true)
                {
                    bool check = ftp.checkConnection();
                    if (check)
                        SetText("Connected", 1);
                    else
                        SetText("Disconnected", 2);

                    Thread.Sleep(20000);
                }
                
            }).Start();

        }

      
    }
}
