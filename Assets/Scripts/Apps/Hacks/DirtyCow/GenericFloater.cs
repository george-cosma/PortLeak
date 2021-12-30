using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericFloater : MonoBehaviour
{
    public RectTransform DespawnPoint;


    public float minSpeed = 1.0f;
    public float maxSpeed = 3.0f;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DespawnPoint.position.x <= this.transform.position.x)
		{
            Destroy(this.gameObject);
		}

        Vector3 newPosition = this.transform.position;

        newPosition.x += speed * Time.fixedDeltaTime;

        this.transform.position = newPosition;
    }
}
