using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private MergeObjects _mergeObjects;

    [SerializeField] private GameObject _panneau;
    [SerializeField] private GameObject _autre;

    private GameObject _dragObject = null;
    private Vector3 PositionMouse;
    private GameObject _instance = null;
    private CollisionWithGoodObject _collisionObj;
    public bool _isDragging = false; 

    private void Update()
    {
        GetMousePosition();

        if (_mergeObjects._drag == true)
        {
            FindObjectWithType(); 
            InstantiateAnObject(_dragObject); // Faire en sorte que l'objet soit instancié PAR DESSUS l'UI !!!!!!!!
        }

        if (_instance != null)
        {
            _collisionObj = _instance.GetComponent<CollisionWithGoodObject>();

            if (_isDragging && !_collisionObj._canBeDrag) // => faire fonctionner ce putain de truc
            {
                _instance.transform.position = PositionMouse;
            }

            else if (_collisionObj._canBeDrag)
            {
                _isDragging = false;
                //faire disparaitre l'objet de l'inventaire lorsqu'il est bien posé au bon endroit
            }
        }
        else
            _collisionObj = null;
    }

    private void GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        PositionMouse = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void InstantiateAnObject(GameObject gameObject)
    { //L'objet en instance (prefab) n'a pas le script "InteractiveObject" sur lui, afin de bloquer l'inventaire pendant le DragAndDrop
        _isDragging = true;
        if (_instance == null)
        {
            _instance = Instantiate(gameObject, PositionMouse, Quaternion.identity);
        }
        else return;
    }

    private void FindObjectWithType()
    { // Lier d'autres gameObject à leur Type si neccessaire.
        if (_mergeObjects.type == TypesManager.Types.Panneau)
        {
            _dragObject = _panneau;
            Debug.Log("Panneau");
        }
        else if (_mergeObjects.type == TypesManager.Types.Autres)
        {
            _dragObject = _autre;
            Debug.Log("Autre");
        }
    }
}
