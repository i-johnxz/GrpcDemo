using Autofac;
using SampleProject.API.Customers.DomainServices;
using SampleProject.Domain.Customers;

namespace SampleProject.API.Modules
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerUniquenessChecker>()
                .As<ICustomerUniquenessChecker>()
                .InstancePerLifetimeScope();
        }
    }
}