using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatEffect : MonoBehaviour
{
    private float timer;
    private GameObject target;

    public void SetUpSoul(GameObject target)
    {
        this.target = target;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        

        if (gameObject.tag == "Soul")
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, 5, target.transform.position.z);

            if (timer > 0.8f)
            {
                this.gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPos, 20 * Time.deltaTime);
            }
        }

        if(timer >1.5f)
        {
            Destroy(gameObject);
        }
    }

}
