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

    public int hp;
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

        //敵の初期Hp、100からgameLevelが1上がる毎に20ずつ増加
        hp = 80 + GameLevel.instance.gameLevel * 20;
        enemyHpBar.value = 1.0f;
        transform.LookAt(target.transform);      
        
        if(isBoss == true)
        {
            gameObject.transform.localScale = gameObject.transform.localScale * 3;

            //ボスの初期HP、500からgameLevelが1上がる毎に100ずつ増加
            hp = 400 + GameLevel.instance.gameLevel * 100;
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
        //attackLevelが1上がる毎に魔法の攻撃力が5ずつ上昇
        hp -= bulletData.power + GameLevel.instance.attackLevel * 5;
        BGMmanager.instance.PlaySE();
        UpdateHpBarValue(hp, maxHp);

        //HPが0になったら魂を生成して敵を破壊
        if(hp <= 0)
        {
            hp = 0;
            enemyGenerator.DecreaseEnemyCount();

            //ボスモンスターの場合
            if(isBoss == true)
            {
                //ボス撃破フラグの切り替え
                gameMaster.ChengeBossClear();
                enemyGenerator.isBossBattle = false;
            }

            //倒した敵の魂の生成、設定
            GameObject soul = Instantiate(downEffect, gameObject.transform.position + soulPos, Quaternion.identity);
            soul.GetComponent<DefeatEffect>().SetUpSoul(target);

            //アイテムを落とすかの判定
            if(itemDrop < 10)
            {
                EnemyDropItem();
            }

            gameMaster.CheckStageClear();

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

        //自爆したらプレイヤーのHPを攻撃力分減らす
        gameMaster.UpDatePlayerHP(DecisionPower(isBoss));

        //自爆演出の生成
        Instantiate(enemyExprode, gameObject.transform.position, Quaternion.identity);

        //自爆して消えた分の敵数を減らす
        if (hp > 0)
        {
            enemyGenerator.DecreaseEnemyCount();
        }

        gameMaster.CheckStageClear();
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
            //敵の基礎攻撃力を20としてGameLevelが上がる毎に攻撃力が10上昇
            attackPower = 10 + GameLevel.instance.gameLevel * 10;
        }
        else
        {
            //ボスの攻撃を即死攻撃に
            attackPower = gameMaster.playerMaxHp;
        }

        return attackPower;
    }

    /// <summary>
    /// 敵からドロップするアイテムの生成
    /// </summary>
    private void EnemyDropItem()
    {
        //生成するアイテムの種類をランダムで決定
        int itemType = Random.Range(0, 5);

        //アイテムの生成、設定
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
