using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "ItemDataSO", menuName = "CreateItemDataSO")]
public class ItemDetaSO : ScriptableObject
{
    public List<ItemData> itemDataList = new List<ItemData>();

    [Serializable]
    public class ItemData
    {
      public int no;
      public GameObject itemPrefab;
      public float value;
    }

}
