using UnityEngine;
using System.Collections;

public class PersistState : MonoBehaviour {

  private static PersistState state;

  public int stage;
  public int score;


	void Start () {
    // singleton
    if(state==null){
      state = this;
      DontDestroyOnLoad(gameObject);
    }else{
      Destroy(gameObject);
    }

    // initialize
    ResetGame();
	}

  public static PersistState GetPersistState(){
    if(state==null){
      state = GameObject.FindWithTag("Persist State").GetComponent<PersistState>();
    }
    return state;
  }

  void ResetGame(){
    stage = 1;
    score = 0;
  }
	
	void Update () {
	
	}
}
