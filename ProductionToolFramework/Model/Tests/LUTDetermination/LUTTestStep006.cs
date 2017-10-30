using System;
using Demcon.ProductionTool.Hardware;

namespace Demcon.ProductionTool.Model.Tests.LUTDetermination
{
    public class LUTTestStep006 : SpecificTestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep006"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep006()
            : this(null)
        { }

        public LUTTestStep006(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Inspect mask location";
            this.Instructions = "Draai de klep dicht zodat het vingersimulator volume op druk blijft\nDruk op OK.\nDeze stap duurt ongeveer twee minuten.";
            this.SupportingImage = @"Images\Foto10.jpg";
            this.ButtonOptions = EButtonOptions.OK;
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Add(new ValueResult("Random 1", "Voor het wachten", DateTime.Now.Second, "", 0, 59));
            this.OnTestUpdated(false);

            this.testManager.SetWaitTimer(3000);
            this.Sleep(3);
            this.Results.Add(new ValueResult("Random 2", "Na het wachten", DateTime.Now.Second, "", 0, 59));
            this.OnTestUpdated(true);
        }
    }
}
