using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ReticleController : MonoBehaviour
{
    [Header("Scene References:")]
    [Tooltip("The reference to the default AR Plane Manager in the scene")]
    [SerializeField]
    private ARPlaneManager planeManager;

    [Tooltip("The reference to the default AR Raycast Manager in the scene")]
    [SerializeField]
    private ARRaycastManager raycastManager;

    [SerializeField]
    private Transform camTransform;

    [Header("Asset References:")]
    [SerializeField]
    private GameObject reticlePrefab;

    [SerializeField]
    private FurnitureConfig selectedFurnitureConfig;

    public GameManager gameManager;

    private GameObject reticle;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Start is called before the first frame update

    void Awake()
    {
        reticle = Instantiate(reticlePrefab);
        reticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 center = ScreenUtils.GetCenterOfScreen();

        if (raycastManager.Raycast(center, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinBounds))
        {
            RepositionReticle();
        }
        else
        {
            reticle.SetActive(false);
        }

    }

    private void RepositionReticle()
    {
        Pose pose = hits[0].pose;

        Vector3 vecToCam = (camTransform.position - pose.position).normalized;

        Vector3 planeNormal = pose.rotation * Vector3.up;

        if (Vector3.Dot(vecToCam, planeNormal) > 0)
        {
            reticle.SetActive(true);
            reticle.transform.SetPositionAndRotation(pose.position, pose.rotation);
        }
        else
        {
            reticle.SetActive(false);
        }
    }


    public void PlaceFurniture()
    {
        if (hits.Count > 0)
        {
            ARPlane hitPlane = planeManager.GetPlane(hits[0].trackableId);
           
            if (hitPlane != null)
            {
                if (selectedFurnitureConfig.FitsOnPlane(hitPlane) && selectedFurnitureConfig.IsCorrectPlaneType(hitPlane))
                {
                    Instantiate(selectedFurnitureConfig.prefab, reticle.transform).GetComponent<PrefabScript>().Init(gameManager);
                }
            }
            else
            {
                Debug.Log("Error: Trackable ID does not correspond to an ARPlane");
            }
        }
        else
        {
            Debug.Log("Error: No hits recorded by raycast manager");
        }
    }
}
