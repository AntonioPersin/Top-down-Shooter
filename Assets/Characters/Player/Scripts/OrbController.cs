// OrbController.cs
using UnityEngine;

public class OrbController : MonoBehaviour
{
	private float damage;
	private float maxDistance;
	private float width;
	private float orbFlightSpeed;

	public void Initialize(SkillshotController skillshotController, float orbDamage, float orbMaxDistance, float orbWidth, float orbSpeed)
	{
		damage = orbDamage;
		maxDistance = orbMaxDistance;
		width = orbWidth;
		orbFlightSpeed = orbSpeed;
		Debug.Log("Orb Instantiated");
	}

	void Update()
	{
		// Move the orb forward
		transform.Translate(Vector3.forward * orbFlightSpeed * Time.deltaTime);
		Debug.Log("Current Position: " + transform.position);


		// Check if the orb exceeds the maximum distance
		if (transform.position.magnitude > maxDistance)
		{
			Destroy(gameObject);
		}
	}


	void OnTriggerEnter(Collider other)
	{
		HandleCollision(other);
	}

	void HandleCollision(Collider other)
	{
		// Handle collisions with other objects
		if (other.CompareTag("Enemy"))
		{
			// Apply damage to the enemy
			EnemyController enemyController = other.GetComponent<EnemyController>();
			if (enemyController != null)
			{
				enemyController.TakeDamage(damage);
			}
		}
		else if (other.CompareTag("Wall") || (other.gameObject.layer == LayerMask.NameToLayer("Terrain")))
		{
			Destroy(gameObject); // Destroy
		}
	}
}
