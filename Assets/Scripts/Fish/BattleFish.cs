using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class BattleFish : MonoBehaviour
{
    public BattleFishData battleFishData = default!;
    [NonSerialized] public FishStats stats = default!;
    [NonSerialized] public Health health = default!;
    [NonSerialized] public Level level = default!;
    [NonSerialized] public FishAI ai = default!;
    [NonSerialized] public FishAbility ability = default!;
    [NonSerialized] public Animator animator = default!;
    [NonSerialized] public Rigidbody2D rb = default!;
    [NonSerialized] public SpriteRenderer spriteRenderer = default!;
    public float invulFrameDuration = 0.4f;
    public bool isInvul;
    public float minSpeedForDamage = 2f;
    
    public int Swimming = Animator.StringToHash("Swimming");
    
    private void Awake()
    {
        Assert.IsNotNull(battleFishData);
        
        level = new Level(this);   // TODO: save in persistence
        
        stats = FishStats.FromFish(this);  // initialize after level, depends on level
        
        health = GetComponent<Health>();  // initialize after stats, depends on stats
        Assert.IsNotNull(health);
        
        ability = GetComponent<FishAbility>();
        Assert.IsNotNull(ability);
        
        ai = GetComponent<FishAI>();
        Assert.IsNotNull(ai);
        
        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator);

        rb = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb);
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer);
    }

    private void Start()
    {
        health.OnHealthDeath += OnDeath;
    }

    private void OnDestroy()
    {
        health.OnHealthDeath -= OnDeath;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isInvul)
            return;

        if (collision.rigidbody == null)
            return;

        BattleFish? attacker = collision.gameObject.GetComponent<BattleFish>();
        if (collision.rigidbody.GetComponent<BattleFish>() == null)
            return;

        float thisMag = rb.velocity.sqrMagnitude;
        float attackMag = attacker.rb.velocity.sqrMagnitude;
        Debug.Log("thisMag" + thisMag + " attackMag = " + attackMag);
        if (thisMag > attackMag) // this fish velocity is stronger, don't take damage
            return;

        if (Math.Max(thisMag, attackMag) < minSpeedForDamage)
            return;

        if (attacker.CompareTag(tag))  // same team
            return;

        StartCoroutine(InvulFrame());
        onAttacked?.Invoke(this, attacker);
    }

    private IEnumerator InvulFrame()
    {
        isInvul = true;

        Color color = Color.red;
        float stopAt = Time.time + invulFrameDuration;
        while (Time.time < stopAt)
        {
            color.a = 100;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.1f);
            color.a = 0;            
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.1f);
        }
        color = Color.white;
        spriteRenderer.color = color;
        
        isInvul = false;
    }

    private void OnDeath(Health _, float _2)
    {
        rb.excludeLayers = LayerMask.GetMask("Fish");
        rb.gravityScale = -0.02f;
        rb.freezeRotation = false;
        rb.AddTorque(0.1f, ForceMode2D.Impulse);
        animator.SetBool(Swimming, true);
    } 

    public delegate void AttackEvent(BattleFish target, BattleFish attacker);
    
    public AttackEvent? onAttacked;
    public AttackEvent? onHeal;
}