using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    /// <summary>
    /// 概要 : 敵死亡処理
    /// Author : 大洞祥太
    /// </summary>

    [SerializeField]
    Material m_Material;
    public Shader m_Shader = null;
    YieldInstruction m_Instruction = new WaitForEndOfFrame ();
    public float fMinCutOff = 0.0f;
    public float fMaxCutOff = 1.0f;
    public float fDuration = 1.0f;
    float fNowCutOff = 0.0f;
    bool bEnd = false;

    public Texture DissolveTex = null;

    TriangleWave<float> m_TriangleWaveFloat = null;

    void Awake ()
    {
        Material mat = new Material (m_Shader);
        m_Material = GetComponent<SpriteRenderer> ().material = mat;
        mat.SetTexture ("_DissolveTex", DissolveTex);
        m_Material.SetFloat ("_CutOff", fMinCutOff);

        transform.DetachChildren ();

        m_TriangleWaveFloat = TriangleWaveFactory.Float (fMinCutOff, fMaxCutOff, fDuration);

        SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_ENEMYDEATH);
    }

    void Update ()
    {
        m_TriangleWaveFloat.Progress ();
        fNowCutOff = m_TriangleWaveFloat.CurrentValue;
        m_Material.SetFloat ("_CutOff", fNowCutOff);

        if (m_TriangleWaveFloat.GetHalfLapCnt <= 0)
            return;

        bEnd = true;
        Destroy (this.gameObject);
    }

    public bool End {
        get { return bEnd; }
    }
}
