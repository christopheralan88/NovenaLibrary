using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Config;
using NovenaLibrary.Exceptions;

namespace NovenaLibrary.Utilities
{
    public class Interpolator
    {
        private string _formattable;
        private IList<Criteria> _criteria;

        public Interpolator(string formattable, IList<Criteria> criteria)
        {
            _formattable = formattable;
            _criteria = criteria;
        }

        public void Interpolate()
        {
            // While the formattable string still contains placeholders
            while (_formattable.Contains('{') && _formattable.Contains('}'))
            {
                // make variable of first interpolated fragment
                var startMark = _formattable.IndexOf('{');
                var length = _formattable.IndexOf('}') - startMark;
                var fragment = _formattable.Substring(startMark, length).ToLower();

                // loop through Criteria objects' Column property
                bool foundMatch = false;
                foreach (var criteria in _criteria)
                {
                    // if Property value = fragment, then interpolate Criteria's Column, Operator, and Filter (add SQLCleanser)
                    if (criteria.Column.ToLower() == fragment)
                    {
                        foundMatch = true;
                        _formattable = _formattable.Replace(fragment, criteria.Column + " " + criteria.Operator + " " + criteria.Filter);
                    }
                }

                // if no Property matches, throw exception
                if (!foundMatch)
                {
                    throw new FragmentNotFoundException(fragment + "was not found");
                }
            }

        }
    }
}
