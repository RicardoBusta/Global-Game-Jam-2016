using UnityEngine;
using System.Collections;

public class LevelDesign : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void LevelStage(GameLoop game){
    game.babeGenerateWord = true;
    game.babeWordCount = 2;
//    game.babeMaxSlider = 30f;
    game.babeSlider.maxValue = 30f;
    game.babeSlider.value = 10f;

    game.devilGenerateWord = false;
    game.devilWordCount = 2;
//    game.devilMaxSlider = 60f;
    game.devilSlider.maxValue = 60f;
    game.devilSlider.value = game.devilSlider.maxValue;

    game.matchPoint = 10f; // 3-second cost
  }
}
