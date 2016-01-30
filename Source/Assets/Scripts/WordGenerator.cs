using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordGenerator : MonoBehaviour
{

    public List<string> prefix = new List<string>();
    public List<string> middle = new List<string>();
    public List<string> suffix = new List<string>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Generate()
    {
        string result = prefix[Random.Range(0, prefix.Count)] +
                     prefix[Random.Range(0, prefix.Count)] +
                     prefix[Random.Range(0, prefix.Count)];
        Debug.Log(result);
    }
}
