using Autofac;
using IDao.Lib;
using IDaoImpl.Lib;

namespace NetCoreWeb
{
    public class DefaultModuleRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExampleImpl>().As<IExampleDao>().InstancePerLifetimeScope();
        }

    }
}
