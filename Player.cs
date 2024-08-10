using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool wDown;

    Vector3 moveV;

    Animator ani;

    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");

        moveV = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveV * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        ani.SetBool("isRun", moveV != Vector3.zero);
        ani.SetBool("isWalk", wDown);

        transform.LookAt(transform.position + moveV);
    }

}
