using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{

    [SerializeField]
    private PlacementObject[] placements;

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
        if (Input.touchCount > 0)
        {
            //get 1st touch
            touchPos = Input.GetTouch(0);

            if (touchPos.phase == TouchPhase.Began)
            {
                Ray ray = aRCamera.ScreenPointToRay(touchPos.position);
                RaycastHit hits;

                //raycast
                if (Physics.Raycast(ray,out hits))
                {
                    //get placementobject if the object has
                    PlacementObject placementObject = hits.transform.GetComponent<PlacementObject>();

                    //change color
                    ChangeColor(placementObject);
                }

            }
        }

    }

    private void ChangeColor(PlacementObject placementObject)
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
            }
            else
            {
                //change color
                current.PlacementObjects = true;  //no need
                meshRenderer.material.color = active;
            }
        }
    }
}
