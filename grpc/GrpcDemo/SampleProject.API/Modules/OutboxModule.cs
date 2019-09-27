using System.Reflection;
using Autofac;
using Quartz;
using Module = Autofac.Module;

namespace SampleProject.API.Modules
{
    public class OutboxModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => typeof(IJob).IsAssignableFrom(x))
                .InstancePerDependency();
        }
    }
}