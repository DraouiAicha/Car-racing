using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Transform cameraTransform;
    private float x, z, rotation, speedSensitivity=0.03f, rotationSensitivity = 1f;
    private bool isClicked=false;
    private int accelerate=0,i=0;
    public GameObject road_boxes, previousRoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }
//-15.04 -0.9958811 11.54175       -24.03  
    // Update is called once per frame
    void Update()
    {
        x=Input.GetAxis("Horizontal");
        z=Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Rotation");
        transform.Translate(new Vector3(x*speedSensitivity, 0f, z*speedSensitivity));
        transform.Rotate(new Vector3(0f, rotation * rotationSensitivity, 0f));
        Vector3 cameraPos = transform.position - transform.forward * 3f + Vector3.up * 2f;
        cameraTransform.position = cameraPos;
        cameraTransform.LookAt(transform.position);
    }
    void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            isClicked=true;
            accelerate=1;
        }
        if(isClicked) {
            if(accelerate==1) {
                transform.Translate(new Vector3(x*speedSensitivity, 0f, z*speedSensitivity*accelerate*200));
            }
            transform.Translate(new Vector3(0f, 0f, speedSensitivity));
            accelerate=0;
        }
    }
    
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("newPlan")) {
            Debug.Log("ipiiiiiiiiiin");
            Instantiate(road_boxes,new Vector3(-6+(-5*i),-0.9958811f,11.54175f), Quaternion.identity);
            i++;
            collision.gameObject.SetActive(false);
            previousRoad = GameObject.FindGameObjectWithTag("road_boxes");
            if (previousRoad != null)
            {
                Destroy(previousRoad, 5f);
            }
        }
        if (collision.gameObject.CompareTag("LeftBox")) {
            Debug.Log("Collided with left roadside");
            GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, 200f));
            transform.Rotate(new Vector3(0f, 5f, 0f));
        }
        if (collision.gameObject.CompareTag("RightBox")) {
            Debug.Log("Collided with right roadside");
            GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, -200f));
            transform.Rotate(new Vector3(0f, -5f, 0f));
        }
    }
}
