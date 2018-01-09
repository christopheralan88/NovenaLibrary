using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NovenaLibrary.SqlGenerators
{
    [Serializable, XmlRoot("Criteria"), XmlType("Criteria")]
    public class Criteria
    {
        [XmlElement] public string AndOr { get; set; }
        [XmlElement] public string FrontParenthesis { get; set; }
        [XmlElement] public string Column { get; set; }
        [XmlElement] public string Operator { get; set; }
        [XmlElement] public string Filter { get; set; }
        [XmlElement] public string EndParenthesis { get; set; }

        public Criteria() { }

        public Criteria(string AndOr = "", string FrontParenthesis = "", string Column = "", 
            string Operator = "", string Filter = "", string EndParenthesis = "", bool Locked = false)
        {
            this.AndOr = AndOr;
            this.FrontParenthesis = FrontParenthesis;
            this.Column = Column;
            this.Operator = Operator;
            this.Filter = Filter;
            this.EndParenthesis = EndParenthesis;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Criteria that = (Criteria)obj;
            if (!Column.Equals(that.Column)) return false;
            if (!Operator.Equals(that.Operator)) return false;
            if (!Filter.Equals(that.Filter)) return false;
            return true;
        }

        public override string ToString()
        {
            return $" {AndOr} {FrontParenthesis}{Column} {Operator} {Filter}{EndParenthesis} ";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
