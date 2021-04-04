using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法の生成
/// </summary>
public class BulletGenerator : MonoBehaviour
{
    [SerializeField]
    private GameMaster gameMaster;

    [SerializeField]
    public BulletDataSO bulletDataSO;

    public bool useHighMagic;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ShotBullet();
        }
    }

    /// <summary>
    /// クリックした場所に向かって魔法を発射
    /// </summary>
    private void ShotBullet()
    {
        BulletDataSO.BulletData bulletData = null;
        GameObject bullet = null;

        bulletData = SelectMagic();

        bullet = Instantiate(bulletData.bulletPrefab, transform.position, Quaternion.identity);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldDir = ray.direction;
        bullet.GetComponent<BulletController>().Shot(worldDir.normalized * bulletData.speed, bulletData, bulletDataSO);

        if(bulletData.no == 1)
        {
            useHighMagic = false;
        }
    }

    BulletDataSO.BulletData SelectMagic()
    {
        BulletDataSO.BulletData bulletData = null;

        if(useHighMagic == true)
        {
            bulletData = bulletDataSO.bulletDataList[1];
        }
        else
        {
            bulletData = bulletDataSO.bulletDataList[0];
        }

        return bulletData;
    }
}
