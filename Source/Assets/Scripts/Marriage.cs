using UnityEngine;
using System.Collections;

public class Marriage : MonoBehaviour {

  public Fade fade;

  public void ExitMarriage(){
    StartCoroutine(ExitToVictory());
  }

  public IEnumerator ExitToVictory(){
    fade.FadeOut();
    yield return new WaitForSeconds( Fade.fadeOutTime );

    Application.LoadLevel("Victory");

    yield return null;
  }
}
