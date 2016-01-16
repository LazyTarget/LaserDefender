using UnityEngine;
using System.Collections;

public class SpawnPosition : MonoBehaviour {

	void OnDrawGizmos () {
		Gizmos.DrawWireSphere(transform.position, 0.75f);
	}
}
