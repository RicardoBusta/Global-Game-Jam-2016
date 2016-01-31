using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SummonCircleDetector : MonoBehaviour
{
    public GameLoop loop;
    public Text phrase;

    public GameObject explosion;
    public GameObject fart;

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
                    bool summon = loop.CheckFinished(phrase.text);
                    phrase.text = "";
                    StartCoroutine(SuccessAnimation(summon));
                }
                else
                {
                    phrase.text += " ";
                }
            }
        }
    }

    public IEnumerator SuccessAnimation(bool summon)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject prefab = summon ? explosion : fart;
        List<GameObject> explosions = new List<GameObject>();
        foreach (Item ci in collidedItems)
        {
            GameObject exp = (GameObject)Instantiate(prefab, ci.transform.position, Quaternion.identity);
            explosions.Add(exp);
            Destroy(ci.gameObject);
        }
        collidedItems.Clear();

        if (summon)
        {
            Vector3 summonShift = new Vector3(0, 0.6f, 0);
            GameObject capetinha = (GameObject)Instantiate(capetinhaPrefab, summonPosition.position + summonShift, capetinhaPrefab.transform.rotation);
            capetinha.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
            float dir = (Random.Range(-1, 1) > 0 ? 1 : -1);
            Vector3 p = capetinha.transform.localScale;
            capetinha.transform.localScale = new Vector3(p.x * dir, p.y, p.z);

            float exitTime = 1f;

            for (float f = 0; f < exitTime; f += 0.01f)
            {
                capetinha.transform.position = Vector3.Lerp(summonPosition.position + summonShift, summonShift + summonPosition.position + new Vector3(dir * 10, 0, 0), f);
                yield return new WaitForSeconds(0.01f);
            }
            Destroy(capetinha);
        }

        yield return new WaitForSeconds(2);
        foreach (GameObject g in explosions)
        {
            Destroy(g);
        }
    }
}
