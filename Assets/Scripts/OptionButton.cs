using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class OptionButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private TextMeshProUGUI text;

    public void SetButton(UnityAction action, string text)
    {
        button.onClick.AddListener(action);
        this.text.text = text;
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
        text.text = "";
    }
}
