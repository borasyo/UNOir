using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurnData : MonoBehaviour
{
    /// <summary>
    /// 概要 : カードを出した時の数字や効果などの情報を保持
    /// Author : 大洞祥太
    /// </summary>

    /// TODO : まだ分割しきれていない
   
    #region Singleton

    private static TurnData instance;

    public static TurnData Instance {
        get {
            if (instance)
                return instance;

            instance = (TurnData)FindObjectOfType (typeof(TurnData));

            if (instance)
                return instance;

            GameObject obj = new GameObject ();
            obj.AddComponent<TurnData> ();
            Debug.Log (typeof(TurnData) + "が存在していないのに参照されたので生成");

            return instance;
        }
    }

    #endregion

    // 入れ物、計算機
    TurnDataHolder m_TurnDataHolder = null;
    TurnDataCalculator m_TurnDataCalculator = null;

    public DrowDataManager m_DrowDataManager { get; private set; }
    public ContinueAtackUpManager m_ContinueAtackUpManager { get; private set; }

    public List<CharaSkillBase> charaSkillList = new List<CharaSkillBase> ();
    public GameObject CharaSkillEffect = null;

    [SerializeField] float[] fMagnification;    //  初期化時以外は使用しない

    void Awake ()
    {
        if (this != Instance) {
            Destroy (this.gameObject);
            return;
        }

        m_TurnDataHolder = new TurnDataHolder ();
        m_TurnDataCalculator = new TurnDataCalculator (fMagnification);

        m_DrowDataManager = new DrowDataManager ();
        m_ContinueAtackUpManager = new ContinueAtackUpManager ();

        Reset ();
    }
	
    // Update is called once per frame
    void Update ()
    {
        m_DrowDataManager.Update ();
    }

    public void Add (UnoStruct.tCard card)
    {
        m_TurnDataHolder.Add (card);

        // キャラカードかチェック
        CharaCardCheck (card);
    }

    void CharaCardCheck (UnoStruct.tCard card)
    {
        // キャラカードかチェック
        List<PlayerCharactor> playerList = CardResource.Instance.GetPlayerList ();
        PlayerCharactor playerChara = playerList.Where (player => 
            player.GetTCard ().m_Color == card.m_Color &&
            player.GetTCard ().m_Number == card.m_Number).FirstOrDefault ();

        if (!playerChara)
            return;

        int index = playerList.IndexOf (playerChara);

        GameObject Obj = (GameObject)Instantiate (CharaSkillEffect);
        Obj.GetComponent<CharaSkillManager> ().Set (charaSkillList [index]);
    }


    public TurnDataHolder GetTurnData ()
    { 
        //  返す値に変換
        TurnDataHolder turnData = m_TurnDataCalculator.Calculate(m_TurnDataHolder);   


        //  追加処理計算
        m_DrowDataManager.Calculate (ref turnData);
        m_ContinueAtackUpManager.Calculate (ref turnData);


        //  音を鳴らす
        TurnSound turnSound = new TurnSound ();
        turnSound.SetCardSound (turnData.nSetCard);
        turnSound.SkipSound (turnData.nSkipNum);
     

        StartCoroutine (Reset ());
        return turnData;
    }

    public int CardAmount {
        get { return m_TurnDataHolder.nSetCard; }
    }

    IEnumerator Reset ()
    {
        // その場でリセットすると、参照する前に消えてしまうため１F待たせる
        yield return new WaitForEndOfFrame ();

        m_TurnDataHolder.Init ();
    }

    public void FastReset ()
    {
        m_TurnDataHolder.Init ();
    }
}
