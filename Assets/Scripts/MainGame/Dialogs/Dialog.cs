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
    public bool InProgress = true;
    //public bool _boxIsActive = false;

    // Faire en sorte que le texte s'écrive au fur et à mesure aussi pour le premier dialogue.

    private void Start()
    {
        if (Dialogs.Length > 0)
            StartCoroutine(UpdateDialog(Dialogs[_sequenceNumber]));
    }

    private IEnumerator UpdateDialog(DialogSequence dialog)
    {
        TextNameCharacter.text = dialog.TextNameCharacter;
        TextButton.text = dialog.TextButton;

        TextDialog.text = "";

        for (int i = 0; i < dialog.TextDialog.Length; i++)
        {
            TextDialog.text += dialog.TextDialog[i];
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void OnClickNextDialog()
    {
        _sequenceNumber++;
        if (_sequenceNumber <= Dialogs.Length-1)
        {
            StartCoroutine(UpdateDialog(Dialogs[_sequenceNumber]));
        }
        else
        {
            InProgress = false;
        }
    }
}
