using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardResource : MonoBehaviour
{
    /// <summary>
    /// 概要 : カードのリソース情報を保持
    /// Author : 大洞祥太
    /// </summary>

    #region Singleton

    private static CardResource instance;

    public static CardResource Instance {
        get {
            if (instance)
                return instance;

            instance = (CardResource)FindObjectOfType (typeof(CardResource));

            if (instance)
                return instance;

            GameObject obj = new GameObject ();
            obj.AddComponent<CardResource> ();
            Debug.Log (typeof(CardResource) + "が存在していないのに参照されたので生成");

            return instance;
        }
    }

    #endregion

    Dictionary<string, Sprite> m_CardResource = new Dictionary<string, Sprite> ();
    //	private List<PlayerCharactor> m_CharaList = new List<PlayerCharactor> ();

    void Awake ()
    {
        if (this != Instance) {
            Destroy (this.gameObject);
            return;
        }

        Sprite[] spriteAll = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_CARD);
		
        foreach (Sprite sprite in spriteAll) {
            m_CardResource.Add (sprite.name, sprite);
        }
    }

    public void SetCharaCard ()
    {
        List<PlayerCharactor> CharaList = GameMainUpperManager.instance.charactorAndFriend;

        foreach (PlayerCharactor chara in CharaList) {

            UnoStruct.tCard card = chara.GetTCard (); 
            int nNumber = ((int)chara.attribute * (int)UnoStruct.eNumber.NUMBER_MAX) + (int)chara.cardNum;
            string key = "Card_" + nNumber.ToString ();
            m_CardResource [key] = chara.noFrameSprite;
            //Debug.Log (cardResource[key].ToString() + "," + card.m_Color.ToString() + "," + card.m_Number.ToString());
        }
    }

    public Sprite GetCardResource (int nNumber)
    {
        Sprite sprite = null;
        string key = "Card_" + nNumber.ToString ();
        m_CardResource.TryGetValue (key, out sprite);

        return sprite;
    }

    public List<PlayerCharactor> GetPlayerList ()
    {
        return GameMainUpperManager.instance.charactorAndFriend;
    }
}
