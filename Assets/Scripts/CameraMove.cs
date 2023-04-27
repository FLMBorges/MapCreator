using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    public GameObject SelectedObject;
    public GameObject Scan;
    public Vector2 turn;
    public CharacterController controller;

    public Text ModeText;

    [SerializeField]
    InputField GridX;
    [SerializeField]
    InputField GridY;

    int Mode;

    // Start is called before the first frame update
    void Start()
    {
        Mode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F1)){
            Mode = 0;
        }else if(Input.GetKey(KeyCode.F2)){
            Mode = 1;
        }
        int X = System.Convert.ToInt32(GridX.text.ToString());
        int Y = System.Convert.ToInt32(GridY.text.ToString());
        // Move mode: rotation and camera control
        if(Mode == 0){
            ModeText.text = "Move";
            if(Input.GetKey(KeyCode.Mouse0)){
                transform.RotateAround(new Vector3(50*X, 0, 50*Y),Vector3.up,1);
            }else if(Input.GetKey(KeyCode.Mouse1)){
                transform.RotateAround(new Vector3(50*X, 0, 50*Y),Vector3.up,-1);
            }else if(Input.GetKey(KeyCode.Mouse2)){
                transform.position = new Vector3(10*X, X*Y, 10*Y);
                transform.LookAt(new Vector3(10*X, 0, 10*Y));
            }
            if(Input.GetKey(KeyCode.W)){
                controller.Move(transform.forward);
            }
            if(Input.GetKey(KeyCode.S)){
                controller.Move(transform.forward * -1);
            }
            if(Input.GetKey(KeyCode.A)){
                controller.Move(transform.right * -1);
            }
            if(Input.GetKey(KeyCode.D)){
                controller.Move(transform.right);
            }
            if(Input.GetAxis("Mouse ScrollWheel") > 0f){
                transform.Rotate(new Vector3(1000,0,0) * Time.deltaTime);
            }else if(Input.GetAxis("Mouse ScrollWheel") < 0f){
                transform.Rotate(new Vector3(-1000,0,0) * Time.deltaTime);
            }
        //Create Mode: placing objects
        }else if(Mode == 1){
            ModeText.text = "Create";
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                int HitX = hitInfo.transform.gameObject.GetComponent<GridPosition>().x;
                int HitY = hitInfo.transform.gameObject.GetComponent<GridPosition>().y;
                GameObject activeUnit = hitInfo.transform.gameObject;
                activeUnit.GetComponent<Renderer>().material.color = Color.cyan;
                foreach(Transform child in Scan.transform){
                    int ChildX = child.gameObject.GetComponent<GridPosition>().x;
                    int ChildY = child.gameObject.GetComponent<GridPosition>().y;
                    if(HitX != ChildX || HitY != ChildY){
                        child.gameObject.GetComponent<Renderer>().material.color = Color.white;
                    }
                }
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                    GameObject SceneObject = Instantiate(SelectedObject, new Vector3(0, 0, 0), SelectedObject.transform.rotation * Quaternion.Euler(0f, 180f,0f));
                    SceneObject.transform.localScale = new Vector3(1,1,1);
                    SceneObject.transform.SetParent(hitInfo.transform, false);
                }
            }else{
                foreach(Transform child in Scan.transform){
                    child.gameObject.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
    }
}


