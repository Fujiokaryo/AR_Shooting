using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TargetIndicator : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    private Image arrow;

    private Camera mainCamera;
    private RectTransform rectTransform;


    public void SetUpTarget(Transform targetTran)
    {
        mainCamera = Camera.main;
        TryGetComponent(out rectTransform);
        target = targetTran;
        arrow.enabled = true;
    }

    private void LateUpdate()
    {
        var center = 0.5f * new Vector3(Screen.width, Screen.height);
        float canvasScale = transform.root.localScale.z;

        //(��ʒ��S�����_(0, 0)�Ƃ���)�A�^�[�Q�b�g�̃X�N���[�����W�����߂�
        if (target != null)
        {
            var pos = mainCamera.WorldToScreenPoint(target.position) - center;


            //�J��������ɂ���^�[�Q�b�g�̃X�N���[�����W�́A��ʒ��S�ɑ΂���_�Ώۂ̍��W�ɂ���
            if (pos.z < 0f)
            {
                pos.x = -pos.x;
                pos.y = -pos.y;

                //�J�����Ɛ����ȃ^�[�Q�b�g�̃X�N���[�����W��␳����
                if (Mathf.Approximately(pos.y, 0f))
                {
                    pos.y = -center.y;
                }
            }

            var halfSize = 0.5f * canvasScale * rectTransform.sizeDelta;
            float d = Mathf.Max(
                Mathf.Abs(pos.x / (center.x - halfSize.x)),
                Mathf.Abs(pos.y / (center.y - halfSize.y))
                );

            //�^�[�Q�b�g�̃X�N���[�����W����ʊO�Ȃ�A��ʒ[�ɂȂ�悤��������
            bool isOffscreen = pos.z < 0f || d > 1f;
            if (isOffscreen)
            {
                pos.x /= d;
                pos.y /= d;
            }
            rectTransform.anchoredPosition = pos / canvasScale;

            arrow.enabled = isOffscreen;
            if (isOffscreen)
            {
                arrow.rectTransform.eulerAngles = new Vector3
                (
                    0f, 0f, Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg
                );
            }
        }
    }
}
