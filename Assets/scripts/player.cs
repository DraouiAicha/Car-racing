using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private float x, z, speedSensitivity=0.03f;
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
        transform.Translate(new Vector3(x*speedSensitivity, 0f, z*speedSensitivity));
    }
    void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            isClicked=true;
            //accelerate=1;
        }
        if(isClicked) {
            /*if(accelerate==1) {
                transform.Translate(new Vector3(x*speedSensitivity, 0f, z*speedSensitivity*accelerate));
            }*/
            transform.Translate(new Vector3(0f, 0f, speedSensitivity));
            //accelerate=0;
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
                Destroy(previousRoad, 0.5f);
            }
        }
    }
}
