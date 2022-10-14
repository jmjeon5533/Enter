using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeAmount;
    float ShakeTime;
    Vector3 initialPosition;

    public void VibrateForTime(float time)
    {
        ShakeTime = time;
    }
    void Start()
    {
        initialPosition = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShakeTime > 0)
        {
            transform.position = Random.insideUnitSphere 
                * ShakeAmount + initialPosition;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;
            transform.position = initialPosition;
        }
    }
}
