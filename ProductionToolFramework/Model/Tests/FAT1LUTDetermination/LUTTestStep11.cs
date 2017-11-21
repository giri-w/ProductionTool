using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;

namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep11 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep11"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep11()
            : this(null)
        { }

        private const string InstructionText =
                                "Measurement Information\n" +
                                "- Grid4 Location   : {0}\n" +
                                "- Grid  Location    : {1}\n" +
                                "- Var Intensity      : {2}\n" +
                                "- Var Mass            : {3}\n" +
                                "- Var Distance      : {4}\n\n" +
                                "Check if every green dots are in white dots\n" +
                                "- Press Next to continue\n" +
                                "- or press Back to change the measurement setting";


        private string Grid4Location = string.Empty;
        private string GridLocation = string.Empty;
        private double Intensity;
        private double Mass;
        private double Distance;
        private string testSetting = @"Setting\config.xml";

        public LUTTestStep11(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Analyze the measurement";
            this.Instructions = string.Empty;
            this.SupportingImage = @"Python\figure\LUTDetermination.png";
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Start()
        {
            this.Results.Clear();
            ChangeXml chg = new ChangeXml();
            new Task(() =>
            {
                Grid4Location = chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID4");
                GridLocation = chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID");
                Intensity = Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "Intensity"));
                Mass = Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "Mass"));
                Distance = Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "Distance"));
                this.Instructions = string.Format(LUTTestStep11.InstructionText, Grid4Location, GridLocation, Intensity, Mass, Distance);

            }).Start();
        }


        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("Analyze the measurement", "Mask has been set up properly", true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                this.OnTestCanceled(true);
            }
        }
    }
}
