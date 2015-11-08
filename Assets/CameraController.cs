using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{

    [System.Serializable]
    public class CameraControllerSettings
    {
        Camera camera;

        public string name = "Default";

        public GameObject onScreenIndicator;

        public Vector3 cameraOffset;
        public GameObject[] targets;
        public bool keepTargetsOnScreen = true;

        public float positionLerpSpeed;
        public float lookAtSpeed;
        public float zoomSpeed;
        Vector3 positionVelocity = new Vector3();
        Vector3 platformVelocity = new Vector3();

        public float positionMaxVelocity;
        public float positionAcceleration;

        public float platformOffset;

        public float topWindow;
        public float bottomWindow;
        public float leftWindow;
        public float rightWindow;

        public Vector3 relativePos;

        Vector3 targetOffsetPosition;
        Vector3 targetPosition;
        Vector3 prev_targetPosition;

        public Vector3 cameraTargetOffset;

        bool shouldLerp = false;
        Vector3 position;
        Quaternion rotation;

        public Vector3 screenPosition;
        public Vector3 onScreenOffset = Vector3.zero;
        public bool SmoothDamp = false;

        public bool Debugging = true;

        public Vector3 downOffset;
        public Vector3 leftOffset;
        public Vector3 rightOffset;

        public float downMargin = 2;
        public float leftMargin = 2;
        public float rightMargin = 2;
        public float changeLookSpeed = 30;
        public float changeLookAccl = 0.1f;

        public float optimal_z = -60;

        public Vector3 testVector;

        bool falling = false;


        public void SetCamera(GameObject c)
        {
            camera = c.GetComponent<Camera>();
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

        void PositionLerp()
        {
            if (positionLerpSpeed > 0)
                position = Vector3.Lerp(position, targetOffsetPosition + cameraOffset, positionLerpSpeed * Time.deltaTime);
        }

        void PositionSmoothDamp()
        {
            if (positionMaxVelocity > 0)
                position = Vector3.SmoothDamp(position,
                targetOffsetPosition + cameraOffset,
                ref positionVelocity,
                positionAcceleration,
                positionMaxVelocity,
                Time.deltaTime);
        }

        void WindowLock()
        {
            Vector3 cp = position;
            Vector3 tp = targetOffsetPosition;

            if (cp.x < tp.x - rightWindow)
            {
                cp.x = tp.x - rightWindow;
            }
            else if (cp.x > tp.x + leftWindow)
            {
                cp.x = tp.x + leftWindow;
            }

            if (cp.y < tp.y - topWindow)
            {

                cp.y = tp.y - topWindow;
            }
            else if (cp.y > tp.y + bottomWindow)
            {
                cp.y = tp.y + bottomWindow;
            }

            position = cp;
        }
           
        Vector3 lookSmoothVelocity;

        public void LookDirection()
        {
            relativePos = targetPosition - position;

            if (relativePos.x - leftMargin > 0)
                testVector = leftOffset;

            if (relativePos.x + rightMargin < 0)
                testVector = rightOffset;

            if (relativePos.y + downMargin < 0)
                testVector.y = downOffset.y;

            if (Mathf.Abs(prev_targetPosition.y - targetPosition.y) < 0.01f)
                testVector.y = 0;

            //cameraTargetOffset = Vector3.Lerp(cameraTargetOffset, testVector, 5*Time.deltaTime); 
            cameraTargetOffset = Vector3.SmoothDamp(cameraTargetOffset,
                                                    testVector,
                                                    ref lookSmoothVelocity,
                                                    changeLookAccl,
                                                    changeLookSpeed,
                                                    Time.deltaTime);
        }

        void KeepTargetsOnScreen()
        {
            bool allOnScreen = true;
            Vector3 sp = Vector3.zero;
            foreach (GameObject target in targets)
            {
                sp = camera.WorldToScreenPoint(target.transform.position);
                sp.x = (sp.x - camera.pixelWidth / 2.0f) / camera.pixelWidth;
                sp.y = (sp.y - camera.pixelHeight / 2.0f) / camera.pixelHeight;
                if (Mathf.Abs(sp.x) > 0.4f || Mathf.Abs(sp.y) > 0.4f)
                    allOnScreen = false;

            }

            screenPosition = sp;
            if (allOnScreen == true)
            {
                onScreenOffset.z += zoomSpeed * Time.deltaTime / 10;
                if (onScreenOffset.z > 0)
                    onScreenOffset.z = 0;
            }
            else
            {
                if (Mathf.Abs(sp.x) > 0.45f || Mathf.Abs(sp.y) > 0.45f)
                    onScreenOffset.z -= zoomSpeed * Time.deltaTime;
                //if(onScreenOffset.z > 0)
                //		onScreenOffset.z = 0;

            }
        }


        public void Update()
        {

            //		screenPosition = camera.WorldToScreenPoint(targetOffsetPosition);

            // position is used in calculations
            position = camera.transform.position - (cameraTargetOffset + onScreenOffset);
            //rotation = camera.transform.rotation;
            calculateAverageTargetPosition();
            if (SmoothDamp)
            {
                PositionSmoothDamp();        
            }
            else
            {
                PositionLerp();
            }
            LookDirection();
            WindowLock();

            KeepTargetsOnScreen();

            camera.transform.position = position + (cameraTargetOffset + onScreenOffset);
            //	camera.transform.rotation = rotation;
        }

        public void DrawDebug()
        {
            if (Debugging == false)
                return;
            Vector3 p = position;

            Vector3 topLeft = p + new Vector3(-leftWindow, topWindow, 0);
            Vector3 topRight = p + new Vector3(rightWindow, topWindow, 0);
            Vector3 bottomLeft = p + new Vector3(-leftWindow, -bottomWindow, 0);
            Vector3 bottomRight = p + new Vector3(rightWindow, -bottomWindow, 0);
            Vector3 platformLeft = p + new Vector3(-leftWindow, -platformOffset, 0);
            Vector3 platformRight = p + new Vector3(rightWindow, -platformOffset, 0);
            Vector3 marginTopLeft = p + new Vector3(-leftMargin, topWindow, 0);
            Vector3 marginBottomLeft = p + new Vector3(-leftMargin, -bottomWindow, 0);
            Vector3 marginTopRight = p + new Vector3(rightMargin, topWindow, 0);
            Vector3 marginBottomRight = p + new Vector3(rightMargin, -bottomWindow, 0);
            Vector3 marginDownRight = p + new Vector3(rightWindow, -downMargin, 0);
            Vector3 marginDownLeft = p + new Vector3(-leftWindow, -downMargin, 0);

            topLeft.z = targetPosition.z;
            topRight.z = targetPosition.z;
            bottomLeft.z = targetPosition.z;
            bottomRight.z = targetPosition.z;
            platformLeft.z = targetPosition.z;
            platformRight.z = targetPosition.z;
            marginTopLeft.z = targetPosition.z;
            marginBottomLeft.z = targetPosition.z;
            marginTopRight.z = targetPosition.z;
            marginBottomRight.z = targetPosition.z;
            marginDownRight.z = targetPosition.z;
            marginDownLeft.z = targetPosition.z;


            Debug.DrawLine(topLeft, topRight, Color.yellow, 0, false);
            Debug.DrawLine(topRight, bottomRight, Color.yellow, 0, false);
            Debug.DrawLine(bottomRight, bottomLeft, Color.yellow, 0, false);
            Debug.DrawLine(bottomLeft, topLeft, Color.yellow, 0, false);
            Debug.DrawLine(platformLeft, platformRight, Color.white, 0, false);
            Debug.DrawLine(marginTopLeft, marginBottomLeft, Color.blue, 0, false);
            Debug.DrawLine(marginTopRight, marginBottomRight, Color.blue, 0, false);
            Debug.DrawLine(marginDownRight, marginDownLeft, Color.blue, 0, false);

        }
    }

    public CameraControllerSettings ccs = new CameraControllerSettings();
    public GameObject camera;

    void Start()
    {
        ccs.SetCamera(camera);
    }

    // Update is called once per frame
    void FixedUpdate()
    {


    }

    void Update()
    {
        ccs.Update();
        ccs.DrawDebug();
    }




}
