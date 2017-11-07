using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Go321 : MonoBehaviour
{
   public Text countDown;

   // Use this for initialization
   void Start()
   {
      StartCoroutine(Timer());
   }

   IEnumerator Timer()
   {

      yield return new WaitForSeconds(1);
      countDown.text = "3";
      yield return new WaitForSeconds(1);
      countDown.text = "2";
      yield return new WaitForSeconds(1);
      countDown.text = "1";
      yield return new WaitForSeconds(1);
      countDown.text = "GO!";
      yield return new WaitForSeconds(1);
      countDown.text = "";
   }

   // Update is called once per frame
   void Update()
   {

   }
}
