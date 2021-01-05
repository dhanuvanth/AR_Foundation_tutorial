using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectSelectionwithCamera : MonoBehaviour
{

    [SerializeField]
    private PlacementObjectAndText[] placements;

    [SerializeField]
    private Color active = Color.red;

    [SerializeField]
    private Color inactive = Color.blue;

    [SerializeField]
    private Camera aRCamera;
    
    [SerializeField]
    private Transform raypoint;

    [SerializeField]
    private float sec = 2f;

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

        StartCoroutine(Raycasting());

    }

    IEnumerator Raycasting()
    {
        Ray ray = aRCamera.ScreenPointToRay(raypoint.position);
        RaycastHit hits;

        //raycast
        if (Physics.Raycast(ray, out hits, 100f))
        {
            //get placementobject if the object has
            PlacementObjectAndText placementObject = hits.transform.GetComponent<PlacementObjectAndText>();

            //change color
            ChangeColor(placementObject);
        }

        yield return new WaitForSeconds(sec);
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
