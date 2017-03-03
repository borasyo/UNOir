using UnityEngine;
using System.Collections;

public class FeverEffect : MonoBehaviour
{
    /// <summary>
    /// 概要 : フィーバー時のエフェクト
    /// Author : 大洞祥太
    /// </summary>

    UnoData m_UnoData = null;
    SpriteRenderer m_SpriteRender = null;

    float m_fAddTime = 0.15f;
    TriangleWave<Color> m_TriangleWaveColor = null;

    float m_fChangeTime = 0.15f;
    Vector3 m_MaxScale = Vector3.zero;
    TriangleWave<Vector3> m_TriangleVector3 = null;

    // Use this for initialization
    void Start ()
    {
        m_SpriteRender = GetComponent<SpriteRenderer> ();
        m_SpriteRender.sortingOrder = -1;
        m_UnoData = GetComponentInParent<UnoData> ();

        Color min = new Color (1, 1, 1, 0);
        Color max = new Color (1, 1, 1, 1);
        m_TriangleWaveColor = TriangleWaveFactory.Color (min, max, m_fAddTime);

        m_MaxScale = new Vector3 (1.2f, 1.2f, 1.2f);
        m_TriangleVector3 = TriangleWaveFactory.Vector3 (Vector3.zero, m_MaxScale, m_fChangeTime);
    }

    // Update is called once per frame
    void Update ()
    {
        // Fever中の処理
        if (FeverEffectManager.Instance.GetFever ()) {

            if (m_TriangleVector3.GetHalfLapCnt <= 0) {
                m_TriangleVector3.Progress ();
                transform.localScale = m_TriangleVector3.CurrentValue;
            } else {
                m_TriangleWaveColor.Progress ();
                m_SpriteRender.color = m_TriangleWaveColor.CurrentValue;
            }
            m_SpriteRender.sortingOrder = m_UnoData.GetOrder () - 1;
            return;
        } 

        // Fever後の処理
        if (m_TriangleVector3.GetHalfLapCnt <= 1) {
            m_TriangleVector3.Progress ();
            transform.localScale = m_TriangleVector3.CurrentValue;
        } else {
            transform.localScale = Vector3.zero;
        }
    }
}
