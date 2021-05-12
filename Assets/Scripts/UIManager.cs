using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtDebugMessage;

    [SerializeField]
    private SubmitBranchButton submitBranchButtonPrefab;

    [SerializeField]
    private List<SubmitBranchButton>submitBranchButtonsList = new List<SubmitBranchButton>();


    [SerializeField]
    private bool isSubmitBranch;

    [SerializeField]
    private int submitBranchNo;

    private void Start()
    {
        
    }
    public void DisplayDebug(string message)
    {
        //txtDebugMessage.text = message;
    }


    public void UpdateDisplayStopMotionCount(int stopMotionCount)
    {
        //txtStopMotionCount.text = stopMotionCount.ToString();
    }

    public IEnumerator GenerateBranchButtons(int[] branchNums, BranchDirectionType[] branchDirectionTypes)
    {
        isSubmitBranch = false;

        //•ªŠò‚Ì”‚¾‚¯ƒ{ƒ^ƒ“‚ğ¶¬
        for(int i = 0; i < branchNums.Length; i++)
        {
           
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
