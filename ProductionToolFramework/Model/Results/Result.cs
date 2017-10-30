using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Demcon.ProductionTool.Model
{
    [XmlInclude(typeof(ValueResult))]
    [XmlInclude(typeof(BooleanResult))]
    [XmlInclude(typeof(ErrorResult))]
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public Result()
        { }

        public Result(string name, string remarks)
        {
            this.Name = name;
            this.Remarks = remarks;
        }

        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Remarks { get; set; }
        [XmlAttribute]
        public virtual ETestConclusion Conclusion { get; set; }
        public override string ToString()
        {
            string retVal = string.Format("Result of test '{0}': {1}.", this.Name, this.Conclusion);
            if (!string.IsNullOrWhiteSpace(this.Remarks))
            {
                retVal += "\nRemarks: " + this.Remarks;
            }

            return retVal;
        }
    }
}
