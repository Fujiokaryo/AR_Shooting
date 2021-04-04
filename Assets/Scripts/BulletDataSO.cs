using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BulletDataSO", menuName = "CreateBulletDataSO")]
public class BulletDataSO : ScriptableObject
{
    public List<BulletData> bulletDataList = new List<BulletData>();

    [Serializable]
    public class BulletData
    {
        public int no;
        public GameObject bulletPrefab;
        public int power;
        public int speed;
    }

}
