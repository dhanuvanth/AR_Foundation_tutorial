using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class AnchorManager : MonoBehaviour
{
    private ARRaycastManager aRRaycastManager;

    private ARAnchorManager anchorManager;

    private ARPlaneManager planeManager;

    private List<ARAnchor> anchorPoints = new List<ARAnchor>();

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        anchorManager = GetComponent<ARAnchorManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }


    public void TogglePlane()
    {
        planeManager.enabled = !planeManager.enabled;

        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(planeManager.enabled);
        }


    }

    [System.Obsolete]
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began)
            return;

        if (aRRaycastManager.Raycast(touch.position,hits,UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            ARAnchor anchor = anchorManager.AddAnchor(hitPose);

            if (anchor == null)
            {
                Debug.Log("Error");
            }
            else
            {
                anchorPoints.Add(anchor);
            }
        }

    }
}
