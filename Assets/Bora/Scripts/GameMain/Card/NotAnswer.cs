using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotAnswer : MonoBehaviour
{
    /// <summary>
    /// 概要 : 出せないカードだった時の処理
    /// Author : 大洞祥太
    /// </summary>

    public float fLife = 0.5f;
    float fInitLife = 0.0f;
    SpriteRenderer spriteRenderer = null;
    int nOrderinLayer = 22;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        fInitLife = fLife;
        spriteRenderer.color = new Color (1, 1, 1, 0);
        spriteRenderer.sortingOrder = nOrderinLayer;

        this.enabled = false;
    }

    void Update ()
    {
        fLife -= Time.deltaTime;
        spriteRenderer.color = new Color (1, 1, 1, fLife / fInitLife);

        if (fLife <= 0.0f) {
            this.enabled = false;
        }
    }

    public void Run ()
    {
        this.enabled = true;
        fLife = fInitLife;
        spriteRenderer.color = new Color (1, 1, 1, 1);
    }
}
