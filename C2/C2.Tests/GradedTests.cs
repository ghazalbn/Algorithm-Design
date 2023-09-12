using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCommon;

namespace C2.Tests
{
    [DeploymentItem("TestData", "C2_TestData")]
    [TestClass()]
    public class GradedTests
    {

        [TestMethod(), Timeout(10000)]
        public void SolveTest_Q1CaptureTheFlag()
        {
            RunTest(new Q1CaptureTheFlag("TD1"));
        }

        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("C2", p.Process, p.TestDataName, p.Verifier,
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}
