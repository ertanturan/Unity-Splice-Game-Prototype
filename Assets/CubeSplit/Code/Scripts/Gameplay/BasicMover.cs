using UnityEngine;

public class BasicMover : MonoBehaviour
{

    public Axes Axis;
    [SerializeField] private float _moveSpeed = 3f;

    [SerializeField] private float _minSpeed = 2f;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _slowdownSpeed = 3f;
    private bool _isSlowingDown = false;

    private void Awake()
    {
        InputManager.Instance.Held += OnHeld;
        InputManager.Instance.MouseReleased += OnMouseReleased;
    }

    private void OnDestroy()
    {

        if (InputManager.Instance)
        {
            InputManager.Instance.Held -= OnHeld;
            InputManager.Instance.MouseReleased -= OnMouseReleased;
        }

    }

    private void Update()
    {
        if (GameManager.Instance.IsPlaying)
        {
            Move();
        }

        if (!_isSlowingDown)
        {
            if (_moveSpeed < _maxSpeed)
            {
                _moveSpeed += Time.deltaTime * _slowdownSpeed;
                ClampSpeed();

            }
        }



    }

    private void OnHeld(object sender, InputEventArgs args)
    {
        SlowDown();
    }

    private void OnMouseReleased(object sender, InputEventArgs args)
    {
        _isSlowingDown = false;
    }
    private void Move()
    {



        float coef = Time.deltaTime * _moveSpeed;
        Vector3 currentPos = transform.position;
        switch (Axis)
        {
            case Axes.X:
                currentPos.x += coef;
                transform.position = currentPos;
                break;

            case Axes.Y:
                currentPos.x += coef;
                transform.position = currentPos;
                break;

            case Axes.Z:
                currentPos.x += coef;
                transform.position = currentPos;
                break;
        }

    }

    private void SlowDown()
    {
        _isSlowingDown = true;
        _moveSpeed -= Time.deltaTime * _slowdownSpeed;
        ClampSpeed();
    }

    private void ClampSpeed()
    {
        _moveSpeed = Mathf.Clamp(_moveSpeed, _minSpeed, _maxSpeed);

    }

}
