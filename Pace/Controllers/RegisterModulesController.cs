using DataAccessLayer;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer;

namespace Pace.Controllers
{
    public class RegisterModulesController
    {
        public static void RegisterModules()
        {
            var container = new UnityContainer();
            container.RegisterType<IPaceData, PaceData>();
            UnityControllerFactory objUnityControllerFactory = new UnityControllerFactory(container);
            ControllerBuilder.Current.SetControllerFactory(objUnityControllerFactory);
        }
    }
}