using System.Collections;
using UnityEngine;

public class Player : Mover
{
    public bool isAlive = true;
    public float maxRage = 50;

    public float rage;
    private SpriteRenderer spriteRenderer;

    private float temp;

    protected override void Start()
    {
        base.Start();
        GetComponent<BoxCollider2D>().enabled = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        ImmuneTime = 0.75f;
        DontDestroyOnLoad(gameObject);
        OnRageChange(0f);
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");


            if (transform.localScale.x == temp)
                GameManager.instance.weapon.animator.SetBool("SameDirection", true);
            else GameManager.instance.weapon.animator.SetBool("SameDirection", false);
            temp = transform.localScale.x;

            UpdateMotor(new Vector3(x, y, 0), 1);
        }
        else
        {
            pushDirection = Vector3.zero;
        }
    }

    public void SwapSprite(int SkinID)
    {
        GetComponent<SpriteRenderer>().sprite = GameManager.instance.playerSprites[SkinID];
    }

    public void OnLevelUp()
    {
        maxHitPoint += 10;
        hitPoint = maxHitPoint;

        GameManager.instance.OnUIChange();
    }

    public void SetLevel(int level)
    {
        for (var i = 0; i < level; i++)
            OnLevelUp();
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return;

        if (Time.time - lastImmune > ImmuneTime)
        {
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;


            if (!GameManager.instance.weapon.raging)
                OnRageChange(dmg.damageAmount);
        }

        if (hitPoint <= 0)
        {
            hitPoint = 0;
            Death();
        }

        GameManager.instance.OnUIChange();
    }

    public void OnRageChange(float alter)
    {
        if (rage < maxRage)
            rage += alter;
        if (rage >= maxRage)
            rage = maxRage;

        if (rage == maxRage)
            GameManager.instance.weapon.CanRageSkill = true;
    }

    public void Heal(int healingAmount)
    {
        if (hitPoint == maxHitPoint)
            return;

        hitPoint += healingAmount;
        if (hitPoint > maxHitPoint)
            hitPoint = maxHitPoint;

        GameManager.instance.ShowText("+" + healingAmount + "hp", 25, Color.green, transform.position, Vector3.up * 30,
            1.0f);
        GameManager.instance.OnUIChange();
    }

    protected override void Death()
    {
        isAlive = false;
        transform.localEulerAngles = new Vector3(0, 0, 90);

        GameManager.instance.UIManager.ShowDeathAnimation();
        StartCoroutine("WaitingForRespawn");
    }

    public void Respawn()
    {
        Heal(maxHitPoint);
        isAlive = true;
        transform.localEulerAngles = Vector3.zero;
    }

    private IEnumerator WaitingForRespawn()
    {
        yield return new WaitForSeconds(6);
        GameManager.instance.Respawn();
        GameManager.instance.OnUIChange();
    }
}