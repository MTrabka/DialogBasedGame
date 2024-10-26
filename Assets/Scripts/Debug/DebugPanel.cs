using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonsPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !buttonsPanel.activeInHierarchy)
        {
            buttonsPanel.SetActive(true);
        }
    }

    public void CloseButtonsPanel()
    {
        buttonsPanel.SetActive(false);
    }

    public void RestartProgram()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseProgram()
    {
        Application.Quit(0);
    }
}
