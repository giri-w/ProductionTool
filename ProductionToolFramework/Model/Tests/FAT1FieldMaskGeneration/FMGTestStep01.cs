﻿using System;
using System.Collections.Generic;

namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    public class FMGTestStep01 : TestStep
    {
        [Obsolete]
        public FMGTestStep01()
            : this(null)
        { }

        public FMGTestStep01(TestManager testManager)
            : base(testManager)
        {
            this.Name 				= "1. Machine Preparation";
            this.Instructions 		=   
										"- Make sure the hand positioning blocks are present\n" +
										"- Glass plate is clean and free\n";
            this.SupportingImage 	= @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions		= EButtonOptions.Next;
            this.Results			= new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                this.Results.Add(new BooleanResult("Machine Preparation", "Checked", true));
				this.OnTestUpdated(true);
            }
    
        }
    }
}
