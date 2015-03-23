using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class CreateBalls : MonoBehaviour {

	public GameObject toCreate;
	public Vector3 position;
	public int layers;
	public int width;
	public float distance = 2.0f;
	[SerializeField]
	private int TotalBalls;
	private int balsCreated;
	

	[SerializeField]
	private List<SerializableList> balls = new List<SerializableList>();
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Create(){
		if((layers*(width*width))>4000){
			print("TO MANY BALS!!");
		}
		else{
			SerializableList toAdd = new SerializableList();
			Vector3 currentPos = position;
			for(int i=0;i<layers;i++){
				for(int o = 0;o<width;o++){
					currentPos.z = position.z+o;
					for(int p = 0;p<width;p++){
						GameObject tmp = (GameObject)Instantiate(toCreate,currentPos,Quaternion.identity);
						toAdd.Add(tmp);
						balsCreated++;
						currentPos.z += distance;
					}
					currentPos.x += distance;
					
					
				}
				currentPos.y += distance;
				currentPos.x = position.x+i;
			}
			TotalBalls += balsCreated;
			balsCreated = 0;
			balls.Add(toAdd);
		}
	}
	public void DeletePrevious(){
		if(balls.Count == 0){
			return;
		}
		int last = balls.Count-1;
		for(int i = 0;i<balls[last].Count();i++){
			DestroyImmediate(balls[last].Get (i));
		}
		TotalBalls -= layers*(width*width);
		balls.RemoveAt(last);
	}
}  
[Serializable]
public class SerializableList : SerializableListGeneric<GameObject>{

}

[Serializable]
public class SerializableListGeneric<type>{
	[SerializeField]
	public List<type> list = new List<type>();
	public void Add(type input){
		list.Add(input);
	}
	public void Remove(int i){
		list.RemoveAt(i);
	}
	public type Get(int i){
		return list[i];
	}
	public int Count(){
		return list.Count;
	}
	public Boolean isEmpty(){
		return list.Count>0;
	}
}