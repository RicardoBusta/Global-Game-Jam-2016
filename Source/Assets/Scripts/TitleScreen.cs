using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
  public void PlayGame()
  {
    SceneManager.LoadScene("Interlude");
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void Credits()
  {
    SceneManager.LoadScene("CreditsScreen");
  }
}
