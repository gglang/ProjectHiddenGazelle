﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class IronSights : MonoBehaviour {

    float startingX;
    float startingY;
    float startingZ;

    private Animator animator;

	// Use this for initialization
	void Start () {
        startingX = gameObject.transform.localPosition.x;
        startingY = gameObject.transform.localPosition.y;
        startingZ = gameObject.transform.localPosition.z;
        animator = GetComponentInParent<NetworkAnimator>().animator;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButton(1))
        {
            gameObject.transform.localPosition = new Vector3(0, startingY, startingZ);
            animator.SetBool("isAiming", true);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            gameObject.transform.localPosition = new Vector3(startingX, startingY, startingZ);
            animator.SetBool("isAiming", false);
        }

	}
}
