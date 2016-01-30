using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    [Header("Prefab List")]
    public List<GameObject> ingredientsPrefabs;
    public List<GameObject> finishersPrefabs;
    [Header("Level Ingredients")]
    public List<GameObject> ingredients;
    public List<GameObject> finishers;

    [Header("References")]
    public Text devilText;
    public Text babeText;

    public Slider devilSlider;
    public Slider babeSlider;

    float devilValue = 100;
    float babeValue = 100;

    public bool devilGenerateWord = true;
    public bool babeGenerateWord = true;

    public float matchPoint = 50;

    public int devilWordCount = 3;
    public int babeWordCount = 3;

    int ingredientCount = 3;
    int finisherCount = 2;

    public BoxCollider tableCollider;

    // Use this for initialization
    void Start()
    {
        devilSlider.maxValue = 100;
        babeSlider.maxValue = 100;

        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        devilValue -= Time.deltaTime;
        babeValue -= Time.deltaTime;

        devilSlider.value = devilValue;
        babeSlider.value = babeValue;

        if (devilGenerateWord)
        {
            devilText.text = GenerateWord(devilWordCount);
            devilGenerateWord = false;
        }
        if (babeGenerateWord)
        {
            babeText.text = GenerateWord(babeWordCount);
            babeGenerateWord = false;
        }
    }

    string GenerateWord(int size)
    {
        string word = "";
        for (int i = 0; i < size; i++)
        {
            Item it = ingredients[Random.Range(0, ingredients.Count)].GetComponent<Item>();
            word += it.word;
        }
        Item fi = finishers[Random.Range(0, finishers.Count)].GetComponent<Item>();
        word += fi.word;

        return word;
    }

    void GenerateLevel()
    {
        for (int i = 0; i < ingredientCount; i++)
        {
            ingredients.Add(ingredientsPrefabs[i]);
        }
        for (int i = 0; i < finisherCount; i++)
        {
            finishers.Add(finishersPrefabs[i]);
        }

        float size = (ingredientCount + finisherCount) * 0.5f;
        float y = tableCollider.transform.position.y + tableCollider.size.y / 2 + 0.5f;
        float z = tableCollider.transform.position.z;
        for (int i = 0; i < ingredientCount + finisherCount; i++)
        {
            float t = ((float)(i) / (float)(ingredientCount + finisherCount - 1));
            Vector3 p = new Vector3( Mathf.Lerp(-size / 2, size / 2, t), y, z);
            Debug.Log("t" + t);
            if (i < ingredientCount)
            {
                Instantiate(ingredients[i], p, Quaternion.identity);
            }
            else
            {
                Instantiate(finishers[i-ingredientCount], p, Quaternion.identity);
            }


        }
    }

    public void CheckFinished(string value)
    {
        Debug.Log("Finished:" + value);
        if (value == devilText.text)
        {
            Debug.Log("Worked!");
            devilSlider.value += matchPoint;
            devilText.text = GenerateWord(devilWordCount);
            return;
        }
        if (value == babeText.text)
        {
            Debug.Log("Worked!");
            babeSlider.value += matchPoint;
            babeText.text = GenerateWord(babeWordCount);
            return;
        }
    }
}
