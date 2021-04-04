using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletDataSO.BulletData bulletData;
    public BulletDataSO bulletDataSO;
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

        Destroy(gameObject);
    }

}
