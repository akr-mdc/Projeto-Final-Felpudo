using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float bgWidth = 20f; // largura total do sprite no mundo

    public Transform bg1;
    public Transform bg2;
    public Transform bg3;

    void Start()
    {
        bgWidth = bg1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // mover todas as imagens
        bg1.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        bg2.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        bg3.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // reposicionar quando saem da tela
        CheckAndReposition(bg1, bg2, bg3);
        CheckAndReposition(bg2, bg3, bg1);
        CheckAndReposition(bg3, bg1, bg2);
    }

    void CheckAndReposition(Transform bg, Transform right1, Transform right2)
    {
        // se bg saiu totalmente da tela para a esquerda...
        if (bg.position.x <= -bgWidth)
        {
            // reposiciona ele para a direita da imagem mais à direita
            float rightmostX = Mathf.Max(right1.position.x, right2.position.x);

            bg.position = new Vector3(
                rightmostX + bgWidth,
                bg.position.y,
                bg.position.z
            );
        }
    }
}
