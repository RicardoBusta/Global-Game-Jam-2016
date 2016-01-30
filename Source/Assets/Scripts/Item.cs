using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public string word;
    public bool finisher;
    public TextMesh name;
    public bool placed = false;
    public Vector3 startPosition;
    public GameObject prefab;

	// Use this for initialization
	void Start () {
        name.text = word;
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}

    public void DisableAndRespawn()
    {
        GameObject go = (GameObject)Instantiate(prefab, startPosition, Quaternion.identity);
        go.GetComponent<Item>().prefab = prefab;
        placed = true;
        GetComponent<DragTransform>().enabled = false;        
    }
}
