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

    void OnTriggerEnter(Collider other)
    {
        Item i = other.GetComponent<Item>();
        if (i != null)
        {
            //if (Vector3.Distance(i.transform.position, transform.position) > GetComponent<BoxCollider>().size.x / 2) return;
            if (!i.placed)
            {
                collidedItems.Add(i);
                phrase.text += i.word;
                i.Disable();
                i.acceptedItem = true;
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
                else
                {
                    phrase.text += " ";
                }
            }
        }
    }
}
