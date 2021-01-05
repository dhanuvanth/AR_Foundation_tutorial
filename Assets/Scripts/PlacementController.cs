using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementController : MonoBehaviour
{

    [SerializeField]
    private GameObject placePrefab;

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

    //get if touching and the position
    bool touching(out Vector2 touchPos)
    {
        if(Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }
        touchPos = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!touching(out Vector2 touchPosition))
        {
            return;
        }

        if(aRRaycastManager.Raycast(touchPosition, hit, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon)){

            var hitPose = hit[0].pose;
            Instantiate(placePrefab, hitPose.position, hitPose.rotation);

        }
    }
}
