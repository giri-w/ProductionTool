using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Xml;

namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep0022 : TestStep
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="LUTTestStep0022"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep0022()
            : this(null)
        { }

        
        private string testResult = string.Empty;
        private string testSetting = @"Setting\config.xml";
        private string GridLocation = string.Empty;

        private const string InstructionText =
                                " Locate GRID folder location in the computer \n" +
                                " GRID current Directory : {0}\n\n" +
                                " Press Browse  to set Grid4  Measurement Location\n" +
                                "\nWhen finished, press Next";

        public LUTTestStep0022(TestManager testManager)
            : base(testManager)
        {
            ChangeXml chg = new ChangeXml();
            GridLocation = chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID");
            this.Name = "GRID Location";
            this.Instructions = " Locate GRID folder location in the computer \n"+
                                 " GRID current Directory :" + GridLocation + "\n\n" +
                                " Press Browse to set Grid  Measurement Location\n" +
                                "\nWhen finished, press Next";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Browse;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
            this.OnTestCanceled(false);
        }

        public override void Start()
        {
            this.Results.Clear();
            ChangeXml chg = new ChangeXml();
            new Task(() =>
            {
                GridLocation = chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID");
                this.Instructions = string.Format(LUTTestStep0022.InstructionText, GridLocation);
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
                // Check or do something (with the hardware?) for the test
                bool check = string.IsNullOrEmpty(GridLocation);
                this.Results.Add(new BooleanResult("GRID Location", GridLocation, !check));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Browse)
            {
                ChangeXml chg = new ChangeXml();
                // save folder location 
                testResult = info;
                chg.modifyElement(testSetting, "Test", "FAT1", "LUT", "GRID", testResult);
                MessageBox.Show("Setting saved");
                
                // Check or do something (with the hardware?) for the test
                bool check = string.IsNullOrEmpty(testResult);
                this.Results.Add(new BooleanResult("GRID Location", testResult, !check));
                this.OnTestUpdated(true);

            }

            if (userAction == EButtonOptions.Back)
            {
                this.OnTestCanceled(true);
            }




        }
    }
}
