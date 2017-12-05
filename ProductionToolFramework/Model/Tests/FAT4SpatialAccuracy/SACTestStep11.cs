using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.IO;

namespace Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy
{
    public class SACTestStep11 : TestStep
    {
        // variables
        bool resultBool = false;
        private string testSetting = @"Setting\config.xml";
        private string SourceLocation = string.Empty;
        private const string InstructionText =
                                    " Measurement configuration:\n" +
                                    " - Source Location  :  {0}\n\n" +
                                    "Press \"Process\" to start analysis\n";
        [Obsolete]
        public SACTestStep11()
            : this(null)
        { }

        public SACTestStep11(TestManager testManager)
            : base(testManager)
        {
            // obtain location from setting
            ChangeXml chg = new ChangeXml();
            SourceLocation = chg.ObtainElement(testSetting, "Test", "FAT4", "SAC", "SourcePA");

            this.Name = "11. Position Accuracy Processing";
            this.Instructions = "Loading...";
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Analyze;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Start()
        {
            this.Results.Clear();
            new Task(() =>
            {
                // obtain location from setting
                ChangeXml chg = new ChangeXml();
                SourceLocation = chg.ObtainElement(testSetting, "Test", "FAT4", "SAC", "SourcePA");
                this.Instructions = string.Format(SACTestStep11.InstructionText, SourceLocation);

            }).Start();
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                if (testResult.Contains("PASS"))
                    resultBool = true;
                else
                    resultBool = false;

                // Continue to the next step
                this.Results.Add(new BooleanResult(this.Name, "Python Script executed", resultBool));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // Back to previous step
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Analyze)
            {
                // Processing using background worker
                string pythonLocation = @"Python/5. PositionAccuracy.py";
                string fullPath = Path.GetFullPath(pythonLocation);
                string[] pythonArgument = { fullPath, SourceLocation };

                // variable for python script
                Python py = new Python();
                this.pyFullPath = fullPath;
                this.pyArgument = py.compArray(pythonArgument);
            }

        }
    }
}
