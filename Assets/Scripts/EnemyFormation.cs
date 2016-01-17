using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyFormation : MonoBehaviour {
	public GameObject[] enemyPrefabs;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 1.0f;

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

		SpawnEnemies();
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

		var members = MembersAlive();
		if (members <= 0) {
			//SpawnEnemies();
			SpawnUntilFull();
		}
	}

	void ChangeDirection() {
		if (direction == Vector3.right) {
			direction = Vector3.left;
		} else if (direction == Vector3.left) {
			direction = Vector3.right;
		}
	}

	int MembersAlive() {
		var count = 0;
		foreach (Transform position in transform) {
			var hasEnemy = position.transform.childCount > 0;
			if (hasEnemy) {
				count++;
			}
		}
		return count;
	}

	void SpawnEnemies() {
		Debug.Log("Spawning enemies");
		transform.position = new Vector3(0, transform.position.y, transform.position.z);		// start position
		foreach (Transform child in transform) {
			SpawnEnemyAtPosition(child);
		}
	}

	void SpawnUntilFull() {
		var position = NextFreePosition();
		if (position != null){
			SpawnEnemyAtPosition(position);
			Invoke("SpawnUntilFull", spawnDelay);
		}
	}
	
	GameObject SpawnEnemyAtPosition(Transform position) {
		Debug.Log("Spawning enemy at position");

		var prefabs = enemyPrefabs.Where(x => x != null).ToList();
		GameObject enemyPrefab = null;
		while (enemyPrefab == null) {
			var rand = Random.Range(0, prefabs.Count);
			enemyPrefab = prefabs.ElementAtOrDefault(rand);
		}
		GameObject enemy = (GameObject) Instantiate(enemyPrefab, position.transform.position, Quaternion.identity);
		enemy.transform.parent = position;
		return enemy;
	}

	Transform NextFreePosition() {
		foreach (Transform position in transform) {
			var hasEnemy = position.transform.childCount > 0;
			if (!hasEnemy) {
				return position.transform;
			}
		}
		return null;
	}

}
