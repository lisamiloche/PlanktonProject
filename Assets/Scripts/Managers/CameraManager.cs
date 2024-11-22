using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _marge;
    [SerializeField] private float _speed;
   
    private bool _isLeft;
    private bool _isRight;
    private Vector3 Playerpos;

    private void Start()
    {
        Playerpos = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        float screenPosX = gameObject.GetComponent<Camera>().WorldToScreenPoint(_player.position).x; 
        float screenWidth = Screen.width;  

        if (screenPosX < _marge)
        {
            _isLeft = true;

        }
        else if (screenPosX > screenWidth - _marge)
        {
            _isRight = true;
        }

        if (_isLeft)
        {
            transform.position = Vector3.MoveTowards(transform.position, Playerpos, _speed * Time.deltaTime);
            if (transform.position == Playerpos)
            {
                _isLeft = false;
            }
        }

        if (_isRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, Playerpos, _speed * Time.deltaTime);
            if (transform.position == Playerpos)
            {
                _isRight = false;
            }
        }
    }
}