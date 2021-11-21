using System.Collections;
using UnityEngine;

public class Trap : EnemyHitBox
{
    private Animator animator;
    public float lateToStart = 1f;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();


        StartCoroutine("StartTrap");
    }

    protected override void OnCollide(Collider2D coll)
    {
        base.OnCollide(coll);
    }

    private IEnumerator StartTrap()
    {
        yield return new WaitForSeconds(lateToStart);
        animator.SetTrigger("start");
    }
}