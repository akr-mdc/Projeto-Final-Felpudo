using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Level3 : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Vector3 originalScale;
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Throwing")]
    public GameObject stonePrefab;
    public Transform throwPoint;

    [Header("Player Flash")]
    public SpriteRenderer spriteRenderer;
    public float flashDuration = 0.1f;

    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        HandleMovement();
        HandleThrow();
        UpdateAnimation();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(moveX, 0f) * moveSpeed;

        // Virar Player sem alterar escala
        if (moveX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveX) * Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x, rb.velocity.y);
    }

    void UpdateAnimation()
    {
        anim.SetFloat("Speed", Mathf.Abs(movement.x));
    }

    void HandleThrow()
    {
        if (Input.GetKeyDown(KeyCode.Space) && stonePrefab != null && throwPoint != null)
        {
            Instantiate(stonePrefab, throwPoint.position, Quaternion.identity);
        }
    }

    public void TriggerFlash()
    {
        if (spriteRenderer != null)
            StartCoroutine(FlashCoroutine());
    }

    System.Collections.IEnumerator FlashCoroutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            GameManager_Level3.Instance.TakeDamage();
        }
    }
}












