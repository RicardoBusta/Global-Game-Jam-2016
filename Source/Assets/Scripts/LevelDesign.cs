using UnityEngine;
using System.Collections;

public class LevelDesign : MonoBehaviour {

  public GameLoop game;
  public static int num_stages = 6;

	void Start () {
	
	}
	
	void Update () {
	
	}

  public void LevelStage(GameLoop myGame){
    game = myGame;

    game.babeGenerateWord = false;
    game.devilGenerateWord = false;

    switch( PersistState.GetPersistState().stage ){
    case 1:
      SetBabeDifficulty(1, 30f, 8f);
      game.matchPoint = 12f; // 2-second cost

      game.ingredientCount = 3;
      game.finisherCount = 1;
      break;
    case 2:
      SetBabeDifficulty(2, 30f, 8f);
      game.matchPoint = 12f; // 2-second cost

      game.ingredientCount = 3;
      game.finisherCount = 2;
      break;
    case 3:
      SetBabeDifficulty(1, 30f, 15f);
      SetDevilDifficulty(1, 40f, 15f);
      game.matchPoint = 12f; // 3-second cost

      game.ingredientCount = 3;
      game.finisherCount = 2;
      break;
    case 4:
      SetBabeDifficulty(1, 30f, 15f);
      SetDevilDifficulty(2, 60f, 20f);
      game.matchPoint = 18f; // 4-second cost

      game.ingredientCount = 5;
      game.finisherCount = 2;
      break;
    case 5:
      SetBabeDifficulty(2, 40f, 15f);
      SetDevilDifficulty(2, 60f, 20f);
      game.matchPoint = 20f; // 5-second cost

      game.ingredientCount = 5;
      game.finisherCount = 3;
      break;
    case 6:
      //SetBabeDifficulty(2, 40f, 15f);
      SetDevilDifficulty(4, 70f, 20f);
      game.matchPoint = 2000f; // 8-second cost

      game.ingredientCount = 6;
      game.finisherCount = 3;
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
