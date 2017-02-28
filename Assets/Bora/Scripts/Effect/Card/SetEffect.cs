using UnityEngine;
using System.Collections;

public class SetEffect : MonoBehaviour
{
    /// <summary>
    /// 概要 : 	カードを出した時のエフェクト
    /// Author : 大洞祥太
    /// </summary>

    Vector3 m_InitScale = Vector3.zero;
    ParticleSystem[] m_Particle = null;
    Vector3[] m_InitSize = null;

    [SerializeField] float[] m_Scale = null;
    [SerializeField] Color[] m_FourColor = new Color[4];

    void Start ()
    {
        m_Particle = new ParticleSystem[4];
        m_InitSize = new Vector3[4];
        m_InitScale = transform.localScale;

        for (int i = 0; i < transform.childCount; i++) {
            m_Particle [i] = transform.GetChild (i).GetComponent<ParticleSystem> ();
            m_InitSize [i] = m_Particle [i].transform.localScale;

            for (int j = 1; j <= m_Particle [i].transform.childCount; j++) {
                m_Particle [i + j] = m_Particle [i].transform.GetChild (j - 1).GetComponent<ParticleSystem> ();
                m_InitSize [i + j] = m_Particle [i + j].transform.localScale; 
            }
        }
    }

    public void ChangeEffectAmount (int raw_Amount, UnoStruct.eColor color)
    {

        Color nowColor = new Color (1, 1, 1, 1);

        if (color != UnoStruct.eColor.COLOR_WILD) {
            nowColor = m_FourColor [(int)color];
        } else {
            nowColor = m_FourColor [Random.Range (0, m_FourColor.Length)];
        }

        // カードを出していない時は終了
        if (raw_Amount <= 0)
            return;

        int nAmount = (raw_Amount - 1) / 4;

        for (int i = 0; i < m_Particle.Length; i++) {
            m_Particle [i].transform.localScale = m_InitSize [i] * m_Scale [nAmount];
            m_Particle [i].startColor = nowColor;
        }
    }
}
