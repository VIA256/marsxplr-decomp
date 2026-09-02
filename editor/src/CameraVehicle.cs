using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class CameraVehicle : MonoBehaviour
{
    public float sensitivityX = 45F;
    public float sensitivityY = 45F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -90F;
    public float maximumY = 90F;
    
    public float baseMoveSpeed = 0.0625f;
    public float moveSpeed = 24.0f;

    float rotationX = 0F;
    float rotationY = 0F;

    Quaternion originalRotation;
    
    public static bool moveCam = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V) && !Game.textInput)
            moveCam = !moveCam;
        
        if(!moveCam)
        {
            Screen.lockCursor = false;
            Screen.showCursor = true;
            return;
        }
        else
        {
            Screen.showCursor = false;
            Screen.lockCursor = true;
        }
        
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);

        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);

        transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            transform.localEulerAngles = Vector3.zero;
            transform.position = Vector3.zero;
        }
        
        Vector3 pos = transform.position;
        Vector3 direction = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if(Input.GetKey(KeyCode.S))
        {
            direction -= Vector3.forward;
        }
        if(Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        if(Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if(Input.GetKey(KeyCode.Space))
        {
            direction += Vector3.up;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            direction += Vector3.down;
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            direction *= 2;
        }
        Quaternion movrot = Quaternion.Euler(0f, transform.localEulerAngles.y, 0f);
        pos += movrot * direction * moveSpeed * Time.deltaTime;
        transform.position = pos;
    }

    void Start()
    {
        originalRotation = transform.localRotation;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}