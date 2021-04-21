using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// “G‚Ì¶¬
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameMaster gameMaster;

    [SerializeField]
    private Transform[] generateTran = new Transform[4];

    public int enemyCount;

    private GameObject target;
    private float enemyGenTime;
    public bool isBossBattle;
    
    

    public void SetUPEnemyGenerator()
    {
        target = GameObject.Find("AR Session Origin");
    }

    private void Update()
    {
        if(gameMaster.isGameStart == true)
        {
            enemyGenTime += Time.deltaTime;
        }
        

        //“G‚ª1‘ÌˆÈã‹‚ê‚ÎisBattleƒtƒ‰ƒO‚ğtrue‚É‚·‚é
        if (enemyCount >= 1)
        {
            gameMaster.ChangeBattle(true);
        }
        //“G‚ª‚¢‚È‚­‚È‚Á‚½isBattleƒtƒ‰ƒO‚ğfalse‚É‚·‚é
        else if (enemyCount == 0)
        {
            //“G‚Ì”‚ª0‚Ìê‡í“¬’†ƒtƒ‰ƒO‚ÌØ‚è‘Ö‚¦
            gameMaster.ChangeBattle();
        }

        if(gameMaster.bossClear == true)
        {
            return;
        }

        if (isBossBattle == false)
        {
            //5•b–ˆ‚É1`‚S‘Ì‚Ì“G‚ğƒ‰ƒ“ƒ_ƒ€‚É¶¬
            if (enemyGenTime > 5)
            {
                for (int i = 1; i <= Random.Range(1, 5); i++)
                {
                    GenerateEnemy();
                }

                enemyGenTime = 0;
            }
        }
        else
        {
            if(enemyGenTime > 3.5f)
            {
                for (int i = 1; i <= Random.Range(1, 5); i++)
                {
                    GenerateEnemy();
                }

                enemyGenTime = 0;
            }
        }

        
    }

    /// <summary>
    ///  “G‚ğ¶¬‚µA¶¬ˆÊ’u‚ğƒ‰ƒ“ƒ_ƒ€‚É‚·‚é
    /// </summary>
    public void GenerateEnemy(bool isBoss = false)
    {
        Vector3 randomPos = new Vector3(0, 0, Random.Range(-10, 10));

        if (isBoss == false)
        {
            if (isBossBattle == false)
            {
                GameObject enemy = Instantiate(enemyPrefab, transform.position + randomPos, Quaternion.identity);
                enemy.GetComponent<EnemyController>().SetUpEnemy(target, this, isBoss);
            }
            else
            {
                GameObject enemy = Instantiate(enemyPrefab, generateTran[Random.Range(0, generateTran.Length)].position + randomPos, Quaternion.identity);
                enemy.GetComponent<EnemyController>().SetUpEnemy(target, this, isBoss);
            }
        }
        else 
        {
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(transform.position.x + 40, 0.1f, transform.position.z), Quaternion.identity);
            enemy.GetComponent<EnemyController>().SetUpEnemy(target, this, isBoss);
            isBossBattle = true;
        }

        enemyCount++;
        
    }

    /// <summary>
    /// “|‚³‚ê‚½“G‚Ì”‚ğŒ¸‚ç‚·
    /// </summary>
    public void DecreaseEnemyCount()
    {
        enemyCount--;
        if(enemyCount < 0)
        {
            enemyCount = 0;
        }
    }

}
