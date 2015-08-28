using System;
using NUnit.Framework;
using Pace.Controllers;
using ServiceLayer;
using Pace.Models;

namespace UnitTestProject1
{
    
    public class UnitTest1
    {
        IPaceData proxy = new TestPaceData();

        [Test]
        public void TestCalculatePace()
        {
            PaceController objPaceController = new PaceController(proxy);
            PaceModel objPaceModel = objPaceController.CalculatePace();
        }

        [TestCase (0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void TestGetPreviousDate(int weekday)
        {
            PaceController objPaceController = new PaceController(proxy);
            DateTime sdate = DateTime.Now;
            DateTime eDate = DateTime.Now;
            sdate = objPaceController.GetPreviousDate((DayOfWeek)weekday, out eDate);
        }

        [Test]
        public void TestGetUpcomingDate()
        {
            PaceController objPaceController = new PaceController(proxy);
            DateTime sdate = DateTime.Now;
            DateTime eDate = DateTime.Now;
            sdate = objPaceController.GetUpcomingDate(DateTime.Now.DayOfWeek, out eDate);
        }

        [Test]
        public void TestGetWeekStartEnd()
        {
            PaceController objPaceController = new PaceController(proxy);
            DateTime sdate = DateTime.Now;
            DateTime eDate = DateTime.Now;
            PaceModel objPaceModel = objPaceController.CalculatePace();
            eDate = objPaceController.GetWeekStartEnd(out sdate);
        }
    }
}
