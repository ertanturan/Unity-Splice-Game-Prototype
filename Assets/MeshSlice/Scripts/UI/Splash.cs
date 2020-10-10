using UnityEngine;

using LightDev;
using LightDev.Core;
using LightDev.UI;

using DG.Tweening;

namespace MeshSlice.UI
{
  public class Splash : CanvasElement
  {
    [Header("References")]
    public Base background;

    public override void Subscribe()
    {
      Events.RequestSplash += Show;
      Events.PostReset += Hide;
    }

    public override void Unsubscribe()
    {
      Events.RequestSplash -= Show;
      Events.PostReset -= Hide;
    }

    protected override void OnStartShowing()
    {
      background.SetFade(0);
      background.Sequence(
        background.Fade(1, 0.5f).SetEase(Ease.InSine),
        OnFinish(() => Events.RequestReset.Call())
      );
    }

    protected override void OnStartHiding()
    {
      background.KillSequences();
      background.Sequence(
        background.Fade(0, 0.4f).SetEase(Ease.InSine)
      );
    }
  }
}
