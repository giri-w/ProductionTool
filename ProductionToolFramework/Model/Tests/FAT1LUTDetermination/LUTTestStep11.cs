﻿using System;
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

  

        public LUTTestStep11(TestManager testManager)
            : base(testManager)
        {
            this.Name						= "11. Analyze the measurement";
            this.Instructions =
                                                "Ensure that every green dots are in white dots\n" +
                                                "If not, change the variable and redo the process analysation";
												

            this.SupportingImage = @"Python\figure\FAT1LUTDetermination\LUTDetermination.png";
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

  
        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                this.Results.Add(new BooleanResult(this.Name, "LookUp Table has been set up properly", true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // Back to previous step
				this.OnTestCanceled(true);
            }
        }
    }
}
