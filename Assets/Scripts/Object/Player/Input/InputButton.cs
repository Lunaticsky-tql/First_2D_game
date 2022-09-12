using UnityEngine;

public class InputButton
{
    public KeyCode keyCode;
    public bool Down;
    public bool Up;
    public bool Held;
    
    public InputButton(KeyCode keyCode)
    {
        this.keyCode = keyCode;
    }
    public void GetInput()
    {
        Down = Input.GetKeyDown(keyCode);
        Up = Input.GetKeyUp(keyCode);
        Held = Input.GetKey(keyCode);
    }
}