using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {
    public void PlayGame()
    {
        Application.LoadLevel("Interlude");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
