using UnityEngine;
using System.Collections;

public class CloudRot : MonoBehaviour {

	/// <summary>
	/// 概要 : 
	/// Author : 大洞祥太
	/// </summary>

	[SerializeField]
	float fRotTime = 1.0f;

	void Update() {
		transform.eulerAngles += new Vector3 (0,0, 360 * (Time.deltaTime / fRotTime));
	}
}
