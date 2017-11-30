using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demcon.ProductionTool.General;
using Demcon.ProductionTool.Model;
using System.Xml;

namespace Demcon.ProductionTool.View
{
    public partial class TabFatTests : UserControl
    {
        public TabFatTests()
        {
            InitializeComponent();
            tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        }

        public TestManager TestManager { get; set; }

        private void btnTestSet1_Click(object sender, EventArgs e)
        {
            this.btnTestSet1.BackgroundImage = global::TestToolFramework.Properties.Resources.btnTest1Click;

            this.StartTest(ETestSequence.Fat1);
          
        }

        private void btnTestSet2_Click(object sender, EventArgs e)
        {
            this.btnTestSet2.BackgroundImage = global::TestToolFramework.Properties.Resources.btnTest2Click;
            //this.StartTest(ETestSequence.Generic);
        }

        private void btnTestSet3_Click(object sender, EventArgs e)
        {
            this.btnTestSet3.BackgroundImage = global::TestToolFramework.Properties.Resources.btnTest3Click;
            //this.StartTest(ETestSequence.None);
        }

        private void btnTestSet4_Click(object sender, EventArgs e)
        {
            this.btnTestSet4.BackgroundImage = global::TestToolFramework.Properties.Resources.btnTest4Click;

            this.StartTest(ETestSequence.Fat4);
        }

        private bool InitSequence(TestSequence sequence)
        {
            if (sequence as CalibrationTestSequence == null)
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
                XmlNode xSVN = doc.SelectSingleNode("/Profile/SVN");

                string serial = string.Format("FNN{0:yy}", DateTime.Now);
                serial = xserial.InnerText;
                string dwo = xWO.InnerText;
                string operatorID = xoperatorID.InnerText;
                string additionalInfo1 = xSVN.InnerText;
                string additionalInfo2 = "SV001";

                // update connection information
                XmlNode xUsername = doc.SelectSingleNode("/Profile/UserName");
                XmlNode xPassword = doc.SelectSingleNode("/Profile/Password");
                XmlNode xIPAdresss = doc.SelectSingleNode("/Profile/IPAddress");
                XmlNode xFingerprint = doc.SelectSingleNode("/Profile/Fingerprint");

                string _username = xUsername.InnerText;
                string _password = xPassword.InnerText;
                string _ipAddress = xIPAdresss.InnerText;
                string _fingerPrint = xFingerprint.InnerText;


                string remarks = "first pass";
                string remarksCon = "second pass";
                bool hiddenVariable = true;

                // cek operator info
                while (!string.IsNullOrWhiteSpace(remarks))
                {
                    remarks = string.Empty;
                    if (MultiInputDialog.GetInput(this.ParentForm, "Test Information",
                        "Serial Number (FNNYYMMXXXXX)", serial,
                        "Work Order", dwo,
                        "Operator ID", operatorID,
                        sequence.AddtionalInformationRequestText1, additionalInfo1,
                        sequence.AddtionalInformationRequestText2, additionalInfo2,
                        !hiddenVariable,
                        out serial, out dwo, out operatorID, out additionalInfo1, out additionalInfo2))
                    {
                        remarks = sequence.AcceptInput(serial, dwo, operatorID, additionalInfo1, additionalInfo2);
                        if (string.IsNullOrWhiteSpace(remarks))
                        {
                            sequence.SerialNumber = serial;
                            sequence.Dwo = dwo;
                            sequence.OperatorID = operatorID;
                            sequence.AddtionalInformation1 = additionalInfo1;
                            sequence.AddtionalInformation2 = additionalInfo2;

                            // Save variable values to XML
                            xserial.InnerText = serial;
                            xWO.InnerText = dwo;
                            xoperatorID.InnerText = operatorID;
                            xSVN.InnerText = additionalInfo1;

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
                                    out _ipAddress, out _fingerPrint, out _username, out _password, out additionalInfo2))
                                {
                                    remarksCon = sequence.AcceptInputCon(_ipAddress, _fingerPrint, _username, _password, additionalInfo2);
                                    if (string.IsNullOrWhiteSpace(remarksCon))
                                    {
                                        sequence.IPAddress = _ipAddress;
                                        sequence.Fingerprint = _fingerPrint;
                                        sequence.Username = _username;
                                        sequence.Password = _password;

                                        // Save variable values to XML
                                        xIPAdresss.InnerText = _ipAddress;
                                        xFingerprint.InnerText = _fingerPrint;
                                        xUsername.InnerText = _username;
                                        xPassword.InnerText = _password;
                                        doc.Save(localSetting);

                                        return true;
                                    }
                                    else
                                    {
                                        MessageBox.Show(remarksCon, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }


                        }
                        else
                        {
                            MessageBox.Show(remarks, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                
            }
            else
            {
                return true;
            }

            return false;
        }

        private bool InitSequenceReport(TestSequence sequence)
        {
            if (sequence as CalibrationTestSequence == null)
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
                XmlNode xSVN = doc.SelectSingleNode("/Profile/SVN");

                string serial = string.Format("FNN{0:yy}", DateTime.Now);
                serial = xserial.InnerText;
                string dwo = xWO.InnerText;
                string operatorID = xoperatorID.InnerText;
                string additionalInfo1 = "001";
                string additionalInfo2 = "SV001";

                string remarks = "first pass";
                bool hiddenVariable = true;

                // cek operator info
                while (!string.IsNullOrWhiteSpace(remarks))
                {
                    remarks = string.Empty;
                    if (MultiInputDialog.GetInput(this.ParentForm, "Test Information",
                        "Serial Number (FNNYYMMXXXXX)", serial,
                        "Work Order", dwo,
                        "Operator ID", operatorID,
                        sequence.AddtionalInformationRequestText1, additionalInfo1,
                        sequence.AddtionalInformationRequestText2, additionalInfo2,
                        !hiddenVariable,
                        out serial, out dwo, out operatorID, out additionalInfo1, out additionalInfo2))
                    {
                        remarks = sequence.AcceptInput(serial, dwo, operatorID, additionalInfo1, additionalInfo2);
                        if (string.IsNullOrWhiteSpace(remarks))
                        {
                            sequence.SerialNumber = serial;
                            sequence.Dwo = dwo;
                            sequence.OperatorID = operatorID;
                            sequence.AddtionalInformation1 = additionalInfo1;
                            sequence.AddtionalInformation2 = additionalInfo2;

                            // Save variable values to XML
                            xserial.InnerText = serial;
                            xWO.InnerText = dwo;
                            xoperatorID.InnerText = operatorID;
                            return true;

                        }
                        else
                        {
                            MessageBox.Show(remarks, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            else
            {
                return true;
            }

            return false;
        }

        private void StartTest(ETestSequence sequenceType)
        {
            this.TestManager.TestStopped += new EventHandler(HandleTestStopped);
            this.TestManager.InformationRequest += new EventHandler<EventArgs<string>>(HandleInformationRequest);
            TestSequence currSequence = this.TestManager.CreateTest(sequenceType);
            if (currSequence != null)
            {
                if (InitSequence(currSequence))
                {
                    this.TestManager.StartTest();
                }
            }
        }

        private void HandleTestStopped(object sender, EventArgs e)
        {
            this.TestManager.TestStopped -= new EventHandler(HandleTestStopped);
            this.TestManager.InformationRequest -= new EventHandler<EventArgs<string>>(HandleInformationRequest);
        }

        private void HandleInformationRequest(object sender, EventArgs<string> e)
        {
            string input = string.Empty;
            this.BeginInvoke((Action)(() =>
            {
                if (InputDialog.GetInput("Measurement Request", e.Value, input, out input) && !string.IsNullOrWhiteSpace(input))
                {
                    new Task(() =>
                    {
                        this.TestManager.CurrentTestStep.InputReceived(input);
                    }).Start();
                }
            }));
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            this.btnReport.BackgroundImage = global::TestToolFramework.Properties.Resources.btnReportClick;

            var sequence = new Demcon.ProductionTool.Model.ReportGeneratorTestSequence(this.TestManager);
            if (InitSequenceReport(sequence))
            {
                try
                {
                    string pdf = sequence.Save(null);
                    if (MessageBox.Show("Report save as " + pdf + "\n\nDo you want to open it now?", "PDF report", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(pdf);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fault in processing the report: " + ex.Message, "PDF report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            this.btnCalibrate.BackgroundImage = global::TestToolFramework.Properties.Resources.btnCalibrateClick;
            //this.StartTest(ETestSequence.Calibration);
        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        // Button Behaviour Button FAT1
        private void btnTestSet1_MouseLeave(object sender, EventArgs e)
        {
            this.btnTestSet1.BackgroundImage = global::TestToolFramework.Properties.Resources.btnTest1L;
        }

        private void btnTestSet1_MouseEnter(object sender, EventArgs e)
        {
            this.btnTestSet1.BackgroundImage = global::TestToolFramework.Properties.Resources.btnTest1Hover;
        }

        // Button Behaviour Button FAT2
        private void btnTestSet2_MouseLeave(object sender, EventArgs e)
        {
            this.btnTestSet2.BackgroundImage = global::TestToolFramework.Properties.Resources.btnTest2L;
        }

        private void btnTestSet2_MouseEnter(object sender, EventArgs e)
        {
            this.btnTestSet2.BackgroundImage = global::TestToolFramework.Properties.Resources.btnTest2Hover;
        }

        // Button Behaviour Button FAT3
        private void btnTestSet3_MouseLeave(object sender, EventArgs e)
        {
            this.btnTestSet3.BackgroundImage = global::TestToolFramework.Properties.Resources.btnTest3L;
        }

        private void btnTestSet3_MouseEnter(object sender, EventArgs e)
        {
            this.btnTestSet3.BackgroundImage = global::TestToolFramework.Properties.Resources.btnTest3Hover;
        }

        // Button Behaviour Button FAT4
        private void btnTestSet4_MouseLeave(object sender, EventArgs e)
        {
            this.btnTestSet4.BackgroundImage = global::TestToolFramework.Properties.Resources.btnTest4L;
        }

        private void btnTestSet4_MouseEnter(object sender, EventArgs e)
        {
            this.btnTestSet4.BackgroundImage = global::TestToolFramework.Properties.Resources.btnTest4Hover;
        }

        // Button Behaviour Button Report
        private void btnReport_MouseLeave(object sender, EventArgs e)
        {
            this.btnReport.BackgroundImage = global::TestToolFramework.Properties.Resources.btnReportL;
        }

        private void btnReport_MouseEnter(object sender, EventArgs e)
        {
            this.btnReport.BackgroundImage = global::TestToolFramework.Properties.Resources.btnReportHover;
        }

        // Button Behaviour Button Calibrate
        private void btnCalibrate_MouseLeave(object sender, EventArgs e)
        {
            this.btnCalibrate.BackgroundImage = global::TestToolFramework.Properties.Resources.btnCalibrateL;
        }

        private void btnCalibrate_MouseEnter(object sender, EventArgs e)
        {
            this.btnCalibrate.BackgroundImage = global::TestToolFramework.Properties.Resources.btnCalibrateHover;
        }
    }
}
