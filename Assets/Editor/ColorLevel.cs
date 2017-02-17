using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class ColorLevel : EditorWindow {
	
	enum ColorType {
		r,
		g,
		b,
		a,

		max,
	};

	Texture2D MainTex;
	Texture2D InputTex;
	Texture2D OutputTex;
	float R_SliderMin = 0.0f;
	float R_SliderMax = 1.0f;
	float G_SliderMin = 0.0f;
	float G_SliderMax = 1.0f;
	float B_SliderMin = 0.0f;
	float B_SliderMax = 1.0f;
	float A_SliderMin = 0.0f;
	float A_SliderMax = 1.0f;

	string Path = "Assets/Resources/CreateTexture/";
	string FileName = "test";
	
	[MenuItem("Window/ColorLevel")]
	static void Open()
	{
		EditorWindow.GetWindow<ColorLevel>( "ColorLevel" ); 
	}

	void OnGUI() {

		// テクスチャ設定
		MainTex = EditorGUILayout.ObjectField ("Texture", MainTex, typeof(Texture2D), true) as Texture2D;

		// レベル調整
		EditorGUILayout.MinMaxSlider ( new GUIContent("R"), ref R_SliderMin, ref R_SliderMax, 0.0f, 1.0f);
		EditorGUILayout.LabelField ( "Min = " + R_SliderMin.ToString() + " , Max = " + R_SliderMax.ToString() );
		EditorGUILayout.MinMaxSlider ( new GUIContent("G"), ref G_SliderMin, ref G_SliderMax, 0.0f, 1.0f);
		EditorGUILayout.LabelField ( "Min = " + G_SliderMin.ToString() + " , Max = " + G_SliderMax.ToString() );
		EditorGUILayout.MinMaxSlider ( new GUIContent("B"), ref B_SliderMin, ref B_SliderMax, 0.0f, 1.0f);
		EditorGUILayout.LabelField ( "Min = " + B_SliderMin.ToString() + " , Max = " + B_SliderMax.ToString() );
		EditorGUILayout.MinMaxSlider ( new GUIContent("A"), ref A_SliderMin, ref A_SliderMax, 0.0f, 1.0f);
		EditorGUILayout.LabelField ( "Min = " + A_SliderMin.ToString() + " , Max = " + A_SliderMax.ToString() );

		if (GUILayout.Button ("Preview")) {
			// 作成
			CrateTexture();
		}

		InputTex = EditorGUILayout.ObjectField ("Input", MainTex, typeof(Texture2D), true) as Texture2D;
		OutputTex = EditorGUILayout.ObjectField ("Output", OutputTex, typeof(Texture2D), true) as Texture2D;

		// パス設定
		Path = EditorGUILayout.TextField ( "Path", Path );
		FileName = EditorGUILayout.TextField ( "FileName", FileName );

		if(GUILayout.Button( "Create" )) {

			// 一応作成
			CrateTexture();

			// 書き出し
			WriteTexture ();

			//Destroy (createTex);
		}
	}

	/// <summary>
	/// テクスチャ作成
	/// </summary>
	void CrateTexture() {
		OutputTex = new Texture2D(MainTex.width, MainTex.height, MainTex.format, true); // Texture2D.CreateExternalTexture (tex.width, tex.height, tex.format, true, true, ptr);

		Color[] setColors = MainTex.GetPixels();
		for (int x = 0; x < MainTex.width; x++){
			for (int y = 0; y < MainTex.height; y++) {
				Color setColor = setColors[(x * MainTex.width) + y];
				setColor.r = OnLevelCheck (setColor.r, ColorType.r);
				setColor.g = OnLevelCheck (setColor.g, ColorType.g);
				setColor.b = OnLevelCheck (setColor.b, ColorType.b);
				setColor.a = OnLevelCheck (setColor.a, ColorType.a);
				setColors [(x * MainTex.width) + y] = setColor;
				//tex.SetPixel (x, y, setColor);
			}
		}
		OutputTex.SetPixels (setColors);
		OutputTex.Apply ();
	}

	/// <summary>
	/// テクスチャ書き出し
	/// </summary>
	void WriteTexture() {

		var bytes = OutputTex.EncodeToPNG ();
		//Debug.Log (Application.dataPath + tex.name);
		File.WriteAllBytes (Path + FileName + ".png", bytes);
		AssetDatabase.Refresh ();
	}

	/// <summary>
	/// 設定したレベルに合わせて色を補正	
	/// </summary>
	float OnLevelCheck(float fColor, ColorType c) {

		float min = 0.0f;
		float max = 0.0f; 

		switch(c) {

		case ColorType.r:
			min = R_SliderMin;
			max = R_SliderMax;
			break;
		case ColorType.g:
			min = G_SliderMin;
			max = G_SliderMax;
			break;
		case ColorType.b:
			min = B_SliderMin;
			max = B_SliderMax;
			break;
		case ColorType.a:
			min = A_SliderMin;
			max = A_SliderMax;
			break;
		}

		if (fColor < min) {
			fColor = min;
		} else if(fColor > max) {
			fColor = max;
		}

		return fColor;
	}
}
