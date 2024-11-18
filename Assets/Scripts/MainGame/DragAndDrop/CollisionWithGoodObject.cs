using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithGoodObject : MonoBehaviour
{
    private GameObject _suitableObject;
    [SerializeField] private string _suitableObjectName;
    [HideInInspector] public bool _canBeDrag = false;

    private void Start()
    {
        _suitableObject = GameObject.Find(_suitableObjectName);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == _suitableObject)
        {
            Debug.Log("Collision With Good Object");
            _canBeDrag = true;
        }
    }
}