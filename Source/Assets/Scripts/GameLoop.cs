using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public GameObject devilBalloon;
    public GameObject babeBalloon;

    public Slider devilSlider;
    public Slider babeSlider;

    public GameObject devilObject;
    public GameObject babeObject;

    public GameObject sign;

    public Transform fireworksLocation;

    public GameObject fireworkPrefab;

    [HideInInspector]
    public bool devilGenerateWord;
    [HideInInspector]
    public bool babeGenerateWord;

    //    public float devilMaxSlider;
    //    public float babeMaxSlider;

    [HideInInspector]
    public float matchPoint;

    [HideInInspector]
    public int devilWordCount;
    [HideInInspector]
    public int babeWordCount;

    public float waitTimeAfterSayWord = 1;

    [HideInInspector]
    public int ingredientCount;
    [HideInInspector]
    public int finisherCount;

    bool devilIsHappy = false;
    bool babeIsHappy = false;

    public LevelDesign levelDesign;

    public BoxCollider tableCollider;

    // Use this for initialization
    void Start()
    {
        levelDesign.LevelStage(this);

        if (!devilGenerateWord)
        {
            devilIsHappy = true;
            devilObject.SetActive(false);
            babeObject.transform.position += new Vector3(-babeObject.transform.position.x, 0f, 0f);
            devilSlider.value = devilSlider.maxValue;
            devilSlider.gameObject.SetActive(false);
        }
        if (!babeGenerateWord)
        {
            babeIsHappy = true;
            babeObject.SetActive(false);
            devilObject.transform.position += new Vector3(-devilObject.transform.position.x, 0f, 0f);
            babeSlider.value = babeSlider.maxValue;
            babeSlider.gameObject.SetActive(false);
        }

        devilBalloon.SetActive(false);
        babeBalloon.SetActive(false);

        //        devilSlider.maxValue = devilMaxSlider;
        //        babeSlider.maxValue = babeMaxSlider;
        //
        //        if (!devilGenerateWord)
        //        {
        //            devilIsHappy = true;
        //            devilSlider.value = devilMaxSlider; 
        //        }else {
        //            devilSlider.value = 0.3f * devilMaxSlider;
        //        }
        //        
        //        babeSlider.value = 0.3f * babeMaxSlider;

        GenerateLevel();
        if (PersistState.GetPersistState().stage == 1)
            SoundManager.GetInstance().loopPlayer.StartPlaying();
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
            StartCoroutine(FinishCelebration());
            if (PersistState.GetPersistState().stage < LevelDesign.num_stages)
            {
                SceneManager.LoadScene("Interlude"); // interlude
            }
            else
            {
                SceneManager.LoadScene("Victory"); // Victory
            }
        }
        if (devilSlider.value <= 0 || babeSlider.value <= 0)
        {
            SceneManager.LoadScene("GameOver"); // Game Over
        }

        if (devilGenerateWord)
        {
            devilText.text = GenerateWord(devilWordCount, devilBalloon);
            devilGenerateWord = false;
        }
        if (babeGenerateWord)
        {
            babeText.text = GenerateWord(babeWordCount, babeBalloon);
            babeGenerateWord = false;
        }
    }

    string GenerateWord(int size, GameObject textToSet)
    {
        string word = "";
        for (int i = 0; i < size; i++)
        {
            Item it = ingredients[Random.Range(0, ingredients.Count)].GetComponent<Item>();
            word += it.word + " ";
            //soundManager.PlayWordSound(it.word);
        }
        Item fi = finishers[Random.Range(0, finishers.Count)].GetComponent<Item>();
        word += fi.word;

        StartCoroutine(SayWord(word, textToSet));

        return word;
    }

    public IEnumerator FinishCelebration()
    {
        GameObject firework = (GameObject)Instantiate(fireworkPrefab, fireworksLocation.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(firework);
    }

    public IEnumerator SayWord(string whole_word, GameObject textToSet)
    {
        string[] words = whole_word.Split(' ');
        textToSet.SetActive(true);
        foreach (string w in words)
        {
            yield return new WaitForSeconds(SoundManager.GetInstance().PlayDevilWordSound(w) - 0.4f);
        }
        yield return new WaitForSeconds(waitTimeAfterSayWord);
        textToSet.SetActive(false);
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int t = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[t];
            list[t] = temp;
        }
    }

    void GenerateLevel()
    {
        List<int> sort = new List<int>();
        List<GameObject> allItems = new List<GameObject>();

        for (int i = 0; i < ingredientsPrefabs.Count; i++)
        {
            sort.Add(i);
        }

        Shuffle<int>(sort);
        for (int i = 0; i < ingredientCount; i++)
        {
            ingredients.Add(ingredientsPrefabs[sort[i]]);
            allItems.Add(ingredientsPrefabs[sort[i]]);
        }
        sort.Clear();
        for (int i = 0; i < finishersPrefabs.Count; i++)
        {
            sort.Add(i);
        }

        Shuffle<int>(sort);
        for (int i = 0; i < finisherCount; i++)
        {
            finishers.Add(finishersPrefabs[sort[i]]);
            allItems.Add(finishersPrefabs[sort[i]]);
        }
        //Shuffle<GameObject>(allItems);
        float size = (ingredientCount + finisherCount) * 0.5f;
        float y = tableCollider.transform.position.y + tableCollider.size.y / 2 + 0.25f;
        float z = tableCollider.transform.position.z;
        for (int i = 0; i < allItems.Count; i++)
        {
            float t = ((float)(i) / (float)(ingredientCount + finisherCount - 1));
            Vector3 p = new Vector3(Mathf.Lerp(-size / 2, size / 2, t), y, z + Random.Range(-0.1f, 0.1f));
            GameObject go = (GameObject)Instantiate(allItems[i], p, allItems[i].transform.rotation);
            go.GetComponent<Item>().prefab = allItems[i];
            go.transform.localScale = allItems[i].transform.localScale;
            GameObject sgo = (GameObject)Instantiate(sign, p + new Vector3(0, 0, -0.6f), sign.transform.rotation);
            sgo.GetComponentInChildren<TextMesh>().text = go.GetComponent<Item>().word;
        }
    }

    public bool CheckFinished(string value)
    {
        Debug.Log("Finished:" + value);
        if (!devilIsHappy && value == devilText.text)
        {
            Debug.Log("Worked! Devil is happy.");
            devilSlider.value += matchPoint;
            if (devilSlider.value >= devilSlider.maxValue)
            {
                devilText.text = "I am very happy!";
                devilIsHappy = true;
            }
            else
            {
                devilText.text = GenerateWord(devilWordCount, devilBalloon);
            }
            return true;
        }
        if (!babeIsHappy && value == babeText.text)
        {
            Debug.Log("Worked! Babe is happy.");
            babeSlider.value += matchPoint;
            if (babeSlider.value >= babeSlider.maxValue)
            {
                babeText.text = "Oh yea, honey!";
                babeIsHappy = true;
            }
            else
            {
                babeText.text = GenerateWord(babeWordCount, babeBalloon);
            }
            return true;
        }
        return false;
    }
}
