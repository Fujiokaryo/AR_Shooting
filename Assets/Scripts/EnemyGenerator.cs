using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の生成
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameMaster gameMaster;

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
        enemyGenTime += Time.deltaTime;

        //敵が1体以上居ればisBattleフラグをtrueにする
        if (enemyCount >= 1)
        {
            gameMaster.ChangeBattle(true);
        }
        //敵がいなくなったisBattleフラグをfalseにする
        else if (enemyCount == 0)
        {
            //敵の数が0の場合戦闘中フラグの切り替え
            gameMaster.ChangeBattle();
        }

        if(gameMaster.bossClear == true)
        {
            return;
        }

        if (isBossBattle == false)
        {
            //5秒毎に1〜４体の敵をランダムに生成
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
            if(enemyGenTime > 2)
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
    ///  敵を生成し、生成位置をランダムにする
    /// </summary>
    public void GenerateEnemy(bool isBoss = false)
    {

        if (isBoss == false)
        {
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(transform.position.x, 0.1f, transform.position.z + Random.Range(-10, 10)), Quaternion.identity);
            enemy.GetComponent<EnemyController>().SetUpEnemy(target, this, isBoss);
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
    /// 倒された敵の数を減らす
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
