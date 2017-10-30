using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Globalization;

namespace Demcon.ProductionTool.Model
{
    public class ValueResult: Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueResult"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public ValueResult()
        { }

        public ValueResult(string name, string remarks, double measuredValue, string units, double minValue, double maxValue)
            : base (name, remarks)
        {
            this.MeasuredValue = measuredValue;
            this.Units = units;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        [XmlAttribute]
        public string Units { get; set; }
        [XmlAttribute]
        public double MinValue { get; set; }
        [XmlAttribute]
        public double MaxValue { get; set; }
        [XmlAttribute]
        public double MeasuredValue { get; set; }
        [XmlAttribute]
        public override ETestConclusion Conclusion
        {
            get
            {
                ETestConclusion retVal;
                if (this.MeasuredValue == null || Double.IsNaN(this.MeasuredValue))
                {
                    retVal = ETestConclusion.NotTested;
                }
                else if (this.MinValue > this.MaxValue)
                {
                    retVal = ETestConclusion.Inconclusive;
                }
                else if (this.MeasuredValue < this.MinValue || this.MeasuredValue > this.MaxValue)
                {
                    retVal = ETestConclusion.Failed;
                }
                else
                {
                    retVal = ETestConclusion.Passed;
                }

                return retVal;
            }
        }
        
        public override string ToString()
        {
            string retVal = string.Format(CultureInfo.InvariantCulture, "Value of test '{0}': {1}. Measured {2:0.###} {3} with range ",
                this.Name,
                this.Conclusion,
                this.MeasuredValue,
                this.Units);

            if (this.MaxValue != Double.PositiveInfinity)
            {
                retVal += string.Format(CultureInfo.InvariantCulture, "[{0:0.###} .. {1:0.###}] {2}.",
                    this.MinValue,
                    this.MaxValue,
                    this.Units);
            }
            else
            {
                retVal += string.Format(CultureInfo.InvariantCulture, " >= {0:0.###} {1}.",
                    this.MinValue,
                    this.Units);
            }

            if (!string.IsNullOrWhiteSpace(this.Remarks))
            {
                retVal += "\nRemarks: " + this.Remarks;
            }

            return retVal;
        }
    }
}
