using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance;


    [SerializeField] private float _speed;
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    [SerializeField] private Collider2D _floorCollider;
    [SerializeField] private TriggerZoneDialog[] _zoneDialog;
    public Animator Anim;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("STOOOOOOOOOOOOOOOOOOOOP");
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _targetPosition = transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && transform.position == _targetPosition)
        {
            foreach (TriggerZoneDialog trigger in _zoneDialog)
            {
                if (!trigger._isTrigger)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                    if (hit.collider == _floorCollider)
                    {
                        _targetPosition = hit.point;
                        Vector3 direction = _targetPosition - transform.position;
                        bool Left = direction.x > 0;
                        if (Left)
                        {
                            transform.localScale = new Vector3(-1, 1, 1);
                        }
                        else
                        {
                            transform.localScale = Vector3.one;
                        }
                        _isMoving = true;
                    }
                }
            }
        }

        if (_isMoving == true)
        {
            if (transform.position != _targetPosition)
            {                
                foreach (TriggerZoneDialog trigger in _zoneDialog)
                {
                    if (!trigger._isTrigger)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, Time.deltaTime * _speed);
                        Anim.SetBool("Iswalking", true);                        
                    }
                }
            }
            else if (transform.position == _targetPosition)
            {
                _isMoving = false;
                Anim.SetBool("Iswalking", false);
            }
        }
    }
}