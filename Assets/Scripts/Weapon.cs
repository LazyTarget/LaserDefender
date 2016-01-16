using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	private UnitBase owner;

	public Projectile projectile;
	public float firingRate = 0.2f;


	internal void SetOwner(UnitBase owner){
		this.owner = owner;
	}

	void Start () {

	}

	void Update () {
		
	}

	public void BeginShooting() {
		InvokeRepeating("Shoot", 0.000001f, firingRate);
	}

	public void StopShooting() {
		CancelInvoke("Shoot");
	}

	public void Shoot() {
		if (owner == null || owner.direction == Vector2.zero) {
			Debug.LogWarning("Cannot shoot weapon, missing owner or direction");
			return;
		}

		var proj = (Projectile) Instantiate(projectile, owner.transform.position, Quaternion.identity);
		proj.GetComponent<Rigidbody2D>().velocity = owner.direction * Mathf.Abs(proj.speed) * Time.deltaTime;
		proj.Init (owner);
	}
}
