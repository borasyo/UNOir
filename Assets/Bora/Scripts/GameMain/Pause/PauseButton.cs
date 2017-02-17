using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

	/// <summary>
	/// 概要 : ポーズのボタン
	/// Author : 大洞祥太
	/// </summary>

	[SerializeField]
	Button button = null;

	[SerializeField]
	GameObject poseWindow = null;

	public void Pause() {
		if (!BattleManager.Instance.GetIsInBattle ())
			return;

		GamePause.Pause ();
		poseWindow.SetActive (true);
		button.enabled = false;
        SoundManager.Instance.PlaySE(SoundManager.eSeValue.SE_ONWINDOW);

        SoundManager.Instance.PauseBGM(SoundManager.eBgmValue.BGM_ATACKUP, true);
        SoundManager.Instance.PauseBGM(SoundManager.eBgmValue.BGM_THUNDERNOW, true);
	}
}
