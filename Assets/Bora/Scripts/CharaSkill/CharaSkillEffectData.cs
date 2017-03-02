using UnityEngine;
using System.Collections;

public class CharaSkillEffectData : MonoBehaviour
{

    /// <summary>
    /// 概要 : キャラスキルエフェクトの情報を管理
    /// Author : 大洞祥太
    /// </summary>

    #region Singleton

	private static CharaSkillEffectData instance;

    public static CharaSkillEffectData Instance {
        get {
            if (instance)
                return instance;

            if (instance = (CharaSkillEffectData)FindObjectOfType (typeof(CharaSkillEffectData)))
                return instance;

            GameObject obj = new GameObject ("CharaSkillEffectData");
            obj.AddComponent<CharaSkillEffectData> ();
            Debug.Log (typeof(CharaSkillEffectData) + "が存在していないのに参照されたので生成");

            return instance;
        }
    }

    #endregion

    static Sprite[] m_EffectSprite = new Sprite[5];

//    bool m_IsAdd = true;

    TriangleWave<Color> triangleWave = null;
 
    public Color m_NowColor { get; private set; }

    void Awake ()
    {
        if (this != Instance) {
            Destroy (this.gameObject);
            return;
        }

        float time = 0.2f;
        Color min = new Color (1, 1, 1, 1);
        Color max = new Color (1, 1, 1, 0);
        triangleWave = TriangleWaveFactory.Color (min, max, time);

        m_NowColor = new Color (1, 1, 1, 1);
    }

    void Start ()
    {
        m_EffectSprite = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_CHARASKILLCARDEFFECT);
    }

    void Update ()
    {
        triangleWave.Progress();
        m_NowColor = triangleWave.CurrentValue;
    }

    public Sprite GetSprite (int raw_CharaType)
    {
        if (!m_EffectSprite [0]) {
            m_EffectSprite = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_CHARASKILLCARDEFFECT);
        }

        return m_EffectSprite [EscapeCharaType (raw_CharaType)];
    }

    // HACK : 自分の対応個所では無い所での数字のズレがあるため、変換して返す
    int EscapeCharaType (int raw_CharaType)
    {
        switch (raw_CharaType) {
        case 0:
            return 4;
        case 1:
            return 0;
        case 2:
            return 1;
        case 3:
            return 3;
        case 4:
            return 2;
        default:
            break;
        }

        Debug.LogError ("変換できない値を指定しています！");
        return 0;
    }
}
