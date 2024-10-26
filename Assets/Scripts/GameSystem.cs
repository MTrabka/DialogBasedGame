using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Button codeButton;
    [SerializeField] private TMP_InputField codeField;
    
    
    private bool codeIsSaved = false;

    private void Start()
    {
        mainText.text = "Podaj kod osoby badanej:";
    }

    public void OnClickCodeButton()
    {
        var code = codeField.text;
        if(string.IsNullOrEmpty(code)) return;
        PlayerPrefs.SetString(PlayerPrefsSaveKeys.CODE, code);
        CodeIsSaved();
        codeButton.gameObject.SetActive(false);
        codeField.gameObject.SetActive(false);
    }
    
    private void CodeIsSaved()
    {
        codeIsSaved = true;
        mainText.text = "Wciśnij spację, aby rozpocząć";
    }

    private void Update()
    {
        if (dialogueHandler.IsStarted) return;
        if (!codeIsSaved) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogueHandler.DialogueStart();
            
        }
    }
    
    private void EnableEnd()
    {
        string text = "";
        dialogueHandler.AllPoints.ForEach(x => text += x);
        mainText.text = text;
    }

    public void HandleError(string errorMessage)
    {
        errorCodePanel.SetActive(true);
        errorCodeText.text = $"Błąd podczas zapisywania do pliku CSV.\nSprawdź czy plik nie jest otwarty w innym programie!\nZrestartuj program!\n<color=white>Kod błędu - {errorMessage}</color>";
    }
}
