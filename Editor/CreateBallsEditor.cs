using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

[CustomEditor (typeof(CreateBalls))]
public class CreateBallsEditor : Editor {

	CreateBalls script;
	public void OnEnable(){
		script = (CreateBalls)target;
	}
	public override void OnInspectorGUI(){
		DrawDefaultInspector();
		GUILayout.BeginVertical();
		if(GUILayout.Button("Create")){
			script.Create();
		}
		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		if(GUILayout.Button("Delete")){
			script.DeletePrevious();
		}
		GUILayout.EndVertical();
		GUILayout.BeginHorizontal();
			
		GUILayout.EndHorizontal();
		
		SceneView.RepaintAll();
	}
}
