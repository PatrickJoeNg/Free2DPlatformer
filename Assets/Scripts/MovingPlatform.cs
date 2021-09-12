using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] Vector2 movementVector = new Vector2(5f, 5f);
    [SerializeField] float period = 2f;

    float movementFactor; // 0 for moved, 1 for fully moved

    Vector2 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // protect from NAN
        float cycles = Time.time / period; // grows cont from 0

        if (period <= Mathf.Epsilon)
        {
            return;
        }

        const float tau = Mathf.PI * 2; // 3.14 * 2
        float rawSineWave = Mathf.Sin(cycles * tau); // goes from -1 to 1

        movementFactor = rawSineWave / 2f + 0.5f;

        Vector2 offset = movementVector * movementFactor;

        transform.position = startingPos + offset;
    }
}
