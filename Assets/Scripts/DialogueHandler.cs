using FM.Runtime.Systems.DialogueNodes;
using FM.Runtime.Systems.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueHandler : BaseDialogueReader
{
    [SerializeField]
    [Tooltip("Default Dialogue File")]
    private DialogueFile _defaultDialogueFile;

    [SerializeField]
    private GameSystem gameSystem;

    [Space]
    [SerializeField]
    private TextMeshProUGUI mainText;
    [SerializeField]
    private OptionButton buttonPrefab;
    [SerializeField]
    private Transform buttonRoot;

    private List<OptionButton> optionButtons = new List<OptionButton>();
    private List<string> points = new List<string>();
    public List<string> AllPoints => points;

    private bool isStarted = false;

    private DialogueBranchRuntimeNode lastDialogueBranchRuntimeNode;

    protected override void ProcessNode(RuntimeNode node)
    {
        RemoveAllButton();
        switch (node)
        {
            case DialogueLineRuntimeNode dialogueLineRuntimeNode:
                mainText.text = dialogueLineRuntimeNode.Line;
                CreateButtonWithoutBranch("Dalej");
                break;

            case DialogueBranchRuntimeNode dialogueBranchRuntimeNode:
                lastDialogueBranchRuntimeNode = dialogueBranchRuntimeNode;

                mainText.text = dialogueBranchRuntimeNode.Line;
                var i = 0;
                foreach (var branch in dialogueBranchRuntimeNode.Branches)
                {
                    CreateButtonBranch(branch, i);
                    i++;
                }

                break;

            case CallStringEventRuntimeNode callStringEventRuntimeNode:

                if (callStringEventRuntimeNode.EventValue == "END")
                {
                    if (!SaveFile.WriteDataToCSV(points, out System.Exception ex))
                    {
                        gameSystem.HandleError(ex.Message);
                        break;
                    }
                }
                else
                {
                    AddPoints(callStringEventRuntimeNode.EventValue);
                }

                // Immediately process this node
                GoToNextNode();
                break;

            case CallVoidEventRuntimeNode callVoidEventRuntimeNode:
                callVoidEventRuntimeNode.RaiseEvent();
                break;
        }
    }

    private void Start()
    {
        mainText.text = "Wciœnij spacjê aby rozpocz¹æ.";
    }

    private void Update()
    {
        if (isStarted) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogueStart();
        }
    }

    private void DialogueStart()
    {
        isStarted = true;

        if (!SaveFile.CheckIfCanWriteToFile(out System.Exception ex))
        {
            gameSystem.HandleError(ex.Message);
            return;
        }

        StartDialogue(_defaultDialogueFile);
    }

    private void SelectBranch(int id)
    {
        lastDialogueBranchRuntimeNode.SelectBranch(id);
        GoToNextNode();
    }

    private void CreateButtonWithoutBranch(string text)
    {
        var newButton = Instantiate(buttonPrefab, buttonRoot);
        newButton.SetButton(() => GoToNextNode(), text);
        optionButtons.Add(newButton);
    }

    private void CreateButtonBranch(string text, int idCall)
    {
        var newButton = Instantiate(buttonPrefab, buttonRoot);
        newButton.SetButton(() => SelectBranch(idCall), text);
        optionButtons.Add(newButton);
    }

    private void RemoveAllButton()
    {
        foreach (var button in optionButtons)
        {
            Destroy(button.gameObject);
        }

        optionButtons.Clear();
    }

    private void AddPoints(string point)
    {
        points.Add(point);
        foreach (var item in points)
        {
            Debug.Log(item);
        }
    }
}
