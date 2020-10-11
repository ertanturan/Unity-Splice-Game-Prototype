using UnityEngine;
using UnityEditor;

using SliceFramework;

namespace MeshSlice.Showcase
{
  [CustomEditor(typeof(MeshSliceShowcase))]
  public class MeshSliceShowcaseEditor : Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      GameObject objectToSlice = ((MeshSliceShowcase)target).objectToSlice;
      Material material = ((MeshSliceShowcase)target).material;

      if (objectToSlice && material)
      {
        if (GUILayout.Button("Slice"))
        {
          Slice(objectToSlice, material);
        }
      }
    }

    private void Slice(GameObject objectToSlice, Material material)
    {
      Transform slicePoint = ((MeshSliceShowcase)target).transform;
      SlicedHull hull = objectToSlice.Slice(slicePoint.position, slicePoint.up, material);
      if (hull != null)
      {
        ((MeshSliceShowcase)target).objectToSlice = null;
        objectToSlice.SetActive(false);

        GameObject lower = hull.CreateLowerHull(objectToSlice, material);
        lower.AddComponent<MeshCollider>().convex = true;
        lower.AddComponent<Rigidbody>();

        GameObject upper = hull.CreateUpperHull(objectToSlice, material);
        upper.AddComponent<MeshCollider>().convex = true;
        upper.AddComponent<Rigidbody>();
      }
    }
  }
}
