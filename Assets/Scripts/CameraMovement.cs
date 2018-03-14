using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform playerTransform;

	private float horizontalMovement;

	void LateUpdate ()
	{
		horizontalMovement = Input.GetAxis ("Horizontal");

		//player movendo para trás no cenário
		if (horizontalMovement < 0) {
			return;
		}
		//player à frente da câmera
		if (playerTransform.position.x > transform.position.x) {
			transform.position = new Vector3 (playerTransform.position.x, transform.position.y, transform.position.z);
		}
	}
}
