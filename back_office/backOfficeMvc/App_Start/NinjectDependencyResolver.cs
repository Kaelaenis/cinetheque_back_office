using backOfficeMvc.DataAccess;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backOfficeMvc.App_Start
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // Liaisons interface -> implémentation
            _kernel.Bind<IAdminDao>().To<AdminDao>();
            _kernel.Bind<IArticleDao>().To<ArticleDao>();
            // Ajouter d'autres DAO ou services ici
        }
    }
}