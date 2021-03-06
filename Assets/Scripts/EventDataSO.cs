using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EventDataSO", menuName = "Create EventDataSO")]
public class EventDataSO : ScriptableObject
{
    public EventType eventType;

    public List<EventData> eventDatasList = new List<EventData>();

    [Serializable]
    public class EventData
    {
        public int eventNo;
        public GameObject eventPrefab;
    }

}
