using UnityEngine;

public class InputManager : SceneSingleton<InputManager>
{


    public delegate void OnPressedEventHandler(object sender, InputEventArgs args);
    public event OnPressedEventHandler Pressed;


    public delegate void OnHeldEventHandler(object sender, InputEventArgs args);
    public event OnHeldEventHandler Held;


    private void Update()
    {

        CheckHeld();
        CheckPressed();

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

}
