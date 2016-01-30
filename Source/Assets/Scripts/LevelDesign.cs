using UnityEngine;
using System.Collections;

public class LevelDesign : MonoBehaviour {

  public GameLoop game;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void LevelStage(GameLoop myGame){
    game = myGame;

    game.babeGenerateWord = false;
    game.devilGenerateWord = false;

    switch( PersistState.GetPersistState().stage ){
    case 1:
      SetBabeDifficulty(2, 30f, 10f);
      game.matchPoint = 100f; // 3-second cost
      break;
    case 2:
      SetBabeDifficulty(2, 30f, 10f);
      SetDevilDifficulty(1, 60f, 20f);
      game.matchPoint = 100f; // 3-second cost
      break;
    case 3:
      SetBabeDifficulty(2, 30f, 10f);
      SetDevilDifficulty(2, 60f, 20f);
      game.matchPoint = 10f; // 3-second cost
      break;
    case 4:
      SetBabeDifficulty(2, 30f, 10f);
      SetDevilDifficulty(2, 60f, 20f);
      game.matchPoint = 10f; // 3-second cost
      break;
    case 5:
      SetBabeDifficulty(2, 30f, 10f);
      SetDevilDifficulty(2, 60f, 20f);
      game.matchPoint = 10f; // 3-second cost
      break;
    }
      
  }

  public void SetBabeDifficulty(int words, float maxValue, float startValue){
    game.babeGenerateWord = true;
    game.babeWordCount = words;
    game.babeSlider.maxValue = maxValue;
    game.babeSlider.value = startValue;
  }

  public void SetDevilDifficulty(int words, float maxValue, float startValue){
    game.devilGenerateWord = true;
    game.devilWordCount = words;
    game.devilSlider.maxValue = maxValue;
    game.devilSlider.value = startValue;
  }
}
