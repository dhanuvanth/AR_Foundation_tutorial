using TMPro;
using UnityEngine;

public class PlacementObjectAndText : MonoBehaviour
{

    public bool PlacementObjects
    {
        get;set;
    }

    [SerializeField]
    private TextMeshPro textMesh;

    [SerializeField]
    private string sphere = "sphere";

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshPro>();
        textMesh.text = sphere;
        textMesh.enabled = false;
    }

}
