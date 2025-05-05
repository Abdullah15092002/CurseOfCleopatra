using Assets.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] sections;
    public Transform player;
    public float deletionDistance = 200f;
    public float sectionLength = 180.6824f;
    public int maxSections = 10;

    public float maxSpeed = 25f;
    public static float moveSpeed = 12f;

    private float speedIncreaseAmount = 0.75f;
    private float speedIncreaseInterval = 25f;
    private bool hasStartedSpeedIncrease = false;
    private int currentSectionIndex;
    private List<GameObject> generatedSections = new List<GameObject>();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GenerateInitialSections();
    }

    private void Update()
    {
       
        if (GameManager.Instance.isGroundMove)
        {
            MoveSections();
            DeleteSectionsBehindPlayer();
            if (generatedSections.Count < maxSections || generatedSections[generatedSections.Count - 1].transform.position.z < player.position.z + deletionDistance)
            {
                GenerateSection();
            }
        }
    }
    void GenerateInitialSections()
    {
        Debug.Log("Oluştu.");
        GameObject initialSection = Instantiate(sections[0], Vector3.zero, Quaternion.identity);
        generatedSections.Add(initialSection);

        for (int i = 1; i < maxSections; i++)
        {
            GenerateSection();
        }
    }

    void GenerateSection()
    {
        int randomIndex = UnityEngine.Random.Range(0, sections.Length);
        Vector3 position = Vector3.zero;

        if (generatedSections.Count > 0)
        {
            position = generatedSections[generatedSections.Count - 1].transform.position + new Vector3(0, 0, sectionLength);
        }

        GameObject newSection = Instantiate(sections[randomIndex], position, Quaternion.identity);
        generatedSections.Add(newSection);
        currentSectionIndex++;
    }
    void MoveSections()
    {
        float delta = moveSpeed * Time.deltaTime;//Skor Artışı burada hesaplanıyor
        EventManager.OnScoreIncreased?.Invoke(delta);
        foreach (GameObject section in generatedSections)
        {
            section.transform.position -= new Vector3(0, 0, moveSpeed* Time.deltaTime);
        }
    }
    
    void DeleteSectionsBehindPlayer()
    {
        for (int i = 0; i < generatedSections.Count; i++)
        {
            if (generatedSections[i].transform.position.z < 0 - deletionDistance)
            {
                Destroy(generatedSections[i]);
                generatedSections.RemoveAt(i);
                i--;
            }
        }
    }

    IEnumerator IncreaseSpeedRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncreaseInterval);
            moveSpeed+= speedIncreaseAmount;

            if (moveSpeed> maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
    }
}
