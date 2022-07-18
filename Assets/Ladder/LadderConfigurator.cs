using Bindito.Core;

namespace Ladder
{
    public class LadderConfigurator : IConfigurator
    {
       
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<LoadingAssets>().AsSingleton();
        }
    }
}