using UnityEngine;
using System.Collections;

public class Breath : MonoBehaviour {	
	// Update is called once per frame
  public float breathSpeed = 0.1f;
  float angle = 0;
  public float intensityX = 0.001f;
  public float intensityY = 0.001f;
  Vector3 originalScale;

  void Start()
  {
    originalScale = transform.localScale;
  }

	void Update () {
    angle += breathSpeed;

    transform.localScale = originalScale + new Vector3(intensityX * Mathf.Cos(angle), intensityY * Mathf.Sin(angle), 0);
	}
}
