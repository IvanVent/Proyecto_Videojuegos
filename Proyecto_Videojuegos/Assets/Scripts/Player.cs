using System.Collections;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

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
    
    public GameObject endingPanel;
    public GameObject endingText;
    public GameObject endingButton;
    public GameObject tutorialPanel1;
    public GameObject tutorialPanel2;

    private Vector3 mouseCoords;
    private Vector2 movement;
    
    [SerializeField] private float shootCooldown=0.6f;
    private float maxlife=3f;
    [FormerlySerializedAs("velocidad")] [SerializeField]private float speed=5;
    private float horizontal;
    private float vertical;
    [SerializeField] private float dashSpeed=10f;
    [SerializeField] private float dashDuration=0.1f;
    [SerializeField] private float dashCooldown=1f;

    private int damage;
    int swapShootSFX=0;

    private bool inmortal=false;
    public bool doubleshot=false;
    private bool canShoot = true;
    private bool isDead=false;
    private bool isFacingRight = true;
    private bool isDashing=false;
    private bool canDash;
    private bool IsInIntroAnim;
    public int autorecolect=0;
    private bool isEnding = false;
    
    
    
    void Start()
    {
        IsInIntroAnim=true;
        vida.Invoke(maxlife,0);
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        damage=1;
        canDash=true;
    }
    
    void Update()
    {
        if (IsInIntroAnim) return;
        if (isDashing) return; //si se hace un dash no queremos que se ejecute nada mas durante el dash

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;        
        if (Time.timeScale == 0) return;
        if (!isDead && !isEnding)
        {
            mouseCoords=camara.ScreenToWorldPoint(Input.mousePosition);
            mouseCoords.z=0;
            float angulo=Mathf.Atan2(mouseCoords.y-transform.position.y,mouseCoords.x-transform.position.x);
            float anguloGrados=(180/Mathf.PI)*angulo;
            bool isMouseRight = transform.position.x < mouseCoords.x;
            
            if (isMouseRight && !isFacingRight || !isMouseRight && isFacingRight)
            {
                Flip();
            }

            

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
            if(Input.GetKeyDown(KeyCode.LeftShift) && canDash){

                if (horizontal < 0 && isFacingRight)
                {
                    Flip();
                }else if(horizontal > 0 && !isFacingRight)
                {
                    Flip();
                }
                StartCoroutine(Dash());
            }
        }


    }
    //------------------------MOVIMIENTO------------------------
    private void FixedUpdate() {

        if (IsInIntroAnim) return;
  
        if (!isDead && !isEnding)
        {
            if (isDashing) return;//si se hace un dash no queremos que se ejecute nada mas durante el dash

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            movement = new Vector2(horizontal, vertical);
            rb.velocity = movement * speed;
            animator.SetFloat("Speed",Mathf.Abs(horizontal)+Mathf.Abs(vertical));
        }
    }

    private IEnumerator Dash(){
        inmortal=true;
        canDash=false;
        isDashing=true;
        animator.SetBool("Dashing",isDashing);
        rb.velocity = new Vector2(movement.x * dashSpeed, movement.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing=false;
        animator.SetBool("Dashing",isDashing);
        inmortal=false;

        yield return new WaitForSeconds(dashCooldown);
        canDash=true;
    }
    //----------------------FIN MOVIMIENTO----------------------
    
    //---------------------- GETTERS Y SETTERS ----------------------
    public int getDamage(){
        return damage;
    }
    public float getShootCooldown(){
        return shootCooldown;
    }
    public float getMaxLife()
    {
        return maxlife;
    }
    public float getSpeed()
    {
        return speed;
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
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void SetIsEnding()
    {
        isEnding=true;
        rb.velocity = Vector2.zero;
        animator.SetFloat("Speed",0f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.Play("Idle");
        StartCoroutine("EndingCinematic");
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
        yield return new WaitForSeconds(0.05f);
        src.Play();
        Shoot();
    }
    
      // CINEMATICA FINAL
    private IEnumerator EndingCinematic()
    {
        yield return new WaitForSeconds(1.5f);
        
        endingPanel.SetActive(true);
        animator.Play("Falling");
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        
        float ascensionDuration = 4f; // Duración de la ascension
        float elapsed = 0f;
        float playerPosY = transform.position.y;
        
        // Color del Image del panel
        UnityEngine.UI.Image panelImage = endingPanel.GetComponent<Image>();
        Color panelColor = panelImage.color;
        panelColor.a = 0f;
        panelImage.color = panelColor;
        
        // Ascension y Fade a blanco
        while (elapsed < ascensionDuration)
        {
            elapsed += Time.deltaTime; 
            float newPos = Mathf.Lerp(playerPosY, playerPosY + 7, elapsed / ascensionDuration);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, newPos, gameObject.transform.position.z);
            
            panelColor.a = Mathf.Lerp(0f, 1f, elapsed / ascensionDuration); // Interpolar opacidad
            panelImage.color = panelColor;
            yield return null;
        }
        
        yield return new WaitForSeconds(1f);

        endingText.SetActive(true);
        float textFadeDuration = 1f;
        elapsed = 0f;

        Text gameCompletedTxt = endingText.GetComponent<Text>();
        Color textColor = gameCompletedTxt.color;
        textColor.a = 0f;
        gameCompletedTxt.color = textColor;
        
        while (elapsed < textFadeDuration)
        {
            elapsed += Time.deltaTime; 
            
            textColor.a = Mathf.Lerp(0f, 1f, elapsed / textFadeDuration); // Interpolar opacidad
            gameCompletedTxt.color = textColor;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        endingButton.SetActive(true);
    }
    //----------------------FIN DE CORRUTINAS ----------------------

    //-------------------METODOS AUXILIARES-------------------

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void Shoot(){
        //Crear flecha y asignarle la dirección
        GameObject ArrowGO=Instantiate(flech,arco.transform.position,Quaternion.identity);
        Flecha Arrow=ArrowGO.GetComponent<Flecha>();
        Arrow.targetVector=(mouseCoords-transform.position).normalized;
        //Ignorar colisiones con el jugador
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D arrowCollider = ArrowGO.GetComponent<Collider2D>();

        if (playerCollider != null && arrowCollider != null){
            Physics2D.IgnoreCollision(playerCollider, arrowCollider);
        }
    }
    public void DecreaseShootCooldown(){
        if(shootCooldown>0.3f){
            shootCooldown-=0.1f;
        }
    }

    public void IncreaseSpeed(){
        if(speed<7f){
            speed+=0.25f;
        }
    }
    public void IncreaseDamage(){
        if(damage<3){
            damage++;
        }
    }

    public void DeactivateShootAnim()
    {
        animator.SetBool("Shoots",false);
    }

    public void OnIntroAnimationEnd(){
        animator.SetTrigger("IntroFinished");
        StartCoroutine(WaitForTutorial());
    }

    private IEnumerator WaitForTutorial()
    {
        yield return new WaitForSecondsRealtime(1f);
        tutorialPanel1.SetActive(true);
    }

    public void TutorialSwitchTo2()
    {
        tutorialPanel1.SetActive(false);
        tutorialPanel2.SetActive(true);
    }
    public void TutorialSwitchTo1()
    {
        tutorialPanel2.SetActive(false);
        tutorialPanel1.SetActive(true);
    }

    public void FinishTutorial()
    {
        tutorialPanel2.SetActive(false);
        IsInIntroAnim=false;
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
