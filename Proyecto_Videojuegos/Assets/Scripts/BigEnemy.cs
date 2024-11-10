using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BigEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float hp=6;


    private Animator animator;
    private bool isFacingRight = true;
    private bool receivingDamage = false;
    private bool followPlayer = false;
    

    void Start(){
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        if (followPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            bool isPlayerRight = transform.position.x < player.position.x;
            Flip(isPlayerRight);
            animator.SetBool("Damaged", receivingDamage);
        }
    }

    private void Flip(bool isPlayerRight)
    {
        if (isPlayerRight && !isFacingRight || !isPlayerRight && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    //se hace que la hitbox del enemigo sea un trigger para que atraviese al jugador mientras que al mismo tiempo,  se destruya las flechas
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        hp--;
        receivingDamage = true;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void DeactivateDamage()
    {
        receivingDamage = false;
    }

    public void EnableFollowPlayer()
    {
        followPlayer = true;
    }
    public void DisableFollowPlayer()
    {
        followPlayer = false;
    }

}
