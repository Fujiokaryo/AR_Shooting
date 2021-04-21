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
    private bool isBoss;
    private bool moveCamera;
    private bool bossBgm;

    /// <summary>
    /// isBattleフラグがtrueの時のみ前進する
    /// </summary>
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -0.9f, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 0.9f, 0));
        }

        if (CheckMoveCamera() == false) 
        {
            return;
        }


        if (CheckMoveCamera() == true)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayBossBGM")
        {
            BGMmanager.instance.PlayBGM(SoundDataSO.BgmType.Boss);
            other.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// カメラの停止判定　moveCameraがfalseならカメラ移動停止
    /// </summary>
    /// <returns></returns>
    private bool CheckMoveCamera()
    {
        moveCamera = true;

        if(gameMaster.isBattle == true)
        {
            moveCamera = false;
        }
        else if (gameMaster.bossClear == true)
        {
            moveCamera = false;
        }
        else if(enemyGenerator.isBossBattle == true)
        {
            moveCamera = false;
        }
        else if(gameMaster.isGameStart == false)
        {
            moveCamera = false;
        }
        else if(gameMaster.isGameOver == true)
        {
            moveCamera = false;
        }

                          
        return moveCamera;
    }

  
}
