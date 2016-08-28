using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
    public float health = 1000.0f;

    private Rigidbody body;

	void Start() {
        body = GetComponent<Rigidbody>();
	}
	
    void OnCollisionEnter(Collision collision) {
        if (body.isKinematic) {
            health -= collision.impulse.magnitude;
            Debug.Log(health);

            if (health < 0.0f) {
                body.isKinematic = false;

                foreach (Collider collider in GetComponentsInChildren<Collider>()) {
                    collider.isTrigger = true;
                }
            }
        }
    }
}
