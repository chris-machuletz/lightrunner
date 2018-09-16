using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schussdamage : MonoBehaviour {

void OnTriggerEnter (Collider other)
    {
            other.SendMessage("ApplyDamage", 1, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
    }
}
