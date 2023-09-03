using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotateCamera : MonoBehaviour
{
    public float Speed = 5;
    public GameObject crosshair;
    public LayerMask capture;
    private GameObject photo;
    private Camera cam;
    public Text text;
    public Vector3 Origin;
    // Start is called before the first frame update
    void Start()
    {
        photo = GameObject.Find("Camera");
        cam = photo.GetComponent<Camera>();
        Origin = cam.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCam();
        ObjectSight();
    }
    void RotateCam()
    {
        // ISSUE: GetMouse Button Overlaps with pressing on the Camera Button
        if (cam.enabled)
        {
            if (Input.GetMouseButton(0))
            {
                transform.eulerAngles += Speed * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        else
        {
            transform.eulerAngles = Origin;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void ObjectSight()
    {
        if (gameObject.GetComponent<Camera>().enabled)
        {
            Image image = crosshair.gameObject.GetComponent<Image>();
            RaycastHit hit;
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "Capture")
                {
                    bool captured = false;
                    foreach (var i in gameObject.GetComponent<Photo>().photos)
                    {
                        if (i.Object.name == hit.transform.name  && i.isCaptured == true)
                        {
                            captured = true; 
                            break;
                        }
                    }
                    if (captured)
                    {
                        image.color = new Color(0, 255, 0, 143);
                        DisplayCaptured();
                    }
                    else
                    {
                        image.color = new Color(255, 0, 0, 143);
                        NotCaptured();
                    }
                }
                else
                {
                    image.color = new Color(255, 255, 255, 143);
                    NotCaptured();
                }

            }
            else
            {
                image.color = new Color(255, 255, 255, 143);
                NotCaptured();
            }
        }
        
    }
    public void DisplayCaptured()
    {
        if (photo.GetComponent<Photo>().timer == false)
        {
            text.text = "Object Already Captured";
            text.color = new Color(0, 255, 0, 255);
        }
    }
    public void NotCaptured()
    {
        text.text = "";
        text.color = new Color(0, 0, 0, 255);
    }
}