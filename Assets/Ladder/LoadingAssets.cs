using Bindito.Core;
using Timberborn.AssetSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using TimberbornAPI;
using TimberbornAPI.AssetLoaderSystem.AssetSystem;
using UnityEngine;

namespace Ladder
{
    public class LoadingAssets : ILoadableSingleton
    {

        private readonly IAssetLoader _assetLoader;
        private readonly IResourceAssetLoader _resourceAssetLoader;
        
        public LoadingAssets(IAssetLoader assetLoader, IResourceAssetLoader resourceAssetLoader)
        {
            _assetLoader = assetLoader;
            _resourceAssetLoader = resourceAssetLoader;
        }
        
        public void Load()
        {
            var platformModel = _resourceAssetLoader.Load<GameObject>("Buildings/Paths/Platform/Platform.Full.Folktails");
            var shader = platformModel.GetComponent<MeshRenderer>().materials[0].shader;
    
            AddBuilding("Ladder", shader);
        
            Plugin.Log.LogInfo($"Loaded buildings!");
        }
    
        private void AddBuilding(string name, Shader shader)
        {
            var building = _assetLoader.Load<GameObject>(Plugin.PluginGuid, $"tobbert_ladder/{name}");
            
            FixMaterialShader(building, shader);
            // TimberAPI.CustomObjectRegistry.AddGameObject(building);
        }
        
        private static void FixMaterialShader(GameObject obj, Shader shader)
        {
            var meshRenderer = obj.GetComponent<MeshRenderer>();
            if (meshRenderer)
            {
                foreach (var mat in meshRenderer.materials)
                {
                    mat.shader = shader;
                }
            }
    
            foreach (Transform child in obj.transform)
            {
                if (child.gameObject)
                {
                    FixMaterialShader(child.gameObject, shader);
                }
            }
        }
    
        public class LadderConfigurator : IConfigurator
        {
            public void Configure(IContainerDefinition containerDefinition)
            {
                containerDefinition.Bind<LoadingAssets>().AsSingleton();
            }
        }
    }
}

