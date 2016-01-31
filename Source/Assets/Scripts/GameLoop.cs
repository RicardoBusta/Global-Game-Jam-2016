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

  public GameObject devilLike;
  public GameObject babeLike;

  public GameObject sign;

  public Transform fireworksLocation;

  public GameObject fireworkPrefab;

  public bool celebrated = false;

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

  float generateCooldown = 0;

  int[] sayCounter = new int[] {0,0};

  // Use this for initialization
  void Start()
  {
    levelDesign.LevelStage(this);

    babeLike.SetActive(false);
    devilLike.SetActive(false);

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
    if (PersistState.GetInstance().stage == 1)
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
      if (!celebrated)
      {
        StartCoroutine(FinishCelebration());
      }
      else
      {
        return;
      }
    }
    if (devilSlider.value <= 0 || babeSlider.value <= 0)
    {
      SceneManager.LoadScene("GameOver"); // Game Over
    }

    generateCooldown -= Time.deltaTime;

    if (generateCooldown <= 0)
    {
      if (babeGenerateWord)
      {
        sayCounter[1]++;
        babeText.text = GenerateWord(babeWordCount, babeBalloon);
        StartCoroutine(SayWord(babeText.text, 1, babeBalloon, false));
        //StartCoroutine(RepeatWord(babeText.text, 1, babeBalloon));
        babeGenerateWord = false;
        generateCooldown = babeWordCount;
      }
      else if(devilGenerateWord)
      {
        sayCounter[0]++;
        devilText.text = GenerateWord(devilWordCount, devilBalloon);
        StartCoroutine(SayWord(devilText.text, 0, devilBalloon, true));
        //StartCoroutine(RepeatWord(devilText.text, 0, devilBalloon));
        devilGenerateWord = false;
        generateCooldown = devilWordCount;
      }
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

    //StopCoroutine(SayWord(word, textToSet));

    return word;
  }

  public IEnumerator FinishCelebration()
  {
    celebrated = true;
    GameObject firework1 = (GameObject)Instantiate(fireworkPrefab, fireworksLocation.position + new Vector3(-2.5f, 0, 0), fireworkPrefab.transform.rotation);
    GameObject firework2 = (GameObject)Instantiate(fireworkPrefab, fireworksLocation.position + new Vector3(2.5f, 0, 0), fireworkPrefab.transform.rotation);
    yield return new WaitForSeconds(5);
    Destroy(firework1);
    Destroy(firework2);

    if (PersistState.GetInstance().stage < LevelDesign.num_stages)
    {
      SceneManager.LoadScene("Interlude"); // interlude
    }
    else
    {
      SceneManager.LoadScene("Victory"); // Victory
    }
  }

  public IEnumerator SayWord(string whole_word, int index, GameObject textToSet, bool isDevilTalking)
  {
    int thisCounter = sayCounter[index];
    while (thisCounter == sayCounter[index])
    {
      if ((devilIsHappy && index == 0) || (babeIsHappy && index == 1)) yield break;
      string[] words = whole_word.Split(' ');
      textToSet.SetActive(true);
      foreach (string w in words)
      {
        if(isDevilTalking)
          yield return new WaitForSeconds(SoundManager.GetInstance().PlayDevilWordSound(w) - 0.4f);
        else
          yield return new WaitForSeconds(SoundManager.GetInstance().PlayBabeWordSound(w) - 0.25f);
      }
      yield return new WaitForSeconds(waitTimeAfterSayWord);
      if (thisCounter == sayCounter[index])
      {
        textToSet.SetActive(false);
      }
      yield return new WaitForSeconds(3);
    }
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
        devilLike.SetActive(true);
      }
      else
      {
        devilGenerateWord = true;
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
        babeLike.SetActive(true);
      }
      else
      {
        babeGenerateWord = true;
      }
      return true;
    }
    return false;
  }
}
