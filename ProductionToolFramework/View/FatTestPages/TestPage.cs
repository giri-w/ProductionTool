using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Demcon.ProductionTool.General;
using Demcon.ProductionTool.Model;
using System.Diagnostics;
using HemicsFat;
using System.Threading;

namespace Demcon.ProductionTool.View.FatTestPages
{
    public partial class TestPage : UserControl
    {
        public enum ImageIndexes
        {
            Nothing = 0,
            Arrow = 1,
            Check = 2,
            Inconclusive = 3,
            Cross = 4,
        }

        private ImageList TestImageList;
        private ImageList TestStepImageList;
        private TestTimeoutDialog timeoutDialog;

        // Hide Cursor
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool HideCaret(IntPtr hWnd);

        

        public TestPage()
        {
            InitializeComponent();

            this.TestImageList = new ImageList();
            this.TestImageList.ImageSize = new Size(24, 24);
            this.TestStepImageList = new ImageList();
            this.TestStepImageList.ImageSize = new Size(24, 24);
            if (Directory.Exists("Images"))
            {
                // Load the images in an ImageList.
                this.TestImageList.Images.Add(Image.FromFile(@"Images\List-24.png"));
                this.TestImageList.Images.Add(Image.FromFile(@"Images\Arrow-24.png"));
                this.TestImageList.Images.Add(Image.FromFile(@"Images\ListCheck-24.png"));
                this.TestImageList.Images.Add(Image.FromFile(@"Images\ListInconclusive-24.png"));
                this.TestImageList.Images.Add(Image.FromFile(@"Images\ListCross-24.png"));


                this.TestStepImageList.Images.Add(Image.FromFile(@"Images\Nothing-24.png"));
                this.TestStepImageList.Images.Add(Image.FromFile(@"Images\Arrow-24.png"));
                this.TestStepImageList.Images.Add(Image.FromFile(@"Images\Check-24.png"));
                this.TestStepImageList.Images.Add(Image.FromFile(@"Images\Inconclusive-24.png"));
                this.TestStepImageList.Images.Add(Image.FromFile(@"Images\Cross-24.png"));
            }
        }

        private TestManager testManager;
        public TestManager TestManager
        {
            get
            {
                return this.testManager;
            }

            set
            {
                if (this.testManager != null)
                {
                    this.testManager.CurrentStepChangedEvent -= this.HandleCurrentStepChangedEvent;
                    this.testManager.SetTestTimeout -= new EventHandler<EventArgs<int>>(HandleSetTestTimeout);
                    this.testManager.TestSequenceRequestedEvent -= new EventHandler<EventArgs<string>>(OnTestSequenceRequestedEvent);
                }

                this.testManager = value;
                if (this.testManager != null)
                {
                    this.testManager.CurrentStepChangedEvent += this.HandleCurrentStepChangedEvent;
                    this.testManager.SetTestTimeout += new EventHandler<EventArgs<int>>(HandleSetTestTimeout);
                    this.testManager.TestSequenceRequestedEvent += new EventHandler<EventArgs<string>>(OnTestSequenceRequestedEvent);
                }
            }
        }

        private void HandleSetTestTimeout(object sender, EventArgs<int> e)
        {
            int timeoutTime = e.Value;
            if (timeoutTime > 0)
            {
                this.BeginInvoke((Action)(() =>
                {
                    this.timeoutDialog = new TestTimeoutDialog(timeoutTime);
                    this.timeoutDialog.ShowDialog(this);
                    if (this.timeoutDialog.TimeoutDialogResult == DialogResult.Cancel)
                    {
                        this.TestManager.SetCurrentTest(null);
                    }

                    this.timeoutDialog.Dispose();
                }));
            }
            else if (this.timeoutDialog != null && this.timeoutDialog.Visible)
            {
                this.timeoutDialog.Stop();
            }
        }

        private void HandleCurrentStepChangedEvent(object sender, EventArgs e)
        {
            if (this.timeoutDialog != null && this.timeoutDialog.Visible)
            {
                this.timeoutDialog.Stop();
            }

            TestSequence currSequence = this.testManager.CurrentSequence;
            Test currTest = this.testManager.CurrentTest;
            TestStep currStep = this.testManager.CurrentTestStep;
            this.BeginInvoke((Action)(() =>
            {
                TestStep senderStep = sender as TestStep;
                if (senderStep != null && senderStep.Results.Count > 0)
                {
                    Label captionLabel = new Label()
                    {
                        AutoSize = true,
                        MinimumSize = new System.Drawing.Size(this.ResultsFlowLayoutPanel.Width - 50, 0),
                        Text = senderStep.Name,
                        ForeColor = Color.Black,
                        Font = new Font(FontFamily.GenericSansSerif, 12,FontStyle.Bold)
                    };
                    this.ResultsFlowLayoutPanel.Controls.Add(captionLabel);
                    this.ResultsFlowLayoutPanel.ScrollControlIntoView(captionLabel);

                    foreach (Result currResult in senderStep.Results)
                    {
                        Color color;
                        switch (currResult.Conclusion)
                        {
                            case ETestConclusion.Failed:
                                color = Color.Red;
                                break;
                            case ETestConclusion.Passed:
                                color = Color.Green;
                                break;
                            case ETestConclusion.NotTested:
                            case ETestConclusion.Inconclusive:
                            default:
                                color = Color.Black;
                                break;
                        }

                        Label resultLabel = new Label()
                        {
                            AutoSize = true,
                            MinimumSize = new System.Drawing.Size(this.ResultsFlowLayoutPanel.Width - 50, 0),
                            Text = currResult.ToString(),
                            ForeColor = color,
                            Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                            Padding = new Padding(10, 0, 0, 0),
                        };
                        this.ResultsFlowLayoutPanel.Controls.Add(resultLabel);
                        this.ResultsFlowLayoutPanel.ScrollControlIntoView(resultLabel);
                    }
                }

                if (currSequence != null)
                {
                    this.SequenceNameLabel.Text = currSequence.Name;
                    this.IDLabel.Text = String.Format("Serienummer: {0}", currSequence.SerialNumber);
                }
                else
                {
                    this.SequenceNameLabel.Text = "- No sequence selected -";
                    this.IDLabel.Text = string.Empty;
                }

                if (currTest != null)
                {
                    this.TestNameLabel.Text = currTest.Name;
                }
                else
                {
                    this.TestNameLabel.Text = string.Empty;
                }

                this.EnableButtons();
                
                if (currStep != null)
                {
                    this.EnableVarLabel();

                    this.TestNameLabel.Text += " - " + currStep.Name;
                    //this.StepDescriptionLabel.Text = currStep.Instructions;
                    this.stepDescriptionText.Text    = currStep.Instructions;
                    this.stepDescriptionText.GotFocus += TextBoxGotFocus;
                    this.SupportingImageBox.ImageLocation = currStep.SupportingImage;
                    this.BrowseButton.Visible        = currStep.ButtonOptions.HasFlag(EButtonOptions.Browse);
                    this.RetryButton.Visible         = currStep.ButtonOptions.HasFlag(EButtonOptions.Update);
                    this.NoButton.Visible            = currStep.ButtonOptions.HasFlag(EButtonOptions.No);
                    this.OKButton.Visible            = currStep.ButtonOptions.HasFlag(EButtonOptions.OK);
                    this.YesButton.Visible           = currStep.ButtonOptions.HasFlag(EButtonOptions.Yes);
                    this.BackButton.Visible          = currStep.ButtonOptions.HasFlag(EButtonOptions.Back);
                    this.AnalyzeButton.Visible       = currStep.ButtonOptions.HasFlag(EButtonOptions.Analyze);
                    this.NextButton.Visible          = currStep.ButtonOptions.HasFlag(EButtonOptions.Next);
                    this.CancelButton.Visible        = !currStep.ButtonOptions.HasFlag(EButtonOptions.Finish);
                    this.FinishedButton.Visible      = currStep.ButtonOptions.HasFlag(EButtonOptions.Finish);
                    this.DownloadButton.Visible = currStep.ButtonOptions.HasFlag(EButtonOptions.Download);
                    this.progressBar1.Visible = false;
                    this.progressLabel.Visible = false;

                    this.varBox1.Visible = counter > 0;
                    this.varBox2.Visible = counter > 1;
                    this.varBox3.Visible = counter > 2;
                    this.varBox4.Visible = counter > 3;
                    this.varLabel1.Visible = counter > 0;
                    this.varLabel2.Visible = counter > 1;
                    this.varLabel3.Visible = counter > 2;
                    this.varLabel4.Visible = counter > 3;
                    

                    
                }
                else
                {
                    //this.StepDescriptionLabel.Text = string.Empty;
                    this.stepDescriptionText.Text = "Test Cancelled \n\n Press Finished to return to Main Menu";
                    this.stepDescriptionText.GotFocus += TextBoxGotFocus;

                    this.SupportingImageBox.ImageLocation = string.Empty;

                    this.DownloadButton.Visible  = false;
                    this.NoButton.Visible        = false;
                    this.NextButton.Visible      = false;
                    this.BackButton.Visible      = false;
                    this.BrowseButton.Visible    = false;
                    this.RetryButton.Visible     = false;
                    this.OKButton.Visible        = false;
                    this.YesButton.Visible       = false;
                    this.AnalyzeButton.Visible   = false;
                    this.progressBar1.Visible = false;
                    this.progressLabel.Visible = false;

                    this.CancelButton.Visible    = false;
                    this.FinishedButton.Visible  = true;

                    

                    this.varBox1.Visible         = false;
                    this.varBox2.Visible         = false;
                    this.varBox3.Visible         = false;
                    this.varBox4.Visible         = false;
                    this.varLabel2.Visible       = false;
                    this.varLabel3.Visible       = false;
                    this.varLabel1.Visible       = false;
                    this.varLabel4.Visible       = false;
                }

                this.FillTestList(currSequence);

                if (currStep != null && (currStep.ButtonOptions == EButtonOptions.None || currStep.ButtonOptions == EButtonOptions.Cancel))
                {
                    this.BeginInvoke((Action)(() =>
                        {
                            this.TestManager.Execute(EButtonOptions.OK,"OK");
                        }));
                }
            }));
        }



        private void OnTestSequenceRequestedEvent(object sender, EventArgs<string> e)
        {
            this.ResultsFlowLayoutPanel.Controls.Clear();
        }

        private void FillTestList(TestSequence currSequence)
        {
            foreach (TestStepListItem currTestItem in this.TestListBox.Controls)
            {
                currTestItem.Click -= new EventHandler(TestItemClick);
            }
            
            this.TestListBox.Controls.Clear();
            this.TestStepListBox.Controls.Clear();
            if (currSequence != null && currSequence.Tests != null)
            {
                TestStepListItem selectedTestItem = null;
                foreach (Test currTest in currSequence.Tests)
                {
                    TestStepListItem newTestItem = new TestStepListItem(currTest, this.TestImageList);
                    newTestItem.IsHighlighted = currTest == currSequence.CurrentTest;
                    newTestItem.Click += new EventHandler(TestItemClick);
                    this.TestListBox.Controls.Add(newTestItem);
                    if (currTest == currSequence.CurrentTest)
                    {
                        selectedTestItem = newTestItem;
                        foreach (TestStep currTestStep in currTest.Steps)
                        {
                            TestStepListItem newTestStepItem = new TestStepListItem(currTestStep, this.TestStepImageList);
                            newTestStepItem.IsHighlighted = currTestStep == currTest.CurrentTestStep;
                            newTestStepItem.Width = this.TestStepListBox.Width - 5;
                            newTestStepItem.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                            this.TestStepListBox.Controls.Add(newTestStepItem);
                        }
                    }
                }

                if (selectedTestItem != null)
                {
                    this.TestListBox.ScrollControlIntoView(selectedTestItem);
                }
            }
        }

        private void TestItemClick(object sender, EventArgs e)
        {
            TestStepListItem typedSender = sender as TestStepListItem;
            if (typedSender != null)
            {
                Test currTest = typedSender.ConclusionItem as Test;
                if (currTest != null)
                {
                    this.testManager.SetCurrentTest(currTest);
                }
            }
        }

  
        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DisableButtons();
            this.TestManager.Execute(EButtonOptions.OK,"OK");
        }

        

        private void NoButton_Click(object sender, EventArgs e)
        {
            this.DisableButtons();
            this.TestManager.Execute(EButtonOptions.No,"No");
        }

        private void FinishedButton_Click(object sender, EventArgs e)
        {
            this.DisableButtons();
            
            MessageBox.Show("Test Finished", "FAT results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string savedPath = this.TestManager.StopTest();

            try
            {
                //string savedPath = this.TestManager.StopTest();
                if (string.IsNullOrWhiteSpace(savedPath))
                {
                    MessageBox.Show("Could not save the results!", "Test results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Results saved to " + savedPath, "Test results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving results: " + ex.Message, "Test results", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void DisableButtons()
        {
            this.CancelButton.Enabled = false;
            this.OKButton.Enabled = false;
            this.YesButton.Enabled = false;
            this.NoButton.Enabled = false;
            this.FinishedButton.Enabled = false;
        }

        private void EnableButtons()
        {
            this.CancelButton.Enabled = true;
            this.OKButton.Enabled = true;
            this.YesButton.Enabled = true;
            this.NoButton.Enabled = true;
            this.FinishedButton.Enabled = true;
        }

        string[] varLabel = new string[4];
        string[] varValue = new string[4];
        double counter;

        private void EnableVarLabel()
        {
            TestStep currStep = this.testManager.CurrentTestStep;
            for (int i = 0;i<4;i++)
            {
                varLabel[i] = "var";
                varValue[i] = "0";
            }
            


            // check if variable is needed
            string text = currStep.VarOptions.ToString();
            string textValue = currStep.VarValue;


            bool result = text.Equals("0", StringComparison.Ordinal);

            if (!result)
            {
                string[] words = text.Split(',');
                string[] value = textValue.Split(',');

                for (int i = 0; i<words.Length;i++)
                {
                    varLabel[i] = words[i];
                    varValue[i] = value[i];
                }

            }

            counter = 0;
            for (int p = 0; p < 4;p++)
            {
                if (varLabel[p] != "var")
                    counter++;

            }
                this.varLabel1.Text = varLabel[0];
            this.varLabel2.Text = varLabel[1];
            this.varLabel3.Text = varLabel[2];
            this.varLabel4.Text = varLabel[3];

            this.varBox1.Text = varValue[0];
            this.varBox2.Text = varValue[1];
            this.varBox3.Text = varValue[2];
            this.varBox4.Text = varValue[3];

        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            string info = "Dekstop";
            this.DisableButtons();
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.Description = " Select Recording Folder";
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                info = fbd.SelectedPath;
            }

            this.TestManager.Execute(EButtonOptions.Browse,info);
            
        }

        private void RetryButton_Click(object sender, EventArgs e)
        {
            this.DisableButtons();
            string info = "Change";
            this.testManager.CurrentTestStep.VarValue = this.varBox1.Text + "," +
                                                        this.varBox2.Text + "," +
                                                        this.varBox3.Text + "," +
                                                        this.varBox4.Text;
            this.TestManager.Execute(EButtonOptions.Update, info);
            this.EnableButtons();
        }

        private void AnalyzeButton_Click(object sender, EventArgs e)
        {
            string info = "Analyze";
            this.testManager.CurrentTestStep.VarValue = this.varBox1.Text + "," +
                                                        this.varBox2.Text + "," +
                                                        this.varBox3.Text + "," +
                                                        this.varBox4.Text;

            this.DisableButtons();
            progressLabel.Text = " ";
            progressBar1.Value = 0;


            // execute Analyze Button at child
            this.TestManager.Execute(EButtonOptions.Analyze, info);

            // get the location and argument data from child
            TestStep currStep = this.testManager.CurrentTestStep;
            string location = currStep.pyFullPath;
            string[] arg = currStep.pyArgument;

            // check if location is still empty
            while (location == string.Empty)
            {
                this.TestManager.Execute(EButtonOptions.Analyze, info);
                location = currStep.pyFullPath;
                arg = currStep.pyArgument;
            }

            MessageBox.Show("Script Executed", "Python Script Execution");
            // run backgroundWorker
            finishFlag = false;
            progressBar1.Visible = true;
            progressLabel.Visible = true;
            //backgroundWorker1.DoWork += (obj, f) => backgroundWorker1_DoWork(location, arg);
            backgroundWorker1.RunWorkerAsync();
            this.EnableButtons();
            Console.WriteLine("SUDAH SELESAI");
            //backgroundWorker1.DoWork -= (obj, f) => backgroundWorker1_DoWork(location, arg);

        }
        
        private void NextButton_Click(object sender, EventArgs e)
        {
            string info = "Next";
            this.DisableButtons();
            this.TestManager.Execute(EButtonOptions.Next, info);
            this.EnableButtons();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            string info = "Back";
            this.DisableButtons();
            this.TestManager.Execute(EButtonOptions.Back, info);
            this.EnableButtons();
        }

        private void SupportingImageBox_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.SupportingImageBox.ImageLocation))
                Process.Start(this.SupportingImageBox.ImageLocation);
        }

        private void CancelButton_Click_1(object sender, EventArgs e)
        {
            this.DisableButtons();
            this.TestManager.SetCurrentTest(null);
        }

        private void TextBoxGotFocus(object sender, EventArgs args)
        {
            HideCaret(this.Handle);
        }


        // BACKGROUND WORKER FOR LOADING SCREEN

        private void YesButton_Click(object sender, EventArgs e)
        {
            // execute Yes Button at child
            this.TestManager.Execute(EButtonOptions.Yes, "Yes");

            // get the location and argument data from child
            TestStep currStep = this.testManager.CurrentTestStep;
            string location = currStep.pyFullPath;
            string[] arg = currStep.pyArgument;

            // check if location is still empty
            while (location == string.Empty)
            {
                this.TestManager.Execute(EButtonOptions.Yes, "Yes");
                location = currStep.pyFullPath;
                arg = currStep.pyArgument;
            }

            // debug to console
            Console.WriteLine("Siap menerima input");
            Console.WriteLine(location);
            
            // run backgroundWorker
            progressBar1.Visible = true;
            progressLabel.Visible = true;
            //backgroundWorker1.DoWork += (obj, f) => backgroundWorker1_DoWork(location, arg);
            backgroundWorker1.RunWorkerAsync();
        }
        public bool finishFlag;
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        //private void backgroundWorker1_DoWork(string location, string[] arg)
        {
            TestStep currStep = this.testManager.CurrentTestStep;
            string location = currStep.pyFullPath;
            string[] arg = currStep.pyArgument;

            Console.WriteLine(location);
            if (!finishFlag)
            {
                // if you put -u, then background worker will stop immediately
                //string fullPath = "-u " + location;
                string fullPath = location;
                string[] param = arg;
                string line = string.Empty;
                int counter = 0;
                Console.WriteLine("Start Processing");

                // initialize process
                backgroundWorker1.ReportProgress(10, "Collecting sources file");
                Thread.Sleep(2000);
                backgroundWorker1.ReportProgress(20, "Processing files");

                // start process
                Process process = new Process();
                process.StartInfo.FileName = @"C:\Python\Demcon2017\python.exe";
                process.StartInfo.Arguments = string.Format(" \"{0}\"  \"{1}\"  \"{2}\"  \"{3}\" \"{4}\" \"{5}\" \"{6}\" \"{7}\" \"{8}\" \"{9}\" \"{10}\"",
                                                              fullPath, param[0], param[1], param[2], param[3], param[4], param[5], param[6], param[7], param[8], param[9]);

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.EnableRaisingEvents = true;

                process.OutputDataReceived += (object xsender, DataReceivedEventArgs f) =>
                {
                    Console.WriteLine(f.Data);
                    line = f.Data;
                    if (!String.IsNullOrEmpty(line))
                    {
                        string[] words = line.Split(',');
                        if (words.Length == 2)
                        {
                            counter = Convert.ToInt16(words[0]);
                            backgroundWorker1.ReportProgress(counter, words[1]); // Update progress

                            // store result to child
                            if (counter == 100)
                                this.testManager.CurrentTestStep.testResult = line;
                            finishFlag = true;

                            Thread.Sleep(1000);
                        }
                    }

                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();

            }
            
            

        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            string result = e.UserState.ToString();
            Console.WriteLine("Update progress");

            // set progress Label and progress Bar Value
            progressLabel.Text = result;
            progressBar1.Value = e.ProgressPercentage;
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // handle the error
            }
            else if (e.Cancelled)
            {
                // handle cancellation
            }
            else
            {
                // use it on the UI thread
                Thread.Sleep(1000);
                Console.WriteLine("FINISH PROCESSING");
                

                // set Progress Bar and Progress Label visbility
                progressLabel.Visible   = true;
                progressBar1.Visible    = false;
                
            }
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            // execute Yes Button at child
            this.TestManager.Execute(EButtonOptions.Yes, "Yes");
        }


        // END OF THE PROGRAM
    }


}
