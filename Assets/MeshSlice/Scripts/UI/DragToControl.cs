using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LightDev;
using LightDev.Core;
using LightDev.UI;

using DG.Tweening;

namespace MeshSlice.UI
{
  public class DragToControl : CanvasElement
  {
    [Header("References")]
    public Base holder;
    public Base finger;

    private int clickTime;

    public override void Subscribe()
    {
      Events.GameStart += Show;
      Events.PointerDown += OnPointerUp;
    }

    public override void Unsubscribe()
    {
      Events.GameStart -= Show;
      Events.PointerDown -= OnPointerUp;
    }

    private void OnPointerUp()
    {
      Hide();
    }

    protected override void OnStartShowing()
    {
      holder.Deactivate();
    }

    protected override void OnFinishShowing()
    {
      holder.Activate();
      AnimateFinger();
    }

    private void AnimateFinger()
    {
      finger.SetPositionX(-153);
      finger.Sequence(
        finger.MoveX(213, 1.2f).SetEase(Ease.InOutQuart),
        finger.MoveX(-153, 1.2f).SetEase(Ease.InOutQuart)
      ).SetLoops(-1);
    }
  }
}
