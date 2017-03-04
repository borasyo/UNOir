using UnityEngine;
using System.Collections;

public class FeverCard : MonoBehaviour
{
    /// <summary>
    /// 概要 : フィーバー時のカード
    /// Author : 大洞祥太
    /// </summary>

    SpriteRenderer m_SpriteRender = null;
    UnoData m_UnoData = null;

    TriangleWave<Color> m_TriangleWaveColor = null;

    void Start ()
    {
        m_SpriteRender = GetComponent<SpriteRenderer> ();
        m_SpriteRender.sortingOrder = 22;
        m_UnoData = GetComponentInParent<UnoData> ();

        Color min = new Color (1.0f, 1.0f, 1.0f, 0.0f);
        Color max = new Color (1.0f, 1.0f, 1.0f, 0.5f);
        float time = 0.15f;

        m_TriangleWaveColor = TriangleWaveFactory.Color (min, max, time);
    }

    void Update ()
    {
        if (!FeverEffectManager.Instance.GetFever () || m_UnoData.OnClick) {
            
            StartCoroutine (WaitChangeEnable ());
            return;
        }

        m_TriangleWaveColor.Progress ();
        m_SpriteRender.color = m_TriangleWaveColor.CurrentValue;
    }

    // スプライトのレンダラーを一時的にOffにする
    IEnumerator WaitChangeEnable ()
    {
        m_SpriteRender.enabled = false;

        yield return new WaitWhile (() => (!FeverEffectManager.Instance.GetFever () || m_UnoData.OnClick) == true);

        m_SpriteRender.enabled = true;
    }
}
