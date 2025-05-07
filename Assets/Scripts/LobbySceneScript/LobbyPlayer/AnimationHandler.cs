using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsPush = Animator.StringToHash("IsPush");
    private static readonly int IsActive = Animator.StringToHash("IsActive");


    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }

    public void Active()
    {
        animator.SetBool(IsActive, true);
    }

    public void DeActive()
    {
        animator.SetBool(IsActive, false);
    }

}
