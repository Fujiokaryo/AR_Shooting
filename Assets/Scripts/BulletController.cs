using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletDataSO.BulletData bulletData;
    private BulletDataSO bulletDataSO;
    public GameObject[] enemy;
    public void Shot(Vector3 dir, BulletDataSO.BulletData bulletData, BulletDataSO bulletDataSO)
    {
        this.bulletData = bulletData;
        this.bulletDataSO = bulletDataSO;

        GetComponent<Rigidbody>().AddForce(dir);

        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(bulletData.no == 1)
        {
            enemy = GameObject.FindGameObjectsWithTag("Enemy");
            Debug.Log(enemy.Length);
            Instantiate(bulletDataSO.bulletDataList[2].bulletPurefab, 
                new Vector3(gameObject.transform.position.x, 2, gameObject.transform.position.z), Quaternion.identity);

            for(int i = 0; i < enemy.Length; i++)
            {
               enemy[i].GetComponent<EnemyController>().Damage(bulletData);
            }
        }

        Destroy(gameObject);
    }

}
