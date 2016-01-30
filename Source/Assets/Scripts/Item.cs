using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public string word;
    public bool finisher;
    public TextMesh name;
    public bool placed = false;
    public Vector3 startPosition;
    public GameObject prefab;
    public bool wasReleased = false;
    float lifeTime = 5.0f;
    public bool acceptedItem = false;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (wasReleased && !acceptedItem)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
	}

    public void Disable()
    {
        //GameObject go = (GameObject)Instantiate(prefab, startPosition, Quaternion.identity);
        //go.GetComponent<Item>().prefab = prefab;
        placed = true;
        GetComponent<DragTransform>().enabled = false;        
    }

    public IEnumerator WaitAndRespawn()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject go = (GameObject)Instantiate(prefab, startPosition, prefab.transform.rotation);
        go.GetComponent<Item>().prefab = prefab;
    }
}
