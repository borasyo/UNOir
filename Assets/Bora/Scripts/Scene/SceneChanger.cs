using UnityEngine;
using System.Collections;

// シーン遷移用
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	/// <summary>
	/// 概要 : シーン遷移を管理
	/// Author : 大洞祥太
    /// </summary>

    #region Singleton
    
    private static SceneChanger instance;

	public static SceneChanger Instance {
		get {
			if (instance) 
                return instance;

			instance = (SceneChanger)FindObjectOfType(typeof(SceneChanger));

            if (instance) 
                return instance;

            GameObject obj = new GameObject();
            obj.AddComponent<SceneChanger>();
			Debug.Log(typeof(SceneChanger) + "が存在していないのに参照されたので生成");
            
            return instance;
		}
	}

    #endregion

    public void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
	}

	public void ChangeScene(string sceneName, float interval, bool bNext, bool bStopBgm = true) {
		if (FadeManager.Instance.GetFadeing()) 
			return;

		FadeManager.Instance.LoadLevel(sceneName, interval, bStopBgm);

		if (bNext) {
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SCENECHANGE);
		} else {
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_OFFWINDOW);
		}
	}

	public void ChangeTitle() {
		if (FadeManager.Instance.GetFadeing ())
			return;
		
		FadeManager.Instance.LoadLevel("Title", 1.0f, true);
		//SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_TOUCHSTART);
	}
		
	public void ChangeMainMenu() {
		if (FadeManager.Instance.GetFadeing ())
			return;
		
		FadeManager.Instance.LoadLevel("MainMenu", 1.0f, true);
		//SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_TOUCHSTART);
	}

	public void ChangeGameMain() {
		if (FadeManager.Instance.GetFadeing ())
			return;
		
		FadeManager.Instance.LoadLevel("Test", 1.0f, true);
		//SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_TOUCHSTART);
	}
}
