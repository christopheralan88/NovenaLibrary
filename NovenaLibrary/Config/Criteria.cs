using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.Config
{
    public class Criteria
    {
        public string AndOr { get; set; }
        public string FrontParenthesis { get; set; }
        public string Column { get; set; }
        public string Operator { get; set; }
        public string Filter { get; set; }
        public string EndParenthesis { get; set; }


        public Criteria()
        {
        }

        public Criteria(string AndOr, string FrontParenthesis, string Column, string Operator, string Filter, string EndParenthesis, bool Locked)
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
    }
}
