using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{

    private Rigidbody2D birdRB;
    private LineRenderer birdLR;
    Vector3 initialPos;
    bool isLaunched = false;
    private float isIdeal = 0;

    private void Awake()
    {
        initialPos = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        birdRB = GetComponent<Rigidbody2D>();
        birdLR = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        birdLR.SetPosition(0, transform.position);
        birdLR.SetPosition(1, initialPos);

        if (isLaunched && birdRB.velocity.magnitude <= 0.1)
        {
            isIdeal += Time.deltaTime;
        }

        if (transform.position.x > 20 || transform.position.x < -15 || transform.position.y > 5 || isIdeal > 2f)
        {
            SceneManager.LoadScene("Scene1");
        }
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Transforms a point from screen space into world space,
        transform.position = new Vector3(newPosition.x, newPosition.y);
        birdLR.enabled = true;
    }

    private void OnMouseUp()
    {
        Vector3 oppToDrag = initialPos - transform.position;
        birdRB.AddForce(oppToDrag * 5, ForceMode2D.Impulse);
        birdRB.gravityScale = 1;
        isLaunched = true;
        birdLR.enabled = false;
    }
}
