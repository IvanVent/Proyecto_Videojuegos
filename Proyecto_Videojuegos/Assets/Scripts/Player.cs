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
    
    [SerializeField] private float shootCooldown=0.6f;
    private float maxlife=3f;
    [SerializeField]private float velocidad=5;

    private int damage;
    int swapShootSFX=0;

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
        if (Time.timeScale == 0) return;
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
                Shoot();
                //Iniciar cooldown de diparo
                StartCoroutine(ShootCooldown());
                if(doubleshot){
                    StartCoroutine(Doubleshot());
                }
            }
        }

    }

    private void FixedUpdate() {
  
        if (!isDead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(horizontal, vertical);
            rb.velocity = movement * velocidad;
            animator.SetFloat("Speed",Mathf.Abs(horizontal)+Mathf.Abs(vertical));
        }
    }
    
    //---------------------- GETTERS Y SETTERS ----------------------
    public int getdamage(){
        return damage;
    }
    public float getShootCooldown(){
        return shootCooldown;
    }
    public float getMaxLife()
    {
        return maxlife;
    }
    public float getVelocidad()
    {
        return velocidad;
    }
    public bool getDobleShot()
    {
        return doubleshot;
    }
    // setter para un booleano que no permite mover el personaje cuando está muerto
    public void SetIsDead()
    {
        animator.SetBool("Dead",true);
        isDead=true;
    }
    public void SetDoubleshot(){
        doubleshot=true;
    }

    //----------------FIN GETTERS Y SETTERS----------------

    //------------------------CORRUTINAS------------------------
    private IEnumerator Invincible()
    {
        
        animator.SetBool("Damage",true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Damage",false);
        inmortal=false;
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
        Shoot();
    }
    //----------------------FIN DE CORRUTINAS ----------------------

    //-------------------METODOS AUXILIARES-------------------
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
    private void Shoot(){
        //Crear flecha y asignarle la dirección
        GameObject ArrowGO=Instantiate(flech,arco.transform.position,Quaternion.identity);
        Flecha Arrow=ArrowGO.GetComponent<Flecha>();
        Arrow.targetVector=mouseCoords-transform.position;
        //Ignorar colisiones con el jugador
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D arrowCollider = ArrowGO.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(playerCollider, arrowCollider);

    }
    public void DecreaseShootCooldown(){
        if(shootCooldown>0.3f){
            shootCooldown-=0.1f;
        }
    }

    public void IncreaseSpeed(){
        if(velocidad<7f){
            velocidad+=0.5f;
        }
    }
    public void IncreaseDamage(){
        if(damage<4){
            damage++;
        }
    }

    public void DeactivateShootAnim()
    {
        animator.SetBool("Shoots",false);
    }
    //-------------------FIN METODOS AUXILIARES-------------------

    //-------------------COLISIONES-------------------
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
    //-------------------FIN COLISIONES-------------------
    

}
