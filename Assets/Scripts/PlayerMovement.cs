using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public float forceMultiplier;

    public GameObject slideThumbstick;
    public GameObject slideBackground;

    public Transform sliderCenter;
    public Transform sliderEdge;

    public float rbVelocityUpperLimit = 0.7f;

    public GameObject playerSprite;
    public float rotationSpeed = 0.2f;
    public GameObject directionOrb;

    CameraControls camControls;
    public float wallImpactDuration = 0.2f;

    public GameObject releasePoint;
    public GameObject releaseFX;

    public GameObject tempArrow;
    bool canMove = true;

    GameManager gameManager;
    public int onTreadmill = 0;

    public float speedReduction = 0.3f;

    //TRAJECTORY LINE
    public float maxLineDistance = 3f;
    public float debugLineMultiplier = 0.001f;

    float circleNum = 8;
    public GameObject trajectoryCircle;
    GameObject[] trajCirArray = new GameObject[8];

    public LayerMask trajectoryLayer;

    [HideInInspector]
    public float dirMag;
    [HideInInspector]
    public float maxDistance;
    [HideInInspector]
    public bool dragging = false;

    int waterCount = 0;
    public float timeToDeath = 2.5f;
    float health;
    public float reviveMultiplier = 0.4f;
    public Slider healthSlider;
    public Gradient gradient;
    public Image fill;
    public Image background;
    ObjectFade objectFade;

    int onBridge = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxDistance = sliderEdge.position.x - sliderCenter.position.x;
        slideThumbstick.SetActive(false);
        slideBackground.SetActive(false);

        camControls = FindObjectOfType<CameraControls>();
        gameManager = FindObjectOfType<GameManager>();
        objectFade = FindObjectOfType<ObjectFade>();

        for (int i = 0; i < circleNum; ++i) {
            trajCirArray[i] = Instantiate(trajectoryCircle, transform);
            trajCirArray[i].SetActive(true);
        }

        health = timeToDeath;
        healthSlider.maxValue = timeToDeath;
        healthSlider.minValue = 0;
        healthSlider.value = health;
        //fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (rb.velocity.magnitude >= rbVelocityUpperLimit && !dragging)
        {
            if (rotationSpeed == 0)
            {
                rotationSpeed = 20f;
            }
            playerSprite.transform.Rotate(new Vector3(0, 0, 1), rotationSpeed *
                (rb.velocity.magnitude - rbVelocityUpperLimit) * Time.deltaTime);
        }
        else if (onTreadmill == 0)
        {
            rotationSpeed = 0;
        }

        if (canMove)
        {
            //Begin Dragging Process
            if (Input.GetMouseButtonDown(0) && !gameManager.paused && !camControls.isMapScaleButton)
            {
                dragging = true;
                slideBackground.transform.position = Input.mousePosition;
                PlayerControlElementsActive(true);

                rb.velocity = rb.velocity * (1 - speedReduction);
            }

            //During Dragging
            if (dragging)
            {
                Vector3 mousePos = Input.mousePosition;

                Vector3 direction = mousePos - sliderCenter.position;
                dirMag = direction.magnitude;

                if (dirMag >= maxDistance)
                {
                    dirMag = maxDistance;
                }

                if (dirMag < maxDistance)
                {
                    slideThumbstick.transform.position = mousePos;
                }
                else
                {
                    slideThumbstick.transform.position = sliderCenter.position + (direction.normalized * maxDistance);
                }


                Vector3 slideDirection = Input.mousePosition - sliderCenter.position;
                directionOrb.transform.localPosition = new Vector2(-slideDirection.x, -slideDirection.y) * debugLineMultiplier;


                Vector3 targetPos = directionOrb.transform.position;
                targetPos.z = 0;
                Vector3 spritePos = playerSprite.transform.position;
                targetPos.x = targetPos.x - spritePos.x;
                targetPos.y = targetPos.y - spritePos.y;

                float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
                playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

                float magnitudePercent = dirMag / maxDistance;
                float totalLineDistance = maxLineDistance * magnitudePercent;


                {
                    List<Vector2> points = new List<Vector2>();

                    //first point is the start point
                    //Loop:
                    //check for collision at the distance
                    //if collision
                    //add collision point to the list
                    //calculate start point + trajectory
                    //if no collision
                    //end loop

                    AddPoints(points, transform.position, new Vector2(-slideDirection.x,
                        -slideDirection.y).normalized, totalLineDistance);

                    for (int i = 1; i < points.Count; ++i)
                    {
                        Debug.DrawLine(points[i - 1], points[i]);
                    }

                    float inc = 1 / circleNum;
                    for (int i = 0; i < circleNum; ++i)
                    {

                        if (magnitudePercent == 0)
                        {
                            trajCirArray[i].transform.localPosition = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            trajCirArray[i].transform.position = ReturnPoint(points,
                                totalLineDistance * (inc + inc * i));
                        }
                        
                    }
                }
            }

            //Release Player
            if (Input.GetMouseButtonUp(0) && dragging)
            {
                dragging = false;

                Vector3 tempRotation = new Vector3(90f, 0f,
                    playerSprite.transform.localEulerAngles.z);
                GameObject tempFX = Instantiate(releaseFX, releasePoint.transform.position,
                    releasePoint.transform.rotation);
                Destroy(tempFX, 0.2f);

                Vector3 slideDirection = Input.mousePosition - sliderCenter.position;

                //Get Direction
                Vector3 slideDirectionNormal = Vector3.Normalize(slideDirection);

                //Get Magnitude
                float dragMagnitude = slideDirection.magnitude;
                if (dragMagnitude > maxDistance)
                {
                    dragMagnitude = maxDistance;
                }
                dragMagnitude = dragMagnitude / maxDistance;

                //Apply Force
                rb.AddRelativeForce((new Vector2((-slideDirectionNormal.x) * dragMagnitude,
                    (-slideDirectionNormal.y) * dragMagnitude) * forceMultiplier));

                PlayerControlElementsActive(false);

                rotationSpeed = Random.Range(-60, 60f);
                if (rotationSpeed < 20f && rotationSpeed > -20f)
                {
                    rotationSpeed = 20f;
                }

                gameManager.Moved();
            }

            if (rb.velocity.magnitude >= 0f && rb.velocity.magnitude < rbVelocityUpperLimit && onTreadmill == 0)
            {
                rb.velocity = Vector2.zero;
            }
        }
        else {
            PlayerControlElementsActive(false);
        }
    }


    private void FixedUpdate()
    {
        if (waterCount > 0 && onBridge == 0)
        {
            health -= Time.deltaTime;
            healthSlider.value = health;
            //fill.color.r = gradient.Evaluate(healthSlider.normalizedValue).r;
            if (health <= 0) {
                StartCoroutine("RespawnTimer");
            }
        }
        else {
            if (health < timeToDeath) {
                health += Time.deltaTime * reviveMultiplier;
                healthSlider.value = health;
                //fill.color = gradient.Evaluate(healthSlider.normalizedValue);
                if (health >= timeToDeath) {
                    health = timeToDeath;
                    objectFade.FadeOut(1.5f, background);
                    objectFade.FadeOut(1.5f, fill);
                }
            }
        }
    }

    void AddPoints(List<Vector2> list, Vector2 startPos, Vector2 direction, float distance) {

        list.Add(startPos);

        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, distance, trajectoryLayer);
        if (hit.collider == null) //No Hit
        {
            list.Add(startPos + (direction.normalized * distance));
        }
        else { //Hit
            Vector2 impactPoint = startPos + (direction.normalized * (hit.distance * 0.97f));
            Vector2 newDirection = Vector2.Reflect(direction, hit.normal);
            float newDistance = distance - hit.distance;

            if (newDistance <= 0.1f || startPos == impactPoint) return;

            AddPoints(list, impactPoint, newDirection.normalized, newDistance);
        }
    }

    Vector2 ReturnPoint(List<Vector2> list, float distance) {
        if (distance == 0) {
            return new Vector2(0, 0);
        }

        Vector2 result = list[list.Count - 1];

        for (int i = 1; i < list.Count; ++i) {
            float segmentDistance = Vector2.Distance(list[i - 1], list[i]);
            if (distance <= segmentDistance)
            {
                float percent = distance / segmentDistance;
                result = list[i - 1] + ((list[i] - list[i - 1]) * percent);
                break;
            }
            else {
                distance -= segmentDistance;
            }
        }
        return result;
    }

    IEnumerator RespawnTimer() {
        rb.velocity = Vector2.zero;
        canMove = false;
        playerSprite.SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PlayerControlElementsActive(bool active) {
        slideBackground.SetActive(active);
        slideThumbstick.SetActive(active);
        for (int i = 0; i < circleNum; ++i)
        {
            trajCirArray[i].transform.localPosition = new Vector3(0, 0, 0);
            //trajCirArray[i].GetComponent<s>
            trajCirArray[i].SetActive(active);
        }
    }

    public void addBridge() {
        ++onBridge;
        print("++ onBridge: " + onBridge);
    }

    public void removeBridge() {
        --onBridge;
        print("-- onBridge: " + onBridge);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            camControls.Shake(wallImpactDuration, rb.velocity.magnitude);
        }
        else if (collision.gameObject.tag == "World Boundary")
        {
            camControls.Shake(wallImpactDuration, rb.velocity.magnitude);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            objectFade.FadeIn(0.1f, background);
            objectFade.FadeIn(0.1f, fill);
            ++waterCount;
        }
        else if (collision.gameObject.tag == "Treadmill") {
            ++onTreadmill;
            print("+ : onTreadmill " + onTreadmill);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            --waterCount;
            if (health == timeToDeath) {
                objectFade.FadeOut(1.5f, background);
                objectFade.FadeOut(1.5f, fill);
            }
        }
        else if (collision.gameObject.tag == "Treadmill")
        {
            --onTreadmill;
            print("- : onTreadmill " + onTreadmill);
        }
    }
}
