using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int hp = 100;
    public int speed;

    private Animator anime;
    private GameObject target;
    private float targetDistance;
    private bool attackPlayer = false;
    private float timer;
    private float attackTime = 1;


    public void SetUpEnemy(GameObject target)
    {
        this.target = target;
        anime = GetComponent<Animator>();
        transform.LookAt(target.transform);        
    }

    private void Update()
    {
        if(attackPlayer == true)
        {
            EnemyAttack();

            return;
        }

        

        this.gameObject.transform.position = Vector3.MoveTowards(transform.position,new Vector3(target.transform.position.x, 0.1f, target.transform.position.z)
            , 6 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    { 
    
            Damage();

            if (other.gameObject.tag == "MainCamera")
            {      

                ChangeAttackFlag();
            }
        
    }

    private void Damage()
    {
        hp -= 20;

        if(hp < 0)
        {
            hp = 0;
            GameObject.Find("GameMaster").GetComponent<GameMaster>().ChangeBattle();
            Destroy(gameObject);
        }
    }

    private void EnemyAttack()
    {
        timer += Time.deltaTime;

        if(timer > attackTime)
        {
            timer = 0;
            anime.SetTrigger("Attack");
        }
    }

    private void ChangeAttackFlag()
    {
        attackPlayer = true;
    }
}
