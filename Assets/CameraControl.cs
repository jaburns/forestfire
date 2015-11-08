﻿using UnityEngine;
using System.Collections;
using System;

public class CameraControl : MonoBehaviour {

    public GameObject cameraObject;
    Camera camera;
    public GameObject[] targets;
    public float zoomSpeed = 5;
    public float zoomInFactor = 0.2f;
    public float zoomFactor1 = 0.4f;
    public float zoomFactor2 = 0.45f;


    public Vector3 cameraOffset;
    Vector3 targetPosition;
    Vector3 prev_targetPosition;
    Vector3 targetOffsetPosition;

    Vector3 positionVelocity = new Vector3();

    public float positionMaxVelocity;
    public float positionAcceleration;

    public Vector3 screenPosition;
    public Vector3 onScreenOffset = Vector3.zero;

    Vector3 position;

    // Use this for initialization
    void Start () {
        camera = cameraObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update () {
        calculateAverageTargetPosition();
        keepTargetsOnScreen();
        positionSmoothDamp();
        camera.transform.position = position + onScreenOffset;
    }
    void keepTargetsOnScreen()
    {
        bool allOnScreen = true;
        Vector3 sp = Vector3.zero;
        foreach (GameObject target in targets)
        {
            sp = camera.WorldToScreenPoint(target.transform.position);
            sp.x = (sp.x - camera.pixelWidth / 2.0f) / camera.pixelWidth;
            sp.y = (sp.y - camera.pixelHeight / 2.0f) / camera.pixelHeight;
            if (Mathf.Abs(sp.x) > zoomFactor1 || Mathf.Abs(sp.y) > zoomFactor1) {
                allOnScreen = false;
                break;
            }
        }

        screenPosition = sp;
        if (allOnScreen)
        {
            onScreenOffset.z += zoomSpeed * Time.deltaTime * zoomInFactor;
            if (onScreenOffset.z > 0)
                onScreenOffset.z = 0;
        }
        else
        {
            if (Mathf.Abs(sp.x) > zoomFactor2 || Mathf.Abs(sp.y) > zoomFactor2)
                onScreenOffset.z -= zoomSpeed * Time.deltaTime;
        }
    }

    void calculateAverageTargetPosition()
    {
        Vector3 average = new Vector3();
        foreach (GameObject target in targets)
        {
            average += target.transform.position;
        }

        prev_targetPosition = targetPosition;
        targetPosition = average / targets.Length;
        targetOffsetPosition = targetPosition + cameraOffset;
    }

    void positionSmoothDamp()
    {
        if (positionMaxVelocity > 0)
            position = Vector3.SmoothDamp(position,
            targetOffsetPosition + cameraOffset,
            ref positionVelocity,
            positionAcceleration,
            positionMaxVelocity,
            Time.deltaTime);
    }
}
