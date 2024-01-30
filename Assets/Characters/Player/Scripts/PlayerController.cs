// PlayerController.cs
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Camera playerCamera;
    public float maxHealth = 100f;
    public float respawnTime = 5f; // Adjustable respawn timer in seconds

    private float currentHealth;
    private bool isAlive = true;
    private bool isRespawning = false;
    private float respawnTimer = 0f;

    public Slider healthSlider; // Reference to the UI Slider component
    private SkillshotController skillshotController;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthSlider(); // Update health slider at the start

		// Find the SkillshotController component on the same object
		skillshotController = GetComponent<SkillshotController>();
		if (skillshotController == null)
		{
			Debug.LogError("SkillshotController reference is not set in the PlayerController script.");
		}
	}

    void Update()
    {
        if (isAlive && !isRespawning && playerCamera != null)
        {
            // Player movement and camera rotation logic
            if (Input.GetMouseButton(0))
            {
                // Call FireBullet method from WeaponController
                WeaponController weaponController = GetComponentInChildren<WeaponController>();
                if (weaponController != null)
                {
                    weaponController.FireBullet();
                }
                else
                {
                    Debug.LogError("WeaponController reference is not set in the PlayerController script.");
                }
            }

			// Check for Q key press to use the skill
			if (Input.GetKeyDown(KeyCode.Q))
			{
				Debug.Log("Q key pressed");
				UseAbilityQ();
			}

			// Example: Move towards mouse cursor
			MovePlayer();
            LookTowardsCursor();
        }
        else if (!isAlive && !isRespawning)
        {
            // Player is defeated, initiate respawn process
            isRespawning = true;
            respawnTimer = 0f;

            // Add logic for monochrome screen effect (e.g., changing post-processing effects or screen overlay)
            ApplyMonochromeScreenEffect();
        }

        if (isRespawning)
        {
            // Update respawn timer
            respawnTimer += Time.deltaTime;

            if (respawnTimer >= respawnTime)
            {
                // Respawn the player after the timer expires
                RespawnPlayer();
            }
        }
    }

	void UseAbilityQ()
	{
		if (skillshotController != null)
		{
			Debug.Log("Firing skillshot");
			skillshotController.FireSkillshot();
		}
	}

	void MovePlayer()
    {
        // Example movement logic towards mouse cursor
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        Vector3 moveDirection = (cameraForward.normalized * vertical + cameraRight.normalized * horizontal).normalized;

        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }
    
    void LookTowardsCursor()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 targetPosition = ray.GetPoint(rayDistance);
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
    }

    void ApplyMonochromeScreenEffect()
    {
        // Add logic to apply monochrome screen effect (e.g., change post-processing effects or overlay a grayscale image)
        // Example: You might use Unity's Post-Processing Stack v2 or a custom shader for the screen effect.
    }

    void RespawnPlayer()
    {
        // Reset player state
        isRespawning = false;
        isAlive = true;
        currentHealth = maxHealth;
        UpdateHealthSlider();

        // Add logic to reset monochrome screen effect

        // Example: Teleport the player to a spawn point
        //transform.position = Vector3.zero;
    }

    public void TakeDamage(float damageAmount)
    {
        Debug.Log("Player is attacked");
        if (isAlive)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                isAlive = false;
                Debug.Log("Player has been defeated!");
            }

            UpdateHealthSlider(); // Update health slider when taking damage
        }
    }

    void UpdateHealthSlider()
    {
        // Update the health slider value based on the current health
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
}
