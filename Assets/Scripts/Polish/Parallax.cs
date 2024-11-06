using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] bool scrollLeft;

    float singleTextureWidth;
    bool isOkayToScroll = false;

    void Start()
    {
        SetUpTexture();
        if (scrollLeft)
            moveSpeed = -moveSpeed;
    }

    void SetUpTexture()
    { 
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTextureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    void Scroll()
    {
        float delta = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(delta, 0f, 0f);
    }

    void CheckReset()
    {
        if ((Mathf.Abs(transform.position.x) - singleTextureWidth) > 0)
        {
            transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);
        }
    }

    void Update()
    {
        if (GameObject.FindWithTag("Spell") == null)
        {
            isOkayToScroll = true;
        }
        else
        {
            isOkayToScroll = false;
        }


        if (isOkayToScroll == true)
        {
            Scroll();
            CheckReset();
        }
    }


}
