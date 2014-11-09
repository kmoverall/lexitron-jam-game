using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	BeatManager beatz;
	public Transform spawnEnemy;
	public PlayerControl.Lanes lane;

	// Use this for initialization
	void Start () {
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
	}

	public void Spawn (string k) {
		Transform newEnemy = (Transform)Instantiate(spawnEnemy, gameObject.transform.position, Quaternion.identity);
		newEnemy.gameObject.GetComponent<Enemy>().buttonString = k;
		newEnemy.gameObject.GetComponent<Enemy>().lane = lane;
	}
}
