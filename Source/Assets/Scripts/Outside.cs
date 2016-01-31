using UnityEngine;
using System.Collections;

public class Outside : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Item i = other.GetComponent<Item>();
        if (i != null)
        {
            //if (Vector3.Distance(i.transform.position, transform.position) > GetComponent<BoxCollider>().size.x / 2) return;
            if (!i.placed && !i.userDragged)
            {
                i.wasReleased = true;
                i.Disable();
                i.Respawn();
            }
        }
    }
}
