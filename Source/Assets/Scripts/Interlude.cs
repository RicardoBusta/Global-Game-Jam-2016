using UnityEngine;
using System.Collections;

public class Interlude : MonoBehaviour {

  public void NextStage(){
    PersistState.GetPersistState().stage++;
    Application.LoadLevel("MainGame");
  }

}
