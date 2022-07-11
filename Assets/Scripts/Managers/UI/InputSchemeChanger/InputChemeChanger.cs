using TMPro;
using UnityEngine;

public class InputChemeChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentSchemeText;
    [SerializeField] private InputSystem inputSystem;

    private void Start()
    {
        SetUiText(inputSystem.GetCurrentScheme());
    }

    private void SetUiText(InputScheme newScheme)
    {
        if(newScheme == InputScheme.Keyboard) 
            SetText("Клавиатура");
        else
            SetText("Клавиатура + мышь");
    }

    private void SetText(string newText)
    {
        currentSchemeText.text = newText;
    }

    public void ChangeCurrentScheme()
    {
        var newScheme = inputSystem.GetCurrentScheme() == InputScheme.Keyboard ? InputScheme.KeyboardAndMouse : InputScheme.Keyboard;
        inputSystem.SetCurrentScheme(newScheme);
        SetUiText(newScheme);
    }
}
