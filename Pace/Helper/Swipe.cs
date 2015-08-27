using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pace.Helper
{
    public class Swipe
    {
        public string EmployeeId { get; set; }
        public DateTime SwipeDate { get; set; }
        public DateTime In { get; set; }
        public DateTime Out { get; set; }

    }

    public class Vacation
    {
        public string EmployeeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}