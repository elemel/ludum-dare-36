using UnityEngine;
using System.Collections;

public class BoulderScript : MonoBehaviour {
    public bool armed = true;
    public float ttl = 10.0f;

	void FixedUpdate() {
        if (armed) {
            ttl -= Time.deltaTime;

            if (ttl < 0.0f) {
                armed = false;

                foreach (Collider collider in GetComponentsInChildren<Collider>()) {
                    collider.isTrigger = true;
                }
            }
        }

        if (transform.position.y < -1000.0f) {
            Debug.Log("Destroy boulder");
            Destroy(gameObject);
        }
	}
}
