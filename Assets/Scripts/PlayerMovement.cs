using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Transform cameraTransform;
	public float movementSpeed;

	private float horizontalMovement;
	private float moveFactor;

	void Start ()
	{
		moveFactor = movementSpeed / 10;
	}

	void Update ()
	{
		horizontalMovement = Input.GetAxis ("Horizontal");

		if (horizontalMovement < 0 && (cameraTransform.position.x - transform.position.x) >= 10) {
			return;
		}		

		transform.Translate (Vector3.right * horizontalMovement * moveFactor);

		if (Input.GetAxisRaw ("Vertical") < 0) {
			transform.localScale = new Vector3 (32f, 32f, 1);
		} else {
			transform.localScale = new Vector3 (32f, 64f, 1);
		}
	}
}