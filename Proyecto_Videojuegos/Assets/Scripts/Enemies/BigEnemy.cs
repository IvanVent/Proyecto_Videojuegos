using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BigEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float hp=6;

    private Player playera; 
    private Animator animator;
    private GameProgress game_progress;

    private bool isFacingRight = true;
    private bool receivingDamage = false;
    private bool followPlayer = false;
    
    public AudioClip sfx1,sfx2;
    private AudioSource src;
    void Start(){
        src=GameObject.Find("Enemydamagesound").GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        playera = GameObject.Find("Player").GetComponent<Player>();
        game_progress = GameObject.Find("GameManager").GetComponent<GameProgress>();
    }
    void Update()
    {
        if (followPlayer)
        {
            Vector2 previousPosition = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            bool isMoving = Vector2.Distance(previousPosition, transform.position) > 0.01f;
            animator.SetBool("IsMoving", isMoving);

            bool isPlayerRight = transform.position.x < player.position.x;
            Flip(isPlayerRight);
            animator.SetBool("Damaged", receivingDamage);
        }else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    //GETTER Y SETTERS

    public float getHP()
    {
        return hp;
    }
    public void setHP(float hp)
    {
        this.hp = hp;
    }
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
    
    //FIN GETTER Y SETTERS
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
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Attack", true);
        }
    }

    public void TakeDamage()
    {
        hp-=playera.getDamage();
        receivingDamage = true;
        if (hp <= 0)
        {
            src.clip=sfx2;
            src.Play();
            Destroy(gameObject);
            game_progress.EnemyKilled();
        }
        else{
            src.clip=sfx1;
            src.Play();
        }
    }
    public void DeactivateDamage()
    {
        receivingDamage = false;
    }

    public void DeactivateAttack()
    {
        animator.SetBool("Attack", false);
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
