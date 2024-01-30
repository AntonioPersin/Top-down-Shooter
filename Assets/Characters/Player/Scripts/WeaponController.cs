using UnityEngine;

public class WeaponController : MonoBehaviour
{
	public Transform firingPoint;
	public GameObject bulletPrefab;
	public float fireRate = 0.5f;
	public float bulletSpeed = 10f;
	public float maxDistance = 50f;

	// Add a damage variable
	public float damage = 10f;

	private float nextFireTime;


	public void FireBullet()
	{
		if (Time.time >= nextFireTime)
		{
			// Instantiate a bullet at the firing point position
			GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);

			// Get the rotation of the firing point
			Quaternion rotation = firingPoint.rotation;

			// Rotate the bullet to match the firing point's rotation with a 90-degree adjustment
			bullet.transform.rotation = rotation * Quaternion.Euler(90f, 0f, 0f);

			// Get the rigidbody component of the bullet
			Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

			// Set the velocity of the bullet based on the firing point's forward direction
			bulletRb.velocity = firingPoint.forward * bulletSpeed;

			// Attach a script to the bullet to freeze its rotation during flight
			bullet.AddComponent<FreezeRotation>();

			// Attach a script to the bullet to set its damage
			BulletController bulletController = bullet.AddComponent<BulletController>();
			bulletController.SetDamage(damage);

			// Set the maximum travel distance of the bullet
			Destroy(bullet, maxDistance / bulletSpeed);


			nextFireTime = Time.time + 1f / fireRate;

			// You may want to add additional logic or effects here
		}
	}
}

public class FreezeRotation : MonoBehaviour
{
	void FixedUpdate()
	{
		// Freeze rotation in all axes
		GetComponent<Rigidbody>().freezeRotation = true;
	}
}
