using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private UnitBase shooter;

	public float speed = 600f;
	public float damage = 40f;

	
	public void Init(UnitBase shooter){
		this.shooter = shooter;
	}

	void Start () {
		//GetComponent<Rigidbody2D>().velocity = direction * Mathf.Abs(speed) * Time.deltaTime;
	}

	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D collider) {
		Debug.Log("Projectile hit something");

		var unit = collider.GetComponent<UnitBase>();
		var proj = collider.GetComponent<Projectile>();
		if (unit != null){
			if (unit == shooter) {
				Debug.Log("Ignoring projectile trigger, as bullet touches the shooter");
				return;
			}

			unit.ProjectileHit(this);

			// Destroy projectile after hit
			Explode();
		} else if (proj != null) {
			// If projectile hits projectile
			//Explode();
		} else {
			// Destroy projectile on any hit
			Explode();
			// todo: some projectiles that can go through items?
		}
	}

	private void Explode() {
		Destroy(gameObject);
		// todo: animate explosion..
	}

	public float GetDamage() {
		return damage;
	}
}
