using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool down;

    bool firstNPC;

    bool secondNPC;

    AudioSource audioSource;

    [Header("Effects Related")]

    [SerializeField] ParticleSystem hellParticles;

    [SerializeField] ParticleSystem heavenParticles;

    [Header("NPC Related")]

    [SerializeField] NPC npcHolder;

    [SerializeField] GameObject[] npcsPrefabs;

    [SerializeField] GameObject[] specialNPCPrefabs;

    [Header("Hell Scores")]
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

    [SerializeField] AudioClip sentSomewhereSound;

    [SerializeField] AudioClip notificationSound;

    int wrathHellScore;

    int lustHellScore;

    int gluttonyHellScore;

    int greedHellScore;

    int slothHellScore;

    int envyHellScore;

    int prideHellScore;

    int randomNumber = -1;

    // Start is called before the first frame update
    void Start()
    {
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
        int previousNumber = randomNumber;

        while (previousNumber == randomNumber)
        {
            randomNumber = Random.Range(0, npcsPrefabs.Length);
        }

        NPCFather currentNPC = npcsPrefabs[randomNumber].GetComponent<NPCFather>();

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
        wrathHellScore += hellScore;

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        StartCoroutine(ScoreNotification(false, hellScore, "Ira"));

        StartCoroutine(SendNPCAway(true));
    }

    public void SendToLust(int hellScore)
    {
        lustHellScore += hellScore;

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        StartCoroutine(ScoreNotification(false, hellScore, "Luxúria"));

        StartCoroutine(SendNPCAway(true));
    }

    public void SendToGluttony(int hellScore)
    {
        gluttonyHellScore += hellScore;

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        StartCoroutine(ScoreNotification(false, hellScore, "Gula"));

        StartCoroutine(SendNPCAway(true));
    }

    public void SendToGreed(int hellScore)
    {
        greedHellScore += hellScore;

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        StartCoroutine(ScoreNotification(false, hellScore, "Ganância"));

        StartCoroutine(SendNPCAway(true));
    }

    public void SendToSloth(int hellScore)
    {
        slothHellScore += hellScore;

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        StartCoroutine(ScoreNotification(false, hellScore, "Preguiça"));

        StartCoroutine(SendNPCAway(true));
    }

    public void SendToEnvy(int hellScore)
    {
        envyHellScore += hellScore;

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        StartCoroutine(ScoreNotification(false, hellScore, "Inveja"));

        StartCoroutine(SendNPCAway(true));
    }

    public void SendToPride(int hellScore)
    {
        prideHellScore += hellScore;

        if (scoreDisplay.alpha != 0)
        {
            StopAllCoroutines();
        }

        StartCoroutine(ScoreNotification(false, hellScore, "Orgulho"));

        StartCoroutine(SendNPCAway(true));
    }

    IEnumerator SendNPCAway(bool isHell)
    {
        Camera.main.GetComponent<Animator>().Play("GoingBack");

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

        audioSource.PlayOneShot(sentSomewhereSound);

        yield return new WaitForSeconds(1.5f);

        ChangeNPC();

        Destroy(npcHolder.npcObject);

        npcHolder.StartCoroutine(npcHolder.StartDialogue());

        yield break;
    }

    IEnumerator ScoreNotification(bool multiplePoints, int hellScore, string hellName)
    {
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

        scoreDisplay.alpha = 1;

        yield return new WaitForSeconds(fadeDuraction);

        scoreDisplay.alpha = 0;

        yield break;
    }
}
