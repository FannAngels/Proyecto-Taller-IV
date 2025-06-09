using TMPro;
using UnityEngine;

public class TextUI : MonoBehaviour
{
    public static TextUI Instance;
    public TextMeshProUGUI textUI;

    void Awake()
    {
        Instance = this;
        textUI.text = "";
    }

    public void ShowText(string text)
    {
        textUI.text = text;
    }

    public void HideText()
    {
        textUI.text = "";
    }
}
