﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestToolFramework.View;

namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep08 : TestStep
    {
        [Obsolete]
        public LUTTestStep08()
            : this(null)
        { }

        public LUTTestStep08(TestManager testManager)
            : base(testManager)
        {
            this.Name			 = "8. Data Selection: GRID";
            this.Instructions	 = 
								   "- Select the measurement result from the list\n" +
								   "- Press \"Download\" to download the measurement to local computer";
			
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions	 = EButtonOptions.Next|EButtonOptions.Back|EButtonOptions.Download;
            this.Results 		 = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                this.Results.Add(new BooleanResult("Data Selection Grid4", "Checked", true));
				this.OnTestUpdated(true);
            }
			
			   if (userAction == EButtonOptions.Back)
            {
                // Back to previous step
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Download)
            {
                MeasurementForm browser = new MeasurementForm();
                Application.Run(browser);
            }


        }
    }
}
