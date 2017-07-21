using UnityEngine;
using System.Collections;

public class Turn_Debug : MonoBehaviour {

    // TODO : 使用していない可能性大
	
	// Update is called once per frame
	void LateUpdate () {
        TurnData.tTurnData data = TurnData.Instance.GetTurnData ();
	}
}
