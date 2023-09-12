using Microsoft.VisualStudio.TestTools.UnitTesting;
using C9;
using TestCommon;

namespace C9.Tests
{
    [DeploymentItem("TestData", "C9_TestData")]
    [TestClass()]
    public class GradedTests
    {

        [TestMethod(), Timeout(1000)]
        public void Q1SuperPalindromesTest()
        {
            RunTest(new Q1CheesyProblem("TD1"));
        }

        public static void RunTest(Processor p)
        { 
            TestTools.RunLocalTest("C9", p.Process, p.TestDataName, p.Verifier, VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}