using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interlude : MonoBehaviour {

  public Text interludeText;

  void Start(){
    switch( PersistState.GetInstance().stage ){
    case 0:
      interludeText.text = "While casually summoning fiends, you summon a cute belzebabe. Try to please her by performing her summoning rituals of preference.";
      break;
    case 1:
      interludeText.text = "She speaks her name to you: it's Satania. Amuze her by performing more successful rituals.";
      break;
    case 2:
      interludeText.text = "Satania is wowed. She brings you to her home, so you can meet her dad.";
      break;
    case 3:
      interludeText.text = "Belzebu likes you, but doesn't approve of your relationship with Satania. You have to earn his trust.";
      break;
    case 4:
      interludeText.text = "Belzebu is still not sure about your marriage. Maybe you can convince him?";
      break;
    case 5:
      interludeText.text = "He asks you to complete some major rituals. Don't fail this time!";
      break;
    }
  }

  public void NextStage(){
    PersistState.GetInstance().stage++;
    Application.LoadLevel("MainGame");
  }

}
