 
using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] GameObject m_slideDust;
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    public bool                m_rolling = false;
    private bool                StopMoving = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;

    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask EnemyLayers;
    public int AttackDamage = 20;
    public int MaxHealth = 100;
    int CurrentHealth;
    public bool IsDead = false;
    public bool PlayerSafe = false;
    public bool Blocking = false;
    public HealthBar healthBar;
    public GameObject DefeatMessage;
    public GameObject VictoryMessage;
    public GameObject Timer;
    public GameObject HealthBar;
    public Timer timer;
    public AudioSource Swing;
    public AudioSource Music;
    public AudioSource Victory;



    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_animator.SetBool("Grounded", true);
        CurrentHealth = MaxHealth;
        int Health = CurrentHealth;
        healthBar.SetHealth(Health);
    }

    // Update is called once per frame
    void Update ()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
            AttackPoint.transform.localPosition = new Vector3(0.876999974f,0.532999992f,0);
        }
            
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
            AttackPoint.transform.localPosition = new Vector3(-0.876999974f,0.532999992f,0);
        }

        // Move
        if (!m_rolling && StopMoving == false)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        

        //Attack
        if(Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
            }
            Swing.Play();
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
            StopMoving = true;
            Blocking = true;
        }

        else if (Input.GetMouseButtonUp(1))
        {
            m_animator.SetBool("IdleBlock", false);
            StopMoving = false;
            Blocking = false;
        }
        // Roll
        else if (Input.GetMouseButtonDown(3) && !m_rolling)
        {
            Physics2D.IgnoreLayerCollision(6, 3, true);
            PlayerSafe = true;
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            Physics2D.IgnoreLayerCollision(6, 3, false);
            PlayerSafe = false;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }
    void OnDrawGizmosSelected()
    {
        if(AttackPoint == null)
        return;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
    public void DamageTaken(int EnemyAttackDamage)
    {
        CurrentHealth -= EnemyAttackDamage;
        int Health = CurrentHealth;
        healthBar.SetHealth(Health);

        m_animator.SetTrigger("Hurt");

        if(CurrentHealth <=0)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            this.enabled = false;
            IsDead = true;
            m_animator.SetTrigger("Death");
            DefeatMessage.SetActive(true);
            Timer.SetActive(false);
            HealthBar.SetActive(false);
        }
    }
    public void PlayerWins()
    {
        Debug.Log("Won");
        GetComponent<Animator>().enabled = false;
        VictoryMessage.SetActive(true);
        Timer.SetActive(false);
        HealthBar.SetActive(false);
        timer.EndTimer();
        Music.Stop();
        Victory.Play();
    }
}
