﻿using UnityEngine;
using System.Collections;

public class ThunderParticle : MonoBehaviour {

	/// <summary>
	/// 概要 : 
	/// Author : 大洞祥太
	/// </summary>

	SkillDelay delay = null;
	[SerializeField] ParticleSystem particle = null;
	bool bRun = false;
	static bool bUse = false;

	// Use this for initialization
	void Start () {
		delay = SkillList.Instance.GetComponentInChildren<SkillDelay> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!bRun) {
			bRun = particle.isPlaying;

			if (!bRun)
				return;
			
			if (bUse) {
				Destroy (this.gameObject);
			} else {
				bUse = true;
			}
		}

		if (bRun && !delay.m_IsRun) {
			bUse = false;
			Destroy (this.gameObject);
			SoundManager.Instance.StopBGM (SoundManager.eBgmValue.BGM_THUNDERNOW);
			return;
		}
	}
}
