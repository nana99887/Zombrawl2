using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    public float health = 100f;              
    public float attackDamage = 10f;          
    public float attackRange = 2f;             
    public float attackCooldown = 1f;         
    private float attackTimer = 0f;            
    public float followRange = 30f;            

    private Transform player;                 
    private UnityEngine.AI.NavMeshAgent agent;                
    private Animator animator;               

    private bool isDead = false;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform; 
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();               
        animator = GetComponent<Animator>();

        int enemyLayer = gameObject.layer;
        int wallLayer = LayerMask.NameToLayer("Wall");

        if (enemyLayer >= 0 && wallLayer >= 0)
        {
            Physics.IgnoreLayerCollision(enemyLayer, wallLayer, true);
        }
    }

    private void Update()
    {
        if (player == null || health <= 0) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

       
        if (distanceToPlayer <= followRange)
        {
            
            agent.SetDestination(player.position);
            animator.SetBool("isMoving", true);

            if (distanceToPlayer <= attackRange && attackTimer <= 0f)
            {
                AttackPlayer();
            }
        }
        else
        {
            
            animator.SetBool("isMoving", false);
        }

       
        attackTimer -= Time.deltaTime;
    }

    private void AttackPlayer()
    {
     
        animator.SetTrigger("Attack");

     
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.TakeDamage(attackDamage);
        }

       
        attackTimer = attackCooldown;
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
     
        animator.SetTrigger("Death");
        GameObject.FindAnyObjectByType<GameManager>().OnEnemyKill();
        Invoke("Kill", 1.5f);
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
