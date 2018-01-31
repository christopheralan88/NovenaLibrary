using Microsoft.VisualStudio.TestTools.UnitTesting;
using NovenaLibrary.SqlGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.SqlGenerators.Tests
{
    [TestClass]
    public class CriteriaTests
    {
        public Criteria criteria;

        [TestInitialize]
        public void run_before_each_test()
        {
            criteria = new Criteria();   
        }

        [TestMethod]
        public void Criteria_SetFilterToNonNullValueAndIsNullIsFalse_DefaultIsNullValue()
        {
            var filter = "bob";

            criteria.Filter = filter;

            Assert.IsTrue(criteria.OrIsNull == false);
            Assert.IsTrue(criteria.Filter == filter);
        }

        [TestMethod]
        public void Criteria_SetFilterToNonNullValueAndIsNullIsFalse_SetIsNullAfterObjectConstruction()
        {
            var isNull = false;
            var filter = "bob";

            criteria.Filter = filter;
            criteria.OrIsNull = isNull;

            Assert.IsTrue(criteria.OrIsNull == isNull);
            Assert.IsTrue(criteria.FrontParenthesis.Length == 1);
            Assert.IsTrue(criteria.EndParenthesis.Length == 1);
            Assert.IsTrue(criteria.Filter == filter);
        }

        [TestMethod]
        public void Criteria_SetFilterToNonNullValueAndIsNullIsTrue_SetIsNullFirst()
        {
            var isNull = true;
            var filter = "bob";

            criteria.OrIsNull = isNull;
            criteria.Filter = filter;

            Assert.IsTrue(criteria.OrIsNull == isNull);
            Assert.IsTrue(criteria.FrontParenthesis.Length == 1);
            Assert.IsTrue(criteria.EndParenthesis.Length == 1);
            Assert.IsTrue(criteria.Filter == filter);
        }

        [TestMethod]
        public void Criteria_SetFilterToNonNullValueAndIsNullIsTrue_SetIsNullLast()
        {
            var isNull = true;
            var filter = "bob";

            criteria.Filter = filter;
            criteria.OrIsNull = isNull;

            Assert.IsTrue(criteria.OrIsNull == isNull);
            Assert.IsTrue(criteria.FrontParenthesis.Length == 1);
            Assert.IsTrue(criteria.EndParenthesis.Length == 1);
            Assert.IsTrue(criteria.Filter == filter);
        }

    }
}