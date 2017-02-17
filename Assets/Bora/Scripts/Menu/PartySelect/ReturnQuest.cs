using UnityEngine;
using System.Collections;

public class ReturnQuest : MonoBehaviour {

	/// <summary>
	/// 概要 : 
	/// Author : 大洞祥太
	/// </summary>

	public void OnClick() {
		SceneChanger.Instance.ChangeScene ("QuestSelect", 1.0f, false);
	}
}
