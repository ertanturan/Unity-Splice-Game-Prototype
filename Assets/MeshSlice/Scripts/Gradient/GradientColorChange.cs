using UnityEngine;

using LightDev.Core;
using DG.Tweening;

namespace MeshSlice
{
  public class GradientColorChange : Base
  {
    public Material material;
    public float changeColorSpeed = 4f;
    public Gradient[] colors;
    
    private int currentColorIndex;
    private Color cachedTopColor;
    private Color cachedBottomColor;

    private readonly int topColorId = Shader.PropertyToID("_ColorTop");
    private readonly int bottomColorId = Shader.PropertyToID("_ColorBot");

    private void Awake()
    {
      CacheColors();
      RunAnimation();
    }

    private void OnDestroy() 
    {
      RestoreColors();
    }

    private void RunAnimation()
    {
      Sequence(
        DOTween.To((v) =>
        {
          material.SetColor(topColorId, UnityEngine.Color.Lerp(colors[currentColorIndex % colors.Length].topColor, colors[(currentColorIndex + 1) % colors.Length].topColor, v));
        }, 0, 1, changeColorSpeed)
      );

      Sequence(
        DOTween.To((v) =>
        {
          material.SetColor(bottomColorId, UnityEngine.Color.Lerp(colors[currentColorIndex % colors.Length].botomColor, colors[(currentColorIndex + 1) % colors.Length].botomColor, v));
        }, 0, 1, changeColorSpeed),
        OnFinish(() =>
        {
          currentColorIndex++;
          RunAnimation();
        }
        )
      );
    }

    private void CacheColors()
    {
      cachedTopColor = material.GetColor(topColorId);
      cachedBottomColor = material.GetColor(bottomColorId);
    }

    private void RestoreColors()
    {
      material.SetColor(topColorId, cachedTopColor);
      material.SetColor(bottomColorId, cachedBottomColor);
    }

    [System.Serializable]
    public class Gradient
    {
      public UnityEngine.Color topColor;
      public UnityEngine.Color botomColor;
    }
  }
}
