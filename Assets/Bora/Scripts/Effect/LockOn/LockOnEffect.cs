using UnityEngine;
using System.Collections;

public class LockOnEffect : MonoBehaviour
{
    /// <summary>
    /// 概要 : 敵ロックオン時のエフェクト
    /// Author : 大洞祥太
    /// </summary>

    [SerializeField]
    float m_fScaleTime = 0.5f;

    [SerializeField]
    float m_fRotTime = 1.0f;

    [SerializeField]
    Vector3 AddScale = new Vector3 (0.3f, 0.3f, 0.0f);

    TriangleWave<Vector3> m_TriangleWaveVector3 = null;

    void Start ()
    {
        Vector3 min = transform.localScale;
        Vector3 max = min + AddScale;
        m_TriangleWaveVector3 = TriangleWaveFactory.Vector3 (min, max, m_fScaleTime);
    }

    void Update ()
    {
        transform.eulerAngles += new Vector3 (0, 0, 360 * (Time.deltaTime / m_fRotTime));

        m_TriangleWaveVector3.Progress ();
        transform.localScale = m_TriangleWaveVector3.CurrentValue;
    }
}
