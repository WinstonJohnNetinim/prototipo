using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Transform cameraTransform;
	public float movementSpeed;
	public float sneakRelativeSpeed;

	private float horizontalMovement;
	private float moveFactor;
	private bool isSneaking = false;

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

		if (Input.GetAxisRaw ("Vertical") < 0) {
			isSneaking = true;
			transform.localScale = new Vector3 (32f, 32f, 1);
		} else {
			isSneaking = false;
			transform.localScale = new Vector3 (32f, 64f, 1);
		}

		if (isSneaking) {
			transform.Translate (Vector3.right * horizontalMovement * moveFactor * sneakRelativeSpeed);
		} else {
			transform.Translate (Vector3.right * horizontalMovement * moveFactor);
		}
	}
}