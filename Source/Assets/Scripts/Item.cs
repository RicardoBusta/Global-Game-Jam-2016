using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public string word;
    public bool finisher;
    public TextMesh name;
    public bool placed = false;

	// Use this for initialization
	void Start () {
        name.text = word;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}
}
