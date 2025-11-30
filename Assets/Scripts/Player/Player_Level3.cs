using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Level3 : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector3 originalScale;

[Header("Animator")]
    public Animator anim;

    [Header("Stone Throwing")]
    public GameObject stonePrefab;
    public Transform throwPoint;
    public float throwForce = 10f;

    [Header("Damage Flash")]
    public SpriteRenderer spriteRenderer;
    public float flashDuration = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        HandleMovement();
        HandleThrow();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(h, 0f);

        // Atualiza Animator
        anim.SetFloat("Speed", Mathf.Abs(h));

        // Flip player sem alterar escala absoluta
        if (h > 0f) transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (h < 0f) transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
    }

    void HandleThrow()
    {
        if (Input.GetKeyDown(KeyCode.Space) && stonePrefab != null)
        {
            GameObject stone = Instantiate(stonePrefab, throwPoint.position, Quaternion.identity);
            Rigidbody2D stoneRb = stone.GetComponent<Rigidbody2D>();
            if (stoneRb != null)
            {
                // Lança para cima (pode ser ajustado para diagonal, se quiser)
                stoneRb.velocity = Vector2.up * throwForce;
            }
        }
    }

    public void TriggerFlash()
    {
        if (spriteRenderer != null)
            StartCoroutine(FlashCoroutine());
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Recebe dano de estalactites
        if (collision.gameObject.CompareTag("Stalactite"))
        {
            if (GameManager_Level3.Instance != null)
            {
                GameManager_Level3.Instance.TakeDamage();
            }
            Destroy(collision.gameObject);
        }
    }

}














