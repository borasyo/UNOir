﻿using UnityEngine;
using System.Collections;

public class Go_Back : MonoBehaviour {

	/// <summary>
	/// 概要 : 
	/// Author : 大洞祥太
	/// </summary>

	[SerializeField]
	float fTime = 0.5f;

	[SerializeField]
	float fMinAlpha = 0.5f;

	[SerializeField]
	Vector3 MinScale = new Vector3 (0.4f, 0.5f, 0.0f);

	Vector3 MaxScale = Vector3.zero;

	//bool bAdd = true;

	SpriteRenderer render = null;

	void Start() {
		render = GetComponent<SpriteRenderer> ();
		MaxScale = transform.localScale;
		transform.localScale = MinScale;
	}

	void Update() {

		/*if (bAdd) {
			render.color += new Color (0, 0, 0, fMaxAlpha * (Time.deltaTime / fTime));
			transform.localScale -= (MaxScale - MinScale) * (Time.deltaTime / fTime);

			if (render.color.a >= fMaxAlpha) {
				render.color = new Color (1, 1, 1, fMaxAlpha);
				bAdd = false;
				transform.localScale = MinScale;
			}
		} else {*/
		render.color -= new Color (0, 0, 0, (1.0f - fMinAlpha) * (Time.deltaTime / fTime));
		transform.localScale += (MaxScale - MinScale) * (Time.deltaTime / fTime);

		if (render.color.a <= fMinAlpha) {
			render.color = new Color (1, 1, 1, 1);
			//bAdd = true;
			transform.localScale = MinScale;
		}
		//}
	}

	/*[SerializeField]
	float fTime = 0.5f;

	[SerializeField]
	float fMaxAlpha = 0.5f;

	//[SerializeField]
	//Vector3 MinScale = new Vector3 (0.4f, 0.5f, 0.0f);

	//Vector3 MaxScale = Vector3.zero;

	bool bAdd = true;

	SpriteRenderer render = null;

	void Start() {
		render = GetComponent<SpriteRenderer> ();
		//MaxScale = transform.localScale;
		//transform.localScale = MinScale;
	}

	void Update() {

		if (bAdd) {
			render.color += new Color (0, 0, 0, fMaxAlpha * (Time.deltaTime / fTime));
			//transform.localScale += (MaxScale - MinScale) * (Time.deltaTime / fTime);

			if (render.color.a >= fMaxAlpha) {
				render.color = new Color (1, 1, 1, fMaxAlpha);
				bAdd = false;
				//transform.localScale = MinScale;
			}
		} else {
			render.color -= new Color (0, 0, 0, fMaxAlpha * (Time.deltaTime / fTime));
			//transform.localScale += (MaxScale - MinScale) * (Time.deltaTime / fTime);

			if (render.color.a <= 0.0f) {
				render.color = new Color (1, 1, 1, 0);
				bAdd = true;
				//transform.localScale = MinScale;
			}
		}
	}*/
}
