using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coc.Modeling.TestPlanStructure
{
    public class TestCaseForTCC
    {
        public String Title { get; set; }
        public List<TestStepForTCC> TestSteps { get; set; }

        public TestCaseForTCC(String title)
        {
            this.Title = title;
            //Summary = "";
            this.TestSteps = new List<TestStepForTCC>();
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
