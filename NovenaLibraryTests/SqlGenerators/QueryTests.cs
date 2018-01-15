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
    public class QueryTests
    {
        private Query query;

        [TestInitialize]
        public void run_before_each_test()
        {
            query = new Query("main");
        }


        [TestMethod]
        public void QueryTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void SetLimit_LimitGreaterThan1Million()
        {
            var limit = "2000000";

            query.SetLimit(limit);

            Assert.IsTrue(query.Limit == "1000000");
        }

        [TestMethod]
        public void SetLimit_LessThan1Million()
        {
            var limit = "50";

            query.SetLimit(limit);

            Assert.IsTrue(query.Limit == "50");
        }

        [TestMethod]
        public void SetLimit_LimitIsNotANumber()
        {
            var limit = "abc";

            query.SetLimit(limit);

            Assert.IsTrue(query.Limit == "abc");
        }

        [TestMethod]
        public void AddSingleCriteriaTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void AddMultipleCriteriaTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RemoveSingleCriteriaTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RemoveSingleCriteriaTest1()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ReplaceAllMatchingCriteriaTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RemoveAllCriteriaWithMatchingColumnTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void FindAllCriteriaByColumnTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CriteriaExistsForColumnTest()
        {
            Assert.Fail();
        }
    }
}