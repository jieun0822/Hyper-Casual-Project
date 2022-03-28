using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaManager : MonoBehaviour
{
    public void Create(GameManager manager)
    {
        int bananaNum1 = 0;
        int bananaNum2 = 0;
        int bananaNum3 = 0;
        for (int i = 0; i < manager.hasBanana.Length; i++)
        {
            if (i < 5 && manager.hasBanana[i]) bananaNum1++;
            else if (5 <= i && i < 10 && manager.hasBanana[i]) bananaNum2++;
            else if (10 <= i && i < 15 && manager.hasBanana[i]) bananaNum3++;
        }

        bool isMax1 = false;
        bool isMax2 = false;
        bool isMax3 = false;
        if (bananaNum1 >= manager.maxBananas) isMax1 = true;
        if (bananaNum2 >= manager.maxBananas) isMax2 = true;
        if (bananaNum3 >= manager.maxBananas) isMax3 = true;

        if (isMax1 && isMax2 && isMax3) return;

        if (isMax1)
        {
            if (isMax2)
            {
                //3.
                if (!isMax3)
                {
                    while (true)
                    {
                        int randomIndex = Random.Range(10, 15);
                        if (!manager.hasBanana[randomIndex])
                        {
                            manager.hasBanana[randomIndex] = true;
                            var banana = Instantiate(manager.bananaObj, manager.bananaSpawnPos[randomIndex].position, Quaternion.identity);
                            var script = banana.GetComponent<Apple>();
                            script.key = randomIndex;
                            manager.activeBananas.Add(randomIndex, banana);
                            return;
                        }
                    }
                }
            }
            else
            {
                if (isMax3)//2.
                {
                    while (true)
                    {
                        int randomIndex = Random.Range(5, 10);
                        if (!manager.hasBanana[randomIndex])
                        {
                            manager.hasBanana[randomIndex] = true;
                            var banana = Instantiate(manager.bananaObj, manager.bananaSpawnPos[randomIndex].position, Quaternion.identity);
                            var script = banana.GetComponent<Apple>();
                            script.key = randomIndex;
                            manager.activeBananas.Add(randomIndex, banana);
                            return;
                        }
                    }
                }
                else //2,3.
                {
                    while (true)
                    {
                        int randomIndex1 = Random.Range(5, 10);
                        int randomIndex2 = Random.Range(10, 15);

                        int randomSelect = Random.Range(0, 10);
                        int randomIndex = (randomSelect < 5) ? randomIndex1 : randomIndex2;

                        if (!manager.hasBanana[randomIndex])
                        {
                            manager.hasBanana[randomIndex] = true;
                            var banana = Instantiate(manager.bananaObj, manager.bananaSpawnPos[randomIndex].position, Quaternion.identity);
                            var script = banana.GetComponent<Apple>();
                            script.key = randomIndex;
                            manager.activeBananas.Add(randomIndex, banana);
                            return;
                        }
                    }
                }
            }
        }
        else
        {
            if (isMax2)
            {
                if (isMax3)//1.
                {
                    while (true)
                    {
                        int randomIndex = Random.Range(0, 5);

                        if (!manager.hasBanana[randomIndex])
                        {
                            manager.hasBanana[randomIndex] = true;
                            var banana = Instantiate(manager.bananaObj, manager.bananaSpawnPos[randomIndex].position, Quaternion.identity);
                            var script = banana.GetComponent<Apple>();
                            script.key = randomIndex;
                            manager.activeBananas.Add(randomIndex, banana);
                            return;
                        }
                    }
                }
                else//1,3
                {
                    while (true)
                    {
                        int randomIndex1 = Random.Range(0, 5);
                        int randomIndex2 = Random.Range(10, 15);

                        int randomSelect = Random.Range(0, 10);
                        int randomIndex = (randomSelect < 5) ? randomIndex1 : randomIndex2;

                        if (!manager.hasBanana[randomIndex])
                        {
                            manager.hasBanana[randomIndex] = true;
                            var banana = Instantiate(manager.bananaObj, manager.bananaSpawnPos[randomIndex].position, Quaternion.identity);
                            var script = banana.GetComponent<Apple>();
                            script.key = randomIndex;
                            manager.activeBananas.Add(randomIndex, banana);
                            return;
                        }
                    }
                }
            }
            else
            {
                if (isMax3)//1,2.
                {
                    while (true)
                    {
                        int randomIndex1 = Random.Range(0, 5);
                        int randomIndex2 = Random.Range(5, 10);

                        int randomSelect = Random.Range(0, 10);
                        int randomIndex = (randomSelect < 5) ? randomIndex1 : randomIndex2;

                        if (!manager.hasBanana[randomIndex])
                        {
                            manager.hasBanana[randomIndex] = true;
                            var banana = Instantiate(manager.bananaObj, manager.bananaSpawnPos[randomIndex].position, Quaternion.identity);
                            var script = banana.GetComponent<Apple>();
                            script.key = randomIndex;
                            manager.activeBananas.Add(randomIndex, banana);
                            return;
                        }
                    }
                }
                else//1,2,3.
                {
                    while (true)
                    {
                        int randomIndex = Random.Range(0, 15);

                        if (!manager.hasBanana[randomIndex])
                        {
                            manager.hasBanana[randomIndex] = true;
                            var banana = Instantiate(manager.bananaObj, manager.bananaSpawnPos[randomIndex].position, Quaternion.identity);
                            var script = banana.GetComponent<Apple>();
                            script.key = randomIndex;
                            manager.activeBananas.Add(randomIndex, banana);
                            return;
                        }
                    }
                }
            }
        }
    }
}
