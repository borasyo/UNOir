using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class CardShuffle : MonoBehaviour {

	/// <summary>
	/// 概要 : 詰んだ時にカードをシャッフル
	/// Author : 大洞祥太
	/// </summary>

    TriangleWave<Vector3> m_TriangleWaveVector3 = null;
    int m_nOldHalfLapCnt = 0;

    UnoData m_UnoData = null;

    void Start()
    {
        m_UnoData = GetComponent<UnoData> ();

        Vector3 min = m_UnoData.GetInitPos;
        Vector3 max = GameObject.Find ("CardBackGround").transform.position; // カード枠の中心
        float time = 0.1f;
        m_TriangleWaveVector3 = TriangleWaveFactory.Vector3 (min ,max, time);

        this.enabled = false;

        // 固定更新処理
        this.UpdateAsObservable ()
            .Where (_ => this.enabled)
            .Subscribe (_ => {
                m_TriangleWaveVector3.Progress ();
                transform.position = m_TriangleWaveVector3.CurrentValue;
            });

        // 増減反転時の処理を行う
        m_TriangleWaveVector3.ObserveEveryValueChanged (x => x.IsAdd)
            .Subscribe (IsAdd => {
                if (!IsAdd) {
                    m_UnoData.Change();
                } else {
                    transform.position = m_UnoData.GetInitPos;
                    this.enabled = false;
                }
            });
    }

    public void Run()
    {
        this.enabled = true;
    }

    public bool IsShuffle 
    { 
        get { return this.enabled; } 
        private set { this.enabled = value; } 
    } 
}
