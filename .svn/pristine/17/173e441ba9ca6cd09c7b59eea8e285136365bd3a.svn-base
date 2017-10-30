using System;
using System.Xml.Serialization;
namespace Demcon.ProductionTool.Model
{
    public class BooleanResult: Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanResult"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public BooleanResult()
        { }

        public BooleanResult(string name, string remarks, bool resultValue)
            : base(name, remarks)
        {
            this.ResultValue = resultValue;
        }

        [XmlAttribute]
        public bool ResultValue { get; set; }

        [XmlAttribute]
        public override ETestConclusion Conclusion
        {
            get
            {
                ETestConclusion retVal;
                if (this.ResultValue)
                {
                    retVal = ETestConclusion.Passed;
                }
                else
                {
                    retVal = ETestConclusion.Failed;
                }

                return retVal;
            }
        }
    }
}
