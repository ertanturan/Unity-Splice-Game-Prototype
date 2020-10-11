using UnityEngine;

namespace HoleBall
{
  [ExecuteInEditMode]
  public class GradientBackground : MonoBehaviour
  {
    private Camera _camera;

    private void Awake()
    {
      SetupCamera();
    }

    private void Update()
    {
      if(_camera == null) SetupCamera();

      float distanceToCamera = _camera.farClipPlane / 2f;

      var frustumHeight = 2.0f * distanceToCamera * Mathf.Tan(_camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
      var frustumWidth = frustumHeight * _camera.aspect;

      transform.localScale = new Vector3(frustumWidth, frustumHeight, 1);
      transform.localPosition = new Vector3(0, 0, distanceToCamera);
    }

    private void SetupCamera()
    {
      _camera = Camera.main;
      transform.parent = _camera.transform;
    }
  }
}
