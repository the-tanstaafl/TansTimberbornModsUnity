using Bindito.Core;
using Bindito.Unity;
using Timberborn.PathSystem;
using Timberborn.TemplateSystem;
using TimberbornAPI.AssetLoaderSystem.AssetSystem;

namespace MorePaths
{
    public class MorePathsConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            // containerDefinition.Bind<StoneDrivewayModelInstantiator>().ToInstance(GetComponentInChildren<StoneDrivewayModelInstantiator>(true));
            // containerDefinition.Bind<StoneDrivewayModelInstantiator>().AsSingleton();
            containerDefinition.Bind<CanReachThis>().AsSingleton();
            containerDefinition.Bind<AssetLoader>().AsSingleton();
            
            // containerDefinition.Bind<StoneDrivewayModelInstantiator>().AsSingleton();
            // containerDefinition.Bind<IPathService>().To<PathService>().AsSingleton();
            // containerDefinition.Bind<IConnectionService>().To<ConnectionService>().AsSingleton();
            containerDefinition.MultiBind<TemplateModule>().ToProvider(ProvideTemplateModule).AsSingleton();
        }

        private static TemplateModule ProvideTemplateModule()
        {
            TemplateModule.Builder builder = new TemplateModule.Builder();
            // builder.AddDedicatedDecorator<DrivewayModel, StoneDrivewayModelInstantiator>((IDedicatedDecoratorInitializer<StoneDrivewayModelInstantiator, DrivewayModel>) GetComponentInChildren<StoneDrivewayModelInstantiator>());
            builder.AddDecorator<DrivewayModel, StoneDrivewayModelInstantiator>();
            return builder.Build();
        }
    }
}