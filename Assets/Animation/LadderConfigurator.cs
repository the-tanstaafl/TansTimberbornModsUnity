using Bindito.Core;
using Animation;

namespace Animation
{
    public class AnimationConfigurator : IConfigurator
    {
       
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<LoadingAssets>().AsSingleton();
        }
    }
}