using UnityEngine;
using System.Collections;

public class GoMain : MonoBehaviour {

	/// <summary>
	/// 概要 : 
	/// Author : 大洞祥太
	/// </summary>

	void Start() {
		SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_PARTY);
	}

	public void OnClick() {
		SceneChanger.Instance.ChangeScene ("Main", 1.0f, true);
	}
}
