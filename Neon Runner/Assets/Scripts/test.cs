using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Update is called once per frame
    public float n;

    private void Start()
    {
        n = GameObject.FindGameObjectWithTag("Player").transform.position.z;
    }
    void Update()
    {

        //  float test = this.transform.position.z;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, n);
        n = n + 4;
        //lightGameObject.transform.position = schuss.transform.position;
        if (this.transform.position.z >= GameObject.FindGameObjectWithTag("Player").transform.position.z + 100 || this.transform.position.z <= GameObject.FindGameObjectWithTag("Player").transform.position.z - 100)
        {
            Destroy(gameObject);
        }
    }
}
