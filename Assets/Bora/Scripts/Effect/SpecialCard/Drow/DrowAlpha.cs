using UnityEngine;
using System.Collections;

public class DrowAlpha : MonoBehaviour
{
    /// <summary>
    /// 概要 : 攻撃アップエフェクトのα値を一括管理
    /// Author : 大洞祥太
    /// </summary>

    #region Singleton

    private static DrowAlpha instance;

    public static DrowAlpha Instance {
        get {
            if (instance)
                return instance;

            instance = (DrowAlpha)FindObjectOfType (typeof(DrowAlpha));

            if (instance)
                return instance;

            GameObject obj = new GameObject ();
            obj.AddComponent<DrowAlpha> ();
            Debug.Log (typeof(DrowAlpha) + "が存在していないのに参照されたので生成");

            return instance;
        }
    }

    #endregion

    public float Alpha { get; private set; }

    TriangleWave<float> m_TriangleWaveFloat = null;

    void Awake ()
    {
        if (this != Instance) {
            Destroy (this.gameObject);
            return;
        }
    }

    void Start ()
    {
        float min = 0.0f;
        float max = 1.0f;
        float time = 0.2f;
        m_TriangleWaveFloat = TriangleWaveFactory.Float (min, max, time);
    }

    void Update ()
    {
        m_TriangleWaveFloat.Progress ();
        Alpha = m_TriangleWaveFloat.CurrentValue;
    }
}
