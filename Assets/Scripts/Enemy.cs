using UnityEngine;
using System.Collections;

public class Enemy : UnitBase {

	void Awake () {
		direction = -Vector2.up;
	}

	void Start () {
		BeginShooting();
	}

	void Update () {
		//Shoot();
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		//Debug.Log("PlayerController:OnTriggerEnter2D()");
		//Destroy(gameObject);
	}
}
