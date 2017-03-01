using UnityEngine;
using System.Collections;

public class FeverSetEffect : MonoBehaviour
{
    /// <summary>
    /// 概要 : フィーバー時のキャラカードカードセットエフェクト
    ///        を生成するファクトリー
    /// Author : 大洞祥太
    /// </summary>

    #region Singleton

    private static FeverSetEffect instance;

    public static FeverSetEffect Instance {
        get {
            if (instance) 
                return instance;

            instance = (FeverSetEffect)FindObjectOfType(typeof(FeverSetEffect));

            if (instance) 
                return instance;

            GameObject obj = new GameObject();
            obj.AddComponent<FeverSetEffect>();
            Debug.Log(typeof(FeverSetEffect) + "が存在していないのに参照されたので生成");

            return instance;
        }
    }

    #endregion

    [SerializeField] GameObject m_FeverSetObj = null;
    [SerializeField] Color[] m_ColorList = new Color[4];

    void Awake ()
    {
        if (this != Instance) {
            Destroy (this.gameObject);
            return;
        }
    }

    public void CreateFeverSetEffect (UnoStruct.eColor color, Vector3 pos)
    {
        GameObject obj = (GameObject)Instantiate (m_FeverSetObj, pos, Quaternion.identity);
        obj.GetComponent<ParticleSystem> ().startColor = m_ColorList [(int)color];
    }
}
