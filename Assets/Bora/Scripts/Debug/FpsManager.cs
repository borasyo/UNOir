using UnityEngine;
using System.Collections;

public class FpsManager : MonoBehaviour {
    
    #region Singleton

    private static FpsManager instance;

    public static FpsManager Instance {
        get {
            if (instance)
                return instance;

            if (instance = (FpsManager)FindObjectOfType (typeof(FpsManager)))
                return instance;

            GameObject obj = new GameObject ("FpsManager");
            obj.AddComponent<FpsManager> ();
            Debug.Log (typeof(FpsManager) + "が存在していないのに参照されたので生成");

            return instance;
        }
    }

    #endregion

	[SerializeField]
	bool bAndroid = true;

	[SerializeField]
	bool bEditor = true;

	void Awake() {
		if (this != Instance) {
			Destroy(this.gameObject);
			return;
		}

		// シーン遷移で破棄させない
		DontDestroyOnLoad(gameObject);
	}


	int frameCount;
	float nextTime;
	int PrevFps;

	public int FontSize = 10;

	public int nCnt = 0;

	GUIStyle Style;

	void Start () {
		// 次の時間を保存
		nextTime = Time.time + 1;

		frameCount = 0;
		PrevFps = 0;

		Style = new GUIStyle();
		Style.fontSize = FontSize;
	}
		
	void Update () {
		// フレームの加算
		frameCount++;
	}


	void OnGUI() {
		if (Application.platform == RuntimePlatform.Android) {
			if (!bAndroid) {
				return;
			}
		} else {
			if (!bEditor) {
				return;
			}
		}

		if (Time.time >= nextTime) {
			// 1秒経ったらFPSを保存
			PrevFps = frameCount;
			frameCount = 0;
			nextTime += 1;
		}
		string label = "  FPS:" + PrevFps;
		GUI.Label (new Rect(0,0,400,400), label, Style);
	}
}
