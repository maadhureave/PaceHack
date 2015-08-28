using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
   public interface IPaceData
    {
        List<Vacation> GetVacationData();

        List<Swipe> GetSwipeData();
    }
}
