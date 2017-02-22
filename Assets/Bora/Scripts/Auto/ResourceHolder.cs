using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceHolder : MonoBehaviour {

	/// <summary>
	/// 概要 : 動的に読み込むResourceを開始時に読み込んで保持しておく
	/// Author : 大洞祥太
	/// </summary>
    /// 

    #region Singleton

    private static ResourceHolder instance;

	public static ResourceHolder Instance {
		get {
            if (instance)
                return instance;

            instance = (ResourceHolder)FindObjectOfType(typeof(ResourceHolder));

            if (instance)
                return instance;

            GameObject obj = new GameObject();
            obj.AddComponent<ResourceHolder>();
            Debug.Log(typeof(ResourceHolder) + "が存在していないのに参照されたので生成");

            return instance;
		}
	}

    #endregion
		
	// リソースID
	public enum eResourceId {

		ID_CHARASKILLCARD = 0,
		ID_CUTIN,
		ID_ATACKEFFECT,
		ID_DROWEFFECT,
		ID_FEVERNUM,
		ID_CARD,
		ID_NUMBER,
		ID_HP_NUMBER,
        ID_CHARAWAKU,
        ID_FEVERROGO,
        ID_UNOFEVEROGO,
        ID_POWERUP,
        ID_HPGAUGE,
        ID_CHARASKILLCARDEFFECT,
		ID_MAX,
	};

	List<Sprite[]> loadResources = new List<Sprite[]> ();

	public void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}

		DontDestroyOnLoad (this.gameObject);

        LoadResource("Textures/CharaSkill/CharaSkillCard");
        LoadResource("Textures/Effect/CharaSkill/CutIn");
        LoadResource("Textures/Effect/PlayerAtack/atack_effect");
        LoadResource("Textures/Effect/SpecialCard/Drow/DrowEffect");
        LoadResource("Textures/Fever/feverNum");
        LoadResource("Textures/Card/Card");
        LoadResource("Textures/Number/number");
        LoadResource("Textures/Number/hp_number");
        LoadResource("Textures/PlayerCharactor/Charawaku_all");
        LoadResource("Textures/Fever/Fever");
        LoadResource("Textures/Fever/UnoFever");
        LoadResource("Textures/Effect/SpecialCard/Drow/powerup");
        LoadResource("Textures/Gauge/player_hp_Gauge");
        LoadResource("Textures/CharaSkill/CharaSkillCardEffect");
	}

    // 外部からも追加したい場合が考えられるのでpublicにしてある
    public void LoadResource(string filePath) {
        Sprite[] resource = Resources.LoadAll<Sprite>(filePath);

        if (resource.Length <= 0) {
            Debug.LogError(filePath + "の読込に失敗しました！");
            return;
        }

        loadResources.Add(resource);
    }

	public Sprite[] GetResource(eResourceId id) {
        if (id < 0 || id >= eResourceId.ID_MAX) {
            Debug.LogError("存在しないリソースを取得しようとしています！");
            return null;
        }

        return loadResources[(int)id];
	}
}
