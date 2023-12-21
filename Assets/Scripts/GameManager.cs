using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMesh subtitle;
    public Instructions[] instructions;

    AudioSource audioSource;
    float fadeDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < instructions.Length; i++)
        {
            StartCoroutine(PlaySentence(i));
        }
    }

    IEnumerator PlaySentence(int index)
    {
        yield return new WaitForSeconds(instructions[index].timeToStart);
        audioSource.clip = instructions[index].audio;

        audioSource.Play();
        subtitle.text = instructions[index].sentence;

        //Fade Audio & Text In
        float currentTime = 0f;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(audioSource.volume, 1f, currentTime / fadeDuration);
            subtitle.color = Color.Lerp(subtitle.color, Color.white, currentTime / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(audioSource.clip.length);

        //Fade Audio & Text Out
        currentTime = 0f;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, currentTime / fadeDuration);
            subtitle.color = Color.Lerp(subtitle.color, new Color(1f,1f,1f,0f), currentTime / fadeDuration);
            yield return null;
        }

        subtitle.text = "";
    }
}
