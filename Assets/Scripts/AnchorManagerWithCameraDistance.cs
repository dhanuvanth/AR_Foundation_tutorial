using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARAnchorManager))]
public class AnchorManagerWithCameraDistance : MonoBehaviour
{

    [SerializeField]
    private Camera arCamera;

    private ARAnchorManager anchorManager;

    private void Awake()
    {
        anchorManager = GetComponent<ARAnchorManager>();
    }

    [System.Obsolete]
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began)
            return;

        Vector3 newPose = arCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0.3f));
        anchorManager.AddAnchor(new Pose(newPose,Quaternion.identity));
    }
}
