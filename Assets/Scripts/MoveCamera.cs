using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 視点の移動
/// </summary>
public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private GameMaster gameMaster;

    [SerializeField]
    private EnemyGenerator enemyGenerator;

    public float dx;
    bool isBoss;

    /// <summary>
    /// isBattleフラグがtrueの時のみ前進する
    /// </summary>
    void Update()
    {
        if (gameMaster.bossClear == true || enemyGenerator.isBossBattle == true)
        {
            return;
        }


        if (gameMaster.isBattle == false)
        {
            this.transform.position += new Vector3(dx * Time.deltaTime, 0, 0);
            if (gameObject.transform.position.x > 75)
            {
                isBoss = true;
            }
        }


        if (isBoss == true)
        {
            enemyGenerator.GenerateEnemy(isBoss);
            isBoss = false;
        }
    }
}
