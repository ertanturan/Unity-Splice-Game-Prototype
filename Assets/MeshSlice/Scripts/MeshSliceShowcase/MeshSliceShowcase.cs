using UnityEngine;

namespace MeshSlice.Showcase
{
  public class MeshSliceShowcase : MonoBehaviour
  {
    public Material material;
    public GameObject objectToSlice;

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
      SliceFramework.Plane cuttingPlane = new SliceFramework.Plane();
      cuttingPlane.Compute(transform);
      cuttingPlane.OnDebugDraw();
    }
#endif
  }
}
