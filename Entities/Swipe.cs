using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Swipe
    {
        public string EmployeeId { get; set; }
        public DateTime SwipeDate { get; set; }
        public DateTime In { get; set; }
        public DateTime Out { get; set; }
        public int Total { get; set; }

    }

    public class Vacation
    {
        public string EmployeeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class SwipeHours
    {
        public DateTime Date { get; set; }
        public int Hours { get; set; }
    }
}
