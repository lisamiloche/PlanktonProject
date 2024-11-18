using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractiveObjects : TypesManager
{
    public Sprite Image;
    public Renderer OrderRenderer;

    bool _isHiden = false;

    void Start()
    {
        OrderRenderer = GetComponent<Renderer>();

        if (OrderRenderer.sortingOrder == 0)
        {
            _isHiden = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {
        if (!_isHiden)
        {
            if (!GameManager.Instance.MGlass.gameObject.activeSelf)
            {
                if (Inventory.Instance.ItemInInventory < Inventory.Instance.InventoryBoxs.Count)
                {
                    transform.position = Vector3.zero;
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
