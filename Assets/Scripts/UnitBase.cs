using UnityEngine;
using System.Collections;

public abstract class UnitBase : MonoBehaviour
{
	protected UnitBase ()
	{

	}

	public AudioClip deathSound;
	public Weapon weapon;
	public float health = 100;
	public Vector2 direction;
	public abstract int teamID { get; }
	
	
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

		AudioSource.PlayClipAtPoint(deathSound, transform.position, 1f);

		Destroy(gameObject);
		// todo: animate explosion..
	}

}