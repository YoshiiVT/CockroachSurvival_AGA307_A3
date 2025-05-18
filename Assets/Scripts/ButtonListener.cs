using UnityEngine;
using UnityEngine.UI;

public class ButtonListener : GameBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string buttonCommand;

    private void Start()
    {
        button.onClick.AddListener(() => _BM.ButtonActivation(buttonCommand));
    }
}
