using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Animal.asset",
    menuName = "Animal/BaseInfo")]
public class AnimalDefinition : ScriptableObject
{
    public string name;

    public int payVitality;
    public int getExp;

    public float satiety; // 포만감
    public float lifeCycle;

    public int getVitality_feed;
    public int getVitality_dead;
   
    public int level;
    public bool isLock = true;
    public bool isSpecial;
   
    public bool swimmable;
    public bool flyable;
    public bool isFish;

    public GameObject prefab;

    //도감 관련 내용.
    public bool owned;
    public bool favorite;

    public Sprite unlockImg;
    public Sprite lockImg;
    public Sprite bookImg;
    public Sprite largeImg;

    public string[] eat;
    public string[] eaten;

    public int totalNum;
    public int activeNum;
}
