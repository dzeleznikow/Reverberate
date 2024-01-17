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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < instructions.Length; i++)
        {
            StartCoroutine(PlaySentence(i));
        }



        people = GameObject.FindGameObjectsWithTag("Person");

        for (int j = 0; j < people.Length; j++)
        {
            anims.Add(people[j].GetComponent<Animator>());
        }

        foreach (Animator anim in anims)
        {
            anim.SetInteger("Random", Random.Range(0, 3));
            anim.Play("Base", 0, Random.Range(0f, 1f));
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Animator anim in anims)
            {
                anim.SetBool("LayDown", true);
                anim.SetFloat("Offset", Random.Range(0f, 0.5f));
            }
        }
    }
}
