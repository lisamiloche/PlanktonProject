using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    [SerializeField] private Collider2D _floorCollider;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if(hit.collider == _floorCollider)
            {
                _targetPosition = hit.point;
                _isMoving = true;
            }
        }

        if(_isMoving == true)
        {
            if(transform.position != _targetPosition)
                transform.position = Vector2.MoveTowards(transform.position, _targetPosition, Time.deltaTime * _speed);
            else    
                _isMoving = false;
        }

    }
}
