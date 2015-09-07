using System.Web.Mvc;
using DAO.Repository;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaymentSystem.Startup))]
namespace PaymentSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var factory = DependencyResolver.Current.GetService(typeof(IRepositoryFactory)) as IRepositoryFactory;
            ConfigureAuth(app, factory);
        }
    }
}
