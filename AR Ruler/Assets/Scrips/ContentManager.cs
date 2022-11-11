using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class ContentManager : MonoBehaviour
{
    public ARPlaneManager arPlaneManager; //AR Session Origin에 있는 ARPlaneManager
    public ARRaycastManager arRaycastManager; //AR Session Origin에 있는 ARRaycastManage

    public GameObject placePrefab; //지면에 생성시킬 오브젝트
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began && !uiCheckPointer())
            {
                if(arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    Instantiate(placePrefab, hitPose.position, hitPose.rotation);
                }
            }
        }
    }

    public GameObject[] placePrefabs;
    public Material[] planeMaterials;

    public void SetPlacePrefabs(int number)
    {
        placePrefab = placePrefabs[number];
    }

    public void SetPlaneMaterial(int number)
    {
        this.GetComponent<ARPlaneManager>().planePrefab.GetComponent<Renderer>().material = planeMaterials[number];

        foreach (var plane in arPlaneManager.trackables)
        {
            plane.GetComponent<Renderer>().material = planeMaterials[number];
        }
    }

    private bool uiCheckPointer()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}
