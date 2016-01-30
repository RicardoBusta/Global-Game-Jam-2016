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

    public bool devilGenerateWord = true;
    public bool babeGenerateWord = true;

    public float matchPoint = 50;

    public int devilWordCount = 3;
    public int babeWordCount = 3;

    int ingredientCount = 3;
    int finisherCount = 2;

    bool devilIsHappy = false;
    bool babeIsHappy = false;

    public BoxCollider tableCollider;

    // Use this for initialization
    void Start()
    {
        devilSlider.maxValue = 100;
        babeSlider.maxValue = 100;
        devilSlider.value = 20;
        babeSlider.value = 20;

        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!devilIsHappy)
        {
            devilSlider.value -= Time.deltaTime;
        }
        if (!babeIsHappy)
        {
            babeSlider.value -= Time.deltaTime;
        }
        if (devilIsHappy && babeIsHappy)
        {
            Application.LoadLevel("GameOver");
        }
        if (devilSlider.value <= 0 || babeSlider.value <= 0)
        {
            Application.LoadLevel("GameOver");
        }

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
            word += it.word + "-";
        }
        Item fi = finishers[Random.Range(0, finishers.Count)].GetComponent<Item>();
        word += fi.word;

        return word;
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int t = Random.Range(i, list.Count - 1);
            int temp = list[i];
            list[i] = list[t];
            list[t] = temp;
        }
    }

    void GenerateLevel()
    {
        List<int> sort = new List<int>();
        for (int i = 0; i < ingredientsPrefabs.Count; i++)
        {
            sort.Add(i);
        }
        Shuffle(sort);
        for (int i = 0; i < ingredientCount; i++)
        {
            ingredients.Add(ingredientsPrefabs[sort[i]]);
        }
        sort.Clear();
        for (int i = 0; i < finishersPrefabs.Count; i++)
        {
            sort.Add(i);
        }
        Shuffle(sort);
        for (int i = 0; i < finisherCount; i++)
        {
            finishers.Add(finishersPrefabs[sort[i]]);
        }

        float size = (ingredientCount + finisherCount) * 0.5f;
        float y = tableCollider.transform.position.y + tableCollider.size.y / 2 + 0.15f;
        float z = tableCollider.transform.position.z;
        for (int i = 0; i < ingredientCount + finisherCount; i++)
        {
            float t = ((float)(i) / (float)(ingredientCount + finisherCount - 1));
            Vector3 p = new Vector3(Mathf.Lerp(-size / 2, size / 2, t), y, z + Random.Range(-0.2f, 0.2f));
            Debug.Log("t" + t);
            if (i < ingredientCount)
            {
                GameObject go = (GameObject)Instantiate(ingredients[i], p, Quaternion.identity);
                go.GetComponent<Item>().prefab = ingredients[i];
            }
            else
            {
                GameObject go = (GameObject)Instantiate(finishers[i - ingredientCount], p, Quaternion.identity);
                go.GetComponent<Item>().prefab = finishers[i - ingredientCount];
            }


        }
    }

    public void CheckFinished(string value)
    {
        Debug.Log("Finished:" + value);
        if (!devilIsHappy && value == devilText.text)
        {
            Debug.Log("Worked!");
            devilSlider.value += matchPoint;
            if (devilSlider.value >= devilSlider.maxValue)
            {
                devilText.text = "I am very happy!";
                devilIsHappy = true;
            }
            else
            {
                devilText.text = GenerateWord(devilWordCount);
            }
            return;
        }
        if (!babeIsHappy && value == babeText.text)
        {
            Debug.Log("Worked!");
            babeSlider.value += matchPoint;
            if (babeSlider.value >= babeSlider.maxValue)
            {
                babeText.text = "Oh yea, honey!";
                babeIsHappy = true;
            }
            else
            {
                babeText.text = GenerateWord(babeWordCount);
            }
            return;
        }
    }
}
