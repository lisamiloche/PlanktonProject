using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractiveObjects : MonoBehaviour
{
    public Sprite Sprite;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnMouseDown()
    {

        transform.position = Vector3.zero;
        transform.localScale = new Vector2(0.5f, 0.5f);
        StartCoroutine(WaitToGoInventory());       
    }

    IEnumerator WaitToGoInventory()
    {
        yield return new WaitForSeconds(2f);
        foreach (var item in Inventory.Instance.InventoryBoxs)
        {
            if (item.sprite == null)
            {
                item.sprite = Sprite;
                break;
            }
        }
        Destroy(gameObject);
    }

}
