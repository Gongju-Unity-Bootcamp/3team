using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    public Slider slider;
    public Button button; // �ٽ� ��� ��ư

    [SerializeField]private Animator animator;
    [SerializeField]private float animationLength;

    void Start()
    {
        animator = GetComponent<Animator>();

        // �ִϸ����Ϳ��� �ִϸ��̼� Ŭ�� ��������
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        if (clips.Length > 0)
        {
            animationLength = clips[0].length;
            slider.maxValue = animationLength;
        }
        else
        {
            Debug.LogError("�ִϸ��̼��� �����ϴ�.");
        }

        button.onClick.AddListener(PlayAnimationFromStart);
    }

    void Update()
    {
        if (animator != null)
        {
            // �ִϸ��̼��� ���� ���� �ð��� �����̴��� �ݿ�
            slider.value = animator.GetCurrentAnimatorStateInfo(0).normalizedTime * animationLength;
        }
    }

    void PlayAnimationFromStart()
    {
        if (animator != null)
        {
            // �ִϸ��̼��� ó������ �ٽ� ���
            animator.Play(0, 0, 0f);
            // �����̴��� 0���� ����
            slider.value = 0f;
        }
    }
}
