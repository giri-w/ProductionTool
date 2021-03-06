﻿using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.IO;

namespace Demcon.ProductionTool.Model.Tests.LUTDetermination
{
    public class LUTTestStep004 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep004"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep004()
            : this(null)
        { }

        Boolean resultBool { get; set; }
        private string Grid4Location = string.Empty;
        private string GridLocation = string.Empty;
        private double Intensity;
        private double Mass;
        private double Distance;
        private string testSetting = @"Setting\config.xml";

        public LUTTestStep004(TestManager testManager)
            : base(testManager)
        {

            // initial value of python argument
            ChangeXml chg = new ChangeXml();
            Grid4Location = chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID4");
            Grid4Location = chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID");
            Intensity = Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "Intensity"));
            Mass = Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "Mass"));
            Distance = Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "Distance"));

            this.Name = "Generate XML";
            this.Instructions = "XML files as a reference for processing camera and motor image" +
                                "Press Process to generate the XML files";
                                
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Analyze;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
            this.OnTestCanceled(false);
            resultBool = false;
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("Generate XML", "XML files are generated", resultBool));
                this.OnTestUpdated(true);
            }

            

            if (userAction == EButtonOptions.Analyze)
            {
                // print XML 
                string printXML = "YES";
               
                // Check or do something (with the hardware?) for the test
                Python py = new Python();
                String pythonLocation = @"Python/1. LUTDetermination.py";
                string fullPath = Path.GetFullPath(pythonLocation);

                String[] pythonArgument = { fullPath, Grid4Location, GridLocation, Intensity.ToString(), Mass.ToString(), Distance.ToString(), printXML }; //additional
                string result = py.run_cmd(fullPath, pythonArgument);
                resultBool = py.BoolPython();
                MessageBox.Show(result, "Python Script Execution");
                
            }

            if (userAction == EButtonOptions.Back)
            {
                // Check or do something (with the hardware?) for the test
                this.OnTestCanceled(true);
            }



        }
    }
}
