using UnityEngine;
using System.Collections;

public class TriangleWaveOscillator
{
    float time;
    public float Value = 0f; // 0-1 の範囲

    float interval_sec; // 間隔
    public void Set (float sec) { interval_sec = sec; }

    public virtual void Progress()
    {
        // 指定した間隔で補正したdeltaTimeを加算
        time += (Time.deltaTime / interval_sec);

        // timeを整数化し、偶数なら加算、奇数なら減衰させるように計算
        float fSet = 0.0f;
        if ((int)time % 2 == 0) {
            fSet = time - Mathf.FloorToInt(time);
        } else {
            fSet = Mathf.CeilToInt (time) - time;
        }
        Value = fSet;
    }
}
