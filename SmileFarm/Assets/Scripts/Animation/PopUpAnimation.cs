using UnityEngine;
using DG.Tweening;

public class PopUpAnimation : MonoBehaviour
{
    public enum AnimationType
    {
        Pop,        // 통통 튀어오르기
        SpinGrow,   // 빙그르르 돌며 나타나기
        Jelly,      // 말랑말랑 젤리
        FlyIn       // 저 멀리서 날아오기 (새 전용)
    }

    public AnimationType animType = AnimationType.Pop;

    [SerializeField] private Vector3 targetScale = new Vector3(1f, 1f, 1f);

    [SerializeField] private float duration = 0.5f;

    [Header("FlyIn option")]
    [SerializeField] private Vector3 flyOffset = new Vector3(0f, 5f, -5f); 
    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        transform.DOKill();

        switch (animType)
        {
            case AnimationType.Pop:
                transform.localScale = Vector3.zero;
                transform.localPosition = originalPosition; // 위치 고정
                transform.DOScale(targetScale, duration).SetEase(Ease.OutBack);
                break;

            case AnimationType.SpinGrow:
                transform.localScale = Vector3.zero;
                transform.localPosition = originalPosition;
                transform.localRotation = Quaternion.Euler(0, -360, 0);
                transform.DOLocalRotate(Vector3.zero, duration, RotateMode.FastBeyond360).SetEase(Ease.OutBack);
                transform.DOScale(targetScale, duration).SetEase(Ease.OutBack);
                break;

            case AnimationType.Jelly:
                transform.localScale = Vector3.zero;
                transform.localPosition = originalPosition;
                transform.DOScale(targetScale, duration + 0.5f).SetEase(Ease.OutElastic);
                break;

            case AnimationType.FlyIn:
                // 1. 크기를 0으로 시작
                transform.localScale = Vector3.zero;

                transform.localPosition = originalPosition + flyOffset;

                transform.DOLocalMove(originalPosition, duration).SetEase(Ease.OutQuad); // 부드럽게 감속하며 날아옴
                transform.DOScale(targetScale, duration).SetEase(Ease.OutBack); // 도착하면서 살짝 통통 튀는 디테일
                break;
        }
    }
}