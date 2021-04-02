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
            Damage();
        }


        if (other.gameObject.tag == "MainCamera")
        {
            ChangeAttackFlag();
        }
        
    }

    private void Damage()
    {
        hp -= 20;
        UpdateHpBarValue(hp, maxHp);

        if(hp <= 0)
        {
            hp = 0;
            enemyGenerator.enemyCount--;

            GameObject soul = Instantiate(downEffect, gameObject.transform.position + soulPos, Quaternion.identity);
            soul.GetComponent<DefeatEffect>().SetUpSoul(target);


            Destroy(gameObject);
        }
    }


    private void ChangeAttackFlag()
    {
        attackPlayer = true;
        StartCoroutine(AttanckEnemy());
    }

    private IEnumerator AttanckEnemy()
    {
        yield return new WaitForSeconds(1f);

        gameMaster.UpDatePlayerHP(20);

        Instantiate(enemyExprode, gameObject.transform.position, Quaternion.identity);
        enemyGenerator.enemyCount--;

        Destroy(gameObject);
    }

    private void UpdateHpBarValue(int hp, int maxHp)
    {
        enemyHpBar.value = (float)hp / (float)maxHp;
    }
}
