using System;
using System.Linq;
using Bindito.Core;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Coordinates;
using Timberborn.PathSystem;
using Timberborn.PrefabOptimization;
using TimberbornAPI.AssetLoaderSystem.AssetSystem;
using UnityEngine;

namespace MorePaths
{
  public class StoneDrivewayModelInstantiator : MonoBehaviour
  {
    public OptimizedPrefabInstantiator _optimizedPrefabInstantiator;
    public AssetLoader _assetLoader;

    [Inject]
    public void InjectDependencies(
      OptimizedPrefabInstantiator optimizedPrefabInstantiator,
      AssetLoader assetLoader)
    {
      this._optimizedPrefabInstantiator = optimizedPrefabInstantiator;
      _assetLoader = assetLoader;
    }
    
    public GameObject InstantiateModel(
      DrivewayModel drivewayModel,
      Vector3Int coordinates,
      Direction2D direction)
    {
      // Plugin.Log.LogInfo(GetComponent<DrivewayModel>().name);
      // Plugin.Log.LogFatal(GetComponent<DrivewayModel>().Driveway);
      // var test2 = _assetLoader.LoadAll<GameObject>("tobbert.morepaths/tobbert_morepaths");
      // Plugin.Log.LogInfo(test2.First());
      
      // GameObject gameObject = this._optimizedPrefabInstantiator.Instantiate(GetModelPrefab(drivewayModel.Driveway), drivewayModel.GetComponent<BuildingModel>().FinishedModel.transform);
      
      GameObject gameObject = _optimizedPrefabInstantiator.Instantiate(GetModelPrefab(drivewayModel.Driveway), drivewayModel.GetComponent<BuildingModel>().FinishedModel.transform);
      gameObject.transform.localPosition = CoordinateSystem.GridToWorld(BlockCalculations.Pivot(coordinates, direction.ToOrientation()));
      gameObject.transform.localRotation = direction.ToWorldSpaceRotation();
      
      // Plugin.Log.LogFatal(gameObject);
      
      return gameObject;


      // GameObject gameObject = this._optimizedPrefabInstantiator.Instantiate(GetModelPrefab(drivewayModel.Driveway), drivewayModel.GetComponent<BuildingModel>().FinishedModel.transform);
      // gameObject.transform.localPosition = CoordinateSystem.GridToWorld(BlockCalculations.Pivot(drivewayModel.GetLocalCoordinates(), drivewayModel.GetLocalDirection().ToOrientation()));
      // gameObject.transform.localRotation = drivewayModel.GetLocalDirection().ToWorldSpaceRotation();
      // return gameObject;
    }

    public GameObject GetModelPrefab(Driveway driveway)
    {
      switch (driveway)
      {
        case Driveway.NarrowLeft:
          return _assetLoader.Load<GameObject>("tobbert.morepaths/tobbert_morepaths/DirtDrivewayNarrowLeft_0");
        case Driveway.NarrowCenter:
          return _assetLoader.Load<GameObject>("tobbert.morepaths/tobbert_morepaths/DirtDrivewayNarrowCenter_0");
        case Driveway.NarrowRight:
          return _assetLoader.Load<GameObject>("tobbert.morepaths/tobbert_morepaths/DirtDrivewayNarrowRight_0");
        case Driveway.WideCenter:
          return _assetLoader.Load<GameObject>("tobbert.morepaths/tobbert_morepaths/DirtDrivewayWideCenter_0");
        case Driveway.LongCenter:
          return _assetLoader.Load<GameObject>("tobbert.morepaths/tobbert_morepaths/DirtDrivewayLongCenter_0");
        case Driveway.StraightPath:
          return _assetLoader.Load<GameObject>("tobbert.morepaths/tobbert_morepaths/DirtDrivewayStraightPath_0");
        default:
          throw new ArgumentOutOfRangeException(nameof (driveway), (object) driveway, (string) null);
      }
    }
  }
}
