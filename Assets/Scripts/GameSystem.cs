using TMPro;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI mainText;
    [SerializeField]
    private DialogueHandler dialogueHandler;

    [Space]
    [SerializeField]
    private TextMeshProUGUI errorCodeText;
    [SerializeField]
    private GameObject errorCodePanel;

    private void EnableEnd()
    {
        string text = "";
        dialogueHandler.AllPoints.ForEach(x => text += x);
        mainText.text = text;
    }

    public void HandleError(string errorMessage)
    {
        errorCodePanel.SetActive(true);
        errorCodeText.text = $"B��d podczas zapisywania do pliku CSV.\nSprawd� czy plik nie jest otwarty w innym programie!\nZrestartuj program!\n<color=white>Kod b��du - {errorMessage}</color>";
    }
}
