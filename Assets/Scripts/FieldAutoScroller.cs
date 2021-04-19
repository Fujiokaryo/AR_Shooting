using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class FieldAutoScroller : MonoBehaviour
{
    [SerializeField]
    private List<PathData> pathDatasList = new List<PathData>();

    Tween tween;

    private bool isPause;

    [SerializeField, Tooltip("�ꎞ��~�\��")]
    private int stopMotionCount;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private GameMaster GameMaster;

    public Vector3 targetPos;
    public int currentTargetPathCount;

    public IEnumerator StartFieldScroll()
    {
        yield return null;

        Vector3[] paths = pathDatasList.Select(x => x.pathTran.position).ToArray();
        float totalTime = pathDatasList.Select(x => x.scrollDuration).Sum();

        Debug.Log(totalTime);
        tween = transform.DOPath(paths, totalTime).SetEase(Ease.Linear);

        uiManager.DisplayDebug("�ړ��J�n");

        //uiManager.UpDateDisplayStopMotionCount(stopMotionCount);

        targetPos = pathDatasList[pathDatasList.Count - 1].pathTran.position;

        currentTargetPathCount = 1;
        

    }

    private void Update()
    {
        //�ꎟ��~�ƍĊJ����
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StopAndPlayMotion();
        }

        //�ړ������̊m�F
        if(transform.position == targetPos && currentTargetPathCount == 1)
        {
            currentTargetPathCount = 0;

            StartCoroutine(GameMaster.CheckNextRootBranch());

            Debug.Log("����m�F");
        }
    }

    public void StopAndPlayMotion()
    {
        if(isPause)
        {
            transform.DOPlay();
            //�G�𓮂���
        }
        else if(!isPause)
        {
            transform.DOPause();
            //�G���~�߂�
        }
        isPause = !isPause;
    }

    public void SetNextField(List<PathData>nextPathDataList)
    {
        pathDatasList = new List<PathData>(nextPathDataList);

        StartCoroutine(StartFieldScroll());
    }
}
