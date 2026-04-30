// Owner: Lee Gangmin
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace SmileFarm.Smile
{
    public static class SmileFaceMeshEstimator
    {
        // These indices map facial landmarks used to estimate a smile from the mesh.
        // These indices are an inferred starting point based on the widely used
        // 468-point canonical face mesh topology. They are not guaranteed by
        // ARCore docs, so we treat them as experimental and tune on device.
        private const int LeftEyeOuter = 33;
        private const int RightEyeOuter = 263;
        private const int LeftMouthCorner = 61;
        private const int RightMouthCorner = 291;
        private const int UpperLipCenter = 13;
        private const int LowerLipCenter = 14;

        public static bool TryEstimate(ARFace face, out SmileMetrics metrics)
        {
            metrics = default;

            if (face == null)
            {
                return false;
            }

            var vertices = face.vertices;
            if (!HasRequiredVertices(vertices.Length))
            {
                return false;
            }

            var leftEyeOuter = vertices[LeftEyeOuter];
            var rightEyeOuter = vertices[RightEyeOuter];
            var leftMouthCorner = vertices[LeftMouthCorner];
            var rightMouthCorner = vertices[RightMouthCorner];
            var upperLipCenter = vertices[UpperLipCenter];
            var lowerLipCenter = vertices[LowerLipCenter];

            var faceScale = Vector3.Distance(leftEyeOuter, rightEyeOuter);
            if (faceScale <= Mathf.Epsilon)
            {
                return false;
            }

            // Normalize mouth movement by face width so scores stay scale-independent.
            var mouthWidth = Vector3.Distance(leftMouthCorner, rightMouthCorner) / faceScale;
            var mouthOpen = Vector3.Distance(upperLipCenter, lowerLipCenter) / faceScale;

            var mouthCenterY = (upperLipCenter.y + lowerLipCenter.y) * 0.5f;
            var cornerAverageY = (leftMouthCorner.y + rightMouthCorner.y) * 0.5f;
            var cornerLift = (cornerAverageY - mouthCenterY) / faceScale;

            metrics = new SmileMetrics(
                NormalizeWidth(mouthWidth),
                NormalizeOpen(mouthOpen),
                NormalizeCornerLift(cornerLift),
                mouthWidth,
                mouthOpen,
                cornerLift);

            return true;
        }

        private static bool HasRequiredVertices(int vertexCount)
        {
            return vertexCount > RightMouthCorner;
        }

        private static float NormalizeWidth(float value)
        {
            // Maps observed width values into a 0-1 gameplay range.
            return Mathf.InverseLerp(0.32f, 0.48f, value);
        }

        private static float NormalizeOpen(float value)
        {
            return Mathf.InverseLerp(0.01f, 0.12f, value);
        }

        private static float NormalizeCornerLift(float value)
        {
            return Mathf.InverseLerp(-0.03f, 0.03f, value);
        }
    }
}

