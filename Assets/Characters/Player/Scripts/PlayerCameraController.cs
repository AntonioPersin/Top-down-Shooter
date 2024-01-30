using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform target; // Drag and drop the player GameObject here in the Inspector.

	[Header("Camera Settings")]
	[SerializeField] private float lockedAngle = 0f; // Adjustable in the Editor but not during runtime
	public float distance = 5f;
	public float minDistance = 2f;
	public float maxDistance = 10f;
	public float zoomSpeed = 5f;

	void Update()
	{
		if (target != null)
		{
			// Handle zooming
			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, minDistance, maxDistance);

			// Update camera position based on spherical coordinates with the locked angle
			float rotationY = transform.eulerAngles.y;
			float rotationX = lockedAngle; // Use the locked angle

			Vector3 direction = new Vector3(0, 0, -distance);
			Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
			Vector3 position = target.position + rotation * direction;

			transform.position = position;
			transform.LookAt(target);
		}
		else
		{
			Debug.LogWarning("Target is not assigned in the CameraController script.");
		}
	}
}
