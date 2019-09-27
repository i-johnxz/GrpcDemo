using Autofac;
using SampleProject.Infrastructure.Emails;

namespace SampleProject.API.Modules
{
    public class EmailModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailSender>()
                .As<IEmailSender>()
                .InstancePerLifetimeScope();
        }
    }
}