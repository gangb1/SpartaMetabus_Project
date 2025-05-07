using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponHandler : WeaponHandler
{
    [Header("Melee Attack Info")]
    public Vector2 colliderBoxSize = Vector2.one;

    protected override void Start()
    {
        base.Start();
        colliderBoxSize = colliderBoxSize * WeaponSize;
    }

    public override void Attack()
    {
        base.Attack();

        RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3)Controller.LookDirection * colliderBoxSize.x,colliderBoxSize, 0, Vector2.zero, 0,target);


        if (hit.collider != null)
        {
            HiddenResouceController resouceController = hit.collider.GetComponent<HiddenResouceController>();
            if(resouceController != null)
            {
                resouceController.ChangeHealth(-Power);
                if(IsOnKnockback)
                {
                    HiddenBaseController controller = hit.collider.GetComponent<HiddenBaseController>();
                    if(controller != null)
                    {
                        controller.ApplyKnockback(transform,KnockbackPower,KnockbackTime);
                    }
                }
            }
        }
    }
    public override void Rotate(bool isLeft)
    {
        if(isLeft)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
