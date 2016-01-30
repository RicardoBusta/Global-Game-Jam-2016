using UnityEngine;
using System.Collections;

public class OutsideDetector : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Item i = other.GetComponent<Item>();
        if (i != null)
        {
            if (!i.placed)
            {
            i.DisableAndRespawn();
            }
        }
    }
}
