using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.Helpers
{
    public class ActionFeedback : IActionFeedback
    {
        public bool? SuccessUnsuccess { get; set; }
    }
}
