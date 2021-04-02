using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if(enemyCount >= 1)
        {
            gameMaster.ChangeBattle(true);
        }

        if (enemyCount < 10)
        {
            if (enemyGenTime > 5)
            {
                for (int i = 1; i <= Random.Range(1, 5); i++)
                {
                    GenerateEnemy();
                }
                
                enemyGenTime = 0;
            }
        }

        if(enemyCount == 0)
        {
            gameMaster.ChangeBattle();
        }
    }

    private void GenerateEnemy()
    {
        
        GameObject enemy = Instantiate(enemyPrefab,new Vector3(transform.position.x, 0.1f, transform.position.z + Random.Range(-10, 10)), Quaternion.identity);
        enemy.GetComponent<EnemyController>().SetUpEnemy(target, this);
        enemyCount++;
        
    }

}
