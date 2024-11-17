using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("Références Obj")]
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _character;
    [SerializeField] private Collider2D _charaCol;

    [SerializeField] private GameObject _dialogBox; //ajouter une deuxième bulle pour le deuxième perso

    [Header("Outline")]
    [SerializeField] private Material _material;
    [SerializeField] private SpriteRenderer _sprRenderer;
    private Material _mat;

    [Header("Blur Effect")]
    [SerializeField] private GameObject _blur;

    [Header("AnimDialog")]
    [SerializeField] private Transform _transitionSpot01;
    [SerializeField] private Transform _transitionSpot02;
    [SerializeField] private Transform _finalSpot01;
    [SerializeField] private Transform _finalSpot02;
    [SerializeField] private float _speed;
    [SerializeField] private Dialog _dialog;
    private bool _isLaunched = false;
    private bool _isFinished = false;


    private Vector3 _lastPosition;
    private float _timeThreshold = 0.1f;
    private float _timeSinceLastMove;
    private Vector3 _playerPos;
    private Vector3 _characterPos;


    private void Start()
    {
        _mat = _sprRenderer.material;
        _dialogBox.SetActive(false);
        _blur.SetActive(false);
        _dialog = GetComponent<Dialog>();
        _lastPosition = _player.position;
        _timeSinceLastMove = 0f;
    }

    private void Update()
    {
        if (_isFinished == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider == _charaCol && !_isLaunched && _dialog.InProgress && Vector3.Distance(_player.position, _lastPosition) < 0.01f)
            {
                _sprRenderer.material = _material;

                if (Input.GetMouseButtonDown(1))
                {
                    _playerPos = _player.position;
                    _characterPos = _character.position;

                    _isLaunched = true;
                    _blur.SetActive(true); // Voir pour que le blur soit que sur le background

                    // calculer : si on est à droite du perso on est déplacé à droite sinon à gauche (+ scale pour tourner sprite dans le bon sens)
                    _player.position = _transitionSpot01.position; _player.localScale += new Vector3(2, 2, 2);
                    _character.position = _transitionSpot02.position; _character.localScale += new Vector3(2, 2, 2);
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
            // code pour jouer le son pour comprendre que l'on ne peut plus dialoguer
        }

    }

    void AnimationDialog()
    {
        if(_player.position != _finalSpot01.transform.position || _character.position != _finalSpot02.transform.position)
        {
            _player.position = Vector3.MoveTowards(_player.position, _finalSpot01.transform.position, _speed * Time.deltaTime);
            _character.position = Vector3.MoveTowards(_character.position, _finalSpot02.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            _dialogBox.SetActive(true);
        }
    }

    void AnimationReturn()
    {
        _dialogBox.SetActive(false);
        _player.position = Vector3.MoveTowards(_player.position, _transitionSpot01.position, _speed * Time.deltaTime);
        _character.position = Vector3.MoveTowards(_character.position, _transitionSpot02.position, _speed * Time.deltaTime);

        if(_player.position == _transitionSpot01.position && _character.position == _transitionSpot02.position)
        {
            _blur.SetActive(false);
            _player.position = _playerPos; _player.localScale = Vector3.one;
            _character.position = _characterPos; _character.localScale = Vector3.one;
            _isFinished = true;
        }
    }
}

//FAIT// Outline apparait quand on passe le curseur sur le personnage 
//FAIT// On clique dessus (clic droit) pour engager le dialogue 
//FAIT// Le bg devient flou
//FAIT// Les persos se TP hors de l'écran et se scale ++
//FAIT// Déplacement des personnages qui reviennent au centre de l'image (linéaire)
//FAIT// Gestion des dialogues
//FAIT// Apparition et disparition dialogues
//FAIT// Déplacement des personnages en sens inverse
//FAIT// Fond d'écran plus flou
//FAIT// Réapparition des persos comme ils étaient 
//FAIT// Si on a déjà dialogué, on ne peut plus le faire / ajouter son pour comprendre

// test => Voir pour que ça fonctionne avec plusieurs dialogues/persos différents
