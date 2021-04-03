using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�̐���
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

        //�G��1�̈ȏ㋏���isBattle�t���O��true�ɂ���
        if (enemyCount >= 1)
        {
            gameMaster.ChangeBattle(true);
        }


        //5�b����1�`�S�̂̓G�������_���ɐ���
        if (enemyGenTime > 5)
        {
            for (int i = 1; i <= Random.Range(1, 5); i++)
            {
                GenerateEnemy();
            }

            enemyGenTime = 0;
        }

        //�G�����Ȃ��Ȃ���isBattle�t���O��false�ɂ���
        if (enemyCount == 0)
        {
            gameMaster.ChangeBattle();
        }
    }

    /// <summary>
    ///  �G�𐶐����A�����ʒu�������_���ɂ���
    /// </summary>
    private void GenerateEnemy()
    {
        
        GameObject enemy = Instantiate(enemyPrefab,new Vector3(transform.position.x, 0.1f, transform.position.z + Random.Range(-10, 10)), Quaternion.identity);
        enemy.GetComponent<EnemyController>().SetUpEnemy(target, this);
        enemyCount++;
        
    }

}
