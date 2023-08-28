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
        LayerMask ground = LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Move()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            if (!cam.enabled && Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ground))
                {
                    Debug.Log("Did Hit");
                    CameraPoint = hit.point;
                    CameraPoint.y = 0.5f;
                }

            }
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            foreach (Touch touch in Input.touches)
            {
                if (!cam && touch.phase == TouchPhase.Began)
                {
                    RaycastHit hit;

                    if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit, Mathf.Infinity))
                    {
                        Debug.Log("Did Hit");
                        CameraPoint = hit.point;
                        CameraPoint.y = 0.5f;
                    }
                }
            } 
        }
        if (this.transform.position != CameraPoint)
        {
            agent.destination = CameraPoint;
        }

    }
}
