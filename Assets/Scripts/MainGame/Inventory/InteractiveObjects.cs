using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractiveObjects : TypesManager
{
    public Sprite Image;
    public Renderer OrderRenderer;
    private Collider2D Collider;
    [SerializeField] private LayerMask _layerMask;

    bool _isHiden = false;

    private void Awake()
    {
        OrderRenderer = GetComponent<Renderer>();
        Collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        if (OrderRenderer.sortingOrder == 0)
        {
            _isHiden = true;
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _layerMask);

        if(Input.GetMouseButtonDown(1))
        {
            if (hit.collider == Collider)
            {
                OnClick();
            }
        }
    }

    private void OnClick()
    {
        if (!_isHiden)
        {
            if (!GameManager.Instance.MGlass.gameObject.activeSelf)
            {
                if (Inventory.Instance.ItemInInventory < Inventory.Instance.InventoryBoxs.Count)
                {
                    transform.position = new Vector3 (GameManager.Instance.MainCam.transform.position.x,0,0);
                    transform.localScale = new Vector2(0.5f, 0.5f);
                    StartCoroutine(WaitToGoInventory());
                }
                else
                    Debug.Log("Can't take it.");
            }
        }
    }

    IEnumerator WaitToGoInventory()
    {
        yield return new WaitForSeconds(2f);
        foreach (var item in Inventory.Instance.InventoryBoxs)
        {
           if (!item.IsOccupied)
            {
                Inventory.Instance.ItemInInventory++;
                item.type = type;
                item.Image.sprite = Image;
                item.IsOccupied = true;
                break;
            }
        }
        Destroy(gameObject);
    }

}