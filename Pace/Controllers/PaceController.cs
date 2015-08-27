using Pace.Helper;
using Pace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pace.Controllers
{
    public class PaceController : Controller
    {
        // GET: Pace
        public ActionResult Index()
        {
            PaceModel objPaceModel = new PaceModel();

            return View(objPaceModel);
        }

        private PaceModel CalculatePace()
        {
            PaceModel objPaceModel = new PaceModel();

            DateTime eDateSwp = DateTime.Now;
            DateTime sDateSwp = GetPreviousDate(DateTime.Now.DayOfWeek, out eDateSwp);

            DateTime eDateVac = DateTime.Now;
            DateTime sDateVac = GetUpcomingDate(DateTime.Now.DayOfWeek, out eDateVac);

            if (HasLeave(DateTime.Now))
            {

            }

            return objPaceModel;
        }

        private bool HasLeave(DateTime Date)
        {
            return true;
        }


        private DateTime GetPreviousDate(DayOfWeek day, out DateTime eDate)
        {
            DateTime retnDate = new DateTime();
            eDate = DateTime.Now;
            switch (day)
            {
                case DayOfWeek.Sunday:
                    retnDate = DateTime.Now;
                    break;
                case DayOfWeek.Monday:
                    retnDate = DateTime.Now.AddDays(-1);
                    eDate = DateTime.Now.AddDays(-1).AddTicks(-1);
                    break;
                case DayOfWeek.Tuesday:
                    retnDate = DateTime.Now.AddDays(-2);
                    eDate = DateTime.Now.AddDays(-1).AddTicks(-1);
                    break;
                case DayOfWeek.Wednesday:
                    retnDate = DateTime.Now.AddDays(-3);
                    eDate = DateTime.Now.AddDays(-1).AddTicks(-1);
                    break;
                case DayOfWeek.Thursday:
                    retnDate = DateTime.Now.AddDays(-4);
                    eDate = DateTime.Now.AddDays(-1).AddTicks(-1);
                    break;
                case DayOfWeek.Friday:
                    retnDate = DateTime.Now.AddDays(-5);
                    eDate = DateTime.Now.AddDays(-1).AddTicks(-1);
                    break;
                case DayOfWeek.Saturday:
                    retnDate = DateTime.Now.AddDays(-6);
                    eDate = DateTime.Now.AddDays(-1).AddTicks(-1);
                    break;
            }

            return retnDate;
        }

        private DateTime GetUpcomingDate(DayOfWeek day, out DateTime eDate)
        {
            DateTime retnDate = new DateTime();
            eDate = DateTime.Now;
            switch (day)
            {
                case DayOfWeek.Sunday:
                    retnDate = DateTime.Now.AddDays(1);
                    eDate = DateTime.Now.AddDays(6).AddTicks(-1);
                    break;
                case DayOfWeek.Monday:
                    retnDate = DateTime.Now.AddDays(1);
                    eDate = DateTime.Now.AddDays(5).AddTicks(-1);
                    break;
                case DayOfWeek.Tuesday:
                    retnDate = DateTime.Now.AddDays(1);
                    eDate = DateTime.Now.AddDays(4).AddTicks(-1);
                    break;
                case DayOfWeek.Wednesday:
                    retnDate = DateTime.Now.AddDays(1);
                    eDate = DateTime.Now.AddDays(3).AddTicks(-1);
                    break;
                case DayOfWeek.Thursday:
                    retnDate = DateTime.Now.AddDays(1);
                    eDate = DateTime.Now.AddDays(2).AddTicks(-1);
                    break;
                case DayOfWeek.Friday:
                    retnDate = DateTime.Now.AddDays(1);
                    eDate = DateTime.Now.AddDays(1).AddTicks(-1);
                    break;
                case DayOfWeek.Saturday:
                    retnDate = DateTime.Now;
                    break;
            }

            return retnDate;
        }


        private List<Vacation> GetVacationData(string empId)
        {
            List<Vacation> lstVacation = new List<Vacation>();
            lstVacation.Add(new Vacation { EmployeeId = "2548695", FromDate = Convert.ToDateTime("08/23/2015"), ToDate = Convert.ToDateTime("08/26/2015") });
            lstVacation.Add(new Vacation { EmployeeId = "2548695", FromDate = Convert.ToDateTime("08/27/2015"), ToDate = Convert.ToDateTime("08/31/2015") });
            lstVacation.Add(new Vacation { EmployeeId = "2548695", FromDate = Convert.ToDateTime("08/1/2015"), ToDate = Convert.ToDateTime("08/4/2015") });
            lstVacation.Add(new Vacation { EmployeeId = "2548696", FromDate = Convert.ToDateTime("08/23/2015"), ToDate = Convert.ToDateTime("08/31/2015") });
            lstVacation.Add(new Vacation { EmployeeId = "2548696", FromDate = Convert.ToDateTime("08/23/2015"), ToDate = Convert.ToDateTime("08/31/2015") });
            return lstVacation;
        }

        private List<Swipe> GetSwipeData(string empId)
        {
            List<Swipe> lstSwipe = new List<Swipe>();
            lstSwipe.Add(new Swipe { EmployeeId = "2548695", SwipeDate = Convert.ToDateTime("08/23/2015"), In = Convert.ToDateTime("08/23/2015 11:00:00:000"), Out = Convert.ToDateTime("08/23/2015 20:10:15:872") });
            lstSwipe.Add(new Swipe { EmployeeId = "2548695", SwipeDate = Convert.ToDateTime("08/24/2015"), In = Convert.ToDateTime("08/24/2015 11:00:00:000"), Out = Convert.ToDateTime("08/24/2015 20:10:15:872") });
            lstSwipe.Add(new Swipe { EmployeeId = "2548695", SwipeDate = Convert.ToDateTime("08/25/2015"), In = Convert.ToDateTime("08/25/2015 11:00:00:000"), Out = Convert.ToDateTime("08/25/2015 20:10:15:872") });
            lstSwipe.Add(new Swipe { EmployeeId = "2548695", SwipeDate = Convert.ToDateTime("08/26/2015"), In = Convert.ToDateTime("08/26/2015 11:00:00:000"), Out = Convert.ToDateTime("08/26/2015 20:10:15:872") });
            lstSwipe.Add(new Swipe { EmployeeId = "2548695", SwipeDate = Convert.ToDateTime("08/27/2015"), In = Convert.ToDateTime("08/27/2015 11:00:00:000"), Out = Convert.ToDateTime("08/27/2015 20:10:15:872") });
            lstSwipe.Add(new Swipe { EmployeeId = "2548695", SwipeDate = Convert.ToDateTime("08/28/2015"), In = Convert.ToDateTime("08/28/2015 11:00:00:000"), Out = Convert.ToDateTime("08/28/2015 20:10:15:872") });
            return lstSwipe;
        }
    }
}