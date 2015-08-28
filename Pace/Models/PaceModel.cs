using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pace.Models
{
    public class PaceModel
    {
        public string EmployeeId { get; set; }
        public List<SwipeHours> SwipeHr { get; set; }
        public int Total { get; set; }
    }
}