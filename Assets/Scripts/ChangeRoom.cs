using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 cameraChagePos;
    public Vector3 playerChangePos;
    public Camera cameraPlayer;
    public Image fadeImage;
    public float fadingTime = 2f;
    private bool isFading = false;
    
    private void Start() 
    {
        cameraPlayer = Camera.main;
        fadeImage.gameObject.SetActive(false);
    }
    
    public void Change()
    {
        StartCoroutine(FadeScreen());
    }

    private IEnumerator FadeScreen()
    {
        fadeImage.gameObject.SetActive(true);
        isFading = true;
        yield return new WaitForSeconds(0.5f);

        float t = 0f;
        while (t < fadingTime)
        {
            t += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(t / fadingTime);
            fadeImage.color = new Color(0f, 0f, 0f, normalizedTime);
            yield return null;
        }

        GameManager.Instance.GetSelectedPlayer().gameObject.transform.position += playerChangePos;
        cameraPlayer.transform.position += cameraChagePos;

        t = 0f;
        while (t < fadingTime)
        {
            t += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(t / fadingTime);
            fadeImage.color = new Color(0f, 0f, 0f, 1.5f - normalizedTime);
            yield return null;
        }

        isFading = false;
        fadeImage.gameObject.SetActive(false);
    }
}



