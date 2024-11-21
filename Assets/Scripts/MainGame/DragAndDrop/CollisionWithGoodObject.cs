using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithGoodObject : MonoBehaviour
{
    private GameObject _suitableObject;
    [SerializeField] private string _suitableObjectName;
    [HideInInspector] public bool _canBeDrop = false;

    private void Start()
    {
        _suitableObject = GameObject.Find(_suitableObjectName);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject == _suitableObject)
        {
            Debug.Log("Collision With Good Object");
            _canBeDrop = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == _suitableObject)
        {
            Debug.Log("Not More Collision With Good Object");
            _canBeDrop = false;
        }
    }
}