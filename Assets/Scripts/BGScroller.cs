using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float bgWidth = 20f; // largura total do sprite no mundo

    public Transform bg1;
    public Transform bg2;

    void Start()
    {
        bgWidth = bg1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // mover os dois
        bg1.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        bg2.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // quando um sai completamente da tela...
        if (bg1.position.x <= -bgWidth)
        {
            bg1.position = new Vector3(bg2.position.x + bgWidth, bg1.position.y, bg1.position.z);
        }

        if (bg2.position.x <= -bgWidth)
        {
            bg2.position = new Vector3(bg1.position.x + bgWidth, bg2.position.y, bg2.position.z);
        }
    }
}
