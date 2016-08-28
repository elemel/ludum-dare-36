using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSpawnerScript : MonoBehaviour {
    public GameObject canvas;
    public GameObject messagePrefab;
    public GameObject mainCamera;
    public GameObject playerPrefab;
    public GameObject player;
    public float currentSpawnDelay = 0.0f;
    public float respawnDelay = 1.0f;

    public void FixedUpdate() {
        if (player == null) {
            currentSpawnDelay -= Time.deltaTime;

            if (currentSpawnDelay < 0.0f) {
                player = (GameObject) Instantiate(playerPrefab, transform.position, transform.rotation);
                BallistaScript ballistaScript = player.GetComponent<BallistaScript>();
                ballistaScript.mainCamera = mainCamera;
                DisplayMessage("DESTROY THE CASTLE");
            }
        }
    }

    private void DisplayMessage(string text, float ttl = 1.0f) {
        GameObject message = Instantiate(messagePrefab);
        message.transform.SetParent(canvas.transform, false);
        message.GetComponent<Text>().text = text;
        message.GetComponent<MessageScript>().ttl = 2.0f;
    }
}
