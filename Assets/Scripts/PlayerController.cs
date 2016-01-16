using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 15f;
	public float padding = 1f;
	private float xMin;
	private float xMax;
	private float yMin;
	private float yMax;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;

		var leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		var rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		var topMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
		var bottomMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
		yMin = bottomMost.y + padding;
		yMax = bottomMost.y + (Mathf.Abs(topMost.y) + Mathf.Abs(bottomMost.y)) * 0.35f - padding;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.UpArrow)){
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			transform.position += Vector3.down * speed * Time.deltaTime;
		}

		// restrict movement to playspace
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
		transform.position = new Vector3(newX, newY, transform.position.z);
	}
}
