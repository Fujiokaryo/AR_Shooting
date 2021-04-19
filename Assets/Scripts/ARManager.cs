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
        Wait, //�ҋ@�B�ǂ̃X�e�[�g�ɂ������Ȃ����
        Tracking, //���ʊ��m��
        Ready, //�Q�[���ҋ@��
        Play, //�Q�[����
        GameUp, //�Q�[���I��
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
            //���ʊ��m
            TrackingPlane();
        }
        else if(currentARState == ARState.Ready)
        {
            currentARState = ARState.Wait;
            uiManager.DisplayDebug(currentARState.ToString());

            //�Q�[���J�n�̏���
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
                uiManager.DisplayDebug("Raycast ����");

                field.SetActive(true);

                currentARState = ARState.Ready;
            }
            else
            {
                uiManager.DisplayDebug("Raycast ��");
                obj.transform.position = hitPose.position;
            }
        }
        else
        {
            uiManager.DisplayDebug("Raycast ���s");
        }
    }

    private IEnumerator PraparateGameReady()
    {
        //TODO�@��������������

        yield return new WaitForSeconds(2.0f);

        currentARState = ARState.Play;

        uiManager.DisplayDebug(currentARState.ToString());

        if (isARDebug == true)
        {
            StartCoroutine(fieldAutoScroller.StartFieldScroll());
        }

        //���ʌ��m���\��
        planeDetection.SetAllPlaneActivate(false);
    }
}
