using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace SmileFarm.Garden
{
    [DisallowMultipleComponent]
    public sealed class FaceFlowerAnchor : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private ARFaceManager faceManager;

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
                    transform.localScale = Vector3.zero;
                }

                return;
            }

            transform.localScale = Vector3.one;
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

