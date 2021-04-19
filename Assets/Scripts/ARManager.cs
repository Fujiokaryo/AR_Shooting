using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ARManager : MonoBehaviour
{
    [SerializeField]
    private GameObject objPrefab = null;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private GameObject field;

    public bool isARDebug;

    private PlaneDetection planeDetection;

    private GameObject obj;

    private ARRaycastManager raycastManager;

    private List<ARRaycastHit> raycastHitList = new List<ARRaycastHit>();

    private FieldAutoScroller fieldAutoScroller;

    public enum ARState
    {
        None, 
        Wait, //待機。どのステートにも属さない状態
        Tracking, //平面感知中
        Ready, //ゲーム待機中
        Play, //ゲーム中
        GameUp, //ゲーム終了
    }

    public ARState currentARState;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();

        TryGetComponent(out planeDetection);

        fieldAutoScroller = GetComponentInChildren<FieldAutoScroller>();
    }

    private void Update()
    {
        if(currentARState == ARState.None)
        {
            return;
        }

        if(Input.touchCount < 0)
        {
            return;
        }

        if(currentARState == ARState.Tracking)
        {
            //平面感知
            TrackingPlane();
        }
        else if(currentARState == ARState.Ready)
        {
            currentARState = ARState.Wait;
            uiManager.DisplayDebug(currentARState.ToString());

            //ゲーム開始の準備
            StartCoroutine(PraparateGameReady());
        }
        else if(currentARState == ARState.Play)
        {
            uiManager.DisplayDebug(currentARState.ToString());
        }
    }


    private void TrackingPlane()
    {
        Touch touch = Input.GetTouch(0);

        if(touch.phase != TouchPhase.Ended)
        {
            return;
        }

        if(raycastManager.Raycast(touch.position, raycastHitList, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = raycastHitList[0].pose;

            if (obj == null)
            {
                uiManager.DisplayDebug("Raycast 成功");

                field.SetActive(true);

                currentARState = ARState.Ready;
            }
            else
            {
                uiManager.DisplayDebug("Raycast 済");
                obj.transform.position = hitPose.position;
            }
        }
        else
        {
            uiManager.DisplayDebug("Raycast 失敗");
        }
    }

    private IEnumerator PraparateGameReady()
    {
        //TODO　準備処理を書く

        yield return new WaitForSeconds(2.0f);

        currentARState = ARState.Play;

        uiManager.DisplayDebug(currentARState.ToString());

        if (isARDebug == true)
        {
            StartCoroutine(fieldAutoScroller.StartFieldScroll());
        }

        //平面検知を非表示
        planeDetection.SetAllPlaneActivate(false);
    }
}
