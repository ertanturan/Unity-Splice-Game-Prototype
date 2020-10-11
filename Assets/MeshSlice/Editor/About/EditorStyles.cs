using UnityEngine;

namespace MeshSlice
{
  public class EditorStyles
  {
    public static GUIStyle AboutAssetName
    {
      get
      {
        GUIStyle style = new GUIStyle();
        style.fontSize = 22;
        style.alignment = TextAnchor.MiddleCenter;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = new Color32(50, 55, 60, 255);
        return style;
      }
    }

    public static GUIStyle AboutLabel
    {
      get
      {
        GUIStyle style = new GUIStyle();
        style.fontSize = 13;
        style.fontStyle = FontStyle.Bold;
        return style;
      }
    }

    public static GUIStyle AboutLink
    {
      get
      {
        GUIStyle style = new GUIStyle();
        style.fontStyle = FontStyle.Bold;
        style.fontSize = 13;
        style.normal.textColor = new Color32(0, 70, 255, 255);
        return style;
      }
    }
  }
}
