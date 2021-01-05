using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PlaneToggle: MonoBehaviour
{
    private PlacementObject placementObject;

    [SerializeField]
    private Camera aRCamera;

    private Touch touchPos;

    [SerializeField]
    private GameObject placePrefab;

    [SerializeField]
    private ARPlaneManager aRPlanManager;

    private Vector2 scale;

    static List<ARRaycastHit> hit = new List<ARRaycastHit>();

    //set prefab
    public GameObject PlacePrefab
    {
        get
        {
            return placePrefab;
        }
        set
        {
            placePrefab = value;
        }
    }

    //raycast manager
    ARRaycastManager aRRaycastManager;
    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    public void SelectObject(GameObject colorMaterial)
    {
        placePrefab = colorMaterial;
    }

    public void TogglePlane()
    {
        aRPlanManager.enabled = !aRPlanManager.enabled;

        foreach (var plane in aRPlanManager.trackables)
        {
            plane.gameObject.SetActive(aRPlanManager.enabled);
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

            if (EventSystem.current.IsPointerOverGameObject(touchPos.fingerId))
            {
                return;
            }

            if (touchPos.phase == TouchPhase.Began)
            {
                Ray ray = aRCamera.ScreenPointToRay(touchPos.position);
                RaycastHit hits;

                if (Physics.Raycast(ray, out hits))
                {
                    //get placementobject if the object has
                    placementObject = hits.transform.GetComponent<PlacementObject>();

                    //check if the game object has the placementobject
                    if (placementObject != null)
                    {
                        //get placementobject of all the gameobject
                        PlacementObject[] placements = FindObjectsOfType<PlacementObject>();

                        //loop over the gameobjects
                        foreach (var current in placements)
                        {
                            //true if the gameobject and raycast hit is same
                            current.PlacementObjects = current == placementObject;
                        }

                    }
                }

            }

            //On touch released
            if (touchPos.phase == TouchPhase.Ended)
            {
                placementObject.PlacementObjects = false;
            }

            

            if (aRRaycastManager.Raycast(touchPos.position, hit, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {

                var hitPose = hit[0].pose;

                //Spawn a gameobject if it is not present
                if (placementObject == null)
                {
                    placementObject = Instantiate(placePrefab, hitPose.position, hitPose.rotation).GetComponent<PlacementObject>();
                    
                }
                //If touching and dragging
                else if (placementObject.PlacementObjects)
                {
                    placementObject.transform.position = hitPose.position;
                    placementObject.transform.rotation = hitPose.rotation;
                }
            }
        }

    }

}
