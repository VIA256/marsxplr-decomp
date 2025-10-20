using System;
using UnityEngine;

[Serializable]
public class JumpPoint : MonoBehaviour
{
    public WhirldObject whirldObject;
    private int time = 1;
    private int randMin = 0;
    private int randMax = 0;
    private int velocity = 50;
    private float lastBlast;

    public void Start()
    {
        if (!(bool)whirldObject || whirldObject.parameters == null)
        {
            return;
        }
        if ((bool)whirldObject.parameters["JumpTime"])
        {
            time = (int)whirldObject.parameters["JumpTime"];
        }
        if ((bool)whirldObject.parameters["JumpRandMin"])
        {
            randMin = (int)whirldObject.parameters["JumpRandMin"];
        }
        if ((bool)whirldObject.parameters["JumpRandMax"])
        {
            randMax = (int)whirldObject.parameters["JumpRandMax"];
        }
        if ((bool)whirldObject.parameters["JumpVelocity"])
        {
            velocity = (int)whirldObject.parameters["JumpVelocity"];
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14) return;
        lastBlast = Time.time + (float)time;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 14) return;
        if (Time.time - 0.1f < lastBlast) return;
        lastBlast = Time.time;
        if (randMin != 0 && randMax != 0)
        {
            other.attachedRigidbody.AddForce(
                transform.up * UnityEngine.Random.Range(randMin, randMax),
                ForceMode.VelocityChange);
        }
        else
        {
            other.attachedRigidbody.AddForce(
                transform.up * velocity,
                ForceMode.VelocityChange);
        }
    }
}

