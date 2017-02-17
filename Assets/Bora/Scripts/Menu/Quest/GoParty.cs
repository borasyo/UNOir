using UnityEngine;
using System.Collections;

public class GoParty : MonoBehaviour {

	/// <summary>
	/// 概要 : 
	/// Author : 大洞祥太
	/// </summary>

	void Awake() {
		SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_MENU);
	}

	public void OnClick() {
		SceneChanger.Instance.ChangeScene ("PartyCheck", 1.0f, true);
	}
}
