using UnityEngine;
using System.Collections;

public class DragTransform : MonoBehaviour {
    private Color mouseOverColor = Color.blue;
    private Color originalColor = Color.yellow;
    private bool dragging = false;
    private float distance;
    public Collider moveCollider;


    void OnMouseEnter()
    {
        //renderer.material.color = mouseOverColor;
        Debug.Log("Mouse enter");
    }

    void OnMouseExit()
    {
        //renderer.material.color = originalColor;
        Debug.Log("Mouse exit");
    }

    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnMouseUp()
    {
        dragging = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);

            //transform.position = rayPoint;
            RaycastHit hitInfo;
            bool hit = moveCollider.Raycast(ray, out hitInfo,1000);
            if (hit)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.MovePosition(Vector3.Lerp(hitInfo.point,rb.position,0.5f));
                //transform.position = hitInfo.point;
            }
        }
    }
}
