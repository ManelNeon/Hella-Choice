using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject firstTutorialUI;

    [SerializeField] GameObject secondTutorialUI;

    [SerializeField] GameObject downButton;

    [SerializeField] GameObject upButton;

    [HideInInspector] public bool down;

    bool firstNPC;

    bool secondNPC;

    AudioSource audioSource;

    [Header("Effects Related")]

    [SerializeField] ParticleSystem hellParticles;

    [SerializeField] ParticleSystem heavenParticles;

    [Header("NPC Related")]

    [SerializeField] NPC npcHolder;

    [SerializeField] GameObject firstNPCPrefab;

    [SerializeField] GameObject secondNPCPrefab;

    [SerializeField] GameObject[] npcsPrefabs;

    [SerializeField] List<GameObject> specialNPCPrefabs;

    [Header("Hell Scores")]

    [SerializeField] GameObject heavenButton;

    [SerializeField] TextMeshProUGUI wrathScoreText;

    [SerializeField] TextMeshProUGUI lustScoreText;

    [SerializeField] TextMeshProUGUI gluttonyScoreText;

    [SerializeField] TextMeshProUGUI greedScoreText;

    [SerializeField] TextMeshProUGUI slothScoreText;

    [SerializeField] TextMeshProUGUI envyScoreText;

    [SerializeField] TextMeshProUGUI prideScoreText;

    [SerializeField] CanvasGroup scoreDisplay;

    [SerializeField] TextMeshProUGUI scoreDisplayText;

    [SerializeField] float fadeDuraction;

    [Header("Audio Related")]

    [SerializeField] AudioClip notificationSound;

    [SerializeField] AudioClip clickSound;

    int wrathHellScore;

    int lustHellScore;

    int gluttonyHellScore;

    int greedHellScore;

    int slothHellScore;

    int envyHellScore;

    int prideHellScore;

    int randomNumber = -1;

    public void OpenTablet()
    {
        down = true;

        Camera.main.GetComponent<Animator>().Play("GoingDown");

        downButton.SetActive(false);

        upButton.SetActive(true);
    }

    public void CloseTablet()
    {
        down = false;

        Camera.main.GetComponent<Animator>().Play("GoingBack");

        downButton.SetActive(true);

        upButton.SetActive(false);
    }

    public void CloseTutorial(bool isOne)
    {
        if (isOne)
        {
            firstTutorialUI.SetActive(false);
        }
        else
        {
            secondTutorialUI.SetActive(false);
        }

        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        heavenButton.SetActive(false);

        firstNPC = true;

        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        audioSource = GetComponent<AudioSource>();

        ChangeNPC();

        npcHolder.StartCoroutine(npcHolder.StartDialogue());

        scoreDisplay.alpha = 0;
    }

    void ChangeNPC()
    {
        NPCFather currentNPC = firstNPCPrefab.GetComponent<NPCFather>();

        if (!firstNPC && !secondNPC)
        {
            int isSpecial = 0;

            if (specialNPCPrefabs.Count != 0)
            {
                isSpecial = Random.Range(1,101);
            }

            if (isSpecial < 70)
            {
                int previousNumber = randomNumber;

                while (previousNumber == randomNumber)
                {
                    randomNumber = Random.Range(0, npcsPrefabs.Length);
                }

                currentNPC = npcsPrefabs[randomNumber].GetComponent<NPCFather>();
            }
            else
            {
                randomNumber = Random.Range(0, specialNPCPrefabs.Count + 1);

                currentNPC = specialNPCPrefabs[randomNumber].GetComponent<NPCFather>();

                specialNPCPrefabs.Remove(specialNPCPrefabs[randomNumber]);
            }
        }
        else if (firstNPC)
        {
            currentNPC = firstNPCPrefab.GetComponent<NPCFather>();

            firstTutorialUI.SetActive(true);

            Time.timeScale = 0;
        }
        else if (secondNPC)
        {
            currentNPC = secondNPCPrefab.GetComponent<NPCFather>();

            OpenTablet();

            down = true;

            downButton.SetActive(false);

            upButton.SetActive(true);

            secondTutorialUI.SetActive(true);

            heavenButton.SetActive(true);

            Time.timeScale = 0;
        }

        npcHolder.isGoodPerson = currentNPC.isGoodPerson;

        npcHolder.dialogues = currentNPC.dialogues;

        npcHolder.complimentLevel = currentNPC.complimentLevel;

        npcHolder.educatedLevel = currentNPC.educatedLevel;

        npcHolder.neutralLevel = currentNPC.neutralLevel;

        npcHolder.rudeLevel = currentNPC.rudeLevel;

        npcHolder.insultLevel = currentNPC.insultLevel;

        npcHolder.ignoreLevel = currentNPC.ignoreLevel;

        npcHolder.wrathLevel = currentNPC.wrathLevel;

        npcHolder.lustLevel = currentNPC.lustLevel;

        npcHolder.gluttonyLevel = currentNPC.gluttonyLevel;

        npcHolder.greedLevel = currentNPC.greedLevel;

        npcHolder.slothLevel = currentNPC.slothLevel;

        npcHolder.envyLevel = currentNPC.envyLevel;

        npcHolder.prideLevel = currentNPC.prideLevel;

        npcHolder.characterName = currentNPC.characterName;

        npcHolder.characterModel = currentNPC.characterModel;
    }

    public void ChangeHellScore()
    {
        wrathScoreText.text = wrathHellScore.ToString();

        lustScoreText.text = lustHellScore.ToString();

        gluttonyScoreText.text = gluttonyHellScore.ToString();

        greedScoreText.text = greedHellScore.ToString();

        slothScoreText.text = slothHellScore.ToString();

        envyScoreText.text = envyHellScore.ToString();

        prideScoreText.text = prideHellScore.ToString();
    }

    public void SendToHeaven(bool isGoodPerson, int hellScore)
    {
        if (isGoodPerson)
        {
            wrathHellScore += hellScore;

            lustHellScore += hellScore;

            gluttonyHellScore += hellScore;

            greedHellScore += hellScore;

            slothHellScore += hellScore;

            envyHellScore += hellScore;

            prideHellScore += hellScore;
        }
        else
        {
            hellScore = -1;

            wrathHellScore -= 1;

            lustHellScore -= 1;

            gluttonyHellScore -= 1;

            greedHellScore -= 1;

            slothHellScore -= 1;

            envyHellScore -= 1;

            prideHellScore -= 1;
        }

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        StartCoroutine(ScoreNotification(true, hellScore, ""));

        StartCoroutine(SendNPCAway(false));
    }

    public void SendToWrath(int hellScore)
    {
        if (npcHolder.isGoodPerson)
        {
            hellScore = -hellScore;
        }

        wrathHellScore += hellScore;

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        if (LanguageManager.Instance.isPortuguese)
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Ira"));
        }
        else
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Wrath"));
        }

        StartCoroutine(SendNPCAway(true));
    }

    public void SendToLust(int hellScore)
    {
        if (npcHolder.isGoodPerson)
        {
            hellScore = -hellScore;
        }

        lustHellScore += hellScore;
        
        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        if (LanguageManager.Instance.isPortuguese)
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Luxúria"));
        }
        else
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Lust"));
        }

        StartCoroutine(SendNPCAway(true));
    }

    public void SendToGluttony(int hellScore)
    {
        if (npcHolder.isGoodPerson)
        {
            hellScore = -hellScore;
        }

        gluttonyHellScore += hellScore;

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        if (LanguageManager.Instance.isPortuguese)
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Gula"));
        }
        else
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Gluttony"));
        }

        StartCoroutine(SendNPCAway(true));
    }

    public void SendToGreed(int hellScore)
    {
        if (npcHolder.isGoodPerson)
        {
            hellScore = -hellScore;
        }

        greedHellScore += hellScore;

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        if (LanguageManager.Instance.isPortuguese)
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Ganância"));
        }
        else
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Greed"));
        }

        StartCoroutine(SendNPCAway(true));
    }

    public void SendToSloth(int hellScore)
    {
        if (npcHolder.isGoodPerson)
        {
            hellScore = -hellScore;
        }

        slothHellScore += hellScore;

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        if (LanguageManager.Instance.isPortuguese)
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Preguiça"));
        }
        else
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Sloth"));
        }

        StartCoroutine(SendNPCAway(true));
    }

    public void SendToEnvy(int hellScore)
    {
        if (npcHolder.isGoodPerson)
        {
            hellScore = -hellScore;
        }

        envyHellScore += hellScore;

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        if (LanguageManager.Instance.isPortuguese)
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Inveja"));
        }
        else
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Envy"));
        }

        StartCoroutine(SendNPCAway(true));
    }

    public void SendToPride(int hellScore)
    {
        if (npcHolder.isGoodPerson)
        {
            hellScore = -hellScore;
        }

        prideHellScore += hellScore;

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        if (LanguageManager.Instance.isPortuguese)
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Orgulho"));
        }
        else
        {
            StartCoroutine(ScoreNotification(false, hellScore, "Pride"));
        }

        StartCoroutine(SendNPCAway(true));
    }

    IEnumerator SendNPCAway(bool isHell)
    {
        if (firstNPC)
        {
            firstNPC = false;
            secondNPC = true;
        }
        else if (secondNPC)
        {
            secondNPC = false;
        }


        audioSource.PlayOneShot(clickSound);

        CloseTablet();

        ChangeHellScore();

        down = false;

        yield return new WaitForSeconds(.7f);

        npcHolder.TakeButtonsPurpose();

        if (isHell)
        {
            npcHolder.npcAnimator.Play("GoingLeft");
        }
        else
        {
            npcHolder.npcAnimator.Play("GoingRight");
        }

        yield return new WaitForSeconds(1.5f);

        if (isHell)
        {
            hellParticles.Play();
        }
        else
        {
            heavenParticles.Play();
        }

        yield return new WaitForSeconds(1.5f);

        ChangeNPC();

        Destroy(npcHolder.npcObject);

        npcHolder.StartCoroutine(npcHolder.StartDialogue());

        yield break;
    }

    IEnumerator ScoreNotification(bool multiplePoints, int hellScore, string hellName)
    {
        yield return new WaitForSeconds(2.2f);

        audioSource.PlayOneShot(notificationSound);

        bool doesGivePoint;

        if (hellScore <= 0)
        {
            doesGivePoint = false;
        }
        else
        {
            doesGivePoint = true;
        }

        if (LanguageManager.Instance.isPortuguese)
        {
            if (!multiplePoints)
            {
                if (doesGivePoint)
                {
                    scoreDisplayText.text = " + " + hellScore + " em " + hellName;
                }
                if (!doesGivePoint)
                {
                    scoreDisplayText.text = " " + hellScore + " em " + hellName;
                }
            }
            if (multiplePoints)
            {
                if (doesGivePoint)
                {
                    scoreDisplayText.text = " + " + hellScore + " em todos os infernos!";
                }
                if (!doesGivePoint)
                {
                    scoreDisplayText.text = " " + hellScore + " em todos os infernos...";
                }
            }
        }
        else
        {
            if (!multiplePoints)
            {
                if (doesGivePoint)
                {
                    scoreDisplayText.text = " + " + hellScore + " in " + hellName;
                }
                if (!doesGivePoint)
                {
                    scoreDisplayText.text = " " + hellScore + " in " + hellName;
                }
            }
            if (multiplePoints)
            {
                if (doesGivePoint)
                {
                    scoreDisplayText.text = " + " + hellScore + " in all Circles of Hell!";
                }
                if (!doesGivePoint)
                {
                    scoreDisplayText.text = " " + hellScore + " in all Circles of Hell...";
                }
            }
        }
        

        scoreDisplay.alpha = 1;

        yield return new WaitForSeconds(fadeDuraction);

        scoreDisplay.alpha = 0;

        yield break;
    }
}
