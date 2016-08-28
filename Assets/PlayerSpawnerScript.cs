using UnityEngine;
using System.Collections;

public class PlayerSpawnerScript : MonoBehaviour {
    public GameObject mainCamera;
    public GameObject playerPrefab;
    public GameObject player;
    public float currentSpawnDelay = 0.0f;
    public float respawnDelay = 1.0f;

	void FixedUpdate() {
        if (player == null) {
            currentSpawnDelay -= Time.deltaTime;

            if (currentSpawnDelay < 0.0f) {
                player = (GameObject) Instantiate(playerPrefab, transform.position, transform.rotation);
                CatapultScript catapultScript = player.GetComponent<CatapultScript>();
                catapultScript.mainCamera = mainCamera;
            }
        }
    }
}
