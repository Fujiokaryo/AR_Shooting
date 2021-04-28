using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySearch : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> enemyList;

    public GameObject[] searchArrows;

    private void Awake()
    {
        searchArrows = GameObject.FindGameObjectsWithTag("SearchArrow");

        for(int i = 0; i < searchArrows.Length; i++)
        {
            searchArrows[i].GetComponent<Image>().enabled = false;
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
       enemyList.Add(enemy);

        int i = enemyList.Count - 1;                   
       
       enemyController.LinkSearchArrow(i, searchArrows[i], this) ;
            
        
    }

}
