using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using HemicsFat;

namespace Demcon.ProductionTool.Model.Tests.SelfTest
{
    public class SelfTestStep005 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfTestStep005"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public SelfTestStep005()
            : this(null)
        { }

        public SelfTestStep005(TestManager testManager)
            : base(testManager)
        {
            this.Name = "SVN Number Information";
            this.Instructions = "Write the SVN Number in the work instruction\n";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next|EButtonOptions.Yes;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("SVN Number", "Checked", true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Yes)
            {
                // Check or do something (with the hardware?) for the test
                Console.WriteLine("Tombol Yes ditekan");
                Python py = new Python();
                String fullPath      = Path.GetFullPath(@"Python/test.py");
                String[] argument    = { "1", "2" };
                pyFullPath = fullPath;
                pyArgument = py.compArray(argument);
                

        /*
        Python py = new Python();
        String pythonLocation = @"Python/2. FieldMaskGeneration.py";
        string fullPath = Path.GetFullPath(pythonLocation);

        String[] pythonArgument = { fullPath, SourceLocation, Threshold.ToString() }; //additional


        string result = py.run_cmd(fullPath, pythonArgument);
        resultBool = py.BoolPython();
        MessageBox.Show(result, "Python Script Execution");
        */
    }

            
        }
    }
}
