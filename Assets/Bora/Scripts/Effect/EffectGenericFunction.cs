using UnityEngine;
using System.Collections;

public class EffectGenericFunction : MonoBehaviour {

	/// <summary>
	/// 概要   : エフェクトの制御で汎用的な関数を保持している
	/// Author : 大洞祥太
	/// </summary>

	#region UpdateEffect

	/* エフェクトの更新処理 */
	// 変化量と加算フラグは変更されるので参照渡し  
	/*static public void UpdateEffectAnim<ChangeType, TimeType>(
	    //	引数 :           変化量,     加算フラグ,                  変化量,            変化時間,    最大値,    最小値)
		ref ChangeType changeTarget, ref bool isAdd, ChangeType changeAmount, TimeType changeTime, float max, float min) {

		if (isAdd) {
			changeTarget += changeAmount * (Time.deltaTime / changeTime);
		} else {
			changeTarget -= changeAmount * (Time.deltaTime / changeTime);
		}

		AddFlgCheck (isAdd, , max, min);
	}

	static void AddFlgCheck(ref bool isAdd, float comparison, float max, float min) {
		
		if(comparison >= max) {
			isAdd = false;
		}
		else if (comparison <= min) {
			isAdd = true;
		}
	}*/

	#endregion
}
