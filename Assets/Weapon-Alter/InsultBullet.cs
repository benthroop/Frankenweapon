using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InsultBullet : MonoBehaviour
{
    public float lifetime = 4f;
    public float TimeBeforeDeath ;
    public GameObject InsultVisual;

    string InsultText;

    public UnityEvent Sound0;
    public UnityEvent Sound1;
    public UnityEvent Sound2;
    public UnityEvent Sound3;
    public UnityEvent Sound4;
    public UnityEvent Sound5;
    public UnityEvent Sound6;
    public UnityEvent Sound7;


    int myIndex;


     List<string> Phrases = new List<string>{   "Thine face is not worth sunburning!", "Thou art as fat as butter!", "Villain, I have done thy mother!", "I’ll beat thee, but I would infect my hands!", "Away, you three inch fool!", "If you spend word for word with me, I shall make your wit bankrupt!", "Dissembling harlot, thou art false in all!",  "Away, you mouldy rogue, away!" };

    void Start()
    {





        myIndex = Random.Range(0, Phrases.Count);
        //Debug.Log(myIndex);


        InsultText = Phrases[myIndex] ;

        PlaySound();


        //Debug.Log(InsultText);
        Destroy(gameObject,lifetime);
        InsultVisual.GetComponent<TextMesh>().text = InsultText;
        InsultVisual.AddComponent<BoxCollider>();
       var ColliderZ = InsultVisual.GetComponent<BoxCollider>();
        ColliderZ.size = new Vector3(ColliderZ.size.x-0.1f, ColliderZ.size.y-0.1f, ColliderZ.size.z + 1f);


        GameObject.Find("Shakespeare(Clone)").GetComponent<ShakespeareGun>().CanFire = false;



    }

    void Update()
    {
        Timer();
        if (TimeBeforeDeath <=1)
        {
            GameObject.Find("Shakespeare(Clone)").GetComponent<ShakespeareGun>().CanFire = true;
            //Debug.Log("Reset");
            Destroy(this);
        }
    }
   

    void Timer()
    {
        TimeBeforeDeath-= Time.deltaTime;
       // Debug.Log(TimeBeforeDeath);
    }


    void PlaySound()
    {
        if (myIndex == 0)
        {
            Sound0.Invoke();
        }
        else if (myIndex == 1)
        {
            Sound1.Invoke();
        }
        else if (myIndex == 2)
        {
            Sound4.Invoke();
        }
        else if (myIndex == 3)
        {
            Sound5.Invoke();
        }
        else  if (myIndex == 4)
        {
            Sound7.Invoke();
        }
        else if (myIndex == 5)
        {
            Sound6.Invoke();
        }
        else if (myIndex == 6)
        {
            Sound2.Invoke();
        }
        else if (myIndex == 7)
        {
            Sound3.Invoke();
        }

    }

    void OnCollisionEnter(Collision col)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        
        //Destroy(InsultVisual);
    }



}
