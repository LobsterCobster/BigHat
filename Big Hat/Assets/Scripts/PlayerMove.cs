using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public Camera cam;
    public LayerMask ground;
    public LayerMask interactable;

    private bool isInteracting = false;

    private GameObject interactObject;


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
        if (isInteracting && (this.transform.position - interactObject.transform.position).magnitude < 1.0f)
        {
            Interact();
        }
        Move();
    }
    public void Move()
    {

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            if (!cam.enabled && Input.GetMouseButtonDown(0)) 
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, interactable))
                {
                    CameraPoint = hit.point;
                    CameraPoint.y = 0.5f;
                    isInteracting = true;
                    interactObject = hit.transform.gameObject;
                    
                }
                else if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ground))
                {
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
    public void Interact()
    {
        if (isInteracting)
        {
            isInteracting = false;
            interactObject.GetComponent<Dialogue>().Talk();
        }
    }
}
