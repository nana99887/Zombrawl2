using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Projectile : MonoBehaviour
{
    public float speed = 20f;           
    public float damage = 10f;         
    public float lifetime = 5f;        

    private Vector3 moveDirection;

    private void Start()
    {
       
        Destroy(gameObject, lifetime);

        
        moveDirection = transform.forward;
    }

    private void Update()
    {
       
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
     
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject); 
        }
    }
}
