using Bindito.Core;
using Timberborn.ConstructibleSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WindSystem;
using UnityEngine;

namespace Animation
{
  public class WindRotationAnimator : TickableComponent, IFinishedStateListener
  {
    private static readonly int WindRotationSpeed = Animator.StringToHash(nameof (WindRotationSpeed));
    [SerializeField]
    public GameObject _rotatingPart;
    private WindService _windService;
    private NonlinearAnimationManager _nonlinearAnimationManager;
    private Animator _animator;

    [Inject]
    public void InjectDependencies(
      NonlinearAnimationManager nonlinearAnimationManager,
      WindService windService)
    {
      this._nonlinearAnimationManager = nonlinearAnimationManager;
      this._windService = windService;
    }

    public void Awake()
    {
      this._animator = this.GetComponentInChildren<Animator>(true);
      this.enabled = false;
    }

    public void OnEnterFinishedState() => this.enabled = true;

    public void OnExitFinishedState() => this.enabled = false;

    public override void Tick() => this.UpdateAnimation();

    private void UpdateAnimation()
    {
      var forward = _rotatingPart.transform.forward;
      Vector2 currentForwardV2 = new Vector2(forward.x, forward.y);
      var f = Vector2.SignedAngle(currentForwardV2, this._windService.WindDirection);
      // Plugin.Log.LogInfo(f);
      // Plugin.Log.LogInfo(currentForwardV2);
      // Plugin.Log.LogInfo(this._windService.WindDirection);
      // Plugin.Log.LogInfo("//////////////////////////");
      if (Mathf.Abs(f) <= 10.0)
        this._animator.SetFloat(WindRotationAnimator.WindRotationSpeed, 0.0f);
      else
        this._animator.SetFloat(WindRotationAnimator.WindRotationSpeed, (double) f < 0.0 ? this._nonlinearAnimationManager.SpeedMultiplier : -this._nonlinearAnimationManager.SpeedMultiplier);
    }
  }
}
