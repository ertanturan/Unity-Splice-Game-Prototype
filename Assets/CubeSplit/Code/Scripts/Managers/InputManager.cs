using UnityEngine;

public class InputManager : SceneSingleton<InputManager>
{


    public delegate void OnPressedEventHandler(object sender, InputEventArgs args);
    public event OnPressedEventHandler Pressed;


    public delegate void OnHeldEventHandler(object sender, InputEventArgs args);
    public event OnHeldEventHandler Held;

    public delegate void OnMouseUpEventHandler(object sender, InputEventArgs args);

    public event OnMouseUpEventHandler MouseReleased;

    private void Update()
    {

        CheckHeld();
        CheckPressed();
        CheckMouseUp();

    }

    private void CheckPressed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPressed();
        }
    }

    private void CheckHeld()
    {
        if (Input.GetMouseButton(0))
        {
            OnHeld();
        }
    }

    private void CheckMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseReleased();
        }
    }

    private void OnPressed()
    {
        if (Pressed != null)
        {
            Pressed(this,
                new InputEventArgs()
            );
        }
    }


    private void OnHeld()
    {
        if (Held != null)
        {
            Held(this, new InputEventArgs());
        }
    }


    private void OnMouseReleased()
    {
        if (MouseReleased != null)
        {
            MouseReleased(this, new InputEventArgs());
        }
    }
}
