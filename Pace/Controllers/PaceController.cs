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

        public PaceModel CalculatePace()
        {
            PaceModel objPaceModel = new PaceModel();
            DateTime currDate = new DateTime(2015, 08, 26);
            DateTime eDateSwp = DateTime.Now;
            DateTime sDateSwp = GetPreviousDate(currDate.DayOfWeek, currDate, out eDateSwp);

            DateTime eDateVac = DateTime.Now;
            DateTime sDateVac = GetUpcomingDate(currDate.DayOfWeek, currDate, out eDateVac);
            PaceData objPaceDataDAL = new PaceData();
            List<Vacation> lstVacation = _proxy.GetVacationData();
            List<Swipe> lstSwipe = objPaceDataDAL.GetSwipeData();
            List<DateTime> vacationDates = new List<DateTime>();
            lstVacation = lstVacation.Where(vac => vac.FromDate <= eDateVac && vac.ToDate >= sDateVac).ToList();
            if (lstVacation.Count() > 0)
            {
                DateTime vacStart = lstVacation.Where(vac => vac.FromDate <= vac.FromDate).First().FromDate;
                DateTime vacEnd = lstVacation.Where(vac => vac.ToDate >= vac.ToDate).Last().ToDate;
                vacationDates = GetVacationDates(vacStart, vacEnd);
            }

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
            DateTime weekSt = currDate;

            DateTime weekEn = GetWeekStartEnd(currDate, out weekSt);

            objPaceModel.SwipeHr = new List<SwipeHours>();
            objPaceModel.VacationHr = new List<SwipeHours>();
            objPaceModel.OtherHr = new List<SwipeHours>();
            foreach (DateTime weekDts in GetVacationDates(weekSt, weekEn))
            {
                SwipeHours objSwipeHours = swipes.Where(a => a.Date == weekDts).FirstOrDefault();
                SwipeHours objVacHours;
                SwipeHours objOthr = new SwipeHours();
                if (objSwipeHours == null)
                {
                    objSwipeHours = new SwipeHours();
                    objSwipeHours.Date = weekDts;
                    objSwipeHours.Hours = 0;
                }

                if(weekDts.DayOfWeek == DayOfWeek.Friday)
                {
                    objSwipeHours = new SwipeHours();
                    objSwipeHours.Date = weekDts;
                    objSwipeHours.Hours = 5;
                    objOthr.Hours = 4;
                }
                objPaceModel.SwipeHr.Add(objSwipeHours);

                if (vacationDates.Any(a => a == weekDts))
                {
                    objVacHours = new SwipeHours();
                    objVacHours.Date = weekDts;
                    objVacHours.Hours = 8;

                }
                else
                {
                    objVacHours = new SwipeHours();
                    objVacHours.Date = weekDts;
                    objVacHours.Hours = 0;
                }
                objPaceModel.VacationHr.Add(objVacHours);
                objOthr.Date = weekDts;
                objPaceModel.OtherHr.Add(objOthr);
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


        public DateTime GetPreviousDate(DayOfWeek day, DateTime currDate, out DateTime eDate)
        {
            DateTime retnDate = new DateTime();
            eDate = currDate.AddDays(1).AddTicks(-1);
            switch (day)
            {
                case DayOfWeek.Sunday:
                    retnDate = DateTime.Now;
                    break;
                case DayOfWeek.Monday:
                    retnDate = currDate.AddDays(-1);
                    break;
                case DayOfWeek.Tuesday:
                    retnDate = currDate.AddDays(-2);
                    break;
                case DayOfWeek.Wednesday:
                    retnDate = currDate.AddDays(-3);
                    break;
                case DayOfWeek.Thursday:
                    retnDate = currDate.AddDays(-4);
                    break;
                case DayOfWeek.Friday:
                    retnDate = currDate.AddDays(-5);
                    break;
                case DayOfWeek.Saturday:
                    retnDate = currDate.AddDays(-6);
                    break;
            }

            return retnDate;
        }

        public DateTime GetUpcomingDate(DayOfWeek day, DateTime currDate, out DateTime eDate)
        {
            DateTime retnDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(1);
            eDate = DateTime.Now;
            switch (day)
            {
                case DayOfWeek.Sunday:
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(7).AddTicks(-1);
                    break;
                case DayOfWeek.Monday:
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(6).AddTicks(-1);
                    break;
                case DayOfWeek.Tuesday:
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(5).AddTicks(-1);
                    break;
                case DayOfWeek.Wednesday:
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(4).AddTicks(-1);
                    break;
                case DayOfWeek.Thursday:
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(3).AddTicks(-1);
                    break;
                case DayOfWeek.Friday:
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(2).AddTicks(-1);
                    break;
                case DayOfWeek.Saturday:
                    retnDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(1).AddTicks(-1);
                    break;
            }

            return retnDate;
        }

        public DateTime GetWeekStartEnd(DateTime currDate, out DateTime sDate)
        {
            sDate = DateTime.Now;
            DateTime eDate = DateTime.Now;
            switch (currDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    sDate = new DateTime(currDate.Year, currDate.Month, currDate.Day);
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(7).AddTicks(-1);
                    break;
                case DayOfWeek.Monday:
                    sDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(-1);
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(6).AddTicks(-1);
                    break;
                case DayOfWeek.Tuesday:
                    sDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(-2);
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(5).AddTicks(-1);
                    break;
                case DayOfWeek.Wednesday:
                    sDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(-3);
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(4).AddTicks(-1);
                    break;
                case DayOfWeek.Thursday:
                    sDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(-4);
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(3).AddTicks(-1);
                    break;
                case DayOfWeek.Friday:
                    sDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(-5);
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(2).AddTicks(-1);
                    break;
                case DayOfWeek.Saturday:
                    sDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(-6);
                    eDate = new DateTime(currDate.Year, currDate.Month, currDate.Day).AddDays(1).AddTicks(-1);
                    break;
            }
            return eDate;
        }


    }
}