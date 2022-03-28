using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int treeLevel;
    public int vitality;

    public bool[] hasBox;
    public bool isBoxExist;
    public float boxTimer;

    //동물.
    public string[] animalName;
    public int[] animalNum;
    public bool[] isLock;
    public bool[] isOwned;
    public bool[] isFavorite;

    public string[] activeAnimalName;
    public float[] animalLifeCycle;
    public float[] animalSatiety;
    public int fishNum;
    public int chickenNum;

    public string[] activeSpecialAnimalName;
    public float[] specialAnimalLifeCycle;

    //사과 관리.
    public bool[] hasApple;
    public bool isFirst;

    //바나나 관리.
    public bool[] hasBanana;

    //스킬 쿨타임 관리.
    public float skill1Timer;
    public float skill2Timer;
    public float skill3Timer;
    public float skill4Timer;

    public bool isSkill1;
    public bool isSkill2;
    public bool isSkill3;
    public bool isSkill4;

    //튜토리얼 했는지 체크.
    public bool isTutorial;

    public PlayerData(GameManager manager)
    {
        treeLevel = manager.treeLevel;
        vitality = manager.vitality;

        //박스.
        hasBox = new bool[manager.hasBox.Length];
        for (int j = 0; j < manager.hasBox.Length; j++)
        {
            if (!manager.hasBox[j]) continue;

            hasBox[j] = true;
        }
        isBoxExist = manager.isBoxExist;
        boxTimer = manager.boxTimer;

        //동물.
        animalName = new string[manager.animals.Count];
        animalNum = new int[manager.animals.Count];
        isLock = new bool[manager.animals.Count];
        isOwned = new bool[manager.animals.Count];
        isFavorite = new bool[manager.animals.Count];

        int i = 0;
        foreach (var element in manager.animals)
        {
            animalName[i] = element.Key;
            animalNum[i] = element.Value.totalNum;
            isLock[i] = element.Value.isLock;
            isOwned[i] = element.Value.owned;
            isFavorite[i] = element.Value.favorite;
            i++;
        }

        activeAnimalName = new string[manager.activeObjs.Count];
        animalLifeCycle = new float[manager.activeObjs.Count];
        animalSatiety = new float[manager.activeObjs.Count];

        fishNum = manager.fishNum;
        chickenNum = manager.chickenNum;

        activeSpecialAnimalName = new string[manager.activeSpecialObjs.Count];
        specialAnimalLifeCycle = new float[manager.activeSpecialObjs.Count];
        i = 0;
        foreach (var element in manager.activeObjs)
        {
            var script = element.GetComponent<AnimalController>();

            activeAnimalName[i] = script.currentAnimal.name;
            animalLifeCycle[i] = script.lifeCycle;
            animalSatiety[i] = script.satiety;
            i++;
        }

        i = 0;
        foreach (var element in manager.activeSpecialObjs)
        {
            var script = element.GetComponent<AnimalController>();

            activeSpecialAnimalName[i] = script.currentAnimal.name;
            specialAnimalLifeCycle[i] = script.lifeCycle;
            i++;
        }

        //사과 관리.
        hasApple = new bool[manager.appleSpawnPos.Length];
        for (int j = 0; j < manager.hasApple.Length; j++)
        {
            hasApple[j] = manager.hasApple[j];
        }

        //바나나 관리.
        hasBanana = new bool[manager.bananaSpawnPos.Length];
        for (int j = 0; j < manager.hasBanana.Length; j++)
        {
            hasBanana[j] = manager.hasBanana[j];
        }

        isFirst = manager.isFirst;

        //스킬 쿨타임 관리.
        skill1Timer = manager.treeWinManager.skill1Timer;
        skill2Timer = manager.treeWinManager.skill2Timer;
        skill3Timer = manager.treeWinManager.skill3Timer;
        skill4Timer = manager.treeWinManager.skill4Timer;

        isSkill1 = manager.treeWinManager.isSkill1;
        isSkill2 = manager.treeWinManager.isSkill2;
        isSkill3 = manager.treeWinManager.isSkill3;
        isSkill4 = manager.treeWinManager.isSkill4;

        //튜토리얼 했는지 체크.
        isTutorial = manager.tutorialManager.isTutorial;
    }
}
