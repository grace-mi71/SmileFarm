using UnityEngine;
using DG.Tweening;

public class PopUpAnimation : MonoBehaviour
{
    public enum AnimationType
    {
        Pop,        // 통통 튀어오르기 (기본)
        SpinGrow,   // 빙그르르 돌며 나타나기
        Jelly       // 말랑말랑 젤리처럼 나타나기
    }

    public AnimationType animType = AnimationType.Pop;

    [SerializeField] private Vector3 targetScale = new Vector3(1f, 1f, 1f);

    [SerializeField] private float duration = 0.5f;

    private void OnEnable()
    {
        transform.DOKill();

        // 시작 크기는0으로 
        transform.localScale = Vector3.zero;

        // 선택한 애니메이션에 맞춰서 재생
        switch (animType)
        {
            case AnimationType.Pop:
                // 1. 기본 팝업: 통통 튀며 커짐
                transform.DOScale(targetScale, duration).SetEase(Ease.OutBack);
                break;

            case AnimationType.SpinGrow:
                // 2. 스핀 팝업: 1바퀴(-360도) 빙글 돌면서 커짐
                transform.localRotation = Quaternion.Euler(0, -360, 0);
                transform.DOLocalRotate(Vector3.zero, duration, RotateMode.FastBeyond360).SetEase(Ease.OutBack);
                transform.DOScale(targetScale, duration).SetEase(Ease.OutBack);
                break;

            case AnimationType.Jelly:
                // 3. 젤리 바운스: 말랑하게 커짐
                transform.DOScale(targetScale, duration + 0.5f).SetEase(Ease.OutElastic);
                break;
        }
    }
}