using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySearch : MonoBehaviour
{
    //[SerializeField]
    //public List<GameObject> enemyList;

    private GameObject[] searchArrows;

    public TargetIndicator[] indicators;

    [SerializeField]
    private Transform canvasTran;


    private void Awake()
    {
        searchArrows = GameObject.FindGameObjectsWithTag("SearchArrow");

        indicators = new TargetIndicator[searchArrows.Length];

        for (int i = 0; i < searchArrows.Length; i++)
        {
            searchArrows[i].TryGetComponent(out indicators[i]);

            indicators[i].ResetTarget();
            //searchArrows[i].GetComponent<Image>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            AddListEnemy(other.gameObject);          
        }

    }

    private void AddListEnemy(GameObject enemy) 
    {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        //enemyList.Add(enemy);

        //int i = enemyList.Count - 1; 

        // �󂢂Ă���C���W�P�[�^�[��������
        for (int i = 0; i < indicators.Length; i++) {
            if (indicators[i].target == null) {
                enemyController.LinkSearchArrow(i, indicators[i], this, canvasTran);
                break;
            }
        }

        // TODO �C���W�P�[�^���v���t�@�u�ɂ��Ă����āAList �ɂ��Ă����āA����Ȃ����͑���


        //enemyController.LinkSearchArrow(i, indicators[i], this, canvasTran);


    }

}
