using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demcon.ProductionTool.General;
using Demcon.ProductionTool.Model;

namespace Demcon.ProductionTool.View
{
    public partial class TabFatTests : UserControl
    {
        public TabFatTests()
        {
            InitializeComponent();
        }

        public TestManager TestManager { get; set; }

        private void btnTestSet1_Click(object sender, EventArgs e)
        {
            this.StartTest(ETestSequence.Generic);
        }

        private void btnTestSet2_Click(object sender, EventArgs e)
        {
            //this.StartTest(ETestSequence.None);
        }

        private void btnTestSet3_Click(object sender, EventArgs e)
        {
            //this.StartTest(ETestSequence.None);
        }

        private void btnTestSet4_Click(object sender, EventArgs e)
        {
            //this.StartTest(ETestSequence.None);
        }

        private bool InitSequence(TestSequence sequence)
        {
            if (sequence as CalibrationTestSequence == null)
            {
                string serial = string.Format("FNN{0:yy}", DateTime.Now);
                //string dwo = string.Empty;
                //string operatorID = string.Empty;
                //string additionalInfo1 = string.Empty;
                //string additionalInfo2 = string.Empty;
                serial = "FNN171000001";
                string dwo = "001";
                string operatorID = "Girindra";
                string additionalInfo1 = "FAT01";
                string additionalInfo2 = "SV001";
                string remarks = "first pass";
                while (!string.IsNullOrWhiteSpace(remarks))
                {
                    remarks = string.Empty;
                    if (MultiInputDialog.GetInput(this.ParentForm, "Test Information",
                        "Serial Number (FNNYYMMXXXXX)", serial,
                        "Work Order", dwo,
                        "Operator ID", operatorID,
                        sequence.AddtionalInformationRequestText1, additionalInfo1,
                        sequence.AddtionalInformationRequestText2, additionalInfo2,
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
            string input;
            this.BeginInvoke((Action)(() =>
            {
                if (InputDialog.GetInput(this.ParentForm, "Measurement Request", e.Value, out input) && !string.IsNullOrWhiteSpace(input))
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
            var sequence = new Demcon.ProductionTool.Model.ReportGeneratorTestSequence(this.TestManager);
            if (InitSequence(sequence))
            {
                try
                {
                    string pdf = sequence.Save(null);
                    if (MessageBox.Show("Rapport opgeslagen als " + pdf + "\n\nWilt u het rapport nu openen?", "PDF rapport", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(pdf);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Fout bij genereren van rapport: " + ex.Message, "PDF rapport", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            this.StartTest(ETestSequence.Calibration);
        }
    }
}
