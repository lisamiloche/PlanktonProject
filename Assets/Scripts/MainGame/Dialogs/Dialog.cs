using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [Header("References")]
    public TMP_Text TextDialog;
    public TMP_Text TextNameCharacter;
    public TMP_Text TextButton;
    public DialogSequence[] Dialogs;

    [Header("Autres variables")]
    private int _sequenceNumber = 0;
    [HideInInspector] public bool InProgress = true;

    // Voir pourquoi erreur au démarrage des dialogues
    // Faire en sorte que le texte s'écrive au fur et à mesure

    private void Start()
    {
        UpdateDialog(Dialogs[_sequenceNumber]);
    }

    void UpdateDialog(DialogSequence dialog)
    {
        TextDialog.text = dialog.TextDialog;
        TextNameCharacter.text = dialog.TextNameCharacter;
        TextButton.text = dialog.TextButton;
    }

    public void OnClickNextDialog() 
    {
        _sequenceNumber++;
        if (_sequenceNumber <= Dialogs.Length-1)
        {
            UpdateDialog(Dialogs[_sequenceNumber]);
        }
        else
        {
            InProgress = false;
        }
    }
}
