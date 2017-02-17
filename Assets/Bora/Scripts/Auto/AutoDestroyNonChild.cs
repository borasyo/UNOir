using UnityEngine;
using System.Collections;

public class AutoDestroyNonChild : MonoBehaviour {

	/// <summary>
	/// 概要 : 子がいなくなったら自動破壊
	/// Author : 大洞祥太
	/// </summary>

	void Update () {
		if (transform.childCount > 0)
			return;
		
		Destroy (this.gameObject);
	}
}
