using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

  public static float fadeOutTime = 1.0f;
  public static float fadeInTime = 1.0f;

  public Image fade;

  void Start(){
    StartCoroutine( FadeIn() );
  }

  public IEnumerator FadeOut(){
    float clock = 0f;

    while(clock <= fadeOutTime){
      clock += Time.deltaTime;
      fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, (clock/fadeOutTime));
      yield return null;
    }
    fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 1.0f);
//
    yield return null;
  }

  public IEnumerator FadeIn(){
    float clock = 0f;

    while(clock <= fadeInTime){
      clock += Time.deltaTime;
      fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 1f-(clock/fadeOutTime));
      yield return null;
    }
    fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 0.0f);
    //
    yield return null;
  }
}
