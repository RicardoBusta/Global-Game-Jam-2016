using UnityEngine;
using System.Collections;

public class Capetinha : MonoBehaviour {
  public GameObject leftWing;
  public GameObject rightWing;
  float angle = 0;

	// Use this for initialization
	void Start () {
	
	}

  public void SetColor()
  {
    Color c1 = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
    Color c2 = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
    GetComponent<SpriteRenderer>().color = c1;
    leftWing.GetComponentInChildren<SpriteRenderer>().color = c2;
    rightWing.GetComponentInChildren<SpriteRenderer>().color = c2;
  }
	
	// Update is called once per frame
	void Update () {
    angle += 0.3f;
    float rot = Mathf.Sin(angle)*30;
    leftWing.transform.rotation = Quaternion.Euler(0, 0, rot);
    rightWing.transform.rotation = Quaternion.Euler(0, 0, -rot);
	}
}
