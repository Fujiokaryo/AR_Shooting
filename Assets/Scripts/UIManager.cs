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

        //����̐������{�^���𐶐�
        for(int i = 0; i < branchNums.Length; i++)
        {
            //�{�^���̐����ʒu��ݒ�
            Transform branchTran = BranchDirectionType.Right == branchDirectionTypes[i] ? rightBranchTran : BranchDirectionType.Left == branchDirectionTypes[i] ? leftBranchTran : centerBranchTran;

            //�{�^������
            SubmitBranchButton submitBranchButton = Instantiate(submitBranchButtonPrefab, branchTran, false);

            //�{�^���ݒ�
            submitBranchButton.SetUpSubmitBranchButton(branchNums[i], this);

            //List�ɒǉ�
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
