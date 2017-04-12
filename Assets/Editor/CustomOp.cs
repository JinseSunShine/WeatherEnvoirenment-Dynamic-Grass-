using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Xml;
public static class CustomOp
{
	[MenuItem("Opeartion/BuildRain")]
	public static void BuildRain()
	{
		RainBuilderDlg.ShowOrHide();
	}

	[MenuItem("Opeartion/BuildRainSplash")]
	public static void BuildRainSplash()
	{
		RainSplashBuilderDlg.ShowOrHide();
	}
}

class RainBuilderDlg : EditorWindow
{
	static RainBuilderDlg _dlg;
	static string savePath = "Assets/Art/Model/Rain_";
	public string name;
	KRainModelBuilder builder = new KRainModelBuilder();  
	public static void ShowOrHide()
	{
		if (_dlg == null)
		{
			_dlg = GetWindow<RainBuilderDlg>();
		}
		_dlg.Show();
	}

	public RainBuilderDlg()
	{
		title = "RainBuilder";
	}

	void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		name = EditorGUILayout.TextField("filename", name);
		builder.areaSize = EditorGUILayout.FloatField("areaSize", builder.areaSize);
		builder.particleSize = EditorGUILayout.FloatField("particleSize", builder.particleSize);
		builder.flakeRandom = EditorGUILayout.FloatField("flakeRandom", builder.flakeRandom);
		builder.numberOfParticles = EditorGUILayout.IntField("numberOfParticles", builder.numberOfParticles);
		builder.areaHeight = EditorGUILayout.FloatField("areaHeight", builder.areaHeight);
		if (GUILayout.Button("build"))
		{
			Mesh mesh = builder.CreateMesh();
			AssetDatabase.CreateAsset(mesh, savePath + name + ".asset");
		}
		EditorGUILayout.EndVertical();
	}
}

class RainSplashBuilderDlg : EditorWindow
{
	static RainSplashBuilderDlg _dlg;
	static string savePath = "Assets/Art/Model/RainSplash_";
	public string name;
	KRainSplashBuilder builder = new KRainSplashBuilder();
	public static void ShowOrHide()
	{
		if (_dlg == null)
		{
			_dlg = GetWindow<RainSplashBuilderDlg>();
		}
		_dlg.Show();
	}

	public RainSplashBuilderDlg()
	{
		title = "RainSplashBuilder";
	}

	void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		name = EditorGUILayout.TextField("filename", name);

		builder.numberOfParticles = EditorGUILayout.IntField("numberOfParticles", builder.numberOfParticles);
		builder.areaSize = EditorGUILayout.FloatField("areaSize", builder.areaSize);
		builder.flakeHeight = EditorGUILayout.FloatField("particleSize", builder.flakeHeight);
		builder.flakeWidth = EditorGUILayout.FloatField("areaHeight", builder.flakeWidth);
		builder.flakeRandom = EditorGUILayout.FloatField("flakeRandom", builder.flakeRandom);
				
		if (GUILayout.Button("build"))
		{
			Mesh mesh = builder.CreateMesh();
			AssetDatabase.CreateAsset(mesh, savePath + name + ".asset");
		}
		EditorGUILayout.EndVertical();
	}



}

