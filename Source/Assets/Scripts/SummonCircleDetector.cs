using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SummonCircleDetector : MonoBehaviour
{
    public GameLoop loop;
    public Text phrase;

    List<Item> collidedItems = new List<Item>();

    // Use this for initialization
    void Start()
    {
        phrase.text = "";
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
                collidedItems.Add(i);
                phrase.text += i.word;
                i.placed = true;
                i.GetComponent<DragTransform>().enabled = false;
                if (i.finisher)
                {
                    Debug.Log("Finisher!");
                    loop.CheckFinished(phrase.text);
                    phrase.text = "";
                    foreach (Item ci in collidedItems)
                    {
                        Destroy(ci.gameObject);
                    }
                    collidedItems.Clear();
                }
            }
        }
    }
}
