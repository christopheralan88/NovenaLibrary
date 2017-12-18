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
                // make fragment variable of first interpolated fragment
                var startMark = _formattable.IndexOf('{');
                var length = _formattable.IndexOf('}') - startMark + 1;
                var fragment = _formattable.Substring(startMark, length).ToLower();

                // loop through Criteria objects' Column property
                bool foundMatch = false;
                var i = 0;
                Criteria[] matchingCriteria = new Criteria[_criteria.Count]; 
                foreach (var criteria in _criteria)
                {
                    // if Property value = fragment, then add criteria object to list of matching criteria
                    if (criteria.Column.ToLower() == fragment)
                    {
                        foundMatch = true;
                        matchingCriteria[i] = criteria;
                        i++;
                    }
                }

                // if no Property matches, throw exception
                if (!foundMatch)
                {
                    throw new FragmentNotFoundException(fragment + "was not found");
                }
                else
                {
                    // Build new string to replace placeholder
                    var replacementString = "";
                    foreach (var criteria in matchingCriteria)
                    {
                        replacementString += criteria.ToString();
                    }

                    _formattable = _formattable.Replace(fragment, replacementString);
                }
            }

        }
    }
}
