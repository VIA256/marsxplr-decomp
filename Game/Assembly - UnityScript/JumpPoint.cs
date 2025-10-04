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
        if (!(bool)whirldObject || whirldObject.@params == null)
        {
            return;
        }
        if ((bool)whirldObject.@params["JumpTime"])
        {
            time = (int)whirldObject.@params["JumpTime"];
        }
        if ((bool)whirldObject.@params["JumpRandMin"])
        {
            randMin = (int)whirldObject.@params["JumpRandMin"];
        }
        if ((bool)whirldObject.@params["JumpRandMax"])
        {
            randMax = (int)whirldObject.@params["JumpRandMax"];
        }
        if ((bool)whirldObject.@params["JumpVelocity"])
        {
            velocity = (int)whirldObject.@params["JumpVelocity"];
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

