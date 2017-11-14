using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HiFiveCounter : MonoBehaviour
{
    public Text MyText;
    private int score;
    public ParticleSystem Bam;

    void Start()
    {

        MyText.text = ("");

    }
    void Update()
    {

        MyText.text = "" + score;

    }

    void OnTriggerEnter(Collider coll)
    {

        score = score + 1;

    }


}
