﻿using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;

public class DummyParse : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ParseObject testObj = new ParseObject("TestObject");
        testObj["a"] = "HI";
        testObj["b"] = "Back4app";
        Task saveTask = testObj.SaveAsync();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
