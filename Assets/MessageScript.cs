using UnityEngine;
using System.Collections;

public class MessageScript : MonoBehaviour {
    public float ttl = 1.0f;

	void Update() {
        ttl -= Time.deltaTime;	

        if (ttl < 0.0f) {
            Destroy(gameObject);
        }
    }
}
