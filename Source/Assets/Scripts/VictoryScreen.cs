using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour {
    public void GoToTitle()
    {
      SceneManager.LoadScene("TitleScreen");
    }
  public void GoToCredit()
    {
      SceneManager.LoadScene("CreditsScreen");
    }
}
