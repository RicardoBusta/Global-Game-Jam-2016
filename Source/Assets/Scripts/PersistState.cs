using UnityEngine;
using System.Collections;

public class PersistState : MonoBehaviour
{

    private static PersistState state;

    [HideInInspector]
    public int stage;
    [HideInInspector]
    public int score;

    public int startLevel = 0;


    void Start()
    {
        // singleton
        if (state == null)
        {
            state = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // initialize
        ResetGame();
    }

    public static PersistState GetInstance()
    {
        if (state == null)
        {
            state = GameObject.FindWithTag("Persist State").GetComponent<PersistState>();
        }
        return state;
    }

    public void ResetGame()
    {
        stage = startLevel;
        score = 0;
    }
}
