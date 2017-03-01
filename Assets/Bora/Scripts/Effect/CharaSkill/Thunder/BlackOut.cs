using UnityEngine;
using System.Collections;

public class BlackOut : MonoBehaviour
{
    /// <summary>
    /// 概要 : 背景を暗くする
    /// Author : 大洞祥太
    /// </summary>

    SpriteRenderer m_SpriteRender = null;
    float fAlpha = 0.0f;

    void Start ()
    {
        m_SpriteRender = GetComponent<SpriteRenderer> ();
    }

    public float Alpha {
        get { return fAlpha; }
        set { fAlpha = value; }
    }

    void Update ()
    {
        m_SpriteRender.color = new Color (0, 0, 0, fAlpha);
    }
}
