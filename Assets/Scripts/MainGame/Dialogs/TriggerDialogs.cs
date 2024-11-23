using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogs : MonoBehaviour
{

    [SerializeField] private TriggerZoneDialog _zoneDialog;
    [SerializeField] private GameObject _blur;
    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private GameObject _player;
    private Dialog _dialog;

    private void Start()
    {
        _dialogBox.SetActive(false);
        _dialog = GetComponent<Dialog>();
    }

    void Update()
    {
        if (_zoneDialog != null && _zoneDialog._isTrigger == true)
        {
            _dialogBox.SetActive(true);
            _blur.SetActive(true);
        }
        else
        {
            Desactived();
        }
        if (_dialog.InProgress == false)
            Desactived();

    }

    private void Desactived()
    {
        _blur.SetActive(false);
        _dialogBox.SetActive(false);
        _zoneDialog._isTrigger = false;
    }
}
