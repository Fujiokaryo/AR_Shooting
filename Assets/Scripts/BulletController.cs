using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
  
    public void Shot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);

        Destroy(gameObject, 2f);
    }

}
