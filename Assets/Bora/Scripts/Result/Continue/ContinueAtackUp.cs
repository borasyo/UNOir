using UnityEngine;
using System.Collections;

public class ContinueAtackUp : MonoBehaviour
{
    /// <summary>
    /// 概要 : コンティニュー時の攻撃アップ処理
    /// Author : 大洞祥太
    /// </summary>

    float fAtackAmount = 1.5f;

    void Start ()
    {
        TurnData.Instance.AddContinueAtack (GetComponent<ContinueAtackUp> ());
    }

    public float AtackAmount {
        get { return fAtackAmount; }
    }
}
