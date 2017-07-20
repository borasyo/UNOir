using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UniRx;
using UniRx.Triggers;

public class PressStart : MonoBehaviour {

	/// <summary>
	/// 概要 : 
	/// Author : 大洞祥太
	/// </summary>

	Image image = null;
	public float fTime = 0.75f; 
	bool bIn = true;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();

		SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_TITLE);
        StartCoroutine (Init());
	}

    IEnumerator Init()
    {
        yield return null;

        if (Application.platform == RuntimePlatform.Android) {
            this.UpdateAsObservable ()
                .Subscribe (_ => {
                if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began && !FadeManager.Instance.GetFadeing ()) {
                    SceneChanger.Instance.ChangeMainMenu ();
                    SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_TOUCHSTART);
                }
            });
        } else {
            this.UpdateAsObservable ()
                .Subscribe (_ => {
                    if (Input.GetMouseButtonDown (0) && !FadeManager.Instance.GetFadeing()) {
                        SceneChanger.Instance.ChangeMainMenu ();
                        SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_TOUCHSTART);
                    }
                });
        }
    }
	
	// Update is called once per frame
	void Update () {

		if (bIn) {
			image.color -= new Color (0, 0, 0, 1.0f * (Time.deltaTime / fTime));

			if (image.color.a <= 0.0f) {
				bIn = false;
			}
		} else {
			image.color += new Color (0, 0, 0, 1.0f * (Time.deltaTime / fTime));

			if (image.color.a >= 1.0f) {
				bIn = true;
			}
		}

		
	}
}
