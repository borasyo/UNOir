using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FeverEffectManager : MonoBehaviour
{
    /// <summary>
    /// 概要 : フィーバー時のエフェクトを管理
    /// Author : 大洞祥太
    /// </summary>
     
    #region Singleton

    private static FeverEffectManager instance;

    public static FeverEffectManager Instance {
        get {
            if (instance)
                return instance;

            instance = (FeverEffectManager)FindObjectOfType (typeof(FeverEffectManager));

            if (instance)
                return instance;

            GameObject obj = new GameObject ();
            obj.AddComponent<FeverEffectManager> ();
            Debug.Log (typeof(FeverEffectManager) + "が存在していないのに参照されたので生成");

            return instance;
        }
    }

    #endregion Singleton

    List<ParticleSystem> m_ParticleList = new List<ParticleSystem> ();
    public FeverGauge feverGauge = null;
    bool bOldFeverFlg = false;

    int nOldFeverPoint = 0;
    Sprite[] feverNumSprite = new Sprite[3];

    [SerializeField] SpriteRenderer feverNum = null;
    Vector3 InitfeverNumScale = Vector3.zero;

    void Awake ()
    {
        if (this != Instance) {
            Destroy (this.gameObject);
            return;
        }

        for (int i = 0; i < transform.childCount - 1; i++) {
            m_ParticleList.Add (transform.GetChild (i).GetComponent<ParticleSystem> ());
            m_ParticleList [i].loop = false;
            m_ParticleList [i].Stop ();
        }

        InitfeverNumScale = feverNum.transform.localScale;
        feverNumSprite = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_FEVERNUM);
    }

    void Update ()
    {

        if (bOldFeverFlg != feverGauge.isFeverMode) {
            Run (feverGauge.isFeverMode);
        } 
        if (feverGauge.isFeverMode) {

            int nAs = feverGauge.feverPointMax / (int)feverGauge.FeverTime; 

            if (!BattleManager.Instance.GetIsInBattle ()) {
                feverNum.enabled = false;
                nOldFeverPoint = feverGauge.feverPoint / nAs;
                return;
            } else {
                feverNum.enabled = true;
            }

            if (feverGauge.feverPoint <= 2 * nAs) {
                if (nOldFeverPoint != feverGauge.feverPoint / nAs) {
                    feverNum.sprite = feverNumSprite [feverGauge.feverPoint / nAs]; 
                    feverNum.transform.localScale = InitfeverNumScale;
                    feverNum.color = new Color (1, 1, 1, 1);
                }
                feverNum.transform.localScale -= InitfeverNumScale * Time.deltaTime;
            }
            nOldFeverPoint = feverGauge.feverPoint / nAs;
        }

        bOldFeverFlg = feverGauge.isFeverMode;
    }

    public bool GetFever ()
    {
        return m_ParticleList [0].loop;
    }

    public void Run (bool bRun)
    {
        if (bRun) {
		
            foreach (ParticleSystem particle in m_ParticleList) {
                particle.Play ();
                particle.loop = true;
            }
            feverNum.enabled = true;
		
        } else {

            foreach (ParticleSystem particle in m_ParticleList) {
                particle.loop = false;
            }
            feverNum.color = new Color (1, 1, 1, 0);
            feverNum.enabled = false;
        }
    }
}
