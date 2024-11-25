using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using static UnityEditor.Progress;
using UnityEditor.Rendering;
using UnityEngine.SceneManagement;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private MergeObjects _mergeObjects;

    [SerializeField] private GameObject _panneau;
    [SerializeField] private GameObject _autre;
    [SerializeField] private GameObject[] _boutDeBois;
    [SerializeField] private GameObject[] _vaisseau;
    [SerializeField] private GameObject[] _boulon;
    [SerializeField] private GameObject[] _affairesPerdues;
    [SerializeField] private GameObject[] _compo01;
    [SerializeField] private GameObject[] _compo02;
    [SerializeField] private GameObject[] _ferraille;
    [SerializeField] private float _speed;
    [SerializeField] private string _imageName;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Image _image;

    private GameObject _dragObject = null; 
    private Vector3 PositionMouse;
    private Vector3 mousePosition;
    private GameObject _instance = null;
    private CollisionWithGoodObject _collisionObj;
    public bool _isDragging = false;
    private bool _stopDrag = false;
    Vector3 _dragPosition;
    InteractiveObjects interactiveObjects;

    private void Update()
    {
        if (_image.sprite != null)
        {
            _imageName = _image.sprite.name;
        }
        else
            Debug.Log("Sprite is null");

        GetMousePosition();

        if (_mergeObjects._drag == true)
        {
            FindObjectWithType(); 
            InstantiateAnObject(_dragObject);
        }

        if (_instance != null)
        {
            _collisionObj = _instance.GetComponent<CollisionWithGoodObject>();

            if (_isDragging)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if(!_collisionObj._canBeDrop)
                        _stopDrag = true;
                    else
                    {
                        _isDragging = false;
                        NoMoreOccupied();
                    }
                }
            }

            if (_stopDrag)
                StopDrag();
            else if (!_stopDrag && _isDragging)
                _instance.transform.position = PositionMouse;
        }
        else
            _collisionObj = null;
    }

    private void NoMoreOccupied()
    {
        _mergeObjects.IsOccupied = false;
        _mergeObjects.Image.sprite = null;
        Inventory.Instance.ItemInInventory--;


        _gameManager._nbOfObjDropped++;
    }

    private void GetMousePosition()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        PositionMouse = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void InstantiateAnObject(GameObject instance)
    {        
        if (_instance == null && !_stopDrag && !_isDragging && Vector2.Distance(transform.position, PositionMouse) < 0.5)
        {
            _isDragging = true;
            _instance = Instantiate(instance, PositionMouse, Quaternion.identity);
            _dragPosition = _instance.transform.position;
            _stopDrag = false;
        }
        else return;
    }

    private void FindObjectWithType()
    {
        if (_mergeObjects.type == TypesManager.Types.Panneau)
            _dragObject = _panneau;
        else if (_mergeObjects.type == TypesManager.Types.BoutDeBois)
        {
            foreach(var item in _boutDeBois)
            {
                SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();

                if (spriteRenderer.sprite != null)
                {
                    if (spriteRenderer.sprite.name == _imageName)
                    {
                        _dragObject = item;
                    }
                }
                else
                    Debug.Log("Sprite is null");
            }
        }
        else if (_mergeObjects.type == TypesManager.Types.Vaisseau)
        {
            foreach (var item in _vaisseau)
            {
                SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();

                if (spriteRenderer.sprite != null)
                {
                    if (spriteRenderer.sprite.name == _imageName)
                    {
                        _dragObject = item;
                    }
                }
                else
                    Debug.Log("Sprite is null");
            }
        }
        else if (_mergeObjects.type == TypesManager.Types.Boulon)
        {
            foreach (var item in _boulon)
            {
                SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();

                if (spriteRenderer.sprite != null)
                {
                    if (spriteRenderer.sprite.name == _imageName)
                    {
                        _dragObject = item;
                    }
                }
                else
                    Debug.Log("Sprite is null");
            }
        }
        else if (_mergeObjects.type == TypesManager.Types.AffairesPerdues)
        {
            foreach (var item in _affairesPerdues)
            {
                SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();

                if (spriteRenderer.sprite != null)
                {
                    if (spriteRenderer.sprite.name == _imageName)
                    {
                        _dragObject = item;
                    }
                }
                else
                    Debug.Log("Sprite is null");
            }
        }
        else if (_mergeObjects.type == TypesManager.Types.Compo01)
        {
            foreach (var item in _compo01)
            {
                SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();

                if (spriteRenderer.sprite != null)
                {
                    if (spriteRenderer.sprite.name == _imageName)
                    {
                        _dragObject = item;
                    }
                }
                else
                    Debug.Log("Sprite is null");
            }
        }
        else if (_mergeObjects.type == TypesManager.Types.Compo02)
        {
            foreach (var item in _compo02)
            {
                SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();

                if (spriteRenderer.sprite != null)
                {
                    if (spriteRenderer.sprite.name == _imageName)
                    {
                        _dragObject = item;
                    }
                }
                else
                    Debug.Log("Sprite is null");
            }
        }
        else if (_mergeObjects.type == TypesManager.Types.Ferraille)
        {
            foreach (var item in _ferraille)
            {
                SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();

                if (spriteRenderer.sprite != null)
                {
                    if (spriteRenderer.sprite.name == _imageName)
                    {
                        _dragObject = item;
                    }
                }
                else
                    Debug.Log("Sprite is null");
            }
        }
        else if (_mergeObjects.type == TypesManager.Types.Autres)
            _dragObject = _autre;
    }
    
    private void StopDrag()
    {
        _instance.transform.position = Vector2.MoveTowards(_instance.transform.position, _dragPosition, _speed * Time.deltaTime);
        Debug.Log(Vector2.Distance(_instance.transform.position, _dragPosition));

        if (new Vector2(_instance.transform.position.x, _instance.transform.position.y) == new Vector2(_dragPosition.x, _dragPosition.y))      
        {
            Debug.Log("Je suis là");
            Destroy(_instance);
            _stopDrag = false; _isDragging = false; _mergeObjects._drag = false;
        }
    }
}
