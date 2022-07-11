using UnityEngine;

public enum InputScheme
{
    Keyboard =0,
    KeyboardAndMouse =1,
}

public class InputSystem : MonoBehaviour
{
    [SerializeField] private bool KeyboardScheme;
    [SerializeField] private bool KeyboardAndMouseScheme;

    public bool Moving { get; private set; }
    public int  Rotating { get; private set; }
    public bool Shooting { get; private set; }
    public bool Pause { get;private set; }

    private void Start()
    {
        KeyboardAndMouseScheme = !KeyboardScheme;
    }

    private void Update()
    {
        if (KeyboardScheme)
        {
            KeyboardSchemeInputs();
        }
        else if (KeyboardAndMouseScheme)
        {
            KeyboardAndMouseInputs();
        }
        HandlePause();
    }

    private void KeyboardSchemeInputs()
    {
        Moving = MoveForwardKeyboardInput();
        Shooting = ShootKeyboradInput();
        Rotating = RotateKeyboardInput();
    }

    private void KeyboardAndMouseInputs()
    {
        Moving = MoveForwardKeyboardInput() || Input.GetKey(KeyCode.Mouse1);
        Shooting = ShootKeyboradInput() || Input.GetKeyDown(KeyCode.Mouse0);
    }

    private void HandlePause()
    {
        Pause = Input.GetKeyUp(KeyCode.Escape);
    }

    private bool MoveForwardKeyboardInput()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    }

    private bool ShootKeyboradInput()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private int RotateKeyboardInput()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            return 1;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            return -1;
        }
        return 0;
    }

    public InputScheme GetCurrentScheme()
    {
        return KeyboardScheme ? InputScheme.Keyboard : InputScheme.KeyboardAndMouse;
    }

    public void SetCurrentScheme(InputScheme scheme)
    {
        if(scheme == InputScheme.Keyboard)
            SetScheme(scheme);
        else
            SetScheme(InputScheme.KeyboardAndMouse); 
    }

    private void SetScheme(InputScheme scheme)
    {
        var newScheme = scheme == InputScheme.Keyboard ? true : false;
        KeyboardScheme = newScheme;
        KeyboardAndMouseScheme = !newScheme;
    }
}
