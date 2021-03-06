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

    private Transform canvasTran;
    float canvasScale;

    /// <summary>
    /// ターゲットを抜く
    /// </summary>
    public void ResetTarget() {
        target = null;
        arrow.enabled = false;
    }

    public void SetUpTarget(Transform targetTran, Transform canvasTran)
    {
        mainCamera = Camera.main;
        TryGetComponent(out rectTransform);
        target = targetTran;
        this.canvasTran = canvasTran;

    }

    private void LateUpdate()
    {
        var center = 0.5f * new Vector3(Screen.width, Screen.height);

        if(canvasTran != null) {
            canvasScale = canvasTran.localScale.z;
        }
        
        //画面中心を原点(0, 0)とした、ターゲットのスクリーン座標を求める
        if (target != null)
        {
            var pos = mainCamera.WorldToScreenPoint(target.position) - center;
            pos.y += 150f;


            //カメラ後方にあるターゲットのスクリーン座標は、画面中心に対する点対象の座標にする
            if (pos.z < 0f)
            {
                pos.x = -pos.x;
                pos.y = -pos.y;

                //カメラと水平なターゲットのスクリーン座標を補正する
                if (Mathf.Approximately(pos.y, 0f))
                {
                    pos.y = -center.y;
                }
            }

            //UI座標系の値をスクリーン座標系の値に変換する
            var halfSize = 0.5f * canvasScale * rectTransform.sizeDelta;
            float d = Mathf.Max(
                Mathf.Abs(pos.x / (center.x - halfSize.x)), Mathf.Abs(pos.y / (center.y - halfSize.y)));

            //ターゲットのスクリーン座標が画面外なら、画面端になるよう調整する
            bool isOffscreen = (pos.z < 0f || d > 1f);
            //Debug.Log("pos = " + pos.y);
            //Debug.Log(d);
            //Debug.Log("center = " + center.y);
            //Debug.Log("halfsize = " + halfSize.y);
            if (isOffscreen)
            {
                pos.x /= d;
                pos.y /= d;
            }
            rectTransform.anchoredPosition = pos / canvasScale;

            //ターゲットのスクリーン座標が画面外なら、ターゲットの方向を刺す矢印を表示する
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
