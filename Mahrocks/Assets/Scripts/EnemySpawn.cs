using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public GameObject[] enemy;
    public Transform enemyPos;
    public int numEnemies = 0;
    public int maxEnemies = 5;
    private float repeatRate = 1.5f;
    public float minWait = 1.0f;
    public float maxWait = 3.0f;
    public float passedTime = 0.0f;



	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            numEnemies = 0;
            foreach(GameObject g in GameObject.FindGameObjectsWithTag("PlaneEnemies"))
            {
                numEnemies++;
            }
            if (numEnemies < maxEnemies)
            {
                if (passedTime < maxWait  && passedTime > minWait)
                {

                    if (Random.Range(0.0f, 1.0f) >= 0.5f)
                    {
                        Instantiate(enemy[Random.Range(0,2)], enemyPos.position, enemyPos.rotation);
                        passedTime = 0.0f;
                    }
                }else{
                    if (passedTime > maxWait)
                    {
                        Instantiate(enemy[Random.Range(0,2)], enemyPos.position, enemyPos.rotation);
                        passedTime = 0.0f;

                    }else
                    {
                        passedTime += Time.deltaTime;

                    }
                }
            }
        }
    }
	

}
