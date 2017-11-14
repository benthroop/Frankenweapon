using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class NewBehaviourScript : MonoBehaviour {
 
 
 
    public Text MyText;
    private int score;
 
 
    // Use this for initialization
    void Start () {
   
        MyText.text = "";
 
    }
 
 
    // Update is called once per frame
    void Update () {
   
        MyText.text = "" + score;
 
    }
 
 
    void OnTriggerEnter(Collider coll) {
 
        score = score + 1;
 
    }
 
 
}
 