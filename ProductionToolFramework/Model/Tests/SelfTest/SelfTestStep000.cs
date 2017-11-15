using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using HemicsFat;

namespace Demcon.ProductionTool.Model.Tests.SelfTest
{
    public class SelfTestStep000 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfTestStep000"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public SelfTestStep000()
            : this(null)
        { }

        public SelfTestStep000(TestManager testManager)
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
                String fullPath      = Path.GetFullPath(@"Python/2. FieldMaskGeneration.py");

                String[] argument    = { fullPath, @"C:\Users\GWA\Desktop\Internship DEMCON\2. Hemics production tools\1. Test Source\20170629_090733_297 Fieldmask",  "40" };
                pyFullPath = fullPath;
                pyArgument = py.compArray(argument);
                
             }

            
        }
    }
}
