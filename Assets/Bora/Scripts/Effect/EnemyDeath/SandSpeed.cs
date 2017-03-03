using UnityEngine;
using System.Collections;

public class SandSpeed : MonoBehaviour
{
    /// <summary>
    /// 概要 : 敵死亡時の砂煙エフェクトの移動処理
    /// Author : 大洞祥太
    /// </summary>

    public float fTime = 1.0f;
    public float fMove = 1.0f;

    TriangleWave<Vector3> m_TriangleWave = null;

    void Start ()
    {
        Vector3 min = transform.position;
        Vector3 max = min + new Vector3 (fMove, 0, 0);
        m_TriangleWave = TriangleWaveFactory.Vector3 (min, max, fTime);
    }

    void Update ()
    {
        m_TriangleWave.Progress ();
        transform.position = m_TriangleWave.CurrentValue;
    }
}
