using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HideTutorial : MonoBehaviour
{
    public Transform[] allChildren;
    [SerializeField] float countdownStartTime = 5f;
    private float currentTime = 0f;
    [SerializeField] private TextMeshProUGUI countdownText;

    public int TutorialDisplayTime = 5;

    void Start()
    {
        //Time.timeScale = 0;
        //allChildren = transform.GetComponentsInChildren<Transform>();
        StartCoroutine(HideTutorialCountdown());
        currentTime = countdownStartTime;
        StartCoroutine(CountdownToStart());
    }

    void Update()
    {
        // if(currentTime > 0f)
        // {
        //     currentTime -= 1 * Time.deltaTime;
        //     countdownText.text = currentTime.ToString("0");
        // }
    }

    IEnumerator HideTutorialCountdown()
    {
        yield return new WaitForSeconds(TutorialDisplayTime);
        //Debug.Log("Hide Tutorial");
        for(int i = 0; i < allChildren.Length; i++)
        {
            //if(i == 0) continue;
            allChildren[i].gameObject.SetActive(false);
        }
    }

    IEnumerator CountdownToStart()
    {
        while(currentTime > 0f)
        {
            countdownText.text = currentTime.ToString();
            //Debug.Log("Update timer");

            yield return new WaitForSeconds(1f);

            currentTime--;
        }

        countdownText.text = "FIGHT!";

        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
        //Time.timeScale = 1;
        PlayerController.GameStart = true;
    }
}
