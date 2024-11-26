using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class HeartsGenerator : MonoBehaviour
{
  
    public List<Image> heartsList;
    public GameObject heart, halfheart;
    private Player playerScript;
    public int pos;
    public float health;
    private float max;
    public Sprite fullHeart_sprite;
    public Sprite halfHeart_sprite;
    public Sprite bg_sprite;
    
    public GameManager gameManager;
    private bool isDead = false;
    
    //GETTER Y SETTERS

    public List<Image> getHeartsList()
    {
        return heartsList;
    }
    
    //FIN GETTER Y SETTERS
    private void Awake(){
        playerScript= GameObject.Find("Player").GetComponent<Player>();
        playerScript.vida.AddListener(UpdateHearts);
    }

    public void UpdateHearts(float corazones, int instruccion)
    {
        if(instruccion==0){
            GenerateHearts(corazones);
        }
        if(instruccion==1){
            loseHearts(corazones);
        }
        if(instruccion==2){
            moreHearts(corazones);
        }
        if(instruccion==3){
            curar(corazones);
        }

    }

    private void curar(float corazones)
    {
        while(corazones>0&&health!=max){
            if(corazones>=1){
                if(max-health>=1){
                    if(health*10%10>0){
                        heartsList[pos].sprite=fullHeart_sprite;
                        health+=0.5f;
                        corazones-=0.5f;
                    }
                    else{
                        pos++;
                        heartsList[pos].sprite=fullHeart_sprite;
                        health++;
                        corazones--;
                    }
                }
                else{
                    if(health*10%10>0){
                        heartsList[pos].sprite=fullHeart_sprite;
                        
                    }       
                    else{
                        pos++;
                        heartsList[pos].sprite=halfHeart_sprite;
                    }             
                    health+=0.5f;
                    corazones-=0.5f;

                }
            }
            else{
                if(health*10%10>0){
                    heartsList[pos].sprite=fullHeart_sprite;
                    health+=0.5f;
                    corazones-=0.5f;
                }
                else{
                    pos++;
                    health+=0.5f;
                    heartsList[pos].sprite=halfHeart_sprite;
                    corazones-=0.5f;
                }

            }
        }
    }

    private void moreHearts(float corazones)
    {
        while(corazones>0){
            if(corazones>=1){
                if(max*10%10==0){
                    GameObject corazon=Instantiate(heart,transform);
                    heartsList.Add(corazon.GetComponent<Image>());
                    heartsList[heartsList.Count-1].sprite=bg_sprite;
                    max++;
                    corazones--;
                }
                else{
                    heartsList[heartsList.Count-1].sprite = fullHeart_sprite;
                    max+=0.5f;
                    corazones-=0.5f;
                }
            }
            else{
                if(max*10%10==0){
                    GameObject corazon=Instantiate(halfheart,transform);
                    heartsList.Add(corazon.GetComponent<Image>());
                    heartsList[heartsList.Count-1].sprite=bg_sprite;
                    max+=0.5f;
                    corazones-=0.5f;
                }
                else{
                    heartsList[heartsList.Count-1].sprite = fullHeart_sprite;
                    max+=0.5f;
                    corazones-=0.5f;
                }
            }
        }
        
    }

    // aqui corazones es la cantidad de corazones que se quieren quitar
    private void loseHearts(float corazones)
    {

        while(corazones>0 && health!=0){
           
            if(health*10%10 == 5){
                
                heartsList[pos].sprite = bg_sprite;
                pos = pos-1;
                health = health - 0.5f;
                corazones = corazones - 0.5f;
            }
            else{
                if(corazones==0.5){
                    heartsList[pos].sprite=halfHeart_sprite;
                    health= health - 0.5f;
                    corazones= corazones - 0.5f;
                }
                else{
                    heartsList[pos].sprite=bg_sprite;
                    pos=pos-1;
                    health= health - 1f;
                    corazones= corazones - 1f;
                }
            }
        }
        if(health <= 0 && !isDead)
        {
            isDead = true;
            playerScript.SetIsDead();
            gameManager.GameOver();
        }

    }

    private void GenerateHearts(float corazones)
    {
        health=corazones;  
        max=corazones; 
        pos=corazones*10%10!=0?(int)corazones:(int)corazones-1;

        while(corazones>0){
            if(corazones>=1){
                GameObject corazon=Instantiate(heart,transform);
                heartsList.Add(corazon.GetComponent<Image>());
            }
            else{
                GameObject corazon=Instantiate(halfheart,transform);
                heartsList.Add(corazon.GetComponent<Image>());
            }
            corazones--;
        }



    }
}
