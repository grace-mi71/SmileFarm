// Owner: Lee Gangmin
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace SmileFarm.Garden
{
    [DisallowMultipleComponent]
    public sealed class FaceFlowerAnchor : MonoBehaviour
    {
        // Reads the currently tracked face so the flower root can follow it.
        [Header("Dependencies")]
        [SerializeField] private ARFaceManager faceManager;

        // Offsets the flower anchor from the face origin toward the forehead area.
        [Header("Forehead Offset")]
        [SerializeField] private Vector3 localPositionOffset = new(0f, 0.12f, 0.06f);
        [SerializeField] private Vector3 localEulerOffset = new(0f, 0f, 0f);
        [SerializeField] private bool hideWhenFaceMissing = true;

        public bool HasTrackedFace => trackedFace != null;

        private ARFace trackedFace;

        private void Reset()
        {
            faceManager = FindFirstObjectByType<ARFaceManager>();
        }

        private void Awake()
        {
            if (faceManager == null)
            {
                faceManager = FindFirstObjectByType<ARFaceManager>();
            }
        }

        private void LateUpdate()
        {
            trackedFace = FindTrackedFace();

            if (trackedFace == null)
            {
                if (hideWhenFaceMissing)
                {
                    // Hide the flower group when no face is available.
                    transform.localScale = Vector3.zero;
                }

                return;
            }

            transform.localScale = Vector3.one;
            // Keep the flower anchor attached to the tracked forehead position.
            transform.SetPositionAndRotation(
                trackedFace.transform.TransformPoint(localPositionOffset),
                trackedFace.transform.rotation * Quaternion.Euler(localEulerOffset));
        }

        private ARFace FindTrackedFace()
        {
            if (faceManager == null)
            {
                return null;
            }

            foreach (var face in faceManager.trackables)
            {
                if (face.trackingState == TrackingState.Tracking)
                {
                    return face;
                }
            }

            return null;
        }
    }
}

