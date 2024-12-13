using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
namespace Portal
{
    public class InteractManager : MonoBehaviour
    {
        // Reference to the ARRaycastManager component
        [SerializeField] private ARRaycastManager raycastManager;
        [SerializeField] private ARPlaneManager arPlaneManager; 
        [SerializeField] private TextMeshProUGUI debugText;
        // List to store ARRaycast hits
        private List<ARRaycastHit> _hits = new List<ARRaycastHit>();
        private bool _portalSpawned;
        private void Awake()
        {
            if (raycastManager == null)
            {
                Debug.LogError("ARRaycastManager is not attached to the GameObject.");
            }
        }
        private void Update()
        {
            if(_portalSpawned)return;
            // Detect if the player taps on the screen
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Get the touch position
                Vector2 touchPosition = Input.GetTouch(0).position;

                // Perform an AR raycast and check if it hits a plane
                if (raycastManager.Raycast(touchPosition, _hits, TrackableType.PlaneWithinPolygon))
                {
                    // Get the pose (position and rotation) of the raycast hit
                    Pose hitPose = _hits[0].pose;

                    // Output the hit position
                    debugText.text = "Plane detected";
                    // Optionally, place an object or perform actions here
                    Instantiate(raycastManager.raycastPrefab, hitPose.position, hitPose.rotation);
                    arPlaneManager.SetTrackablesActive(false);
                    arPlaneManager.enabled = false;
                    _portalSpawned = true;
                }
                else
                {
                    debugText.text = "No plane detected";
                }
            }
        }
        private void OnEnable()
        {
            if (arPlaneManager != null)
            {
                arPlaneManager.planesChanged += OnPlanesChanged;
            }
        }

        private void OnDisable()
        {
            if (arPlaneManager != null)
            {
                arPlaneManager.planesChanged -= OnPlanesChanged;
            }
        }

        private void OnPlanesChanged(ARPlanesChangedEventArgs args)
        {
            /*var arPlanes = FindObjectsOfType<ARPlane>().ToList();
            foreach (var arPlane in arPlanes)
            {
                arPlane.gameObject.SetActive(false);
            }
            arPlanes.Last().gameObject.SetActive(true);
            Debug.Log("Last AR Plane " + arPlanes.Last().gameObject);*/
        }
    }
}
