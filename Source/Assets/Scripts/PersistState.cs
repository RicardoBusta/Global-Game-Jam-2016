using UnityEngine;
using System.Collections;

public class PersistState : MonoBehaviour
{

    private static PersistState state;

    [HideInInspector]
    public int stage;
    [HideInInspector]
    public int score;

    public int startLevel = 1;


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

    public static PersistState GetPersistState()
    {
        if (state == null)
        {
            state = GameObject.FindWithTag("Persist State").GetComponent<PersistState>();
        }
        return state;
    }

    void ResetGame()
    {
        stage = startLevel;
        score = 0;
    }

    void Update()
    {

    }
}
