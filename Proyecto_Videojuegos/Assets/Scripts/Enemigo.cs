using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float moveDuration;
    [SerializeField] private float cooldownParada;
    [SerializeField] private float hp=3;
    public Playerintro playera; 
    private Animator animator;
    private Rigidbody2D rb;

    private bool isFacingRight = true;
    private bool isWaiting = false;
    private bool receivingDamage = false;
    private bool followPlayer = true;
    private bool si=false;
    public AudioClip sfx1,sfx2;
    public AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(MoveAndWait());
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (si)
        {
            if(!isWaiting)
            {
                Vector2 previousPosition = transform.position;
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                bool isMoving = Vector2.Distance(previousPosition, transform.position) > 0.01f;
                animator.SetBool("IsMoving", isMoving);
            }else
            {
                animator.SetBool("IsMoving", false);
            }
            bool isPlayerRight = transform.position.x < player.position.x;
            Flip(isPlayerRight);
            animator.SetBool("Damaged", receivingDamage);
        }else
        {
            animator.SetBool("IsMoving", false);
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
    private IEnumerator MoveAndWait()
    {
        while (si)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Playerintro"))
        {
            print("aaaaaaaaaaaaaaa");
            si=false;
            playera.Cae();
            

        }
    }
    public void actua(){
        si=true;
    }
    
}
