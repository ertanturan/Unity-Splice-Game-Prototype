using UnityEngine;

public class Guillotine : MonoBehaviour
{
    private Blade _blade;

    private void Awake()
    {
        _blade = GetComponentInChildren<Blade>();
    }
}
