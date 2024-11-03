using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float moveDuration;
    [SerializeField] private float cooldownParada;
    [SerializeField] private float hp=3;

    private Animator animator;

    private bool isFacingRight = true;
    private bool isWaiting = false;
    private bool receivingDamage = false;

    void Start(){
        animator = GetComponent<Animator>();
        StartCoroutine(MoveAndWait());
    }
    void Update()
    {
        if(!isWaiting)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        bool isPlayerRight = transform.position.x < player.position.x;
        Flip(isPlayerRight);
        animator.SetBool("Damaged", receivingDamage);
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

    private IEnumerator MoveAndWait()
    {
        while (true)
        {
            //Mientras isWaiting es false, el enemigo se mueve al jugador durante "moveDuration" segundos
            isWaiting = false;
            yield return new WaitForSeconds(moveDuration);

            //Despues de que el enemigo se mueva "moveDuration" segundos, isWaiting se vuelve true 
            //y el enemigo se detiene durante "cooldownParada" segundos
            isWaiting = true;
            yield return new WaitForSeconds(cooldownParada);
        }
    }

    //se hace que la hitbox del enemigo sea un trigger para que atraviese al jugador mientras que al mismo tiempo,  se destruya las flechas
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            hp--;
            receivingDamage = true;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void DeactivateDamage()
    {
        receivingDamage = false;
    }
    
   }

