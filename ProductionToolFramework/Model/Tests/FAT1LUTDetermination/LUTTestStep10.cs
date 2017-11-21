﻿using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.IO;

namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep10 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTest10"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep10()
            : this(null)
        { }

        private bool resultBool = false;
        private string Grid4Location = string.Empty;
        private string GridLocation = string.Empty;
        private Double Intensity;
        private Double Mass;
        private Double Distance;
        private string testSetting = @"Setting\config.xml";
        private const string InstructionText =
                                " Setting up variable that are used in measurement \n" +
                                " - Grid4 Location  -> {0} \n" +
                                " - Grid Location   -> {1} \n" +
                                " - var Intensity   -> {2} (Default : 0.4)\n" +
                                " - var Mass        -> {3} (Default : 500000)\n" +
                                " - var Distance  ->   {4} (Default : 50)\n" +
                                "\n\nTo start measurement, press Process" +
                                "To check the result, press Next";


        public LUTTestStep10(TestManager testManager)
            : base(testManager)
        {
            ChangeXml chg = new ChangeXml();
            // set initial value for setting from XML
            Grid4Location = chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID4");
            Grid4Location = chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID");
            Intensity = Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "Intensity"));
            Mass = Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "Mass"));
            Distance = Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "Distance"));


            this.Name = "Data Processing";
            this.Instructions = string.Empty;
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Analyze;
            this.VarOptions = EVarOptions.Intensity | EVarOptions.Mass | EVarOptions.Distance;
            this.VarValue = Intensity.ToString() + "," + Mass.ToString() + "," + Distance.ToString();
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
                this.Instructions = string.Format(LUTTestStep10.InstructionText, Grid4Location, GridLocation, Intensity, Mass, Distance);

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


                Console.WriteLine("Execute LUT");
                // retrieve variable value from text Box
                string[] textValue = VarValue.Split(',');
                string printXML = "YES";
                Intensity = Convert.ToDouble(textValue[0]);
                Mass = Convert.ToDouble(textValue[1]);
                Distance = Convert.ToDouble(textValue[2]);

                chg.modifyElement(testSetting, "Test", "FAT1", "LUT", "Intensity", Intensity.ToString());
                chg.modifyElement(testSetting, "Test", "FAT1", "LUT", "Mass", Mass.ToString());
                chg.modifyElement(testSetting, "Test", "FAT1", "LUT", "Distance", Distance.ToString());

                // new approach
                Python py = new Python();
                String pythonLocation = @"Python/1. LUTDetermination.py";
                string fullPath = Path.GetFullPath(pythonLocation);
                String[] pythonArgument = { fullPath, Grid4Location, GridLocation, Intensity.ToString(), Mass.ToString(), Distance.ToString(), printXML }; //additional
                pyFullPath = fullPath;
                pyArgument = py.compArray(pythonArgument);

            }


        }
    }
}
