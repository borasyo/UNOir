using UnityEngine;
using System.Collections;

public class PowerUpAlpha : MonoBehaviour
{
    /// <summary>
    /// 概要 : 攻撃アップエフェクトの値を管理
    /// Author : 大洞祥太
    /// </summary>

    #region Singleton

    private static PowerUpAlpha instance;

    public static PowerUpAlpha Instance {
        get {
            if (instance) 
                return instance;

            instance = (PowerUpAlpha)FindObjectOfType(typeof(PowerUpAlpha));

            if (instance) 
                return instance;

            GameObject obj = new GameObject();
            obj.AddComponent<PowerUpAlpha>();
            Debug.Log(typeof(PowerUpAlpha) + "が存在していないのに参照されたので生成");

            return instance;
        }
    }

    #endregion

    public Color NowColor { get; private set; }

    public Vector3 NowPos { get; private set; }

    [SerializeField]
    float fTime = 0.5f;

    [SerializeField]
    Vector3 MaxMove = new Vector3 (0, -0.3f, 0);

    bool bWait = false;
    float fNowWait = 0.0f;
    [SerializeField]
    float fWait = 0.5f;

    void Awake ()
    {
        if (this != Instance) {
            Destroy (this.gameObject);
            return;
        }
        NowColor = new Color (1, 1, 1, 1);
        NowPos = Vector3.zero;
    }

    void Update ()
    {
        if (!BattleManager.Instance.GetIsInBattle ())
            return;

        if (bWait) {
            fNowWait += Time.deltaTime;
            if (fNowWait >= fWait) {
                fNowWait = 0.0f;
                bWait = false;
                NowColor = new Color (1, 1, 1, 1);
            }
            return;
        }

        NowColor -= new Color (0, 0, 0, 1.0f * (Time.deltaTime / fTime));
        NowPos += MaxMove * (Time.deltaTime / fTime);

        if (NowColor.a <= 0.0f) {
            NowColor = new Color (1, 1, 1, 0);
            NowPos = Vector3.zero;
            bWait = true;
        }
    }


    public void Reset ()
    {
        NowColor = new Color (1, 1, 1, 1);
        NowPos = Vector3.zero;
        fNowWait = 0.0f;
        bWait = false;
    }
}
