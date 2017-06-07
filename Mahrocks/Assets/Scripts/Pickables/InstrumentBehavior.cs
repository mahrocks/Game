using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentBehavior : MonoBehaviour {
	public int rotateSpeed = 30;
	private GameObject playerObject;
	private PlayerScore ps;
	private AreaEnemySpawn parentSpawnArea;

	void Start(){
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		ps = playerObject.GetComponent <PlayerScore> ();
		parentSpawnArea = GetComponentInParent <AreaEnemySpawn> ();
		ps.incrementInstrumentQuantity ();
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0, rotateSpeed, 0) * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			if (parentSpawnArea.getEnemiesLeft() == 0) {
				ps.incrementCollectedInstruments ();
				Destroy (this.gameObject);
			}
		}
	}
}
