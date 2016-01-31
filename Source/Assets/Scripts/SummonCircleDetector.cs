using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SummonCircleDetector : MonoBehaviour
{
    public GameLoop loop;
    public Text phrase;

    public GameObject explosion;

    public Transform summonPosition;

    public GameObject capetinhaPrefab;

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
                    StartCoroutine(SuccessAnimation());
                }
                else
                {
                    phrase.text += " ";
                }
            }
        }
    }

    public IEnumerator SuccessAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject exp = (GameObject)Instantiate(explosion, summonPosition.position, Quaternion.identity);
        foreach (Item ci in collidedItems)
        {
            Destroy(ci.gameObject);
        }
        collidedItems.Clear();

        GameObject capetinha = (GameObject)Instantiate(capetinhaPrefab,summonPosition.position,capetinhaPrefab.transform.rotation);
        float dir = (Random.Range(-1, 1) > 0 ? 1 : -1);
        Vector3 p = capetinha.transform.localScale;
        capetinha.transform.localScale = new Vector3(p.x * dir, p.y, p.z);
        for (float f = 0; f < 1; f += 0.01f)
        {
            capetinha.transform.position = Vector3.Lerp(summonPosition.position,summonPosition.position + new Vector3(dir*10,0,0),f);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(capetinha);
    }
}
