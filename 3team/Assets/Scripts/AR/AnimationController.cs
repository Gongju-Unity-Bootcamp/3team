using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    public Slider slider;
    public Button button; // 다시 재생 버튼

    [SerializeField]private Animator animator;
    [SerializeField]private float animationLength;

    void Start()
    {
        animator = GetComponent<Animator>();

        // 애니메이터에서 애니메이션 클립 가져오기
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        if (clips.Length > 0)
        {
            animationLength = clips[0].length;
            slider.maxValue = animationLength;
        }
        else
        {
            Debug.LogError("애니메이션이 없습니다.");
        }

        button.onClick.AddListener(PlayAnimationFromStart);
    }

    void Update()
    {
        if (animator != null)
        {
            // 애니메이션의 현재 진행 시간을 슬라이더에 반영
            slider.value = animator.GetCurrentAnimatorStateInfo(0).normalizedTime * animationLength;
        }
    }

    void PlayAnimationFromStart()
    {
        if (animator != null)
        {
            // 애니메이션을 처음부터 다시 재생
            animator.Play(0, 0, 0f);
            // 슬라이더를 0으로 리셋
            slider.value = 0f;
        }
    }
}
