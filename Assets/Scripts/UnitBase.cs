using UnityEngine;
using System.Collections;

public abstract class UnitBase : MonoBehaviour
{
	protected UnitBase ()
	{

	}
	
	public Weapon weapon;
	public float health = 100;
	public Vector2 direction;
	
	
	void Awake () {
	}
	
	void Start () {

	}
	
	void Update () {

	}

	protected void Shoot() {
		if (weapon != null) {
			weapon.SetOwner (this);
			weapon.Shoot();
		}
	}

	protected void BeginShooting() {
		if (weapon != null) {
			weapon.SetOwner (this);
			weapon.BeginShooting();
		}
	}
	
	protected void StopShooting() {
		if (weapon != null) {
			weapon.SetOwner (this);
			weapon.StopShooting();
		}
	}

	public virtual void ProjectileHit(Projectile projectile) {

		if (health > 0) {
			var damage = projectile.GetDamage();
			health -= damage;			
			if (health <= 0) {
				Explode ();
			}
		} else {
			Destroy(gameObject);
		}
	}

	protected virtual void Explode() {
		Debug.Log("Unit died");
		
		StopShooting();

		Destroy(gameObject);
		// todo: animate explosion..
	}

}