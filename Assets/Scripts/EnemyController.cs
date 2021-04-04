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
    /// �G�l�~�[�̏����ݒ�
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
    /// �v���C���[�̋߂��܂őO�i
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
            //�����������@�̏����擾
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
    /// ���@�������������̃_���[�W����
    /// </summary>
    public void Damage(BulletDataSO.BulletData bulletData)
    {
        int itemDrop = Random.Range(0, 100);

        //�G��HP�����Z�AHP�ʂ�HP�o�[�𓯊�
        hp -= bulletData.power;
        UpdateHpBarValue(hp, maxHp);

        //HP��0�ɂȂ����獰�𐶐����ēG��j��
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
    /// �v���C���[�ւ̐ڋߔ�������čU������
    /// </summary>
    private void ChangeAttackFlag()
    {
        attackPlayer = true;
        StartCoroutine(AttanckEnemy());
    }

    /// <summary>
    /// ���ȏ�v���C���[�̋߂��ɂ����玩���U���A�����G�t�F�N�g�̐���
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
    /// �G��HP�o�[�̍X�V
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="maxHp"></param>
    private void UpdateHpBarValue(int hp, int maxHp)
    {
        enemyHpBar.value = (float)hp / (float)maxHp;
    }

    /// <summary>
    /// �G�̍U���͌���
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
    /// �G����h���b�v����A�C�e���̐���
    /// </summary>
    private void EnemyDropItem()
    {
        int itemType = Random.Range(0, 5);

        GameObject item = Instantiate(SelectItem(itemType).itemPrefab, gameObject.transform.position + soulPos, Quaternion.identity);
        item.GetComponent<DefeatEffect>().SetUpItem(SelectItem(itemType), target);
    }

    /// <summary>
    /// �h���b�v����A�C�e���̎�ނ̐ݒ�
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
