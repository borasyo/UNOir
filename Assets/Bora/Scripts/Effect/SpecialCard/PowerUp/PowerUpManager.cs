using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    /// <summary>
    /// 概要 : 攻撃アップ中かを管理
    /// Author : 大洞祥太
    /// </summary>

    #region Singleton

    private static PowerUpManager instance;

    public static PowerUpManager Instance {
        get {
            if (instance)
                return instance;

            instance = (PowerUpManager)FindObjectOfType (typeof(PowerUpManager));

            if (instance)
                return instance;

            GameObject obj = new GameObject ();
            obj.AddComponent<PowerUpManager> ();
            Debug.Log (typeof(PowerUpManager) + "が存在していないのに参照されたので生成");

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
