// Owner: Choi Eun-young
// Description: Custom UI/Object animation controller using DOTween for various 'Pop-Up' effects.

using UnityEngine;
using DG.Tweening;

public class PopUpAnimation : MonoBehaviour
{
    public enum AnimationType
    {
        Pop,       // Standard bouncy pop-up
        SpinGrow,  // Spiraling growth appearance
        Jelly,     // Elastic, liquid-like scaling
        FlyIn      // Flying in from an offset position
    }

    public AnimationType animType = AnimationType.Pop;

    [SerializeField] private Vector3 targetScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private float duration = 0.5f;

    [Header("FlyIn Settings")]
    [SerializeField] private Vector3 flyOffset = new Vector3(0f, 5f, -5f);
    private Vector3 originalPosition;

    private void Awake()
    {
        // Store the design-time position to ensure consistent resets
        originalPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        // Stop any running tweens to prevent animation overlapping
        transform.DOKill();

        switch (animType)
        {
            case AnimationType.Pop:
                // Resets scale and uses OutBack for a snappy bounce effect
                transform.localScale = Vector3.zero;
                transform.localPosition = originalPosition;
                transform.DOScale(targetScale, duration).SetEase(Ease.OutBack);
                break;

            case AnimationType.SpinGrow:
                // Combines 360-degree rotation with scaling
                transform.localScale = Vector3.zero;
                transform.localPosition = originalPosition;
                transform.localRotation = Quaternion.Euler(0, -360, 0);
                transform.DOLocalRotate(Vector3.zero, duration, RotateMode.FastBeyond360).SetEase(Ease.OutBack);
                transform.DOScale(targetScale, duration).SetEase(Ease.OutBack);
                break;

            case AnimationType.Jelly:
                // Uses OutElastic for a wobbly, high-frequency vibration
                transform.localScale = Vector3.zero;
                transform.localPosition = originalPosition;
                transform.DOScale(targetScale, duration + 0.5f).SetEase(Ease.OutElastic);
                break;

            case AnimationType.FlyIn:
                // Moves the object from a specific offset back to its origin while scaling up
                transform.localScale = Vector3.zero;
                transform.localPosition = originalPosition + flyOffset;

                // Move uses OutQuad for smooth deceleration; Scale uses OutBack for a landing "thud"
                transform.DOLocalMove(originalPosition, duration).SetEase(Ease.OutQuad);
                transform.DOScale(targetScale, duration).SetEase(Ease.OutBack);
                break;
        }
    }
}