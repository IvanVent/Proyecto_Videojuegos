using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent<float,int> vida;
    public AudioSource src;
    public AudioClip sfx1,sfx2,sfx3;
    public Camera camara;
    public Rigidbody2D rb;
    public GameObject flech,arco,heart,halfheart,noheart;
    SpriteRenderer sprite_renderer;
    public Animator animator;

    private Vector3 mouseCoords;
    private Vector2 ActualSpeed;             

    [SerializeField] private float shootCooldown;
    private float maxlife=3f;
    [SerializeField]private float velocidad=5;
    [SerializeField]private float suavizado=0.1f;

    private int damage;
    int swapShootSFX=0;
    private int rooms_completed = 0;

    private bool inmortal=false;
    public bool doubleshot=false;
    private bool canShoot = true;
    private bool isDead=false;
    private bool isFacingRight = true;
    
    void Start()
    {
        vida.Invoke(maxlife,0);
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        damage=1;
    }
    
    void Update()
    {
        
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;        

        if (!isDead)
        {
            mouseCoords=camara.ScreenToWorldPoint(Input.mousePosition);
            float angulo=Mathf.Atan2(mouseCoords.y-transform.position.y,mouseCoords.x-transform.position.x);
            float anguloGrados=(180/Mathf.PI)*angulo;
            bool isMouseRight = transform.position.x < mouseCoords.x;
            Flip(isMouseRight);
            
            if((Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0)) && canShoot){
                if(swapShootSFX==0){
                    
                    src.clip=sfx2;
                    src.Play();
                    swapShootSFX=1;
                }
                else{
                    src.clip=sfx2;
                    src.Play();
                    swapShootSFX=0;
                }
                //Crear flecha y asignarle la dirección
                GameObject ArrowGO=Instantiate(flech,arco.transform.position,Quaternion.identity);
                Flecha Arrow=ArrowGO.GetComponent<Flecha>();
                Arrow.targetVector=mouseCoords-transform.position;
                //Ignorar colisiones con el jugador
                Collider2D playerCollider = GetComponent<Collider2D>();
                Collider2D arrowCollider = ArrowGO.GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(playerCollider, arrowCollider);
                //Iniciar cooldown de diparo
                StartCoroutine(ShootCooldown());
                if(doubleshot){
                    StartCoroutine(Doubleshot());
                }
            }
        }

    }

    
    //MOVIMIENTO DEL JUGADOR
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 wantedDirection = new UnityEngine.Vector2(horizontalInput, verticalInput).normalized;
        Vector2 smoothedDirection = Vector2.Lerp(ActualSpeed, wantedDirection, suavizado);
        MoveObject(smoothedDirection);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        ActualSpeed = smoothedDirection;
        
    }

    void MoveObject(Vector2 direction)
    {
        Vector2 displacement = direction * velocidad * Time.deltaTime;
        transform.Translate(displacement);

    }
    
    private void Flip(bool isMouseRight)
    {
        if (isMouseRight && !isFacingRight || !isMouseRight && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    
    /*void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Wall")) {
            //rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            //rb.velocity = Vector2.zero;
        }
      
    }*/
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Enemy") && !isDead){
          
            if(!inmortal){
                src.clip=sfx1;
                src.Play();
                vida.Invoke(0.5f, 1);
                inmortal=true;
                StartCoroutine(Invincible());
            }
        }
    }
    public void IncrementRoomsCompleted()
    {
        rooms_completed++;
    }
    public int GetRoomsCompleted()
    {
        return rooms_completed;
    }
    public void XLR8(){
        if(shootCooldown>0.3f){
            shootCooldown-=0.1f;
        }
    }
    public int getdamage(){
        return damage;
    }
    public void ishowspeed(){
        if(velocidad<2.5f){
            velocidad+=0.5f;
        }
    }
    public void moredamage(){
        if(damage<4){
            damage++;
        }
    }

    public void DeactivateShootAnim()
    {
        animator.SetBool("Shoots",false);
    }
    private IEnumerator Invincible()
    {
        
        animator.SetBool("Damage",true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Damage",false);
        inmortal=false;
    }
    public void SetDoubleshot(){
        doubleshot=true;
    }
    private IEnumerator ShootCooldown()
    {
        animator.SetBool("Shoots",true);
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
    private IEnumerator Doubleshot()
    {
        
        yield return new WaitForSeconds(0.2f);
        src.Play();
        GameObject flechadis=Instantiate(flech,arco.transform.position,Quaternion.identity);
        Flecha flecha=flechadis.GetComponent<Flecha>();
        flecha.targetVector=transform.right;
    }

    void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Enemy") && !isDead){
            
            if(!inmortal){
                src.clip=sfx1;
                src.Play();
                vida.Invoke(0.5f, 1);
                inmortal=true;
                StartCoroutine(Invincible());
            }
        }
    }

    /*void OnCollisionExit2D(Collision2D collision) {
    // Restore regular movement when leaving the wall
        if (collision.gameObject.CompareTag("Wall")) {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }*/
    
    // setter para un booleano que no permite mover el personaje cuando está muerto
    public void SetIsDead()
    {
        animator.SetBool("Dead",true);
        isDead=true;
    }

}
