using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pace.Models
{
    public class WeekModel
    {
        public Tuple<DateTime,int> Sunday { get; set; }

        public Tuple<DateTime, int> Monday { get; set; }

        public Tuple<DateTime, int> Tuesday { get; set; }

        public Tuple<DateTime, int> Wednesday { get; set; }

        public Tuple<DateTime, int> Thursday { get; set; }

        public Tuple<DateTime, int> Friday { get; set; }

        public Tuple<DateTime, int> Saturday { get; set; }
    }
}