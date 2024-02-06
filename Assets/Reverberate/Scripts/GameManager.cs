using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMesh subtitle;
    public Instructions[] instructions;

    AudioSource audioSource;
    float fadeDuration = 0.5f;

    GameObject[] people;
    public List<Animator> anims;

    public Light light;
    float colTimer = 5f;
    float t = 0;

    //public GameObject fog;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < instructions.Length; i++)
        {
            StartCoroutine(PlaySentence(i));
        }



        people = GameObject.FindGameObjectsWithTag("Person"); //Get the people in the scene

        for (int j = 0; j < people.Length; j++)
        {
            anims.Add(people[j].GetComponent<Animator>()); //Get each person's animator controller
        }

        foreach (Animator anim in anims) //For every animator controller...
        {
            anim.SetInteger("Random", Random.Range(0, 3)); //...give them a random pose of the 3
            anim.Play("Base", 0, Random.Range(0f, 1f)); //...play the animation
        }

        Invoke("BeginExperience", 32f);
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

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    BeginExperience();
        //}
    }

    //private IEnumerator ChangeColor()
    //{
    //    Color endColor = Random.ColorHSV(0.5f, 0.8f, 0.25f, 0.3f, 0.9f, 1f);

    //    float tick = 0f;
    //    while (light.color != endColor)
    //    {
    //        tick += Time.deltaTime * 0.05f;
    //        light.color = Color.Lerp(light.color, endColor, tick);

    //        light.intensity = Mathf.Lerp(light.intensity, 50, tick);
    //        RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, 40, tick);
    //        yield return null;
    //    }

    //    StartCoroutine(ChangeColor());
    //}

    void BeginExperience()
    {
        //StartCoroutine(ChangeColor());

        foreach (Animator anim in anims) //For each person's animator controller...
        {
            anim.SetBool("LayDown", true); //...make them lay down
            anim.SetFloat("Offset", Random.Range(0f, 1f)); //...offset the animation for a more natural effect
        }

        //fog.SetActive(true);
    }
}
