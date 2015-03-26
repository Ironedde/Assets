using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class CreateBalls : MonoBehaviour {

	/*Remove the comments for the code to remove 
	 * clutter in the inspector if you choose to  
	 * show the list of balls in the inspector (not default)
	 * And remove code above the comment
	 */
	public GameObject toCreate;
	public Vector3 position;
	public int layers = 5;
	public int width = 5;
	public float distance = 2.0f;
	[SerializeField]
	private int TotalBalls;
	private int balsCreated;

	[HideInInspector]
	[SerializeField]
	private List<SerializableList> balls = new List<SerializableList>();

	[HideInInspector]
	[SerializeField]
	private List<GameObject> ballParents = new List<GameObject>();
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Create(){
		TotalBalls = CountBalls();
		if(toCreate == null){
			throw new UnassignedReferenceException("Enter a Object to create");
		}
		if((layers*(width*width))>4000){
			print("TO MANY BALS!!");
		}
		else{
			GameObject ballParent = new GameObject("Balls");
			ballParents.Add(ballParent);
			SerializableList toAdd = new SerializableList();
			Vector3 currentPos = position;
			for(int i=0;i<layers;i++){
				for(int o = 0;o<width;o++){
					currentPos.z = position.z+o;
					for(int p = 0;p<width;p++){
						GameObject tmp = (GameObject)Instantiate(toCreate,currentPos,Quaternion.identity);
						tmp.transform.parent = ballParent.transform;
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
			//balls.Add(ballParent);
		}
	}
	public void DeletePrevious(){
		TotalBalls = CountBalls();
		if(balls.Count == 0){
			if(ballParents.Count >0){
				ballParents.Clear();
			}
			return;
		}
		int balls_last = balls.Count-1;
		int parent_last = ballParents.Count-1;

		//Destroys the ballgroups parent (Destroys all the balls in the group) 
		DestroyImmediate(ballParents[parent_last]);

		TotalBalls -= layers*(width*width);

		//Removes the now empty ball list and the parent
		balls.RemoveAt(balls_last);
		ballParents.RemoveAt(parent_last);

		#region
		/*Use this for a less clutered inspector 
		(if you choose to show the list, not default)*/
		/*
		if(balls.Count() == 0){
			return;
		}
		int last = balls.Count()-1;
		for(int i = 0;i<balls.Count();i++){
			DestroyImmediate(balls.Get(i));
		}
		TotalBalls -= layers*(width*width);
		balls.RemoveAt(last);
		*/
		#endregion
	}
	private int CountBalls(){
		int tmpCounter = 0;
		GameObject[] array;
		foreach(SerializableList ballGroup in balls){
			array = ballGroup.Copy();
			foreach(GameObject obj in array){
				if(obj != null){
					tmpCounter += 1;
				}
				else{
					ballGroup.Remove(obj);
				}
			}
		}
		return tmpCounter;
	}
}  

[Serializable]
public class SerializableList : SerializableListGeneric<GameObject>{

}

//[Serializable]
public class SerializableListGeneric<type>{
	public List<type> list = new List<type>();
	public void Add(type input){
		list.Add(input);
	}
	public void Remove(type obj){
		list.Remove(obj);
	}
	public void RemoveAt(int i){
		list.RemoveAt(i);
	}
	/*
	public type this[int i]{
		get{
			CreateBalls.printf("GET");
			return list[i];
		}
		set{
		}

	}*/
	public int Count(){
		return list.Count;
	}
	public type Get(int i){
		return list[i];
	}
	public Boolean isEmpty(){
		return list.Count>0;
	}
	public List<type> GetAll(){
		return list;
	}
	public type[] Copy(){
		type[] array = new type[list.Count];
		list.CopyTo(array);
		return array;
	}
}