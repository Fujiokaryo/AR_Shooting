using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int hp = 100;
    public int speed;
    private GameObject target;
    private float targetDistance;


    public void SetUpEnemy(GameObject target)
    {
        this.target = target;

        transform.LookAt(target.transform);        
    }

    private void Update()
    {
        this.gameObject.transform.position = Vector3.MoveTowards(transform.position,new Vector3(target.transform.position.x, 0.1f, target.transform.position.z)
            , 6 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Damage();
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

}
