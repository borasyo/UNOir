using UnityEngine;
using System.Collections;

public class CharaSkillEffectData : MonoBehaviour {

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

			if (instance = (CharaSkillEffectData)FindObjectOfType(typeof(CharaSkillEffectData))) 
				return instance;

			GameObject obj = new GameObject("CharaSkillEffectData");
			obj.AddComponent<CharaSkillEffectData>();
			Debug.Log(typeof(CharaSkillEffectData) + "が存在していないのに参照されたので生成");

			return instance;
		}
	}

	#endregion

	static Sprite[] m_EffectSprite = new Sprite[5]; 

	bool m_IsAdd = true;
	public Vector3 m_InitScale { get; private set; }
	float m_fTime = 0.2f;
	Vector3 m_AddScale = new Vector3(0.1f, 0.1f, 0.1f);
	public float m_fAddAlphaAmount { get; private set; } 

	public Color m_NowColor { get; private set; }
	public Vector3 m_NowScale { get; private set; }

	void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}

		m_fAddAlphaAmount = 1.0f;
		m_NowColor = new Color (1, 1, 1, 1);
		m_InitScale = m_NowScale = new Vector3 (1.0f,1.0f,1.0f);
	}

	void Start() {
		m_EffectSprite = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_CHARASKILLCARDEFFECT);
	}

	void Update() {
		
        // TODO : 汎用関数にできる
        if (m_IsAdd) {
			m_NowColor += new Color (0,0,0, m_fAddAlphaAmount) * (Time.deltaTime / m_fTime);

			if(m_NowColor.a >= 1.0f)
				m_IsAdd = false;

        } else {
            m_NowColor -= new Color (0,0,0, m_fAddAlphaAmount * (Time.deltaTime / m_fTime));

            if(m_NowColor.a <= 1.0f - m_fAddAlphaAmount) 
                m_IsAdd = true;
		}
	}
		
	public Sprite GetSprite(int raw_CharaType) {

		if (!m_EffectSprite [0]) {
			m_EffectSprite = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_CHARASKILLCARDEFFECT);
		}

		return m_EffectSprite [EscapeCharaType(raw_CharaType)];
	}

	// HACK : 自分の対応個所では無い所での数字のズレがあるため、変換して返す
	int EscapeCharaType(int raw_CharaType) {
		
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
