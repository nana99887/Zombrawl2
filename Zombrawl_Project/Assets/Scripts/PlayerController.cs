using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private Camera mainCamera;

    [SerializeField] private PlayerModel model;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask groundLayer; // Define ground layer to check ground

    [SerializeField] private GameUIController gameUIController;
    private Vector3 movementInput;
    private bool isGrounded;


    private float lastShootTime = 0f; // To track the last time the player shot
    public float shootCooldown = 0.5f;


    private float survivalTime = 0f;
    private bool isPlayerAlive = true;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;

        rb.constraints = RigidbodyConstraints.FreezeRotation; // Prevent unintended rotations
    }

    void Update()
    {
        if (isPlayerAlive)
        {
            survivalTime += Time.deltaTime;  // Increase survival time

            gameUIController.UpdateSurvivalTime(survivalTime);
        }
        HandleInput();
        HandleRotation();
        HandleShooting();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleInput()
    {
        // Capture input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementInput = new Vector3(horizontal, 0, vertical).normalized;
    }

    private void HandleMovement()
    {
        if (movementInput.magnitude >= 0.1f)
        {
            Vector3 moveDirection = movementInput * model.moveSpeed;
            moveDirection.y = rb.velocity.y; // Preserve vertical velocity (gravity)
            rb.velocity = moveDirection;

            animator.SetBool("isRunning", true);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0); // Stop horizontal movement
            animator.SetBool("isRunning", false);
        }

        CheckGrounded(); 
    }

    private void HandleRotation()
    {
        if (movementInput.magnitude >= 0.1f)
        {
            // Use movement direction to rotate the player while moving
            Vector3 targetDirection = new Vector3(movementInput.x, 0, movementInput.z);
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, model.rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Rotate towards mouse only when stationary
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 lookDirection = hit.point - transform.position;
                lookDirection.y = 0; 
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), model.rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Shoot();
        }
    }

    public void TakeDamage(float damage)
    {
        model.health -= damage;
        gameUIController.UpdateHealth(model.health);
        if (model.health <= 1)
        {
            Debug.Log("Player Daed");
            GameOver();
        }
    }

    private void GameOver()
    {
        gameUIController.ActiveGameOver();
    }

    private void Shoot()
    {
        // Update the last shoot time
        if (Time.time - lastShootTime < shootCooldown)
        {
            return; 
        }

        lastShootTime = Time.time; 

       
        Quaternion projectileRotation = Quaternion.Euler(-90f, 90f, 0f);
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation,null);   // Bullet Creation
        SoundsManager.Instance.PlaySound(SoundsManager.Instance.shootSound);
    }

        
    private void CheckGrounded()  // Check if the player is grounded using a raycast
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        isGrounded = Physics.Raycast(ray, 0.2f, groundLayer);

        if (!isGrounded)
        {
            rb.velocity += Vector3.down * 9.81f * Time.deltaTime;
        }
    }

    public void UpdateScore()
    {
        gameUIController.UpdateCoins(+1);
        SoundsManager.Instance.PlaySound(SoundsManager.Instance.coinCollectSound);
    }
}
