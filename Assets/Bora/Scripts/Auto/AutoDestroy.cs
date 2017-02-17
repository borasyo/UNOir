using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	/// <summary>
	/// 概要 : 生成後、自動破壊
	/// Author : 大洞祥太
	/// </summary>

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject);
	}
}
