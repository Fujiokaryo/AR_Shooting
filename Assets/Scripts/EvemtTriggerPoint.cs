using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvemtTriggerPoint : MonoBehaviour
{
    private GameMaster gameMaster;

    [SerializeField, Header("発生するイベントの種類")]
    private EventType[] eventTypes;

    [SerializeField, Header("発生するイベントの番号")]
    private int[] eventNos;

    [SerializeField, Tooltip("イベントの発生地点")]
    private Transform[] eventTrans;

    [SerializeField, Header("発生するイベント")]
    private EventDataSO.EventData[] eventDatas;

    public void SetUpEventTriggerPoint(GameMaster gameMaster)
    {
        this.gameMaster = gameMaster;

        eventDatas = new EventDataSO.EventData[eventTypes.Length];

        //
        for(int i = 0; i < eventTypes.Length; i++)
        {
            eventDatas[i] = DataBaseManager.instance.GetEventDataFromEventType(eventTypes[i], eventNos[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("通過");

            //
            for(int i = 0; i < eventDatas.Length; i++)
            {
                switch(eventTypes[i])
                {
                    case EventType.Enemy:
                        gameMaster.GenerateEnemy(eventDatas[i], eventTrans[i]);
                        continue;

                    case EventType.Gimmick:
                        gameMaster.GenerateGimmick(eventDatas[i], eventTrans[i]);
                        continue;
                }
            }
        }
    }
}
