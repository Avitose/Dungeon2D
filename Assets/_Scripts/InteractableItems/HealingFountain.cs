using UnityEngine;

public class HealingFountain : Colliderable
{
    private readonly float healCoolDown = 0.5f;
    public int healingAmount = 1;
    public int healingTotal = 10;
    private float lastHeal;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name != "Player" || !GameManager.instance.player.isAlive) return;
        //Debug.Log("HealingFountain.coll = " + coll.name);
        if (Time.time - lastHeal > healCoolDown && healingTotal > 0)
        {
            Debug.Log(coll.name);

            lastHeal = Time.time;
            healingTotal--;

            GameManager.instance.player.Heal(healingAmount);
        }
    }
}