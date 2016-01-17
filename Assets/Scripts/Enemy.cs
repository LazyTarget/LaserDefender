using UnityEngine;
using System.Collections;

public class Enemy : UnitBase {

	public override int teamID { get { return 0; } }

	public float shotsPerSecond = 1.0f;
	public int scoreValue = 150;

	void Awake () {
		direction = -Vector2.up;
	}

	void Start () {
		//BeginShooting();
	}

	void Update () {
		//Shoot();

		var fireProbability = Time.deltaTime * shotsPerSecond;
		var rand = Random.value;
		if (rand < fireProbability) {
			Shoot();
		}
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		//Debug.Log("PlayerController:OnTriggerEnter2D()");
		//Destroy(gameObject);
	}

	protected override void Explode ()
	{
		base.Explode ();
		ScoreKeeper.Add(scoreValue);
	}
}
