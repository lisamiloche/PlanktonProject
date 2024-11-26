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
    private bool _isTextAppear = false;


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
            if (i == dialog.TextDialog.Length -1)
                _isTextAppear = true;
            else
                _isTextAppear = false;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void Update()
    {
        Debug.Log(_isTextAppear);
    }

    public void OnClickNextDialog()
    {
        if(_isTextAppear)
        {
            _sequenceNumber++;
            AudioManager.Instance.PlaySFX(2);
            if (_sequenceNumber <= Dialogs.Length - 1)
            {
                StartCoroutine(UpdateDialog(Dialogs[_sequenceNumber]));
            }
            else
            {
                InProgress = false;
            }
        }
    }
}
