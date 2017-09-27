using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public Transform sphere;

    private void Update()
    {
        
    }

    [ContextMenu("test")]
    public void DoTweenTest()
    {
        sphere.transform.DOPunchScale(new Vector3(1f, 1f, 1f), 2f);
    }

}
