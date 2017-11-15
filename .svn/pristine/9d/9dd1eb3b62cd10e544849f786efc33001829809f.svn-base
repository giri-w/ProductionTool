using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.IO;

namespace Demcon.ProductionTool.Model.Tests.FAT4SignalStability
{
    public class SSTestStep003 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep003"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public SSTestStep003()
            : this(null)
        { }

        Boolean resultBool { get; set; }
        private string SourceLocation = string.Empty;
        private Double Threshold;
        private string testSetting = @"Setting\config.xml";
        private const string InstructionText =
                                " Setting up variable that are used in measurement \n" +
                                " - Source Location  -> {0}\n" +
                                "\n\nTo start measurement, press Process" +
                                "To check the result, press Next";

        public SSTestStep003(TestManager testManager)
            : base(testManager)
        {
            // set initial value for setting from XML
            ChangeXml chg = new ChangeXml();
            SourceLocation = chg.ObtainElement(testSetting, "Test", "FAT4", "SS", "Source");
            
            this.Name = "Data Processing";
            this.Instructions =
                                " Setting up variable that are used in measurement \n" +
                                " - Source Location  -> " + SourceLocation + "\n" +
                                "\n\nTo start measurement, press Process" +
                                "To check the result, press Next";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Analyze;
            
           


            this.Results = new List<Result>();
            this.OnTestUpdated(false);
            this.OnTestCanceled(false);
            resultBool = false;
        }

        public override void Start()
        {
            this.Results.Clear();
            ChangeXml chg = new ChangeXml();
            new Task(() =>
            {
                SourceLocation = chg.ObtainElement(testSetting, "Test", "FAT4", "SS", "Source");
                
                this.Instructions = string.Format(SSTestStep003.InstructionText, SourceLocation);

            }).Start();
        }

        public override void Stop()
        {
            base.Stop();

        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Stop();
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                if (testResult.Contains("PASS"))
                    resultBool = true;
                else
                    resultBool = false;

                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("Data Processing", "Python Script executed", resultBool));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Analyze)
            {
                ChangeXml chg = new ChangeXml();

                
                // new approach
                Python py = new Python();
                String pythonLocation = @"Python/3. SignalStability.py";
                string fullPath = Path.GetFullPath(pythonLocation);
                String[] pythonArgument = { fullPath, SourceLocation }; //additional
                pyFullPath = fullPath;
                pyArgument = py.compArray(pythonArgument);
                
            }


        }
    }
}
