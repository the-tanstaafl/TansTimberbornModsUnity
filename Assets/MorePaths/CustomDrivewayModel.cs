using System;
using Bindito.Core;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Coordinates;
using Timberborn.PathSystem;
using Timberborn.PrefabOptimization;
using TimberbornAPI.AssetLoaderSystem.AssetSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace MorePaths
{
  public class CustomDrivewayModel : MonoBehaviour
  {
    [SerializeField]
    private GameObject _narrowLeftDrivewayPrefab;
    [SerializeField]
    private GameObject _narrowCenterDrivewayPrefab;
    [SerializeField]
    private GameObject _narrowRightDrivewayPrefab;
    [SerializeField]
    private GameObject _wideCenterDrivewayPrefab;
    [SerializeField]
    private GameObject _longCenterDrivewayPrefab;
    [SerializeField]
    private GameObject _straightPathDrivewayPrefab;
    
    public OptimizedPrefabInstantiator OptimizedPrefabInstantiator;
    public AssetLoader AssetLoader;
    public GameObject model;

    [Inject]
    public void InjectDependencies(
      OptimizedPrefabInstantiator optimizedPrefabInstantiator,
      AssetLoader assetLoader)
    {
      this.OptimizedPrefabInstantiator = optimizedPrefabInstantiator;
      AssetLoader = assetLoader;
    }
    
    public void InstantiateModel(
      DrivewayModel drivewayModel,
      Vector3Int coordinates,
      Direction2D direction)
    {
      model = OptimizedPrefabInstantiator.Instantiate(GetModelPrefab(drivewayModel.Driveway), drivewayModel.GetComponent<BuildingModel>().FinishedModel.transform);
      model.transform.localPosition = CoordinateSystem.GridToWorld(BlockCalculations.Pivot(coordinates, direction.ToOrientation()));
      model.transform.localRotation = direction.ToWorldSpaceRotation();
    }

    public GameObject GetModelPrefab(Driveway driveway)
    {
      switch (driveway)
      {
        case Driveway.NarrowLeft:
          return AssetLoader.Load<GameObject>("tobbert.morepaths/tobbert_morepaths/DirtDrivewayNarrowLeft_0");
        case Driveway.NarrowCenter:
          return AssetLoader.Load<GameObject>("tobbert.morepaths/tobbert_morepaths/DirtDrivewayNarrowCenter_0");
        case Driveway.NarrowRight:
          return AssetLoader.Load<GameObject>("tobbert.morepaths/tobbert_morepaths/DirtDrivewayNarrowRight_0");
        case Driveway.WideCenter:
          return AssetLoader.Load<GameObject>("tobbert.morepaths/tobbert_morepaths/DirtDrivewayWideCenter_0");
        case Driveway.LongCenter:
          return AssetLoader.Load<GameObject>("tobbert.morepaths/tobbert_morepaths/DirtDrivewayLongCenter_0");
        case Driveway.StraightPath:
          return AssetLoader.Load<GameObject>("tobbert.morepaths/tobbert_morepaths/DirtDrivewayStraightPath_0");
        default:
          throw new ArgumentOutOfRangeException(nameof (driveway), (object) driveway, (string) null);
      }
    }
  }
}
