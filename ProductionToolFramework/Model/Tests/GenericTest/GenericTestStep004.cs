using System;
using System.Globalization;

namespace Demcon.ProductionTool.Model.Tests.GenericTest
{
    public class GenericTestStep004 : SpecificTestStep
    {
        private float inputTemperature = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep004"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public GenericTestStep004()
            : this(null)
        { }

        public GenericTestStep004(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Temperatuursensor testen";
            this.Instructions = "Druk op 'OK' om de test te starten";
            this.SupportingImage = @"Images\Foto1.jpg";
            this.ButtonOptions = EButtonOptions.OK;
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.testManager.RequestInfo("Meet de temperatuur en vul deze in, in graden celcius.");
        }

        public override void InputReceived(string input)
        {
            float value;
            if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                this.inputTemperature = value;
                this.ExecuteTest(10000);
            }
            else
            {
                this.testManager.RequestInfo("Uw invoer werd niet herkend.\nMeet de temperatuur en vul deze in, in °C.");
            }
        }

        protected override void HandleResult(object status)
        {
            this.Results.Add(new ValueResult("User input temperature", "", this.inputTemperature, "°C", 15, 50));
            this.Results.Add(new ValueResult("Mean temperature", "", 20, "°C", inputTemperature - 0.5, inputTemperature + 0.5));
            this.Results.Add(new ValueResult("St.dev. temperature", "", 0, "°C", 0, 0.1));

            this.OnTestUpdated(true);
        }
    }
}
