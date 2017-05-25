using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    // public variables for camera configuration
	public GameObject playerObject;

    public float cameraDistance = 10f; 
    public float cameraDistanceTolerance = 5f; // how close/far can camera be from the player

    public float cameraYOffset = 1; // camera initial height relative to the player

    public float xRotationSpeed = 2.0f;
    public float yRotationSpeed = 2.0f;

    public float cameraMinYAngle = -20f; 
    public float cameraMaxYAngle = 80f;

    // private variables for camera control
	private Transform playerTransform;
	private PlayerHealth player;

    private Rigidbody cameraRb;

    private float cameraX;
    private float cameraY;

    private float cameraMinDistance;
    private float cameraMaxDistance;
    
    private const float correctionFactor = 0.02f;

    void Start () {
		Cursor.visible = false;


		playerTransform = playerObject.transform;
		player = playerObject.GetComponent <PlayerHealth> ();

        Vector3 angles = transform.eulerAngles;
        cameraX = angles.y;
        cameraY = angles.x;

        cameraMinDistance = cameraDistance - cameraDistanceTolerance;
        cameraMaxDistance = cameraDistance + cameraDistanceTolerance;

        cameraRb = GetComponent<Rigidbody>();
        if (cameraRb != null){
            cameraRb.freezeRotation = true;
        }

	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (playerTransform && !player.isDead()){
            cameraX += Input.GetAxis("Mouse X") * xRotationSpeed * cameraDistance * correctionFactor;
            cameraY -= Input.GetAxis("Mouse Y") * yRotationSpeed * cameraDistance * correctionFactor;

            cameraY = ClampAngle(cameraY, cameraMinYAngle, cameraMaxYAngle);

            Quaternion rotation = Quaternion.Euler(cameraY, cameraX, 0.0f);

            // change camera distance with mouse scrollwheel
            cameraDistance = Mathf.Clamp(cameraDistance - Input.GetAxis("Mouse ScrollWheel") * 5, cameraMinDistance, cameraMaxDistance);

            RaycastHit raycast;
            if (Physics.Linecast(playerTransform.position, transform.position, out raycast)){
                cameraDistance -= raycast.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -cameraDistance);
            Vector3 position = rotation * negDistance + playerTransform.position + new Vector3(0, cameraYOffset, 0);

            transform.rotation = rotation;
            transform.position = position;
        }
	}

    float ClampAngle(float angleValue, float angleMinValue, float angleMaxValue){
        // value correction
        if (angleValue < -360F)
            angleValue += 360F;
        if (angleValue > 360F)
            angleValue -= 360F;

        // clamp angle value
        return Mathf.Clamp(angleValue, angleMinValue, angleMaxValue);
    }
}
