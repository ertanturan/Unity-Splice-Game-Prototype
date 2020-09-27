using UnityEngine;

[RequireComponent(typeof(Camera))]
public class UserCamera : SceneSingleton<UserCamera>
{
    private Camera _mainCamera;

    public Camera MainCamera
    {
        get { return _mainCamera; }
    }

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
    }

}
