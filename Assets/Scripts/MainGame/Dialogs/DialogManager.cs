using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class DialogManager : MonoBehaviour
{
    [Header("Références Obj")]
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _character;
    [SerializeField] private Collider2D _charaCol;
    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private GameObject[] _dialogsList;
    [SerializeField] private Transform _camera;
    [SerializeField] private LayerMask _layerMask;

    [Header("Outline")]
    [SerializeField] private Material _material;
    [SerializeField] private SpriteRenderer _sprRenderer;
    private Material _mat;

    [Header("Blur Effect")]
    [SerializeField] private GameObject _blur;

    [Header("AnimDialog")]
    private Vector3 _transitionSpot01;
    private Vector3 _transitionSpot02;
    private Vector3 _finalSpot01;
    private Vector3 _finalSpot02;
    [SerializeField] private float _speed;
    [SerializeField] private Dialog _dialog;
    private bool _isLaunched = false;
    private bool _isFinished = false;
    public bool _isInDialog = false;

    private Vector3 _lastPosition;
    private Vector3 _playerPos;
    private Vector3 _characterPos;
    private bool _isPlayerLeft = false;
    private Vector3 _playerScale;
    private Vector3 _characterScale;
    [SerializeField] private CameraManager[] _cameraManager;

    private void Start()
    {
        _mat = _sprRenderer.material;
        _dialogBox.SetActive(false);
        _blur.SetActive(false);
        _dialog = GetComponent<Dialog>();
        _lastPosition = _player.position;
        _playerScale = _player.localScale;
        _characterScale = _character.localScale;
    }

    private void Update()
    {
        if (_isFinished == false) //Corriger ce truc moche
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _layerMask);

            if (hit.collider == _charaCol && !_isLaunched && _dialog.InProgress && Vector3.Distance(_player.position, _lastPosition) < 0.01f)
            {
                _sprRenderer.material = _material;

                if (Input.GetMouseButtonDown(1))
                {
                    _blur.SetActive(true);

                    foreach (var other in _dialogsList)
                        other.SetActive(false);

                    _playerPos = _player.position;
                    _characterPos = _character.position;

                    _isLaunched = true;
                    _isInDialog = true;
                    

                    if(Vector2.Distance(_player.position, _transitionSpot01) < Vector2.Distance(_character.position, _transitionSpot01))
                    {
                        Placement(_transitionSpot01, _transitionSpot02, true);
                    }
                    else
                    {
                        Placement(_transitionSpot02, _transitionSpot01, false);
                    }
                }
            }
            else
                _sprRenderer.material = _mat;
                

            if (_isLaunched)
            {                
                AnimationDialog();
            }

            if (!_dialog.InProgress)
            {
                _isLaunched = false;
                AnimationReturn();
            }

            _lastPosition = _player.position;
        }
        else
        {
            foreach (var other in _dialogsList)
                other.SetActive(true);
            //Jouer le son pour comprendre que l'on ne peut plus dialoguer
        }
        if (!_isInDialog)
        {
            foreach(var camera in _cameraManager)
            {
                camera._isMoving = true;
            }
        }

        _transitionSpot01 = new Vector3(_camera.position.x - 12, _camera.position.y, 0);
        _transitionSpot02 = new Vector3(_camera.position.x + 12, _camera.position.y, 0);
        _finalSpot01 = new Vector3((_camera.position.x - 3), _camera.position.y, 0);
        _finalSpot02 = new Vector3((_camera.position.x + 3), _camera.position.y, 0);
    }

    private void Placement(Vector3 spot01, Vector3 spot02, bool _isLeft)
    {
        _player.position = spot01; _player.localScale += new Vector3(2, 2, 2);
        _character.position = spot02; _character.localScale += new Vector3(2, 2, 2);
        _isPlayerLeft = _isLeft;
    }

    private void GetBack(Vector2 spot01, Vector2 spot2)
    {
        _player.position = Vector3.MoveTowards(_player.position, spot01, _speed * Time.deltaTime);
        _character.position = Vector3.MoveTowards(_character.position, spot2, _speed * Time.deltaTime);
    }

    private void Finished()
    {
        _blur.SetActive(false);
        _player.position = _playerPos; _player.localScale = _playerScale;
        _character.position = _characterPos; _character.localScale = _characterScale;
        _isFinished = true;
        _isInDialog = false;
    }

    void AnimationDialog()
    {
        if (_isPlayerLeft)
        {
            DirectionAndLaunch(_finalSpot01, _finalSpot02);
        }
        else
        {
            DirectionAndLaunch(_finalSpot02, _finalSpot01);
        }
    }

    private void DirectionAndLaunch(Vector3 spot01, Vector3 spot02)
    {
        if (_player.position != spot01 || _character.position != spot02)
        {
            _player.position = Vector3.MoveTowards(_player.position, spot01, _speed * Time.deltaTime);
            _character.position = Vector3.MoveTowards(_character.position, spot02, _speed * Time.deltaTime);
        }
        else
        {
            _dialogBox.SetActive(true);

            if (_dialog.TextNameCharacter.text == _player.name)
            { //Si nécessaire possibilité de scale -1 pour retourner la box si jamais il y a un sens particulier
                if(_isPlayerLeft)
                    _dialogBox.transform.position = new Vector2(_player.position.x+2, _player.position.y+0.5f);
                else
                    _dialogBox.transform.position = new Vector2(_player.position.x-2, _player.position.y + 0.5f);
            }
            else if (_dialog.TextNameCharacter.text == _character.name)
            {
                if(_isPlayerLeft)
                    _dialogBox.transform.position = new Vector2(_character.position.x-2, _character.position.y+0.5f);
                else
                    _dialogBox.transform.position = new Vector2(_character.position.x+2, _character.position.y + 0.5f);
            }
        }  
    }

    void AnimationReturn()
    {
        _dialogBox.SetActive(false);
        if(_isPlayerLeft)
            GetBack(_transitionSpot01, _transitionSpot02);
        else
            GetBack(_transitionSpot02, _transitionSpot01);

        if ((_player.position == _transitionSpot02 && _character.position == _transitionSpot01) || (_player.position == _transitionSpot01 && _character.position == _transitionSpot02))
            Finished(); 
    }
}