using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitPoint = 10;


    protected float ImmuneTime = 0.2f;
    protected float lastImmune;
    public int maxHitPoint = 10;

    protected Vector3 pushDirection;
    public float pushRecoverySpeed = 0.2f;


    protected virtual void ReceiveDamage(Damage dmg)
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

    protected virtual void Death()
    {
    }
}