// SkillshotController.cs
using UnityEngine;

public class SkillshotController : MonoBehaviour
{
	public GameObject orbPrefab;
	public float maxDistance = 10f;
	public float damage = 30f;
	public float width = 1f;
	public float cooldown = 9f; // Adjustable cooldown in seconds
	public float orbFlightSpeed = 5f; // Adjustable orb flight speed
	public float spawnDistance = 2f; // Distance in front of the player

	private bool canUseSkill = true;

	public void FireSkillshot()
	{
		if (canUseSkill)
		{
			// Calculate the spawn position in front of the player
			Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;

			// Debug statement to check the spawn position
			Debug.Log("Spawn Position: " + spawnPosition);

			// Instantiate the orb at the calculated spawn position
			GameObject orb = Instantiate(orbPrefab, spawnPosition, transform.rotation);

			// Set properties of the orb directly
			OrbController orbController = orb.AddComponent<OrbController>();
			if (orbController != null)
			{
				orbController.Initialize(this, damage, maxDistance, width, orbFlightSpeed);

			}

			// Start the cooldown
			canUseSkill = false;
			Invoke("ResetCooldown", cooldown);
		}
	}


	void ResetCooldown()
	{
		canUseSkill = true;
	}
}
