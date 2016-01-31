using UnityEngine;
using System.Collections;

public class DragTransform : MonoBehaviour {
    private Color mouseOverColor = Color.blue;
    private Color originalColor = Color.yellow;
    private bool dragging = false;
    private float distance;
    Collider moveCollider;
    Collider floorCollider;
    Vector3 previousPos;

    void Start()
    {
        moveCollider = GameObject.FindWithTag("Ingredient Collider").GetComponent<Collider>();
        floorCollider = GameObject.FindWithTag("Floor").GetComponent<Collider>();
        previousPos = transform.position;
    }

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
        if (!enabled) return;
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Item i = GetComponent<Item>();
        i.StartCoroutine(i.WaitAndRespawn());
        i.userDragged = true;
        SoundManager.GetInstance().PlayItemGetSound( gameObject.name );
    }

    void OnMouseUp()
    {
        if (!enabled) return;
        dragging = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        //rb.AddForce(25 * (rb.position - previousPos) + new Vector3(0,0,2),ForceMode.VelocityChange);
        // ideia do renan
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool hit = floorCollider.Raycast(ray, out hitInfo, 1000);
        if (hit)
        {
            rb.AddForce(5*((hitInfo.point - rb.position).normalized) + new Vector3(0,0.5f,0), ForceMode.VelocityChange);
            float torque = 20.0f;
            rb.AddTorque(new Vector3(Random.Range(-torque,torque),Random.Range(-torque,torque),Random.Range(-torque,torque)));
        }
        // ideia do renan
        GetComponent<Item>().wasReleased = true;
        enabled = false;
        SoundManager.GetInstance().PlayItemFallSound( gameObject.name );
    }

    void Update()
    {
        if (!enabled) return;
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
                previousPos = rb.position;
                rb.MovePosition(Vector3.Lerp(hitInfo.point,rb.position,0.5f));
               // rb.AddForce(Time.deltaTime*20*(hitInfo.point - rb.position), ForceMode.VelocityChange);
                //transform.position = hitInfo.point;
            }
        }
    }
}
