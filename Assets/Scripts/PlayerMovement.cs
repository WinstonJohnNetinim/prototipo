using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
	public LayerMask groundLayer;
	public Transform cameraTransform;
	public GameObject leftHand, rightHand;
	public float horizontalAcceleration;
	public float maxSpeed;
	public float sneakRelativeSpeed;
	public float jumpForce;

	private Rigidbody2D rB2D;
	private SpriteRenderer spriteRenderer;
	private float horizontalInput, horizontalInputRaw;
	private float verticalInput, verticalInputRaw;
	private bool isSneaking = false;

	void Start()
	{
		rB2D = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	bool wat = false;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (rB2D.velocity.y < 0 && !wat)
		{
			Debug.Log(transform.position);
			GameObject.Find("Platform").GetComponent<PlatformHandle>().PlayerGrapple(spriteRenderer);
			wat = true;
		}
	}

	void Update()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		horizontalInputRaw = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxis("Vertical");
		verticalInputRaw = Input.GetAxisRaw("Vertical");

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

		//inverte o sprite dependendo do input
		if (horizontalInputRaw > 0)
		{
			spriteRenderer.flipX = false;
			rightHand.SetActive(true);
			leftHand.SetActive(false);
		}
		if (horizontalInputRaw < 0)
		{
			spriteRenderer.flipX = true;
			rightHand.SetActive(false);
			leftHand.SetActive(true);
		}

		//quando o player tenta voltar no cenário
		if (horizontalInput < 0 && (cameraTransform.position.x - transform.position.x) >= 9)
		{
			rB2D.velocity = new Vector2(0f, rB2D.velocity.y);
			return;
		}

		//quando o player se esconde/se movimenta furtivamente
		if (verticalInputRaw < 0 && hit)
		{
			isSneaking = true;
			transform.localScale = new Vector3(1f, 0.5f, 1f);
		}
		else
		{
			isSneaking = false;
			transform.localScale = new Vector3(1f, 1f, 1f);
		}

		//quando o player se movimenta, de fato
		if (isSneaking && Mathf.Abs(rB2D.velocity.x) < maxSpeed * sneakRelativeSpeed)
		{
			rB2D.AddForce(Vector2.right * horizontalAcceleration * sneakRelativeSpeed * horizontalInputRaw * Time.deltaTime);
		}
		if (!isSneaking && Mathf.Abs(rB2D.velocity.x) < maxSpeed)
		{
			rB2D.AddForce(Vector2.right * horizontalAcceleration * horizontalInputRaw * Time.deltaTime);
		}

		//quando o player pula
		if (Input.GetButtonDown("Jump") && !isSneaking && hit)
		{
			if (Mathf.Approximately(rB2D.velocity.x, 0f))
			{
				//quando parado consegue pular mais alto
				rB2D.AddForce(Vector2.up * jumpForce * 1.15f, ForceMode2D.Impulse);
			}
			else
			{
				rB2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			}
		}
	}
}