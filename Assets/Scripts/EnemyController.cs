using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyExprode;

    [SerializeField]
    private GameObject downEffect;

    [SerializeField]
    private Slider enemyHpBar;

    [SerializeField]
    private ItemDetaSO itemDataSO;

    public int hp = 100;
    public int speed;
    public GameObject[] enemy;

    private EnemyGenerator enemyGenerator;
    private Animator anime;
    private GameObject target;
    private float targetDistance;
    private bool attackPlayer = false;
    private int attackPower;
    private float timer;
    private float attackTime = 1;
    private Vector3 soulPos = new Vector3(0, 5f, 0);
    private int maxHp;
    private GameMaster gameMaster;
    private bool isBoss;


    /// <summary>
    /// エネミーの初期設定
    /// </summary>
    /// <param name="target"></param>
    /// <param name="enemyGenerator"></param>
    public void SetUpEnemy(GameObject target, EnemyGenerator enemyGenerator, bool isBoss)
    {
        this.target = target;
        this.enemyGenerator = enemyGenerator;
        this.isBoss = isBoss;
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        anime = GetComponent<Animator>();
        enemyHpBar.value = 1.0f;
        transform.LookAt(target.transform);      
        
        if(isBoss == true)
        {
            gameObject.transform.localScale = gameObject.transform.localScale * 3;
            hp = 500;
        }

        maxHp = hp;
    }

    /// <summary>
    /// プレイヤーの近くまで前進
    /// </summary>
    private void Update()
    {
        Vector3 targetPos = new Vector3(target.transform.position.x, 0.1f, target.transform.position.z);

        if(attackPlayer == true)
        {          
            return;
        }

        if (isBoss == false)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPos, 8 * Time.deltaTime);
        }
        else
        {
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPos, 4 * Time.deltaTime);
        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Bullet")
        {
            //当たった魔法の情報を取得
            BulletDataSO.BulletData bulletData = other.gameObject.GetComponent<BulletController>().bulletData;

            if(bulletData.no == 0)
            {
                Damage(bulletData);
            }
            else if(bulletData.no == 1)
            {
                enemy = GameObject.FindGameObjectsWithTag("Enemy");

                Instantiate(other.gameObject.GetComponent<BulletController>().bulletDataSO.bulletDataList[2].bulletPrefab,
                    new Vector3(gameObject.transform.position.x, 2, gameObject.transform.position.z), Quaternion.identity);

                for (int i = 0; i < enemy.Length; i++)
                {
                    enemy[i].GetComponent<EnemyController>().Damage(bulletData);
                }
            }
            

        }


        if (other.gameObject.tag == "MainCamera")
        {
            ChangeAttackFlag();
        }
        
    }

    /// <summary>
    /// 魔法が当たった時のダメージ処理
    /// </summary>
    public void Damage(BulletDataSO.BulletData bulletData)
    {
        int itemDrop = Random.Range(0, 100);

        //敵のHPを減算、HP量にHPバーを同期
        hp -= bulletData.power;
        UpdateHpBarValue(hp, maxHp);

        //HPが0になったら魂を生成して敵を破壊
        if(hp <= 0)
        {
            hp = 0;
            enemyGenerator.DecreaseEnemyCount();

            if(isBoss == true)
            {
                gameMaster.ChengeBossClear();
                enemyGenerator.isBossBattle = false;
                
            }

            GameObject soul = Instantiate(downEffect, gameObject.transform.position + soulPos, Quaternion.identity);
            soul.GetComponent<DefeatEffect>().SetUpSoul(target);

            if(itemDrop < 10)
            {
                EnemyDropItem();
            }

            Destroy(gameObject);
        }
    }

    /// <summary>
    /// プレイヤーへの接近判定をして攻撃準備
    /// </summary>
    private void ChangeAttackFlag()
    {
        attackPlayer = true;
        StartCoroutine(AttanckEnemy());
    }

    /// <summary>
    /// 一定以上プレイヤーの近くにいたら自爆攻撃、自爆エフェクトの生成
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttanckEnemy()
    {
        yield return new WaitForSeconds(0.5f);

        gameMaster.UpDatePlayerHP(DecisionPower(isBoss));

        Instantiate(enemyExprode, gameObject.transform.position, Quaternion.identity);
        enemyGenerator.DecreaseEnemyCount();

        Destroy(gameObject);
    }

    /// <summary>
    /// 敵のHPバーの更新
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="maxHp"></param>
    private void UpdateHpBarValue(int hp, int maxHp)
    {
        enemyHpBar.value = (float)hp / (float)maxHp;
    }

    /// <summary>
    /// 敵の攻撃力決定
    /// </summary>
    /// <param name="isBoss"></param>
    /// <returns></returns>
    private int DecisionPower(bool isBoss)
    {
        if(isBoss == false)
        {
            attackPower = 20;
        }
        else
        {
            attackPower = 100;
        }

        return attackPower;
    }

    /// <summary>
    /// 敵からドロップするアイテムの生成
    /// </summary>
    private void EnemyDropItem()
    {
        int itemType = Random.Range(0, 5);

        GameObject item = Instantiate(SelectItem(itemType).itemPrefab, gameObject.transform.position + soulPos, Quaternion.identity);
        item.GetComponent<DefeatEffect>().SetUpItem(SelectItem(itemType), target);
    }

    /// <summary>
    /// ドロップするアイテムの種類の設定
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    private ItemDetaSO.ItemData SelectItem(int itemType)
    {
        
        ItemDetaSO.ItemData dropItem;

        if(itemType == 0)
        {
           dropItem = itemDataSO.itemDataList[1];
        }
        else
        {
            dropItem = itemDataSO.itemDataList[0];
        }

        return dropItem;
    }
}
