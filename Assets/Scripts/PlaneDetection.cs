using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class PlaneDetection : MonoBehaviour
{
    private ARPlaneManager arPlaneManager;
    private bool isPlaneVisible = true;

    private void Awake()
    {
        TryGetComponent(out arPlaneManager);
    }

    private void Update()
    {
        foreach(var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(isPlaneVisible);
        }
    }

    public void SetAllPlaneActivate(bool isSwitch)
    {
        isPlaneVisible = isSwitch;
    }
}
