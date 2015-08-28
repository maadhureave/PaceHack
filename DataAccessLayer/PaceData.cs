using Entities;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PaceData : IPaceData
    {
        public List<Vacation> GetVacationData()
        {
            List<Vacation> lstVacation = new List<Vacation>();
            lstVacation.Add(new Vacation { EmployeeId = "2548695", FromDate = Convert.ToDateTime("08/23/2015"), ToDate = Convert.ToDateTime("08/26/2015") });
            lstVacation.Add(new Vacation { EmployeeId = "2548695", FromDate = Convert.ToDateTime("08/27/2015"), ToDate = Convert.ToDateTime("08/31/2015") });
            lstVacation.Add(new Vacation { EmployeeId = "2548695", FromDate = Convert.ToDateTime("08/1/2015"), ToDate = Convert.ToDateTime("08/4/2015") });
            lstVacation.Add(new Vacation { EmployeeId = "2548696", FromDate = Convert.ToDateTime("08/23/2015"), ToDate = Convert.ToDateTime("08/31/2015") });
            lstVacation.Add(new Vacation { EmployeeId = "2548696", FromDate = Convert.ToDateTime("08/23/2015"), ToDate = Convert.ToDateTime("08/31/2015") });
            return lstVacation;
        }

        public List<Swipe> GetSwipeData()
        {
            List<Swipe> lstSwipe = new List<Swipe>();
            lstSwipe.Add(new Swipe { EmployeeId = "2548695", SwipeDate = Convert.ToDateTime("08/23/2015"), In = Convert.ToDateTime("08/23/2015 11:00:00 AM"), Out = Convert.ToDateTime("08/23/2015 8:34:15 PM") });
            lstSwipe.Add(new Swipe { EmployeeId = "2548695", SwipeDate = Convert.ToDateTime("08/24/2015"), In = Convert.ToDateTime("08/24/2015 11:00:00 AM"), Out = Convert.ToDateTime("08/24/2015 9:51:15 PM") });
            lstSwipe.Add(new Swipe { EmployeeId = "2548695", SwipeDate = Convert.ToDateTime("08/25/2015"), In = Convert.ToDateTime("08/25/2015 11:00:00 AM"), Out = Convert.ToDateTime("08/25/2015 8:12:15 PM") });
            lstSwipe.Add(new Swipe { EmployeeId = "2548695", SwipeDate = Convert.ToDateTime("08/26/2015"), In = Convert.ToDateTime("08/26/2015 11:00:00 AM"), Out = Convert.ToDateTime("08/26/2015 8:56:15 PM") });
            lstSwipe.Add(new Swipe { EmployeeId = "2548695", SwipeDate = Convert.ToDateTime("08/27/2015"), In = Convert.ToDateTime("08/27/2015 11:00:00 AM"), Out = Convert.ToDateTime("08/27/2015 8:23:15 PM") });
            lstSwipe.Add(new Swipe { EmployeeId = "2548695", SwipeDate = Convert.ToDateTime("08/28/2015"), In = Convert.ToDateTime("08/28/2015 11:00:00 AM"), Out = Convert.ToDateTime("08/28/2015 9:29:15 PM") });
            return lstSwipe;
        }
    }
}
