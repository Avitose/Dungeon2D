using UnityEngine;

public class Crate : Fighter
{
    private void Start()
    {
        ImmuneTime = 0.5f;
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > ImmuneTime)
        {
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
        }

        if (hitPoint <= 0)
        {
            hitPoint = 0;
            Death();
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
    }
}