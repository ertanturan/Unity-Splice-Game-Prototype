using UnityEngine;

public class Guillotine : MonoBehaviour, IPooledObject
{
    private Blade _blade;
    private bool _seenOnce = false;
    private bool _shoulCount = false;

    private void Awake()
    {
        _blade = GetComponentInChildren<Blade>();
    }

    public PooledObjectType PoolType { get; set; }
    public void Init()
    {

    }

    public void OnObjectSpawn()
    {
    }

    public void OnObjectDespawn()
    {
        _seenOnce = false;
        _shoulCount = false;
    }

    public void Despawn()
    {
        ObjectPooler.Instance.Despawn(gameObject);
    }

    private void Update()
    {
        if (!_seenOnce)
        {

            _seenOnce = GameManager.Instance.IsTargetVisible(UserCamera.Instance.MainCamera, gameObject);
            if (_seenOnce)
            {
                _shoulCount = true;
            }
        }


        if (_shoulCount)
        {
            if (!GameManager.Instance.IsTargetVisible(UserCamera.Instance.MainCamera, gameObject))
            {
                Despawn();
            }
        }
    }


}
