using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    private readonly Collider2D[] hits = new Collider2D[10];
    public bool canRespawn = true;

    public float chaseLength = 1.0f;
    public bool chasing;
    public bool collidingWithPlayer;

    public bool drawTriggerLength;


    public SpriteRenderer enemyStateSprite;

    public ContactFilter2D filter;
    private BoxCollider2D hitBox;
    private bool isAlive = true;

    private Transform playTransform;

    public float speedMultiple = 0.75f;

    private Vector3 startingPosition;
    public List<Sprite> stateSprites;
    public float timeToRespawn = 10f;
    public float triggerLength = 1.0f;
    public int xpValue = 1;

    protected override void Start()
    {
        base.Start();
        playTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;

        hitBox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        CloseStateSprite();
    }

    protected virtual void Update()
    {
        collidingWithPlayer = false;

        if (drawTriggerLength)
        {
            Debug.DrawLine(transform.position,
                new Vector3(transform.position.x + triggerLength, transform.position.y, transform.position.z),
                Color.green);
            Debug.DrawLine(transform.position,
                new Vector3(transform.position.x - triggerLength, transform.position.y, transform.position.z),
                Color.green);
            Debug.DrawLine(transform.position,
                new Vector3(transform.position.x, transform.position.y + triggerLength, transform.position.z),
                Color.green);
            Debug.DrawLine(transform.position,
                new Vector3(transform.position.x, transform.position.y - triggerLength, transform.position.z),
                Color.green);
        }

        hitBox.OverlapCollider(filter, hits);
        if (hitBox == null)
            return;
        for (var i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].CompareTag("Fighter") && hits[i].name == "Player") collidingWithPlayer = true;
            hits[i] = null;
        }
    }

    private void FixedUpdate()
    {
        if (isAlive)
            ChasingTarget();
        else
            pushDirection = Vector3.zero;
    }

    protected virtual void ChasingTarget()
    {
        if (Vector3.Distance(playTransform.position, startingPosition) < chaseLength &&
            GameManager.instance.player.isAlive)
        {
            if (Vector3.Distance(playTransform.position, startingPosition) < triggerLength)
                chasing = true;

            if (chasing)
            {
                OpenStateSprite();


                if (!collidingWithPlayer)
                    UpdateMotor((playTransform.position - transform.position).normalized, speedMultiple);
            }
            else
            {
                UpdateMotor(startingPosition - transform.position, speedMultiple);
                CloseStateSprite();
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position, speedMultiple);
            chasing = false;
            CloseStateSprite();
        }
    }


    private void OpenStateSprite()
    {
        enemyStateSprite.enabled = true;
        if (hitPoint / (float) maxHitPoint < 0.5)
            enemyStateSprite.sprite = stateSprites[1];
        else
            enemyStateSprite.sprite = stateSprites[0];
    }

    private void CloseStateSprite()
    {
        enemyStateSprite.enabled = false;
    }


    protected override void Death()
    {
        GameManager.instance.GrantXP(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40,
            1.0f);

        if (canRespawn)
        {
            isAlive = false;
            hitBox.enabled = false;
            CloseStateSprite();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine("WaitingForRespawn");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator WaitingForRespawn()
    {
        yield return new WaitForSeconds(timeToRespawn);
        isAlive = true;
        hitBox.enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        OpenStateSprite();
        hitPoint = maxHitPoint;
        gameObject.transform.position = startingPosition;
    }
}