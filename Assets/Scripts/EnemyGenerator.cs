using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameMaster gameMaster;

    private GameObject target;
    private float enemyGenTime;

    private void Start()
    {
        target = GameObject.Find("Camera");
    }

    private void Update()
    {
        enemyGenTime += Time.deltaTime;

        if(enemyGenTime > 5)
        {
            GenerateEnemy();
            enemyGenTime = 0;
        }
    }

    private void GenerateEnemy()
    {
        
        GameObject enemy = Instantiate(enemyPrefab,new Vector3(transform.position.x, 0.1f, transform.position.z + Random.Range(-10, 10)), Quaternion.identity);
        enemy.GetComponent<EnemyController>().SetUpEnemy(target);
        gameMaster.ChangeBattle(true);
        
    }

}
