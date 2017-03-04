using UnityEngine;
using System.Collections;

public class ResetCard : MonoBehaviour
{
    /// <summary>
    /// 概要 : 
    /// Author : 大洞祥太
    /// </summary>

    [SerializeField] float m_fTime = 0.25f;

    [SerializeField] float m_fMaxAlpha = 0.75f;

    SpriteRenderer m_SpriteRender = null;
   
    TriangleWave<Color> m_TriangleWaveColor = null;

    void Start ()
    {
        m_SpriteRender = GetComponent<SpriteRenderer> ();

        Color min = new Color (1,1,1,0); 
        Color max = new Color (1,1,1,m_fMaxAlpha); 
        m_TriangleWaveColor = TriangleWaveFactory.Color (min, max, m_fTime);

        SetEnable (false);
    }

    void Update ()
    {
        m_TriangleWaveColor.Progress ();
        m_SpriteRender.color = m_TriangleWaveColor.CurrentValue;

        if (!m_TriangleWaveColor.IsReverseTiming)
            return;

        // 1周したので終了
        if (m_TriangleWaveColor.IsAdd) {
            SetEnable (false);
        }
    }

    public void Run ()
    {
        SetEnable (true);
    }

    void SetEnable(bool IsEnable)
    {
        this.enabled = IsEnable;
        m_SpriteRender.enabled = IsEnable;
    }
}
