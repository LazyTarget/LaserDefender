using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;

	private Vector3 direction = Vector3.right;
	private float xmin;
	private float xmax;

	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;

		var topLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		var bottomRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distanceToCamera));
		xmin = topLeftEdge.x;
		xmax = bottomRightEdge.x;

		foreach (Transform child in transform) {
			GameObject enemy = (GameObject) Instantiate(enemyPrefab, child.transform.position, Quaternion.identity);
			enemy.transform.parent = child;
		}
	}

	void OnDrawGizmos () {
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
		var pos = direction * speed * Time.deltaTime;
		transform.position += pos;
		
		var leftEdgeOfFormation = transform.position.x - (0.5f * width);
		var rightEdgeOfFormation = transform.position.x + (0.5f * width);
		if (leftEdgeOfFormation < xmin) {
			direction = Vector3.right;
		} else if (rightEdgeOfFormation > xmax) {
			direction = Vector3.left;
		}
	}

	void ChangeDirection() {
		if (direction == Vector3.right) {
			direction = Vector3.left;
		} else if (direction == Vector3.left) {
			direction = Vector3.right;
		}
	}
}
