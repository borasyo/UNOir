using UnityEngine;
using System.Collections;

public class DrowData {

    public UnoStruct.eColor DrowColor = UnoStruct.eColor.COLOR_MAX;
    public float m_fLife { get; private set; }
    public float m_fAmount { get; private set; }
  
    public DrowData(UnoStruct.eColor color, float life, float amount) 
    {
        DrowColor = color;
        m_fLife = life;
        m_fAmount = amount;
    }

    public void Update()
    {
        m_fLife -= Time.deltaTime;
    }

    public void Run (bool bFlg)
    {
        if (DrowColor != UnoStruct.eColor.COLOR_WILD) {
            PowerUpManager.Instance.Run (DrowColor, bFlg);
        } else {
            for (int i = 0; i < (int)UnoStruct.eColor.COLOR_WILD; i++) {
                PowerUpManager.Instance.Run ((UnoStruct.eColor)i, bFlg);
            }
        }
    }
}
