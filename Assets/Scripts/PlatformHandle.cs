using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandle : MonoBehaviour
{
	public GameObject platformElevator;
	public GameObject platformPusher;
	public float riseTime, pushTime;

	void Start()
	{
		Time.timeScale = Time.timeScale / 2;
	}

	public void PlayerGrapple(SpriteRenderer spriteRenderer)
	{
		platformElevator.transform.position = spriteRenderer.bounds.min - Vector3.up * 0.55f;
		platformElevator.transform.position -= platformElevator.transform.position.z * Vector3.forward;
		platformElevator.SetActive(true);

		PushPlayer(spriteRenderer);
	}

	private void PushPlayer(SpriteRenderer spriteRenderer)
	{
		Debug.Log(spriteRenderer.bounds.ToString());
		Debug.Log(spriteRenderer.bounds.min.ToString());

		platformPusher.transform.position = new Vector3(spriteRenderer.bounds.min.x - 1.25f, transform.position.y + 0.75f, 0f);
		platformPusher.SetActive(true);

		StartCoroutine(ElevatorRise());
	}

	IEnumerator ElevatorRise()
	{
		float initTime = Time.time;
		float yIncrement = (transform.localPosition.y - 0.001f) / riseTime;

		while (platformElevator.transform.localPosition.y < 0.001f)
		{
			platformElevator.transform.localPosition += Vector3.up * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		StartCoroutine(PlatformPush());
	}

	IEnumerator PlatformPush()
	{
		yield return null;
	}
}
