using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTutorial : MonoBehaviour
{
    public Transform[] allChildren;

    public int DisplayTime = 5;

    void Start()
    {
        allChildren = transform.GetComponentsInChildren<Transform>();
        StartCoroutine(HideTutorialCountdown());
    }

    IEnumerator HideTutorialCountdown()
    {
        yield return new WaitForSeconds(DisplayTime);
        Debug.Log("Hide Tutorial");
        for(int i = 0; i < allChildren.Length; i++)
        {
            if(i == 0) continue;
            allChildren[i].gameObject.SetActive(false);
        }
    }
}
