using System.Collections.Generic;
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
        [SerializeField] private TextMeshProUGUI debugText;
        // List to store ARRaycast hits
        private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

        void Awake()
        {
            if (raycastManager == null)
            {
                Debug.LogError("ARRaycastManager is not attached to the GameObject.");
            }
        }

        void Update()
        {
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
                    // Example: Instantiate(prefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    debugText.text = "No plane detected";
                }
            }
        }
    }
}
