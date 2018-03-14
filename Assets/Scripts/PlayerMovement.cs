using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public LayerMask groundLayer;
	public Transform cameraTransform;
	public float movementSpeed;
	public float sneakRelativeSpeed;
	public float jumpForce;

	private Rigidbody2D rB2d;
	private float horizontalMovement;
	private float moveFactor;
	private bool isSneaking = false;

	void Start ()
	{
		rB2d = GetComponent<Rigidbody2D> ();
		moveFactor = movementSpeed / 10;
	}

	void Update ()
	{
		horizontalMovement = Input.GetAxis ("Horizontal");
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, 0.1f, groundLayer);

		//quando o player tenta voltar no cenário
		if (horizontalMovement < 0 && (cameraTransform.position.x - transform.position.x) >= 9) {
			return;
		}		

		//quando o player se esconde/se movimenta furtivamente
		if (Input.GetAxisRaw ("Vertical") < 0 && hit.collider != null) {
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

		//quando o player pula
		if (Input.GetButtonDown ("Jump") && !isSneaking) {
			if (hit.collider != null) {
				Debug.Log ("pulando");
				rB2d.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
			} else {
				Debug.Log ("hit null");
			}
		}
	}
}