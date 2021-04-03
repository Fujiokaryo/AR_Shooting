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

    public int enemyCount = 0;

    private GameObject target;
    private float enemyGenTime;
    

    private void Start()
    {
        target = GameObject.Find("Camera");
    }

    private void Update()
    {
        enemyGenTime += Time.deltaTime;

        //“G‚ª1‘ÌˆÈã‹‚ê‚ÎisBattleƒtƒ‰ƒO‚ğtrue‚É‚·‚é
        if (enemyCount >= 1)
        {
            gameMaster.ChangeBattle(true);
        }


        //5•b–ˆ‚É1`‚S‘Ì‚Ì“G‚ğƒ‰ƒ“ƒ_ƒ€‚É¶¬
        if (enemyGenTime > 5)
        {
            for (int i = 1; i <= Random.Range(1, 5); i++)
            {
                GenerateEnemy();
            }

            enemyGenTime = 0;
        }

        //“G‚ª‚¢‚È‚­‚È‚Á‚½isBattleƒtƒ‰ƒO‚ğfalse‚É‚·‚é
        if (enemyCount == 0)
        {
            gameMaster.ChangeBattle();
        }
    }

    /// <summary>
    ///  “G‚ğ¶¬‚µA¶¬ˆÊ’u‚ğƒ‰ƒ“ƒ_ƒ€‚É‚·‚é
    /// </summary>
    private void GenerateEnemy()
    {
        
        GameObject enemy = Instantiate(enemyPrefab,new Vector3(transform.position.x, 0.1f, transform.position.z + Random.Range(-10, 10)), Quaternion.identity);
        enemy.GetComponent<EnemyController>().SetUpEnemy(target, this);
        enemyCount++;
        
    }

}
