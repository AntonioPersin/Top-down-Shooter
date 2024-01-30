using UnityEngine;

public class BulletController : MonoBehaviour
{
	private float damage;

	// Set the damage value
	public void SetDamage(float damageAmount)
	{
		damage = damageAmount;
	}

	void OnTriggerEnter(Collider other)
	{
		// Check if the bullet hit an enemy
		if (other.CompareTag("Enemy"))
		{
			// Deal damage to the enemy
			EnemyController enemyController = other.GetComponent<EnemyController>();
			if (enemyController != null)
			{
				enemyController.TakeDamage(damage);
			}

			// Destroy the bullet on impact
			Destroy(gameObject);
		}
		// Add more conditions for different types of collisions if needed
	}
}
