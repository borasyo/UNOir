using UnityEngine;
using System.Collections;

public class FeverEffectFrame : MonoBehaviour
{
    /// <summary>
    /// 概要 : フィーバー時の外枠エフェクト
    /// Author : 大洞祥太
    /// </summary>

    SpriteRenderer m_SpriteRender = null;

    [SerializeField] float m_fTime = 0.15f;
    TriangleWave<Color> m_TriangleWaveColor = null;

    void Start ()
    {
        m_SpriteRender = GetComponent<SpriteRenderer> ();

        Color min = new Color (1,1,1,0);
        Color max = new Color (1,1,1,1);
        m_TriangleWaveColor = TriangleWaveFactory.Color (min, max, m_fTime);
    }

    // Update is called once per frame
    void Update ()
    {
        if (FeverEffectManager.Instance.GetFever ()) {

            m_TriangleWaveColor.Progress ();
            m_SpriteRender.color = m_TriangleWaveColor.CurrentValue;

        } 
        else {
            m_SpriteRender.color = new Color (1, 1, 1, 0);
        }
    }
}
