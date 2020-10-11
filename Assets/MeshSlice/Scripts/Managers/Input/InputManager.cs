using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshSlice
{
  public class InputManager : MonoBehaviour
  {
    public static float horizontal;

    public static float GetHorizontal()
    {
      return horizontal;
    }

    public static void SetHorizontal(float value)
    {
      horizontal = value;
    }
  }
}
