using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualOffset : MonoBehaviour
{
    public int magnitude = 2;
    void Start()
    {
        Vector3 offset = transform.position += new Vector3(Random.value * magnitude, Random.value * magnitude, Random.value * magnitude);
        transform.position = offset;
    }
}
