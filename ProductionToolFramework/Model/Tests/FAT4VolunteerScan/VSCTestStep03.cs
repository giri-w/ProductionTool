using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.IO;

namespace Demcon.ProductionTool.Model.Tests.FAT4VolunteerScan
{
    public class VSCTestStep03 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public VSCTestStep03()
            : this(null)
        { }

        bool resultBool = false;

        public VSCTestStep03(TestManager testManager)
            : base(testManager)
        {
            this.Name = "3. Volunteerscan Check";
            this.Instructions = "Press 'Update' to run the Python script checking the volunteerscan";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next|EButtonOptions.Back|EButtonOptions.Update;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
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
                // Check or do something (with the hardware?) for the test
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Update)
            {
                // Processing using background worker
                string pythonLocation = @"Python/6. VolunteerScan";
                string fullPath = Path.GetFullPath(pythonLocation);
                string[] pythonArgument = { fullPath, "2" };

                             
                // variable for python script
                Python py = new Python();
                //this.pyFullPath = Path.GetFullPath(pythonLocation);
                this.pyArgument = py.compArray(pythonArgument);
                string hasil = py.run_cmd(pythonLocation, pythonArgument);
                resultBool = py.BoolPython();

                Console.WriteLine("Beres Python nya");
                Console.WriteLine(hasil);
            }

        }
    }
}
