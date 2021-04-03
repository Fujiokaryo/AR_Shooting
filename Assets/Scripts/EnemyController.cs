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

    public int hp = 100;
    public int speed;

    private EnemyGenerator enemyGenerator;
    private Animator anime;
    private GameObject target;
    private float targetDistance;
    private bool attackPlayer = false;
    private float timer;
    private float attackTime = 1;
    private Vector3 soulPos = new Vector3(0, 5f, 0);
    private int maxHp;
    private GameMaster gameMaster;
    
    /// <summary>
    /// エネミーの初期設定
    /// </summary>
    /// <param name="target"></param>
    /// <param name="enemyGenerator"></param>
    public void SetUpEnemy(GameObject target, EnemyGenerator enemyGenerator)
    {
        this.target = target;
        this.enemyGenerator = enemyGenerator;
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        anime = GetComponent<Animator>();
        maxHp = hp;
        enemyHpBar.value = 1.0f;
        transform.LookAt(target.transform);        
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

        this.gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPos, 6 * Time.deltaTime);

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Bullet")
        {
            //当たった魔法の情報を取得
            BulletDataSO.BulletData bulletData = other.gameObject.GetComponent<BulletController>().bulletData;

            Damage(bulletData);

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
        //敵のHPを減算、HP量にHPバーを同期
        hp -= bulletData.power;
        UpdateHpBarValue(hp, maxHp);

        //HPが0になったら魂を生成して敵を破壊
        if(hp <= 0)
        {
            hp = 0;
            enemyGenerator.enemyCount--;

            GameObject soul = Instantiate(downEffect, gameObject.transform.position + soulPos, Quaternion.identity);
            soul.GetComponent<DefeatEffect>().SetUpSoul(target);


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
    /// 1秒以上プレイヤーの近くにいたら自爆攻撃、自爆エフェクトの生成
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttanckEnemy()
    {
        yield return new WaitForSeconds(1f);

        gameMaster.UpDatePlayerHP(20);

        Instantiate(enemyExprode, gameObject.transform.position, Quaternion.identity);
        enemyGenerator.enemyCount--;

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
}
