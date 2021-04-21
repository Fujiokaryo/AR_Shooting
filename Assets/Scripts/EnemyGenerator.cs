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
        

        //�G��1�̈ȏ㋏���isBattle�t���O��true�ɂ���
        if (enemyCount >= 1)
        {
            gameMaster.ChangeBattle(true);
        }
        //�G�����Ȃ��Ȃ���isBattle�t���O��false�ɂ���
        else if (enemyCount == 0)
        {
            //�G�̐���0�̏ꍇ�퓬���t���O�̐؂�ւ�
            gameMaster.ChangeBattle();
        }

        if(gameMaster.bossClear == true)
        {
            return;
        }

        if (isBossBattle == false)
        {
            //5�b����1�`�S�̂̓G�������_���ɐ���
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
    ///  �G�𐶐����A�����ʒu�������_���ɂ���
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
    /// �|���ꂽ�G�̐������炷
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
