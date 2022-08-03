using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.PathSystem;
using TimberbornAPI;
using TimberbornAPI.AssetLoaderSystem.AssetSystem;
using UnityEngine;

namespace MorePaths
{
    public class MorePathsService
    {
        private IEnumerable<Object> _pathObjects;

        private BlockService _blockService;
        private AssetLoader _assetLoader;
        public MorePathsService(
            BlockService blockService,
            AssetLoader assetLoader)
        {
            _blockService = blockService;
            _assetLoader = assetLoader;
        }
        
        public void Awake(DrivewayModel instance)
        {
            var parameters = new object[] { };
            MethodInfo methodInfo;
            
            methodInfo = typeof(DrivewayModel).GetMethod("GetLocalCoordinates", BindingFlags.NonPublic | BindingFlags.Instance);
            var LocalCoordinates = (Vector3Int)methodInfo.Invoke(instance, parameters);
            methodInfo = typeof(DrivewayModel).GetMethod("GetLocalDirection", BindingFlags.NonPublic | BindingFlags.Instance);
            var LocalDirection = (Direction2D)methodInfo.Invoke(instance, parameters);
            
            
            var myCustomDriveways = _assetLoader.LoadAll<CustomDrivewayModel>(Plugin.PluginGuid, "tobbert_morepaths").Where(obj => obj.GetComponent<CustomDrivewayModel>());

            foreach (var customDriveway in myCustomDriveways)
            {
                instance.gameObject.AddComponent<CustomDrivewayModel>();
                instance.gameObject.AddComponent<CustomDrivewayModel>();
            }
            
            instance.GetComponent<CustomDrivewayModel>().InstantiateModel(instance, LocalCoordinates, LocalDirection);
            
            
            
            
            
            // This code needs to be move, as it now gets called every time a new driveway is initiated. 
            var timberbornpathObjects = Resources.LoadAll("", typeof(DynamicPathModel));
            var mypathObjects = _assetLoader.LoadAll<GameObject>(Plugin.PluginGuid, "tobbert_morepaths").Where(obj => obj.GetComponent<DynamicPathModel>());

            _pathObjects = timberbornpathObjects.Concat(mypathObjects);
        }
        
        public void UpdateAllDriveways(DrivewayModel instance, GameObject model)
        {
            GameObject path = TimberAPI.DependencyContainer.GetInstance<MorePathsService>().GetPath(instance);

            foreach (var pathObject in _pathObjects)
            {
                if (path != null)
                {
                    if (path.name.Replace("(Clone)", "") == pathObject.name)
                    {
                        if (pathObject.name == "Path.Folktails" | pathObject.name == "Path.IronTeeth")
                        {
                            model.SetActive(true);
                            instance.GetComponent<CustomDrivewayModel>().model.SetActive(false);
                        }
                        else
                        {
                            model.SetActive(false);
                            instance.GetComponent<CustomDrivewayModel>().model.SetActive(true);
                        }
                    }
                }
                else
                {
                    model.SetActive(false);
                    instance.GetComponent<CustomDrivewayModel>().model.SetActive(false);
                }
            }
        }

        public GameObject GetPath(DrivewayModel instance)
        {
            var parameters = new object[] { };
            MethodInfo methodInfo;
            
            methodInfo = typeof(DrivewayModel).GetMethod("GetPositionedDirection", BindingFlags.NonPublic | BindingFlags.Instance);
            var direction = (Direction2D)methodInfo.Invoke(instance, parameters);
            methodInfo = typeof(DrivewayModel).GetMethod("GetPositionedCoordinates", BindingFlags.NonPublic | BindingFlags.Instance);
            var coordinates = (Vector3Int)methodInfo.Invoke(instance, parameters);
            
            Vector3Int checkObjectCoordinates =  coordinates + direction.ToOffset();
            IEnumerable<DynamicPathModel> paths = _blockService.GetObjectsWithComponentAt<DynamicPathModel>(checkObjectCoordinates);

            foreach (var path in paths)
            {
                return path.gameObject;
            }
            return null;
        }
    }
}