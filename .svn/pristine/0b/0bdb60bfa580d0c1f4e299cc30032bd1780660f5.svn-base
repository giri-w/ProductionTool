using System;
using System.Xml.Serialization;
namespace Demcon.ProductionTool.Model
{
    public class ErrorResult: Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResult"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public ErrorResult()
        { }

        public ErrorResult(string name, string remarks)
            : base(name, remarks)
        {
        }

        [XmlAttribute]
        public override ETestConclusion Conclusion
        {
            get
            {
                return ETestConclusion.Failed;
            }
        }

        public override string ToString()
        {
            string retVal = string.Format("Error during test '{0}': {1}.", this.Name, this.Remarks);
            if (!string.IsNullOrWhiteSpace(this.Remarks))
            {
                retVal += "\nRemarks: " + this.Remarks;
            }

            return retVal;
        }
    }
}
