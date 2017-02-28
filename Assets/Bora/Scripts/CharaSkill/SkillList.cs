using UnityEngine;
using System.Collections;

public class SkillList : MonoBehaviour
{
    /// <summary>
    /// 概要 : スキルのリストを保持
    /// Author : 大洞祥太
    /// </summary>

    #region Singleton

    private static SkillList instance;

    public static SkillList Instance {
        get {
            if (instance) 
                return instance;

            if (instance = (SkillList)FindObjectOfType(typeof(SkillList)))
                return instance;

            GameObject obj = new GameObject("SkillList");
            obj.AddComponent<SkillList>();
            Debug.Log(typeof(SkillList) + "が存在していないのに参照されたので生成");

            return instance;
        }
    }

    #endregion

    void Awake ()
    {
        if (this != Instance) {
            Destroy (this.gameObject);
            return;
        }
    }
}
