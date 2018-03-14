using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform playerTransform;

	private float horizontalMovement;

	void LateUpdate ()
	{
		horizontalMovement = Input.GetAxis ("Horizontal");

		if (horizontalMovement < 0) {
			return;
		}

		if (transform.position.x < playerTransform.position.x) {
			
		}
	}
}
