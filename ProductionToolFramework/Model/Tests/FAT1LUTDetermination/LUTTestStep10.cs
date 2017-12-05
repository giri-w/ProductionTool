using System;
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
        [Obsolete]
        public LUTTestStep10()
            : this(null)
        { }

        private bool resultBool 		= false;
        private string Grid4Location 	= string.Empty;
        private string GridLocation 	= string.Empty;
        private Double Intensity		= 0.4;
        private Double Mass				= 500000;
        private Double Distance			= 50;
        private string testSetting		 = @"Setting\config.xml";
      
        public LUTTestStep10(TestManager testManager)
            : base(testManager)
        {
            ChangeXml chg = new ChangeXml();
            
            // set initial value for setting from XML
            Grid4Location 	= chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID4");
            GridLocation 	= chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID");
            Intensity 		= Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "Intensity"));
            Mass 			= Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "Mass"));
            Distance 		= Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "Distance"));

            // initiate step
            this.Name 				= "10. Data Processing";
            this.Instructions       = 
                                        " Setting up variable that are used in measurement \n" +
                                        " - Default Intensity\t\t: 0.4\n" +
                                        " - Default Mass\t\t: 500000\n" +
                                        " - Default Distance\t\t: 50\n\n" +
                                        "Press \"Process\" to start measurement";

            this.SupportingImage	= @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions		= EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Analyze;
            this.VarOptions			= EVarOptions.Intensity | EVarOptions.Mass | EVarOptions.Distance;
            this.VarValue			= Intensity.ToString() + "," + Mass.ToString() + "," + Distance.ToString();
            this.Results 			= new List<Result>();
			
			// forward and backward handler
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
				if (testResult.Contains("PASS"))
                    resultBool = true;
                else
                    resultBool = false;

                
                this.Results.Add(new BooleanResult("Data Processing", "Python Script executed", resultBool));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // Back to previous step
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
                String[] pythonArgument = { fullPath, Grid4Location, GridLocation, Intensity.ToString(), Mass.ToString(), Distance.ToString(), printXML }; //script arguments
                pyFullPath = fullPath;
                pyArgument = py.compArray(pythonArgument);

            }


        }
    }
}
