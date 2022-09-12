using UnityEngine;
using UnityEngine.PlayerLoop;


public class InputAxis
{
    public KeyCode key1;
    public KeyCode key2;
    public int value;

    public InputAxis(KeyCode key1, KeyCode key2)
    {
        this.key1 = key1;
        this.key2 = key2;
    }

    public void GetInput()
    {
        if (Input.GetKey(key1))
        {
            value = -1;
            return;
        }

        if (Input.GetKey(key2))
        {
            value = 1;
            return;
        }

        value = 0;
    }
}