using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class heartsgnerator : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Image> listacorazones;
    public GameObject heart,halfheart,empty;
    public jugador sjugador;
    public int pos;
    public float vida;
    public Sprite fullheart;
    public Sprite half_heart;
    public Sprite background;
    
    private void Awake(){
        sjugador.vida.AddListener(ActualizarCorazones);
    }

    private void ActualizarCorazones(float corazones, int instruccion)
    {
        if(instruccion==0){
            generarcorazones(corazones);
        }
        if(instruccion==1){
            perdervida(corazones);
        }

    }

    private void perdervida(float corazones)
    {
        while(corazones>0&&vida!=0){
            print("aaaaaaaaaaaaaaaaa\n");
            if(vida*10%10==5){
                print("ccccccccccccccccccc\n");
                listacorazones[pos].sprite=background;
                pos=pos-1;
                vida= vida - 0.5f;
                corazones= corazones - 0.5f;
            }
            else{
                if(corazones==0.5){
                    listacorazones[pos].sprite=half_heart;
                    vida= vida - 0.5f;
                    corazones= corazones - 0.5f;
                }
                else{
                    listacorazones[pos].sprite=background;
                    pos=pos-1;
                    vida= vida - 1f;
                    corazones= corazones - 1f;
                }
            }
            

        }
    }

    private void generarcorazones(float corazones)
    {
        vida=corazones;
        pos=corazones*10%10!=0?(int)corazones:(int)corazones-1;

        while(corazones>0){
            if(corazones>=1){
                GameObject corazon=Instantiate(heart,transform);
                listacorazones.Add(corazon.GetComponent<Image>());

            }
            else{
                GameObject corazon=Instantiate(halfheart,transform);
                listacorazones.Add(corazon.GetComponent<Image>());
            }
            corazones--;
        }



    }
}
