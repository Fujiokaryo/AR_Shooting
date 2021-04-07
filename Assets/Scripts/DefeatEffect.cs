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
    private ItemDetaSO.ItemData itemData;

    /// <summary>
    /// 魂の場合の初期設定
    /// </summary>
    /// <param name="target"></param>
    public void SetUpSoul(GameObject target)
    {
        this.target = target;
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    /// <summary>
    /// アイテムの場合の初期設定
    /// </summary>
    /// <param name="itemData"></param>
    /// <param name="target"></param>
    public void SetUpItem(ItemDetaSO.ItemData itemData, GameObject target)
    {
        this.target = target;
        this.itemData = itemData;
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        
        if (gameObject.tag == "Soul" || gameObject.tag == "Item")
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
                gameMaster.UpDatePlayerMP(0.2f);
            }

            if(gameObject.tag == "Item")
            {
                if(itemData.no == 0)
                {
                    gameMaster.UpDatePlayerHP((int)-itemData.value);
                }
                else
                {
                    gameMaster.UpDatePlayerMP(itemData.value);
                }
            }

            Destroy(gameObject);
        }
    }

}
