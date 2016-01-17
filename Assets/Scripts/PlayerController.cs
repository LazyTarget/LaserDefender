using UnityEngine;
using System.Collections;

public class PlayerController : UnitBase {

	public float speed = 15f;
	public float padding = 1f;

	private float xMin;
	private float xMax;
	private float yMin;
	private float yMax;

	public override int teamID { get { return 1; } }
	
	protected override void Awake () {
		direction = Vector2.up;
	}

	protected override void Start () {
		base.Start ();

		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;

		var leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		var rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
		var topMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distanceToCamera));
		var bottomMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
		yMin = bottomMost.y + padding;
		yMax = bottomMost.y + (Mathf.Abs(topMost.y) + Mathf.Abs(bottomMost.y)) * 0.35f - padding;
	}

	protected override void Update () {

		if(Input.GetKeyDown(KeyCode.Space)) {
			BeginShooting();
			//Shoot();
		}
		if(Input.GetKeyUp(KeyCode.Space)) {
			StopShooting();
		}

		UpdatePosition();
	}


	void UpdatePosition() {
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

	
	void OnTriggerEnter2D (Collider2D collider) {
		//Debug.Log("PlayerController:OnTriggerEnter2D()");
		//Destroy(gameObject);
	}
	
	protected override void Explode ()
	{
		base.Explode ();

		var lvlManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		lvlManager.LoadLevel("Gameover");
	}
}
