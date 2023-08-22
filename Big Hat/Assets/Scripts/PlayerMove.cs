using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    public Camera cam;
    public LayerMask ground;
    Vector3 CameraPoint;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        cam.enabled = false;
        CameraPoint = transform.position;

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Move()
    {
        if (!cam.enabled && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                Debug.Log("Did Hit");
                CameraPoint = hit.point;
                CameraPoint.y = 0.5f;
            }

        }
        if (this.transform.position != CameraPoint)
        {
            agent.destination = CameraPoint;
        }

    }
}
