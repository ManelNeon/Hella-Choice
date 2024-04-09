using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public bool canPlayerSend;

    AudioSource audioSource;

    [Header("NPC Related")]

    [SerializeField] NPC npcHolder;

    [SerializeField] GameObject[] npcsPrefabs;

    [SerializeField] GameObject[] specialNPCPrefabs;

    [Header("Tablet Related")]
    [SerializeField] GameObject tablet;

    [SerializeField] GameObject hellPart1;

    [SerializeField] GameObject hellPart2;

    [Header("Hell Scores")]
    [SerializeField] TextMeshProUGUI wrathScoreText;

    [SerializeField] TextMeshProUGUI lustScoreText;

    [SerializeField] TextMeshProUGUI gluttonyScoreText;

    [SerializeField] TextMeshProUGUI greedScoreText;

    [SerializeField] TextMeshProUGUI slothScoreText;

    [SerializeField] TextMeshProUGUI envyScoreText;

    [SerializeField] TextMeshProUGUI prideScoreText;

    [Header("Audio Related")]
    [SerializeField] AudioClip notificationSound;

    int wrathHellScore;

    int lustHellScore;

    int gluttonyHellScore;

    int greedHellScore;

    int slothHellScore;

    int envyHellScore;

    int prideHellScore;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        audioSource = GetComponent<AudioSource>();

        ChangeNPC();

        npcHolder.StartDialogue();
    }

    void ChangeNPC()
    {
        int randomNumber = Random.Range(0, npcsPrefabs.Length);

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
    }

    public void TabletOpenAndClose()
    {
        if (tablet.activeInHierarchy)
        {
            tablet.SetActive(false);
            hellPart1.SetActive(true);
            hellPart2.SetActive(false);
        }
        else
        {
            tablet.SetActive(true);
        }
    }

    public void OpenHellChoices()
    {
        hellPart1.SetActive(false);

        wrathScoreText.text = wrathHellScore.ToString();

        lustScoreText.text = lustHellScore.ToString();

        gluttonyScoreText.text = gluttonyHellScore.ToString();

        greedScoreText.text = greedHellScore.ToString();

        slothScoreText.text = slothHellScore.ToString();

        envyScoreText.text = envyHellScore.ToString();

        prideScoreText.text = prideHellScore.ToString();

        hellPart2.SetActive(true);
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
            wrathHellScore -= 1;

            lustHellScore -= 1;

            gluttonyHellScore -= 1;

            greedHellScore -= 1;

            slothHellScore -= 1;

            envyHellScore -= 1;

            prideHellScore -= 1;
        }

        StartCoroutine(SendNPCAway());

    }

    public void SendToWrath(int hellScore)
    {
        wrathHellScore += hellScore;

        StartCoroutine(SendNPCAway());

    }

    public void SendToLust(int hellScore)
    {
        lustHellScore+= hellScore;

        StartCoroutine(SendNPCAway());

    }

    public void SendToGluttony(int hellScore)
    {
        gluttonyHellScore+= hellScore;

        StartCoroutine(SendNPCAway());

    }

    public void SendToGreed(int hellScore)
    {
        greedHellScore+= hellScore;

        StartCoroutine(SendNPCAway());

    }

    public void SendToSloth(int hellScore)
    {
        slothHellScore+= hellScore;

        StartCoroutine(SendNPCAway());

    }

    public void SendToEnvy(int hellScore)
    {
        envyHellScore += hellScore;

        StartCoroutine(SendNPCAway());

    }

    public void SendToPride(int hellScore)
    {
        prideHellScore+= hellScore;

        StartCoroutine(SendNPCAway());
    }

    IEnumerator SendNPCAway()
    {
        npcHolder.TakeButtonsPurpose();

        audioSource.PlayOneShot(notificationSound);

        canPlayerSend = false;

        TabletOpenAndClose();

        //Play Animation Here, then we open the tablet again, we show the player how hell has changed and we send the new NPC

        yield return new WaitForSeconds(1.5f);

        TabletOpenAndClose();

        OpenHellChoices();

        yield return new WaitForSeconds(1.5f);

        TabletOpenAndClose();

        canPlayerSend = true;

        ChangeNPC();

        npcHolder.StartDialogue();

        yield break;
    }
}
