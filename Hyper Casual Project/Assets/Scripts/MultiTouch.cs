using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MultiTouch : MonoBehaviour
{
    public GameManager manager;
    public IndicatorManager indicatorManager;

    public Transform target;
    Transform originTrans;

    //Tap
    Touch touchTap;

    private float rotX = 0f;
    private float rotY = 0f;
    public float dir = -1;

    public float xRightOffsetMax = 5f;
    public float xLeftOffsetMax = -5f;
    bool isRightLimit;
    bool isLeftLimit;

    public float yUpOffsetMax = 20f;
    public float yDownOffsetMax = -10f;

    //Swipe
    //private float rotY = 0f;
    private Vector3 origRot;

    public float rotSpeed = 10f;

    public float minDistanceInch = 0.1f; // Inch
    private float minSwipeDistancePixels;
    public float maxSwipeTime = 1f;

    private int fingerId = int.MinValue;
    private Vector2 touchStart;
    private float touchStartTime;

    public float sensitivity = .5f;

    public int maxRightSwipe;
    public int maxLeftSwipe;

    //Pinch to Zoom
    const float minPinchDistance = 5;
    public float zoomOutMin = 1;
    public float zoomOutMax = 22;//22

    //안겹치게.
    bool isPinch;
    bool isZoom;

    //코루틴 체크.
    bool isCoClicked;
   
    // Start is called before the first frame update
    void Start()
    {
        origRot = transform.eulerAngles;
        rotX = origRot.x;
        rotY = origRot.y;

        minSwipeDistancePixels = Screen.dpi * minDistanceInch;
        originTrans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.bookDisplay.isOpen && !manager.optionDisplay.isOpen && !manager.optionDisplay.isAdWinOpen && !manager.optionDisplay.isMmWinOpen)
        {
            if (Input.touchCount == 1)
            { //if there is any touch
                touchTap = Input.GetTouch(0);

                Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치
                Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // 변환 안하고 바로 Vector3로 받아도 되겠지.

                Ray ray = Camera.main.ScreenPointToRay(theTouch);    // 터치한 좌표 레이로 바꾸엉
                RaycastHit hit;    // 정보 저장할 구조체 만들고

                int layerMask = LayerMask.GetMask("Apple");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))    // 레이저를 끝까지 쏴블자. 충돌 한넘이 있으면 return true다.
                {
                    var script = hit.collider.gameObject.GetComponent<Apple>();
                    script.isTouched = true;
                    if (!script.isCollide)
                    {
                        Rigidbody rigid = hit.collider.gameObject.GetComponent<Rigidbody>();
                        rigid.useGravity = true;
                        rigid.isKinematic = false;
                    }
                }

                //동물.
                layerMask = LayerMask.GetMask("Animal");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))   
                {
                    var script = hit.collider.gameObject.GetComponent<AnimalController>();
                    var agent = hit.collider.gameObject.GetComponent<NavMeshAgent>();
                    if (agent == null) return;

                    if (!isCoClicked) StartCoroutine(CoClicked(hit.collider.gameObject));
                }

                //상자.
                layerMask = LayerMask.GetMask("Box");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    manager.optionDisplay.OpenAdWin();
                }

                foreach (var element in manager.animals)
                {
                    if (!element.Value.isSpecial && !(element.Value.name == "Hen" || element.Value.name == "Cock"))
                    {
                        if (manager.animals[element.Key].payVitality <= manager.vitality) manager.animals[element.Key].isLock = false;
                        else manager.animals[element.Key].isLock = true;
                    }
                }
            }

                isPinch = false;
                isZoom = false;

                if (Input.touchCount == 2)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                    float difference = currentMagnitude - prevMagnitude;

                if (Mathf.Abs(difference) > minPinchDistance)
                {
                    if (difference > 0)
                    {
                        isZoom = true;
                    }
                    else
                    {
                        isPinch = true;
                    }

                    zoom(difference * 0.01f);
                }
                //else 
                //{
                //    Vector3 diff = touchOne.position - touchZero.position;
                //    var angle = (Mathf.Atan2(diff.y, diff.x));
                //    transform.RotateAround(target.position, -Vector3.up, angle * 30 * Time.deltaTime);
                //    //transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * angle);
                //}
            }

                //Swipe.
                if (!isPinch && !isZoom && Input.touchCount == 1)
                {
                    var touch = Input.touches[0];
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            touchStart = touch.position;
                            break;
                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                            var endPos = touch.position;
                            var delta = endPos - touchStart;
                        //Debug.Log($"distance : {Vector3.Distance(transform.position, target.position)}");
                        if ((Mathf.Abs(delta.x) > minPinchDistance))
                        {
                            if (delta.x < 0)
                            {
                                transform.RotateAround(target.position, -Vector3.up, 90 * Time.deltaTime);
                                //if (manager.cameraMode) transform.RotateAround(target.position, -Vector3.up, 90 * Time.deltaTime);
                                //else
                                //{
                                //    //if (transform.position.x < target.position.x + xRightOffsetMax)
                                //    if (Vector3.Distance(transform.position, target.position) < 80)
                                //        transform.Translate(Vector3.right * 10 * Time.deltaTime);
                                //    else if (isLeftLimit)
                                //    {
                                //        transform.Translate(Vector3.right * 20 * Time.deltaTime);
                                //        isLeftLimit = false;
                                //    }
                                //    else isRightLimit = true;
                                //}
                                //else transform.Translate(Vector3.right * 5 * Time.deltaTime);
                            }
                            else if (delta.x > 0)
                            {
                                transform.RotateAround(target.position, Vector3.up, 90 * Time.deltaTime);
                                //if (manager.cameraMode) transform.RotateAround(target.position, Vector3.up, 90 * Time.deltaTime);
                                ////else transform.Translate(-Vector3.right * 5 * Time.deltaTime);
                                //else
                                //{
                                //    //if (transform.position.x > target.position.x + xLeftOffsetMax)
                                //    if (Vector3.Distance(transform.position, target.position) < 80)
                                //        transform.Translate(-Vector3.right * 10 * Time.deltaTime);
                                //    else if (isRightLimit)
                                //    {
                                //        transform.Translate(-Vector3.right * 20 * Time.deltaTime);
                                //        isRightLimit = false;
                                //    }
                                //    else isLeftLimit = true;
                                //}
                            }
                        }

                        if (!manager.cameraMode)
                        {
                            if (Mathf.Abs(delta.y) > minPinchDistance)
                            {
                                if (delta.y < 0)
                                {
                                    if (transform.position.y < target.position.y + yUpOffsetMax)
                                    {
                                        transform.Translate(Vector3.up * 3 * Time.deltaTime);
                                    }
                                }
                                else if (delta.y > 0)
                                {
                                    if (transform.position.y > target.position.y - yDownOffsetMax)
                                    {
                                        transform.Translate(-Vector3.up * 3 * Time.deltaTime);
                                    }
                                }
                            }
                        }
                            transform.LookAt(target);
                          
                            break;
                    }
                }
        }
    }//update.

    void zoom(float increment)
    {
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - increment, zoomOutMin, zoomOutMax);
        //Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

    float FilterGyroValues(float axis)
    {
        float thresshold = 0.5f;
        if (axis < -thresshold || axis > thresshold)
        {
            return axis;
        }
        else
        {
            return 0;
        }
    }

    public void RotateRightLeft(float axis)
    {
        transform.RotateAround(transform.position, Vector3.up, -axis * Time.deltaTime);
    }

    IEnumerator CoClicked(GameObject animal)
    {
        isCoClicked = true;

        var script = animal.GetComponent<AnimalController>();
        var agent = animal.GetComponent<NavMeshAgent>();

        if (agent.enabled == true) agent.enabled = false;

        if (script.anim.GetBool("isWalk") == true) script.anim.SetBool("isWalk", false);
        if (!script.currentAnimal.isFish) script.isSpin = false;
        script.isRoll = false;
        script.isPeck = false;
        script.isBounce = false;
        script.isClicked = true;
        script.isFear = false;
        script.isIdle_B = false;
        script.isIdle_C = false;
        script.isJump = false;
        script.isFly = false;
        script.isSwim = false;

        if (manager.soundManager.clickSound.isPlaying == true)
        {
            manager.soundManager.clickSound.Stop();
            manager.soundManager.clickSound.Play();
        }
        else
            manager.soundManager.clickSound.Play();

        yield return new WaitForSeconds(1f);

        isCoClicked = false;
        script.isClicked = false;
        script.stayTimer += script.moveCycle;
    }
}
