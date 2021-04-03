using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���_�̈ړ�
/// </summary>
public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private GameMaster gameMaster;

    public float dx;

    /// <summary>
    /// isBattle�t���O��true�̎��̂ݑO�i����
    /// </summary>
    void Update()
    {
        if (gameMaster.isBattle == false)
        {
            this.transform.position += new Vector3(dx * Time.deltaTime, 0, 0);
        }
    }
}
