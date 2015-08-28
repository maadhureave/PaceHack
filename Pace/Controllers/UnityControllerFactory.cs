using Microsoft.Practices.Unity;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pace.Controllers
{
    internal class UnityControllerFactory : DefaultControllerFactory
    {
        public readonly IUnityContainer _container;

        public UnityControllerFactory(UnityContainer container)
        {
            this._container = container;
        }

        protected override IController GetControllerInstance(RequestContext request, Type type)
        {
            if (request == null)
            {
                return null;
            }
            return _container.Resolve(type) as IController;
        }
    }
}