using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHighMagic : MonoBehaviour
{
    [SerializeField]
    private GameMaster gameMaster;

    [SerializeField]
    private BulletGenerator bulletGenerator;

    private bool useHighMagic;

    public void UseHighMagic()
    {
        bulletGenerator.useHighMagic = true;
        gameMaster.UpDatePlayerMP((float)-gameMaster.highMagicCost);
        
    }
}
