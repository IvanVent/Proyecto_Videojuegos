using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class back : MonoBehaviour

{
    public List<Image> backList;
    public GameObject backi;
    private Player playerScript;
    public Sprite backsprite;
    public Sprite firstsprite;
    public Sprite lastsprite;
    public int pos;
    public float health;
    private float max;
    
    public HeartsGenerator heartsGenerator;
    // Start is called before the first frame update
    private void Awake(){
        playerScript= GameObject.Find("Player").GetComponent<Player>();
        playerScript.vida.AddListener(UpdateHearts);
    }
    public void UpdateHearts(float corazones, int instruccion)
    {
        if(instruccion==0){
            GenerateHearts(corazones);
            heartsGenerator.repite();
        }
        if(instruccion==2){
            moreHearts(corazones);
        }
    }

    private void moreHearts(float corazones)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    private void GenerateHearts(float corazones)
    {
        health=corazones;  
        max=corazones; 
        pos=corazones*10%10!=0?(int)corazones:(int)corazones-1;

        while(corazones>0){
            if(corazones==max){
                GameObject first=Instantiate(backi,transform);
                backList.Add(first.GetComponent<Image>());
                backList[0].sprite=firstsprite;
            }
            else if(corazones>1){
                GameObject middle=Instantiate(backi,transform);
                backList.Add(middle.GetComponent<Image>());
                
            }
            else{
                GameObject last=Instantiate(backi,transform);
                backList.Add(last.GetComponent<Image>());
                backList[pos].sprite=lastsprite;
            }
            corazones--;
        }



    }
}
