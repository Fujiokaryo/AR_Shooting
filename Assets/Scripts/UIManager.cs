using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtDebugMessage;

    [SerializeField]
    private Button btnStopMotion;

    [SerializeField]
    private FieldAutoScroller autoScroller;

    [SerializeField]
    private Text txtStopMotionCount;

    [SerializeField]
    private SubmitBranchButton submitBranchButtonPrefab;

    [SerializeField]
    private List<SubmitBranchButton>submitBranchButtonsList = new List<SubmitBranchButton>();

    [SerializeField]
    private Transform rightBranchTran;

    [SerializeField]
    private Transform leftBranchTran;

    [SerializeField]
    private Transform centerBranchTran;

    [SerializeField]
    private bool isSubmitBranch;

    [SerializeField]
    private int submitBranchNo;

    private void Start()
    {
        btnStopMotion.onClick.AddListener(OnClickStopMotion);
    }
    public void DisplayDebug(string message)
    {
        txtDebugMessage.text = message;
    }

    private void OnClickStopMotion()
    {
        autoScroller.StopAndPlayMotion();
    }

    public void UpdateDisplayStopMotionCount(int stopMotionCount)
    {
        txtStopMotionCount.text = stopMotionCount.ToString();
    }

    public IEnumerator GenerateBranchButtons(int[] branchNums, BranchDirectionType[] branchDirectionTypes)
    {
        isSubmitBranch = false;

        //分岐の数だけボタンを生成
        for(int i = 0; i < branchNums.Length; i++)
        {
            //ボタンの生成位置を設定
            Transform branchTran = BranchDirectionType.Right == branchDirectionTypes[i] ? rightBranchTran : BranchDirectionType.Left == branchDirectionTypes[i] ? leftBranchTran : centerBranchTran;

            //ボタン生成
            SubmitBranchButton submitBranchButton = Instantiate(submitBranchButtonPrefab, branchTran, false);

            //ボタン設定
            submitBranchButton.SetUpSubmitBranchButton(branchNums[i], this);

            //Listに追加
            submitBranchButtonsList.Add(submitBranchButton);
        }
        yield return null;
    }

    public void SubmitBranch(int rootNo)
    {
        for(int i = 0; i < submitBranchButtonsList.Count; i++)
        {
            //
            submitBranchButtonsList[i].InactivateSubmitButton();
            Destroy(submitBranchButtonsList[i].gameObject);
        }
        submitBranchButtonsList.Clear();

        submitBranchNo = rootNo;
        isSubmitBranch = true;
    }

    public(bool, int)GetSubmitBranch()
    {
        return (isSubmitBranch, submitBranchNo);
    }
}
