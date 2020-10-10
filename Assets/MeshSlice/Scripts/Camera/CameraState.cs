using UnityEngine;

using DG.Tweening;

namespace MeshSlice
{
  [System.Serializable]
  public class CameraState
  {
    public Vector3 position;
    public Vector3 rotation;
    public float fov;
    public float duration;
    public Ease ease;
  }
}
