using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 cameraChagePos;
    public Vector3 playerChangePos;
    public Camera cameraPlayer;
    public float fadingTime = 0.5f;
    private bool isFading = false;

    private void Start() {
        cameraPlayer = Camera.main;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<PlayerController>(out PlayerController playerController) && !isFading)
        {
            StartCoroutine(FadeScreen(playerController));
        }
    }

      private IEnumerator FadeScreen(PlayerController playerController)
    {
        isFading = true;
        yield return new WaitForSeconds(1f);

        float t = 0f;
        while (t < fadingTime)
        {
            t += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(t / fadingTime);
            RenderSettings.ambientIntensity = Mathf.Lerp(1f, 0f, normalizedTime);
            yield return null;
        }

        playerController.gameObject.transform.position += playerChangePos;
        cameraPlayer.transform.position += cameraChagePos;

        t = 0f;
        while (t < fadingTime)
        {
            t += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(t / fadingTime);
            RenderSettings.ambientIntensity = Mathf.Lerp(0f, 1f, normalizedTime);
            yield return null;
        }
        isFading = false;
    }
}
