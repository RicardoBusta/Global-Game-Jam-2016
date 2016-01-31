using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleGenerator : MonoBehaviour {
  public GameObject particlePrefab;
  public int numberOfParticles;
  List<GameObject> particles = new List<GameObject>();

	// Use this for initialization
	void Start () {
    for (int i = 0; i < numberOfParticles; i++)
    {
      Vector3 pos = new Vector3(transform.position.x + Random.Range(-1.0f,1.0f),transform.position.y + Random.Range(0.0f,2.0f),transform.position.z);
      GameObject p = (GameObject)Instantiate(particlePrefab,pos,particlePrefab.transform.rotation);
      p.transform.parent = transform;
      particles.Add(p);
    }
	}
	
	// Update is called once per frame
	void Update () {
    foreach (GameObject p in particles)
    {
      float newy = p.transform.position.y + 0.01f;
      float x = p.transform.position.x;
      if (p.transform.position.y > transform.position.y+2.0f)
      {
        newy = transform.position.y;
        x = transform.position.x + Random.Range(-1.0f,1.0f);
      }
      p.transform.position = new Vector3(x, newy, p.transform.position.z);
    }
	}
}
