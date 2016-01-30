using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    public void GoToTitleScreen() {
        Application.LoadLevel("TitleScreen");
    }
}
