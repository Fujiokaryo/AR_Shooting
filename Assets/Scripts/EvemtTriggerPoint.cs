using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvemtTriggerPoint : MonoBehaviour
{
    private GameMaster gameMaster;

    [SerializeField, Header("��������C�x���g�̎��")]
    private EventType[] eventTypes;

    [SerializeField, Header("��������C�x���g�̔ԍ�")]
    private int[] eventNos;

    [SerializeField, Tooltip("�C�x���g�̔����n�_")]
    private Transform[] eventTrans;

    [SerializeField, Header("��������C�x���g")]
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
            Debug.Log("�ʉ�");

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
