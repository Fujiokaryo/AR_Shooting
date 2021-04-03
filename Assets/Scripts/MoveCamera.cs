using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 視点の移動
/// </summary>
public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private GameMaster gameMaster;

    public float dx;

    /// <summary>
    /// isBattleフラグがtrueの時のみ前進する
    /// </summary>
    void Update()
    {
        if (gameMaster.isBattle == false)
        {
            this.transform.position += new Vector3(dx * Time.deltaTime, 0, 0);
        }
    }
}
