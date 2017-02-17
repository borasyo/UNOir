using UnityEngine;
using System.Collections;

public class SkillList : MonoBehaviour {

	/// <summary>
	/// 概要 : スキルのリストを保持
	/// Author : 大洞祥太
	/// </summary>

	static SkillList instance;

	public static SkillList Instance {
		get {
			if (instance == null) {
				instance = (SkillList)FindObjectOfType(typeof(SkillList));

				if (instance == null) {
					Debug.LogError("SkillList Instance Error");
				}
			}
			return instance;
		}
	}

	void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}
	}
}
