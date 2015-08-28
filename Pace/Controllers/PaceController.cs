using DataAccessLayer;
using Entities;
using Pace.Models;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pace.Controllers
{
    public class PaceController : Controller
    {
        private IPaceData _proxy;

        public PaceController(IPaceData proxy)
        {
            this._proxy = proxy;
        }

        // GET: Pace
        public ActionResult Index()
        {
            PaceModel objPaceModel = new PaceModel();
            objPaceModel = CalculatePace();
            return View(objPaceModel);
        }

        private PaceModel CalculatePace()
        {
            PaceModel objPaceModel = new PaceModel();

            DateTime eDateSwp = DateTime.Now;
            DateTime sDateSwp = GetPreviousDate(DateTime.Now.DayOfWeek, out eDateSwp);

            DateTime eDateVac = DateTime.Now;
            DateTime sDateVac = GetUpcomingDate(DateTime.Now.DayOfWeek, out eDateVac);
            PaceData objPaceDataDAL = new PaceData();
            List<Vacation> lstVacation = objPaceDataDAL.GetVacationData();
            List<Swipe> lstSwipe = objPaceDataDAL.GetSwipeData();

            lstVacation = lstVacation.Where(vac => vac.FromDate <= eDateVac && vac.ToDate >= sDateVac).ToList();

            DateTime vacStart = lstVacation.Where(vac => vac.FromDate <= vac.FromDate).First().FromDate;
            DateTime vacEnd = lstVacation.Where(vac => vac.ToDate >= vac.ToDate).First().ToDate;

            List<DateTime> vacationDates = GetVacationDates(vacStart, vacEnd);

            lstSwipe = lstSwipe.Where(vac => vac.SwipeDate >= sDateSwp && vac.SwipeDate <= eDateSwp).ToList();

            List<DateTime> lstDates = lstSwipe.GroupBy(a => a.SwipeDate).Select(b => b.First().SwipeDate).ToList();

            List<SwipeHours> swipes = new List<SwipeHours>();



            foreach (DateTime date in lstDates)
            {
                foreach (Swipe sp in lstSwipe.Where(a => a.SwipeDate == date).ToList())
                {
                    SwipeHours spHr = swipes.Where(b => b.Date == sp.SwipeDate).FirstOrDefault();
                    if (spHr != null)
                    {
                        if (sp.In < sp.Out)
                        {
                            double hrs = (sp.Out - sp.In).TotalHours;
                            spHr.Hours = spHr.Hours + Convert.ToInt32(hrs);
                            swipes.Remove(spHr);
                            swipes.Add(spHr);
                        }
                    }
                    else
                    {
                        if (sp.In < sp.Out)
                        {
                            double hrs = (sp.Out - sp.In).TotalHours;
                            spHr = new SwipeHours();
                            spHr.Hours = Convert.ToInt32(hrs);
                            spHr.Date = sp.SwipeDate;
                            swipes.Add(spHr);
                        }
                    }
                    lstSwipe.Remove(sp);
                }
            }
            DateTime weekSt = DateTime.Now;

            DateTime weekEn = GetWeekStartEnd(out weekSt);

            objPaceModel.SwipeHr = new List<SwipeHours>();
            foreach (DateTime weekDts in GetVacationDates(weekSt, weekEn))
            {
                SwipeHours objSwipeHours = swipes.Where(a => a.Date == weekDts).FirstOrDefault();
                if (objSwipeHours == null)
                {
                    objSwipeHours = new SwipeHours();
                    objSwipeHours.Date = weekDts;
                    objSwipeHours.Hours = 0;
                }
                objPaceModel.SwipeHr.Add(objSwipeHours);
            }

            ////foreach (DateTime dtm in lstDates)
            ////{
            ////    if (!swipes.Any(a => a.Date == dtm))
            ////        swipes.Add(new SwipeHours { Date = dtm, Hours = 8 });
            ////}

            ////objPaceModel.SwipeHr = swipes;

            return objPaceModel;
        }

        private List<DateTime> GetVacationDates(DateTime vacStart, DateTime vacEnd)
        {
            List<DateTime> lstDates = new List<DateTime>();

            if (vacStart > vacEnd)
            {
                return null;
            }
            DateTime tmpDate = vacStart;
            do
            {
                lstDates.Add(tmpDate);
                tmpDate = tmpDate.AddDays(1);
            } while (tmpDate <= vacEnd);

            return lstDates;
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

        private DateTime GetWeekStartEnd(out DateTime sDate)
        {
            sDate = DateTime.Now;
            DateTime eDate = DateTime.Now;
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    sDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    eDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(7).AddTicks(-1);
                    break;
                case DayOfWeek.Monday:
                    sDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-1);
                    eDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(6).AddTicks(-1);
                    break;
                case DayOfWeek.Tuesday:
                    sDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-2);
                    eDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(5).AddTicks(-1);
                    break;
                case DayOfWeek.Wednesday:
                    sDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-3);
                    eDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(4).AddTicks(-1);
                    break;
                case DayOfWeek.Thursday:
                    sDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-4);
                    eDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(3).AddTicks(-1);
                    break;
                case DayOfWeek.Friday:
                    sDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-5);
                    eDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(2).AddTicks(-1);
                    break;
                case DayOfWeek.Saturday:
                    sDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-6);
                    eDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1).AddTicks(-1);
                    break;
            }
            return eDate;
        }


    }
}