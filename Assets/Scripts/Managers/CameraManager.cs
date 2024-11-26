using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _inventory;
    [SerializeField] private Transform _boxDialog;
    [SerializeField] private Vector2 _minBounds;
    [SerializeField] private Vector2 _maxBounds;
    [SerializeField] private DialogManager[] _dialogManagers;

    private Camera _camera;
    private float _cameraWidth;
    public bool _isMoving = true;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _cameraWidth = _camera.orthographicSize * _camera.aspect;
    }

    private void Update()
    {
        foreach (DialogManager dialogManager in _dialogManagers)
        {
            Debug.Log(dialogManager._isInDialog);

            if (dialogManager._isInDialog)
                _isMoving = false;
        }

        if(_isMoving)
        {
            Vector3 targetPos = _player.transform.position;
            transform.position = new Vector3(targetPos.x, transform.position.y, -1);

            float clampedX = Mathf.Clamp(targetPos.x, _minBounds.x + _cameraWidth, _maxBounds.x - _cameraWidth);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

            if(_boxDialog != null)
            {
                _boxDialog.transform.position = new Vector3(transform.position.x, _boxDialog.transform.position.y, _boxDialog.transform.position.z);
            }

            _inventory.transform.position = new Vector3(transform.position.x, _inventory.transform.position.y, _inventory.transform.position.z);
        }
    }
}