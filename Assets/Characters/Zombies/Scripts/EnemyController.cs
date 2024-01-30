using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float maxHealth = 50f;
	public float damageToPlayer = 10f;
	public float movementSpeed = 3f;
	public float attackRange = 2f;
	public float attackSpeed = 1f; // Attacks per second

	private float currentHealth;
	private float timeSinceLastAttack = 0f;
	private GameObject player;

	void Start()
	{
		currentHealth = maxHealth;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		if (player != null)
		{
			// Move towards the player until within attack range
			float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
			if (distanceToPlayer > attackRange)
			{
				MoveTowardsPlayer();
			}
			else
			{
				// Attack the player periodically
				timeSinceLastAttack += Time.deltaTime;
				if (timeSinceLastAttack >= 1f / attackSpeed)
				{
					AttackPlayer();
					timeSinceLastAttack = 0f;
				}
			}
		}
	}

	void MoveTowardsPlayer()
	{
		Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
		transform.Translate(directionToPlayer * movementSpeed * Time.deltaTime);
	}

	void AttackPlayer()
	{
		Debug.Log("attacked player");
		// Damage the player
		PlayerController playerController = player.GetComponent<PlayerController>();
		Debug.Log(playerController);
		if (playerController != null)
		{
			
			playerController.TakeDamage(damageToPlayer);
		}
	}

	public void TakeDamage(float damageAmount)
	{
		currentHealth -= damageAmount;

		// Check if the enemy is defeated
		if (currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}
}
