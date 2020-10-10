using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace LightDev.UI
{
  [RequireComponent(typeof(Image))]
  public class ButtonColor : BaseButton
  {
    [Space]
    public Color pressedColor = new Color(200 / 255f, 200 / 255f, 200 / 255f, 255 / 255f);

    protected Color normalColor;

    protected override void Awake()
    {
      base.Awake();

      normalColor = target.color;
    }

    protected override void AnimatePress()
    {
      KillSequences();
      Sequence(target.DOColor(pressedColor, 0.2f));
    }

    protected override void AnimateUnpress()
    {
      KillSequences();
      Sequence(target.DOColor(normalColor, 0.2f));
    }
  }
}
