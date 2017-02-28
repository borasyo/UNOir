﻿using UnityEngine;
using System.Collections;

public class Skill_Debug : MonoBehaviour {

    /// <summary>
    /// /* 概要 */ 
    /// スキルの挙動をテストする
    /// </summary>

	CharaSkillBase atack;
	CharaSkillBase hell;
	CharaSkillBase delay;
	CharaSkillBase rise;

	// Use this for initialization
	void Start () {
		atack = GetComponentInChildren<SkillAtack> ();
		hell  = GetComponentInChildren<SkillHell>  ();
		delay = GetComponentInChildren<SkillDelay> ();
		rise  = GetComponentInChildren<SkillRise>  ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Z)) {
            atack.Run();
		}
		if (Input.GetKeyDown (KeyCode.X)) {
            hell. Run();
		}
		if (Input.GetKeyDown (KeyCode.C)) {
            delay.Run();
		}
		if (Input.GetKeyDown (KeyCode.V)) {
            rise. Run();
		}
	}
}
