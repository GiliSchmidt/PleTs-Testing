﻿using System;
using System.Collections.Generic;
using Lesse.Core.ControlAndConversionStructures;

namespace Lesse.Modeling.TestPlanStructure
{
    public class TestPlanForTCC : GeneralUseStructure
    {
        public String Name { set; get; }
        //public String NameUseCase { set; get; }
        //public String Id { set; get; }
        public List<TestCaseForTCC> TestCases { get; set; }

        public TestPlanForTCC()
        {
            this.Name = "";
            //this.NameUseCase = "";
            //this.Id = Guid.NewGuid().ToString();
            this.TestCases = new List<TestCaseForTCC>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
