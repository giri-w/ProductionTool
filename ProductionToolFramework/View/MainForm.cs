using System;
using System.Linq;
using System.Windows.Forms;
using Demcon.ProductionTool.General;
using Demcon.ProductionTool.Hardware;
using Demcon.ProductionTool.Model;
using Demcon.ProductionTool.View.FatTestPages;
using HemicsFat;
using System.Threading;
using System.Xml;
using System.Diagnostics;

namespace Demcon.ProductionTool.View
{
    public partial class MainForm : Form
    {
        string helpDocument = @"Setting\Help.pdf";
        
        public MainForm()
        {

            try
            {
                this.HwManager = new HwManager();
                string versionString = "HandScanner Tool; " + GetSvnRevision();
                this.TestManager = new TestManager(versionString);
                checkConnection();
                InitializeComponent();
                this.Text = versionString;
                this.tabFatTests1.TestManager = this.TestManager;
                this.testPage1.TestManager = this.TestManager;

                // info visibility
                operatorInfo.Visible         = false;
                workInfo.Visible             = false;
                serialInfo.Visible           = false;

                // Set Tooltip for the FAT TEST element
                toolTip1.SetToolTip(tabFatTests1.btnTestSet1,   "Setting and Calibration of the System");
                toolTip1.SetToolTip(tabFatTests1.btnTestSet2,   "System Safety With Respect to the Lasers");
                toolTip1.SetToolTip(tabFatTests1.btnTestSet3,   "Grounding, Leakage Current, and High Voltage Tests");
                toolTip1.SetToolTip(tabFatTests1.btnTestSet4,   "System Validation Test");
                toolTip1.SetToolTip(tabFatTests1.btnReport,     "Generate Report Based on the Test Results");
                toolTip1.SetToolTip(tabFatTests1.btnCalibrate,  "Calibrate software configuration");

                // Set Tooltip for the TEST PAGE element
                toolTip1.SetToolTip(testPage1.SupportingImageBox, "Click to Zoom the Image");



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
            // read setting from profile.xml
            string localSetting = @"Setting\profile.xml";
            XmlDocument doc = new XmlDocument();

            // load variable values
            doc.Load(localSetting);

            // update test information
            XmlNode xserial = doc.SelectSingleNode("/Profile/Serial");
            XmlNode xoperatorID = doc.SelectSingleNode("/Profile/ID");
            XmlNode xWO = doc.SelectSingleNode("/Profile/WO");

            string serial = string.Format("FNN{0:yy}", DateTime.Now);
            serial = xserial.InnerText;
            string dwo = xWO.InnerText;
            string operatorID = xoperatorID.InnerText;

            if (this.TestManager.CurrentSequence == null)
            {
                // info visibility
                operatorInfo.Visible    = false;
                workInfo.Visible        = false;
                serialInfo.Visible      = false;
                this.tabFatTests1.BringToFront();
                this.testPage1.SendToBack();
                
            }
            else
            {
                // info visibility
                operatorInfo.Visible    = true;
                workInfo.Visible        = true;
                serialInfo.Visible      = true;

                // info text
                operatorInfo.Text    = "Operator : " + operatorID;
                workInfo.Text        = "Work Order : " + dwo;
                serialInfo.Text      = "Serial Number : " + serial;

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
                    this.statusText1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(173)))), ((int)(((byte)(8)))));
                else if (color == 2)
                    this.statusText1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            }
        }


        private void linkStatus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            TestSequence sequence = this.TestManager.CreateTest(ETestSequence.Generic);

            // read setting from profile.xml
            string localSetting = @"Setting\profile.xml";
            XmlDocument doc = new XmlDocument();

            // load variable values
            doc.Load(localSetting);
            
            // update connection information
            XmlNode xUsername = doc.SelectSingleNode("/Profile/UserName");
            XmlNode xPassword = doc.SelectSingleNode("/Profile/Password");
            XmlNode xIPAdresss = doc.SelectSingleNode("/Profile/IPAddress");
            XmlNode xFingerprint = doc.SelectSingleNode("/Profile/Fingerprint");

            string _username = xUsername.InnerText;
            string _password = xPassword.InnerText;
            string _ipAddress = xIPAdresss.InnerText;
            string _fingerPrint = xFingerprint.InnerText;

            string remarksCon = "second pass";
            string additionalInfo2 = null;
            bool hiddenVariable = true;

            // output from Setting
            string yusername = string.Empty;
            string ypassword = string.Empty;
            string yipAddress = string.Empty;
            string yfingerPrint = string.Empty;

            // Check Connection
            while (!string.IsNullOrWhiteSpace(remarksCon))
            {
                remarksCon = string.Empty;
                if (MultiInputDialog.GetInput(this.ParentForm, "Connection Information",
                    "Machine IP", _ipAddress,
                    "FingerPrint", _fingerPrint,
                    "Username", _username,
                    "Password", _password,
                    sequence.AddtionalInformationRequestText2, additionalInfo2,
                    hiddenVariable,
                    out yipAddress, out yfingerPrint, out yusername, out ypassword, out additionalInfo2))
                {
                    remarksCon = sequence.AcceptInputCon(yipAddress, yfingerPrint, yusername, ypassword, additionalInfo2);
                    if (string.IsNullOrWhiteSpace(remarksCon))
                    {
                        if ((_ipAddress != yipAddress) |
                            (_fingerPrint != yfingerPrint) |
                            (_username != yusername) |
                            (_password != ypassword))
                        {
                            // Save variable values to XML
                            xIPAdresss.InnerText = yipAddress;
                            xFingerprint.InnerText = yfingerPrint;
                            xUsername.InnerText = yusername;
                            xPassword.InnerText = ypassword;
                            doc.Save(localSetting);

                            // Restart Application
                            if (MessageBox.Show("New connection setting saved\n\nPress OK to restart the program!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                ProcessStartInfo Info = new ProcessStartInfo();
                                Info.Arguments = "/C ping 127.0.0.1 -n 2 && \"" + Application.ExecutablePath + "\"";
                                Info.WindowStyle = ProcessWindowStyle.Hidden;
                                Info.CreateNoWindow = true;
                                Info.FileName = "cmd.exe";
                                Process.Start(Info);
                                Application.Exit();
                            }


                        }
                        
                        
                    }
                    else
                    {
                        MessageBox.Show(remarksCon, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            //checkConnection();

        }

        private void checkConnection()
        {
            Console.WriteLine("Hello, Start FTP Connection");
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

                    Thread.Sleep(10000);
                }

            }).Start();

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
           
                Process.Start(helpDocument);
        }

        public void updateProgress (int pBarValue, string pBarLabel)
        {
            progressResult.Text = pBarLabel;
            progressBarValue.Value = pBarValue;
        }
    }
}
