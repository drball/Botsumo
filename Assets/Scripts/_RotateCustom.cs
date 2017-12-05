using UnityEngine;
using System.Collections;

public class _RotateCustom : MonoBehaviour {
    // Here some variable we'll use
    public float rotationSpeed = 1.8f;
    public float moveSpeed = 1;
    bool rotateL = false;
    bool stopAll = false;
    public GameObject obj;
	// Use this for initialization
	void Start () {
        stopAll = false;
	}


    void Update()
    {
        // Check constantly if the bot is not standing up correctly
        if (obj.transform.rotation.x >= 0.4 || obj.transform.rotation.z >= 0.4)
        {
            stopAll = true;
            // Debug.Log("Stop");
        }else if (obj.transform.rotation.x <= -0.4 || obj.transform.rotation.z <= -0.4)
        {
            stopAll = true;
            // Debug.Log("Stop");
        }
        else
        {
            stopAll = false;
        }
        //Debug.Log(this.transform.rotation.x);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // If bot is not stopped
        if (!stopAll)
        {
            // if the moveforward variable in SeekPlayer script attached to the bot raycaster is true...
            if (SeekPlayer.moveForward)
            {
                // ... we want to move forwad learping to smooth the movement
                obj.transform.Translate(Vector3.Lerp(obj.transform.position, Vector3.forward, 1) * moveSpeed);
                if (rotateL)
                {
                    rotateL = false;
                }
                else
                {
                    rotateL = true;
                }
            }
            else
            { // else we want the bot to rotate on its vertical axis, and we wantto alternate it clockwise/ anticlockwise rotation
                if (rotateL)
                {
                    obj.transform.Rotate(-Vector3.up * rotationSpeed);
                }
                else if (!rotateL)
                {
                    obj.transform.Rotate(Vector3.up * rotationSpeed);
                }
            }
        }
        else
        {
            //this.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, 0);
        }
       
    }
}