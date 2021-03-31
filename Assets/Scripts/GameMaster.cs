using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public bool isBattle;

    public void ChangeBattle(bool isBattle = false)
    {
        this.isBattle = isBattle;
    }
}
