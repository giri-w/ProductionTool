﻿using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Demcon.ProductionTool.Model.Tests.LUTDetermination
{
    public class LUTTestStep0022 : TestStep
    {
        
        private const string InstructionText =
                                "- Veranderende instructie met huidige waarde ter controle voor het doorgaan?\n" +
                                "- Huidige tijd: {0}.\n" +
                                "- Druk op OK";

        private bool keepUpdating;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep0022"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep0022()
            : this(null)
        { }

        public LUTTestStep0022(TestManager testManager)
            : base(testManager)
        {
            this.Name = "GRID Location";
            this.Instructions = " Locate GRID folder location in the computer \n"+
                                " Press GRID to set Grid  Measurement Location\n" +
                                "\nWhen finished, press OK";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.OK |  EButtonOptions.GRID;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

     

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Stop();

            this.Results.Clear();
            if (userAction == EButtonOptions.OK)
            {
                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("Result", "Just a random result", Math.IEEERemainder(DateTime.Now.Second, 2) > 0));
                
            }

            if (userAction == EButtonOptions.GRID)
            {

                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("Result", info, true));
            }

            this.OnTestUpdated(true);



        }
    }
}
