using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectSelectionAndText : MonoBehaviour
{

    [SerializeField]
    private PlacementObjectAndText[] placements;

    [SerializeField]
    private Color active = Color.red;

    [SerializeField]
    private Color inactive = Color.blue;

    [SerializeField]
    private Camera aRCamera;

    private Touch touchPos;

    private MeshRenderer meshRenderer;

    void Awake()
    {
        foreach (var sphere in placements)
        {
            sphere.GetComponent<MeshRenderer>().material.color = inactive;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //chech if touch is > 0
        if (Input.touchCount > 0 && touchPos.phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            //get 1st touch
            touchPos = Input.GetTouch(0);

            //var mousePos = Input.mousePosition;
            //Ray ray = aRCamera.ScreenPointToRay(mousePos);


            Ray ray = aRCamera.ScreenPointToRay(touchPos.position);
            RaycastHit hits;

            //raycast
            if (Physics.Raycast(ray,out hits))
            {
                //get placementobject if the object has
                PlacementObjectAndText placementObject = hits.transform.GetComponent<PlacementObjectAndText>();

                //change color
                ChangeColor(placementObject);
            }
        }

    }

    private void ChangeColor(PlacementObjectAndText placementObject)
    {
        //loop over the gameobjects
        foreach (var current in placements)
        {
            //get meshrenderer from the game objects
            meshRenderer = current.GetComponent<MeshRenderer>();

            //check if the gameobject the raycast hit gameobject is not equal
            if(current != placementObject)
            {
                //change color
                current.PlacementObjects = false;  //no need
                meshRenderer.material.color = inactive;
                current.GetComponentInChildren<TextMeshPro>().enabled = false;
            }
            else
            {
                //change color
                current.PlacementObjects = true;  //no need
                meshRenderer.material.color = active;
                current.GetComponentInChildren<TextMeshPro>().enabled = true;
                Debug.Log("working");
            }
        }
    }
}
