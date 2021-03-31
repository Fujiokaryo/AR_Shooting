using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public GameObject bullletPrefab;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ShotBullet();
        }
    }

    private void ShotBullet()
    {
        GameObject bullet = Instantiate(bullletPrefab, transform.position, Quaternion.identity);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldDir = ray.direction;
        bullet.GetComponent<BulletController>().Shot(worldDir.normalized * 2000);
    }
}
