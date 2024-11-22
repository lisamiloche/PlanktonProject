using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneDialog : MonoBehaviour
{
    private Collider2D _colPlayer;
    public bool _isTrigger = false;

    private void Start()
    {
        _colPlayer = GameManager.Instance.Player.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == _colPlayer)
        {
            _isTrigger = true;
        }
    }
}