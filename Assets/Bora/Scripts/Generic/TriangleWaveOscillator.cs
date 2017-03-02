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

// 演算を行うオブジェクトを考える
public interface ICalculator<T>
{
    T Add(T l, T r);
    T Sub(T l, T r);
    T Mul(T l, float r);
}

// 型ごとに演算を用意する
public class FloatCalculator : ICalculator<float>
{
    public float Add(float l, float r) { return l + r; }
    public float Sub(float l, float r) { return l - r; }
    public float Mul(float l, float r) { return l * r; }
}

public class Vector3Calculator : ICalculator<Vector3>
{
    public Vector3 Add(Vector3 l, Vector3 r) { return l + r; }
    public Vector3 Sub(Vector3 l, Vector3 r) { return l - r; }
    public Vector3 Mul(Vector3 l, float   r) { return l * r; }
}

public class ColorCalculator : ICalculator<Color>
{
    public Color Add(Color l, Color r) { return l + r; }
    public Color Sub(Color l, Color r) { return l - r; }
    public Color Mul(Color l, float r) { return l * r; }
}

// 指定された型で、指定された範囲の三角波生成器
public class TriangleWave<T>
{
    // オシレータ(発振器) を持つ
    private TriangleWaveOscillator oscillator = new TriangleWaveOscillator();

    public readonly T Min;
    public readonly T Max;

    private T range;
    private ICalculator<T> calculator;

    // operator に頼らず演算ができる
    public T CurrentValue { get { return calculator.Add(Min, calculator.Mul(range, oscillator.Value)); } }

    // 型の演算方法を指定して生成する
    public TriangleWave(T min, T max, float time_sec, ICalculator<T> calculator)
    {
        Min = min;
        Max = max;
        this.calculator = calculator;
        range = calculator.Sub(max, min);
        oscillator.Set (time_sec);
    }

    // 事前に時間を指定しておき、自律的に発振するように変更
    public void Progress() { oscillator.Progress(); }
}

// ファクトリ
public class RangedTriangleWave
{
    public static TriangleWave<float> Float(float min, float max, float time_sec)
    {
        return new TriangleWave<float> (min, max, time_sec, new FloatCalculator());
    }

    public static TriangleWave<Vector3> Vector3(Vector3 min, Vector3 max, float time_sec)
    {
        return new TriangleWave<Vector3> (min, max, time_sec, new Vector3Calculator());
    }

    public static TriangleWave<Color> Color(Color min, Color max, float time_sec)
    {
        return new TriangleWave<Color> (min, max, time_sec, new ColorCalculator());
    }
}

