using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrowEffectManager : MonoBehaviour
{
    /// <summary>
    /// 概要 : 攻撃アップエフェクト管理
    /// Author : 大洞祥太
    /// </summary>

    #region Singleton

    private static DrowEffectManager instance;

    public static DrowEffectManager Instance {
        get {
            if (instance)
                return instance;

            instance = (DrowEffectManager)FindObjectOfType (typeof(DrowEffectManager));

            if (instance)
                return instance;

            GameObject obj = new GameObject ();
            obj.AddComponent<DrowEffectManager> ();
            Debug.Log (typeof(DrowEffectManager) + "が存在していないのに参照されたので生成");

            return instance;
        }
    }

    #endregion

    Dictionary<UnoStruct.eColor, bool> DictColor = new Dictionary<UnoStruct.eColor, bool> ();

    void Awake ()
    {
        if (this != Instance) {
            Destroy (this.gameObject);
            return;
        }

        for (int i = 0; i < (int)UnoStruct.eColor.COLOR_MAX; i++) {
            DictColor.Add ((UnoStruct.eColor)i, false);
        }
    }

    public bool GetUse (UnoStruct.eColor color)
    {
        return DictColor [color];
    }

    public void Run (UnoStruct.eColor color, bool bUse)
    {
        DictColor [color] = bUse;
    }
}
