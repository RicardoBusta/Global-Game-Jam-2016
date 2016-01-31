using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    public void GoToTitleScreen() {
      PersistState.GetInstance().ResetGame();
      Application.LoadLevel("TitleScreen");
    }
}
