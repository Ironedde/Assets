﻿using UnityEngine;
using System.Collections;

public class CollisonDeletion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}// Destroy everything that enters the trigger
	
	void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}
}
