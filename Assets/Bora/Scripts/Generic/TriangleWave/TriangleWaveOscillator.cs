using UnityEngine;
using System.Collections;

public class TriangleWaveOscillator
{
    float m_fTime;
    public float m_fValue = 0f; // 0-1 の範囲

    float m_fHarfPeriod_Sec; // 間隔(半往復)
    public void Set (float sec) { m_fHarfPeriod_Sec = sec; }

    public virtual void Progress()
    {
        // 指定した間隔で補正したdeltaTimeを加算
        m_fTime += (Time.deltaTime);// / interval_sec);

        float period = m_fHarfPeriod_Sec * 2.0f; // 半周期を1周期に変換
        m_fValue = Mathf.Acos (Mathf.Cos (2.0f * Mathf.PI * m_fTime / period)) / Mathf.PI;
    }
}
