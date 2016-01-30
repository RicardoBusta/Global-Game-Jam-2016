using UnityEngine;
using System.Collections;

public class BackToTitle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void GoBackToTitle(){
    Application.LoadLevel("TitleScreen");
  }
}
