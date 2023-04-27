using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class generateGridScript : MonoBehaviour
{
    [SerializeField]
    Camera MyCamera;
    [SerializeField]
    InputField GridX;
    [SerializeField]
    InputField GridY;
    int ScreenX;
    int ScreenY;
    [SerializeField]
    GameObject UnitPrefab;
    [SerializeField]
    
    // Start is called before the first frame update
    void Start()
    {
        GridX.text = "10";
        GridY.text = "10";
        GridX.onValueChanged.AddListener(delegate{GridChangeCheck(GridX.text, GridY.text);});
        GridY.onValueChanged.AddListener(delegate{GridChangeCheck(GridX.text, GridY.text);});
        GridChangeCheck("10", "10");
    }

    // Update is called once per frame
    public void GridChangeCheck(string x, string y)
    {
         foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        if(GridX.text != "" || GridY.text != ""){
            int X = System.Convert.ToInt32(x.ToString());
            int Y = System.Convert.ToInt32(y.ToString());
            for(int i = 0; i < X; i++){
                for(int zed = 0; zed < Y; zed++){
                    GameObject Unit = Instantiate(UnitPrefab, new Vector3(UnitPrefab.GetComponent<RectTransform>().rect.width/9.9f * i, 0 ,UnitPrefab.GetComponent<RectTransform>().rect.width/9.9f * zed), Quaternion.identity);
                    Unit.transform.SetParent(gameObject.transform, false);
                    Unit.transform.GetComponent<GridPosition>().x = i;
                    Unit.transform.GetComponent<GridPosition>().y = zed;
                }
            }
            if(X*Y > 1900){
                MyCamera.transform.position = new Vector3(10*X, 1900, 10*Y);
            }else{
               MyCamera.transform.position = new Vector3(10*X, X*Y, 10*Y);
            }
        }
    }
}
