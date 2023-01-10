using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public GameObject box;
    public Rigidbody2D boxPhysicsRb;
    public GameObject redBox;
    public GameObject blueBox;
    public GameObject boxFacingLine;

    public GameObject keys;
    public GameObject topKey;
    public GameObject rightKey;
    public GameObject bottomKey;
    public GameObject leftKey;

    public GameObject physicsBox;
    public GameObject physicsRedBox;
    public GameObject physicsGround;
    public GameObject physicsRedPlatform;
    public Rigidbody2D physicsRedPlatformRb;
    
    public GameObject distanceGroup;
    public GameObject timerGroup;
    public TMP_Text timerText;
    public TMP_Text distanceText;
    public TMP_Text exampleValue;
    private float visualTime = 0.0f;
    private float visualDistance = 0.0f;
    private float timeElapsed = 0.0f;
    private bool start = false;
    private int example = 0;
    private Vector3 boxStartPosition = new Vector3(0,0,0);
    private Vector2 movementDirection;
    public void ResetPosition() {
        HideFacingLine();
        physicsBox.active = false;
        physicsGround.active = false;
        physicsRedBox.active = false;
        physicsRedBox.transform.position = new Vector2(-5.72f, 0.46f);
        physicsRedPlatform.active = false;
        physicsRedPlatform.transform.position = new Vector2(-5.72f, 0.46f);
        
        box.active = false;
        box.transform.rotation = Quaternion.identity;
        start = false;
        visualDistance = 0;
        distanceGroup.active = false;
        distanceText.gameObject.active = false;
        timeElapsed = 0;
        visualTime = 0;
        timerGroup.active = false;
        redBox.active = false;
        timerText.gameObject.active = false;
        box.transform.position = new Vector2(-5.72f, 1.12f);
        redBox.transform.position = new Vector2(-5.72f, 1.12f);
        keys.active = false;
    }
    public void Trigger() { 
        boxStartPosition = box.transform.position;
        start = true;

        if (example == 1) {
            Example1();
        } else if (example == 2) {
            Example2();
        }
    }
    

    // Example 1 - transform.position
    // Example 2 - transform.Translate 
    // Example 3 = transform.position onUpdate
    // Example 4 = transform.position onUpdate
    // ** Vector.right
    // Example 5 = transform.position onUpdate
    // * Show - Red Box
    // ** Vector.right 
    // Example 6 = transform.position onUpdate
    // * Show - Red Box, Rotate Current Box
    // ** Vector.right
    // Example 7 = transform.Translate onUpdate
    // * Show - Red Box
    // ** Vector.right
    // Example 8 = transform.Translate onUpdate
    // * Show - Red Box, Rotate Current Box
    // ** Vector.right
    // Example 9 = transform.Translate onUpdate
    // * Show - Red Box, Distance
    // ** Vector.right
    // Example 10 = transform.Translate onUpdate
    // * Show - Red Box, Distance
    // ** Direction based on Magnitude
    // Example 11 = transform.Translate onUpdate
    // * Show - Red Box, Distance
    // ** Direction based on Magnitude
    // ** Change Y destination of end target
    // Example 12 = transform.Translate onUpdate
    // * Show - Red Box, Distance
    // ** Direction based on Magnitude
    // ** Change Y destination of end target
    // ** Set Y of heading as 0 so we move only X
    // Example 13 = transform.Translate onUpdate
    // * Show - Red Box, Distance
    // ** Direction based on Magnitude
    // ** Get starting box position on Trigger
    // ** Move for X Seconds to Detination
    // ** Calculate Speed based on Distance/Time
    // Example 14 = transform.position onUpdate
    // * Show - Red Box
    // ** Direction based on Vector3.right
    // ** MoveTowards ensures we never go beyond the target
    // Example 15 = transform.position onUpdate
    // * Show - Red Box
    // ** Direction based on Vector3.right
    // ** MoveTowards ensures we never go beyond the target
    // ** Vector3.Distance stop Timer
    // Example 16 = transform.position onUpdate
    // * Show - Red Box
    // ** Mathf.Lerp - returns float, change X value to move 
    // Example 17 = transform.position onUpdate
    // * Show - Red Box
    // ** Vector3.Lerp - returns Vector3, from target to target
    // Example 18 = transform.position onUpdate
    // * Show - Red Box
    // ** Vector3.Lerp - returns Vector3, from target to target
    // ** EaseIn - changes the time value
    // Example 19 = transform.position onUpdate
    // * Show - Red Box
    // ** Vector3.Lerp - returns Vector3, from target to target
    // ** EaseOut - changes the time value
    // Example 20 = transform.position onUpdate
    // * Show - Red Box
    // ** Vector3.Lerp - returns Vector3, from target to target
    // ** BounceOut - changes the time value
    // Example 21 = transform.Translate onUpdate
    // * Show - Red Box, Keys
    // ** MovementDirection
    // ** Doesn't Interact with objects
    // Example 22 = FixedUpdate
    // * Show - Red Box Physics, Keys
    // ** MovementDirection AddForce
    // ** Interacts with objects
    // Example 23 = FixedUpdate
    // * Show - Red Box Physics, Keys
    // ** MovementDirection Velocity
    // ** Interacts with objects
    // ** No accelaration we just add speed
    // Example 24 = FixedUpdate
    // * Show - Red Box Physics, Keys
    // ** MovementDirection MovePosition
    // ** Interacts with objects
    // ** Explanation - https://www.reddit.com/r/Unity3D/comments/ph75yy/rigidbody_moveposition_or_addforce/
    // Example 25 = FixedUpdate
    // * Show - Red Platform Physics, Keys
    // ** MovementDirection MovePosition for Platform, Kinetic platform
    // ** Velocity for Box
    // ** Interacts with objects
    // ** Explanation - https://www.reddit.com/r/Unity3D/comments/ph75yy/rigidbody_moveposition_or_addforce/

    // Directional Vectors
    // Vector3.right     // (1,  0,  0)
    // Vector3.left      // (-1, 0,  0)
    // Vector3.up        // (0,  1,  0)
    // Vector3.down      // (0, -1,  0)
    // Vector3.forward   // (0,  0,  1)
    // Vector3.back      // (0,  0, -1)

    public void SetExample(int value) {
        example = value;
        exampleValue.text = example.ToString();

        var physicsExamples_1 = new List<int> {22,23,24};
        var physicsExamples_2 = new List<int> {25};
        if (example >= 0 && example <= 21) { 
            ShowBox();
        } else if (physicsExamples_1.Contains(example)) {
            ShowPhysicsBox();
            ShowPhysicsGround();
            ShowRedBoxPhysics(2);
        } else if (physicsExamples_2.Contains(example)) {
            ShowPhysicsBox();
            ShowPhysicsGround();
            ShowRedPlatformPhysics(2);
        }
    }
    // Example 1
    // transform.position - set the position anywhere in the map
    // move x position
    // keep y position the same
    public void SetXPosition(float x) { 
        box.transform.position = new Vector3(x, box.transform.position.y);
    }
    // Example 2
    // change the position based on the current box position
    // add to the box x amount
    public void TranslateX(float x) { 
        box.transform.Translate(x,0,0);
    }
    private void Example1()  { 
        SetXPosition(2);
    }
    private void Example2()  { 
        TranslateX(2);
    }
    void Update() { 
        if (!start) {
            return;
        }
        // 0 - neutral
        // -1 to 1 values
        // Horizontal: Left = -1, Right = 1
        // Vertical: Bottom = -1, Top = 1
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        timeElapsed += Time.deltaTime;
        if (example == 3) {
            box.transform.position += new Vector3(2 * Time.deltaTime,0);   
        }
        if (example == 4) {
            box.transform.position += Vector3.right * 2 * Time.deltaTime;    
        }
        if (example == 5) {
            ShowRedBox(2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }
            box.transform.position += Vector3.right * 2 * Time.deltaTime;    
            ShowTimer();
        }
        if (example == 6) {
            ShowBoxFacingLine();
            RotateBox(10);
            ShowRedBox(2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }
            box.transform.position += Vector3.right * 2 * Time.deltaTime;  
            ShowTimer();  
        }
        if (example == 7) {
            ShowRedBox(2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }
            box.transform.Translate(Vector3.right * 2 * Time.deltaTime);
            ShowTimer();
        }
        if (example == 8) {
            ShowBoxFacingLine();
            RotateBox(10);
            ShowRedBox(2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }
            box.transform.Translate(Vector3.right * 2 * Time.deltaTime);
            ShowTimer();
        }
        if (example == 9) {
            ShowRedBox(2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }

            Vector3 heading = redBox.transform.position - box.transform.position;
            var distance = heading.magnitude;
            SetDistance(distance);

            box.transform.Translate(Vector3.right * 2 * Time.deltaTime);

            ShowTimer();
            ShowDistance();
        }
        if (example == 10) {
            ShowRedBox(2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }

            Vector3 heading = redBox.transform.position - box.transform.position;
            var distance = heading.magnitude;
            var direction = heading.normalized;
            box.transform.Translate(direction * 2 * Time.deltaTime);

            SetDistance(distance);

            ShowTimer();
            ShowDistance();
        }
        if (example == 11) {
            ShowRedBox(2, 2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }

            Vector3 heading = redBox.transform.position - box.transform.position;
            var distance = heading.magnitude;
            var direction = heading.normalized;
            box.transform.Translate(direction * 2 * Time.deltaTime);

            SetDistance(distance);

            ShowTimer();
            ShowDistance();
        }
        if (example == 12) {
            ShowRedBox(2, 2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }

            Vector3 heading = redBox.transform.position - box.transform.position;
            heading.y = 0;
            var distance = heading.magnitude;
            var direction = heading.normalized;
            box.transform.Translate(direction * 2 * Time.deltaTime);

            SetDistance(distance);

            ShowTimer();
            ShowDistance();
        }
        if (example == 13) {
            ShowRedBox(2);
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }

            Vector3 heading = redBox.transform.position - boxStartPosition;
            var distance = heading.magnitude;
            var direction = heading.normalized;
            var totalTime = 1.5f; // seconds
            var speed = distance / totalTime;
            box.transform.Translate(direction * speed * Time.deltaTime);

            SetDistance(distance);

            ShowTimer();
            ShowDistance();
        }
        if (example == 14) {
            ShowRedBox(2);

            var speed = 2; // seconds
            var step = speed * Time.deltaTime;
            var target = redBox.transform.position;
            box.transform.position = Vector2.MoveTowards(box.transform.position, target, step);

            ShowTimer();
        }
        if (example == 15) {
            ShowRedBox(2);

            var speed = 2; // seconds
            var step = speed * Time.deltaTime;
            var target = redBox.transform.position;
            box.transform.position = Vector2.MoveTowards(box.transform.position, target, step);
            
            // if (box.transform.position.x >= redBox.transform.position.x) {
            //     return;
            // }
            if (Vector3.Distance(box.transform.position, target) < 0.001f)
            {
                return;
            }

            ShowTimer();
        }
        if (example == 16) {
            ShowRedBox(2);

            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }

            var target = redBox.transform.position;
            var totalTime = 1.5f;
            var x = Mathf.Lerp(boxStartPosition.x, target.x, timeElapsed / totalTime);
            SetXPosition(x);
            
            ShowTimer();
        }
        if (example == 17) {
            ShowRedBox(2);

            var target = redBox.transform.position;
            var totalTime = 1.5f;
            box.transform.position = Vector3.Lerp(boxStartPosition, target, timeElapsed / totalTime);
            
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }

            ShowTimer();
        }
        if (example == 18) {
            ShowRedBox(2);

            var target = redBox.transform.position;
            var totalTime = 1.5f;
            var time = timeElapsed / totalTime;
            box.transform.position = Vector3.Lerp(boxStartPosition, target, EaseIn(time));
            
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }
            ShowTimer();
        }
        if (example == 19) {
            ShowRedBox(2);

            var target = redBox.transform.position;
            var totalTime = 1.5f;
            var time = timeElapsed / totalTime;
            box.transform.position = Vector3.Lerp(boxStartPosition, target, EaseOut(time));
            
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }
            ShowTimer();
        }
        if (example == 20) {
            ShowRedBox(2);

            var target = redBox.transform.position;
            var totalTime = 1.5f;
            var time = timeElapsed / totalTime;
            box.transform.position = Vector3.Lerp(boxStartPosition, target, BounceOut(time));
            
            if (box.transform.position.x >= redBox.transform.position.x) {
                return;
            }
            ShowTimer();
        }
        if (example == 21) {
            ShowRedBox(2);
            ShowKeys();
            
            box.transform.Translate(movementDirection * 2 * Time.deltaTime);
        }
    }
    void FixedUpdate() { 
        if (!start) {
            return;
        }

        if (example == 22) {
            ShowKeys();
            var speed = 10f;
            boxPhysicsRb.AddForce(movementDirection * speed);
        }
        if (example == 23) {
            ShowKeys();
            
            var speed = 10f;
            boxPhysicsRb.velocity = movementDirection * speed;
        }
        if (example == 24) {
            ShowKeys();
            
            var speed = 10f;
            boxPhysicsRb.MovePosition((Vector2)physicsBox.transform.position + (movementDirection * speed * Time.deltaTime));
        }
        if (example == 25) {
            ShowKeys();
            
            var speed = 2f;
            float time = Mathf.PingPong(timeElapsed*speed, 1);
            Vector3 startPosition = new Vector2(1,0);
            Vector3 endPosition = new Vector2(3,0);
            Vector3 position = Vector3.Lerp(startPosition, endPosition, time);
            physicsRedPlatformRb.MovePosition(position);
            
            var boxSpeed = 10f;
            boxPhysicsRb.velocity = movementDirection * boxSpeed;
       }
    }
    private void SetDistance(float dist) { 
        visualDistance = dist;
    }
    private void ShowDistance() { 
        distanceGroup.active = true;
        distanceText.gameObject.active = true;
        distanceText.text = visualDistance.ToString("f2");
    }
    // Show Timer
    private void ShowTimer() { 
        timerGroup.active = true;
        timerText.gameObject.active = true;
        visualTime = timeElapsed;
        timerText.text = visualTime.ToString("f2");
    }


    private void ShowBox() { 
        box.active = true;
    }
    private void ShowPhysicsBox() { 
        physicsBox.active = true;
    }
    private void ShowPhysicsGround() { 
        physicsGround.active = true;
    }
    private void ShowRedPlatformPhysics(float x) { 
        physicsRedPlatform.active = true;
        physicsRedPlatform.transform.position = new Vector2(x, physicsRedBox.transform.position.y);
    }
    private void ShowRedPlatformPhysics(float x, float y) { 
        physicsRedPlatform.active = true;
        physicsRedPlatform.transform.position = new Vector2(x, y);
    }
    private void ShowRedBoxPhysics(float x) { 
        physicsRedBox.active = true;
        physicsRedBox.transform.position = new Vector2(x, physicsRedBox.transform.position.y);
    }
    private void ShowRedBoxPhysics(float x, float y) { 
        physicsRedBox.active = true;
        physicsRedBox.transform.position = new Vector2(x, y);
    }
    // Show Destination
    private void ShowRedBox(float x) { 
        redBox.active = true;
        redBox.transform.position = new Vector2(x, redBox.transform.position.y);
    }
    // Show Destination
    private void ShowRedBox(float x, float y) { 
        redBox.active = true;
        redBox.transform.position = new Vector2(x, y);
    }
    // Rotate box
    private void RotateBox(float angle) { 
        box.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private void ShowBoxFacingLine() { 
        boxFacingLine.active = true;
    }
    
    private void HideFacingLine() {  
        boxFacingLine.active = false;
    }

    // Easing Functions: https://gist.github.com/Fonserbc/3d31a25e87fdaa541ddf
    private float EaseIn(float k) {
        return k*k*k;
    }
    private float EaseOut(float k) {
        return 1f + ((k -= 1f)*k*k);
    }
    private float BounceIn (float k) {
        return 1f - BounceOut(1f - k);
    }
    private float BounceOut (float k) {			
        if (k < (1f/2.75f)) {
            return 7.5625f*k*k;				
        }
        else if (k < (2f/2.75f)) {
            return 7.5625f*(k -= (1.5f/2.75f))*k + 0.75f;
        }
        else if (k < (2.5f/2.75f)) {
            return 7.5625f *(k -= (2.25f/2.75f))*k + 0.9375f;
        }
        else {
            return 7.5625f*(k -= (2.625f/2.75f))*k + 0.984375f;
        }
    }
    private float BounceInOut (float k) {
        if (k < 0.5f) return BounceIn(k*2f)*0.5f;
        return BounceOut(k*2f - 1f)*0.5f + 0.5f;
    }

    private void ShowKeys() { 
        keys.active = true;

        bool showTop = (movementDirection.y > 0);
        SetKeyColor(topKey, showTop);
        bool showBottom = (movementDirection.y < 0);
        SetKeyColor(bottomKey, showBottom);
        bool showLeft = (movementDirection.x < 0);
        SetKeyColor(leftKey, showLeft);
        bool showRight = (movementDirection.x > 0);
        SetKeyColor(rightKey, showRight);
    }
    private void SetKeyColor(GameObject keyImage, bool select) { 
        if (select) {
            keyImage.GetComponent<Image>().color = new Color32(152,187,108,255);
        } else {
            keyImage.GetComponent<Image>().color = new Color32(255,255,225,255);
        }

        
    }
}
