using UnityEngine;
using System.Collections;

// TODO : いつ破壊されるのかクラス名からは分からない
public class AutoDestroy : MonoBehaviour {

	/// <summary>
	/// Author : 大洞祥太
	/// </summary>

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject);
	}
}
