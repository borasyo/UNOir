using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UnoCreateManager : MonoBehaviour
{

    /// <summary>
    /// 概要 : 下画面を情報を管理
    /// Author : 大洞祥太
    /// </summary>

    #region Singleton

    private static UnoCreateManager instance;

    public static UnoCreateManager Instance {
        get {
            if (instance)
                return instance;

            instance = (UnoCreateManager)FindObjectOfType (typeof(UnoCreateManager));

            if (instance)
                return instance;

            GameObject obj = new GameObject ();
            obj.AddComponent<UnoCreateManager> ();
            Debug.Log (typeof(UnoCreateManager) + "が存在していないのに参照されたので生成");

            return instance;
        }
    }

    #endregion

    // 変数宣言
    public Vector2 Panel = new Vector2 (4, 4);
    //public GameObject CardObj = null;
    Dictionary<UnoStruct.tCard, int> DictData = new Dictionary<UnoStruct.tCard, int> ();
    List<UnoData> CardList = new List<UnoData> ();
    [SerializeField] int nNonSelect = 0;

    int nLottery = 0;
    List<UnoStruct.tCard> Lottery = new List<UnoStruct.tCard> ();

    public UnoProbability probability = null;

    [SerializeField]
    ResetCard resetCard = null;

    [SerializeField]
    GameObject PowerUpObj = null;

    [SerializeField]
    Vector3 PowerUpLocalPos = Vector3.zero;

    [SerializeField]
    GameObject RiseEffect = null;

    [SerializeField]
    GameObject FeverCardObj = null;

    void Awake ()
    {
        if (this != Instance) {
            Destroy (this.gameObject);
            return;
        }

        // 確率リスト
        for (int color = 0; color < (int)UnoStruct.eColor.COLOR_MAX; color++) {
            for (int number = 0; number < (int)UnoStruct.eNumber.NUMBER_MAX; number++) {
                if (color == (int)UnoStruct.eColor.COLOR_WILD && number < (int)UnoStruct.eNumber.NUMBER_MAX - 2) {
                    continue;
                }
                if (color != (int)UnoStruct.eColor.COLOR_WILD && number >= (int)UnoStruct.eNumber.NUMBER_MAX - 2) {
                    continue;
                }

                UnoStruct.tCard Card;
                Card.m_Color = (UnoStruct.eColor)color;
                Card.m_Number = (UnoStruct.eNumber)number;
                DictData.Add (Card, 1); // とりあえず1で
            }
        }
        // 設定を適応
        probability.Set ();

        // 抽選箱作成。
        foreach (UnoStruct.tCard key in DictData.Keys) {
            for (int i = 0; i < DictData [key]; i++) {
                Lottery.Add (key);
            }
        }
    }

    public void Init ()
    {
		
        // カードリスト
        Vector2 ScreenSize = new Vector2 (Screen.width / (Panel.x + 1), Screen.height / 2.0f / (Panel.y + 1));
        for (int i = 0; i < Panel.x * Panel.y; i++) {
            UnoData temp = transform.FindChild ("CardHolder").GetChild (i).GetComponent<UnoData> ();

            GameObject tempObj = (GameObject)Instantiate (PowerUpObj);
            tempObj.transform.SetParent (temp.transform);
            tempObj.transform.localPosition = PowerUpLocalPos;

            GameObject feverCard = (GameObject)Instantiate (FeverCardObj);
            feverCard.transform.SetParent (temp.transform);
            feverCard.transform.localPosition = Vector3.zero;

            temp.Init ();
            CardList.Add (temp);
        }
			
        // 初期カード選定
        FieldCard.Instance.Init ();
        UnoData data = new UnoData ();
        data.CardData = GetLotteryCardData (FieldCard.Instance.transform.position);
        FieldCard.Instance.Change (data, false);
        Destroy (data);
    }

    void Update ()
    {
        if (nNonSelect >= CardList.Count) {
            resetCard.Run ();
            List<Enemy> enemyList = GameMainUpperManager.instance.enemyList;
            for (int i = 0; i < enemyList.Count; i++) {
                enemyList [i].FullChargeTimeGauge ();
            }

            for (int i = 0; i < CardList.Count; i++) {
                //CardList [i].Change ();
                CardList [i].Shuffle ();
            }
            //SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_NONSETCARD);
            SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_CARDSHUFFLE);
        }
        nNonSelect = 0;
        /// TODO : 必要なのか不明
        SkillRise.m_nCardNum = nLottery;
        nLottery = 0;

        /*#if DEBUG
		if (Input.GetKeyDown (KeyCode.Q)) {
			foreach (UnoStruct.tCard key in DictData.Keys) {
				Debug.Log (DictData[key].ToString() + "は" + key.m_Color.ToString() + "," + key.m_Number.ToString());
			}
		}
		#endif*/
    }

    // 変更するカード、値、倍率か直入か、抽選箱再作成するか(処理軽減)
    public int SetDict (UnoStruct.tCard card, int n, bool bAdd, bool bReCreate = true)
    {
        // 現在値を返す(変更を戻す時に必要)
        int nReturn = DictData [card];

        // 更新
        if (!bAdd) {
            // 減衰として計算
            DictData [card] = DictData [card] / n;
        } else {
            // 倍率として計算
            DictData [card] = DictData [card] * n;
        }

        if (!bReCreate)
            return nReturn;

        // 抽選箱再作成。
        foreach (UnoStruct.tCard key in DictData.Keys) {
            for (int i = 0; i < DictData [key]; i++) {
                Lottery.Add (key);
            }
        }

        /*#if DEBUG
		Debug.Log ("抽選箱再作成");
		#endif*/

        return nReturn;
    }

    public UnoStruct.tCard GetLotteryCardData (Vector3 pos)
    {
        nLottery++; 	// セットするのでカウント増加

        if (SkillRise.m_IsRun) {
            UnoStruct.tCard card = Lottery [Random.Range (0, Lottery.Count)];

            // TODO : LINQが使えそう 
            while (card.m_Color != UnoStruct.eColor.COLOR_RED) {
                card = Lottery [Random.Range (0, Lottery.Count)];

                if (card.m_Color != UnoStruct.eColor.COLOR_WILD) {
                    if (card.m_Color != UnoStruct.eColor.COLOR_RED) {
                        if (card.m_Number == UnoStruct.eNumber.NUMBER_FOUR ||
                           card.m_Number == UnoStruct.eNumber.NUMBER_EIGHT) {
                            continue;
                        }
                    }

                    card.m_Color = UnoStruct.eColor.COLOR_RED;
                }
            }
            // エフェクト生成
            Instantiate (RiseEffect, pos, Quaternion.identity);
            SkillRise.Reduce (1); // カウント減らす
            return card;
        }

        return Lottery [Random.Range (0, Lottery.Count)];
    }

    public void NonSelect ()
    {
        nNonSelect++;
    }

    public void TouchOnly (bool bFlg)
    {
        foreach (UnoData card in CardList) {
            if (card.OnClick)
                continue;

            card.EventTrigger = bFlg;
        }
    }

    public List<UnoData> GetCardList ()
    {
        return CardList;
    }
}
