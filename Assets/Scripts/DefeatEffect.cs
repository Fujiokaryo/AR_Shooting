using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敵を消えた際のエフェクトの制御
/// </summary>
public class DefeatEffect : MonoBehaviour
{
    private float timer;
    private GameObject target;
    private Slider playerMPBar;
    private GameMaster gameMaster;

    public void SetUpSoul(GameObject target)
    {
        this.target = target;
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        
        if (gameObject.tag == "Soul")
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, 5, target.transform.position.z);

            if (timer > 0.8f)
            {

                this.gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPos, 20 * Time.deltaTime);            
             
            }
        }

        if(timer >1.5f)
        {

            if (gameObject.tag == "Soul")
            {
                gameMaster.UpDatePlayerMP();
            }

            Destroy(gameObject);
        }
    }

}
