using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.TestExecutionUtil
{
    public class StepMessage
    {
        public string Message { get; set; }
        public string StepName { get; set; }
        public int StepNumber { get; set; }

    }
}
