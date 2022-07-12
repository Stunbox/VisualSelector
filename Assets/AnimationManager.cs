using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public float sizeAnimDuration = 1f;

    public void SetHighlightOption(Animator animator)
    {
        animator.SetTrigger("H"+animator.gameObject.name);
    }
    public void SetUnHighlightOption(Animator animator)
    {
        animator.SetTrigger("UH" + animator.gameObject.name);
    }
}
