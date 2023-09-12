using Microsoft.VisualStudio.TestTools.UnitTesting;
using C7;
using TestCommon;

namespace C7.Tests
{
    [DeploymentItem("TestData", "C7_TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(1000)]
        public void Q1BuildHashTest()
        {
            string sample = "carpediem";
            var prefixHash = Q1SuffixArrayHashing.BuildHash(sample);
            CollectionAssert.AreEqual(prefixHash, new long[] { 3, 34, 17332, 493988, 5111593, 119628197, 107161254, 670230576, 253617517 });
        }

        [TestMethod(), Timeout(5000)]
        public void Q1SuffixArraySolveTest()
        {
            RunTest(new Q1SuffixArrayHashing("TD1"));
        }

        public static void RunTest(Processor p)
        { 
            TestTools.RunLocalTest("C7", p.Process, p.TestDataName, p.Verifier, VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}