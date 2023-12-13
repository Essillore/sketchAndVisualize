using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 10f;
    public float timer;

    public GameObject target;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(target != null)
        {
            float x = Mathf.Clamp(target.transform.position.x, xMin, xMax);
            float y = Mathf.Clamp(target.transform.position.y, yMin, yMax);
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);

            //transform.position = transform.position + Vector3.right * speed * Time.deltaTime;

        }

    }
}
