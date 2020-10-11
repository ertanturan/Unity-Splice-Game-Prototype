using UnityEngine;
using UnityEngine.EventSystems;

using LightDev;

namespace MeshSlice.UI
{
  public class Touchpad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
  {
    public float sensitivity = 1;
    private float _pixelDelta;

    public void OnDrag(PointerEventData eventData)
    {
      _pixelDelta = eventData.delta.x * 0.001f * sensitivity;
    }

    private void Update()
    {
      InputManager.SetHorizontal(_pixelDelta);
      _pixelDelta = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      Events.PointerUp.Call();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      Events.PointerDown.Call();
    }
  }
}
