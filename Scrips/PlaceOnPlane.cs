using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;
    private Vector2 touchPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Utility.TryGetInputPosition(out touchPosition)) return;

        if (Utility.Raycast(touchPosition, out Pose hitPose))
        {
            Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
        }
    }
}
