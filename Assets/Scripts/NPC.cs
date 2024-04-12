using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : NPCFather
{
    [Header("UI Layouts")]
    //the text mesh where we will put our dialogue
    [SerializeField] TextMeshProUGUI displayText;

    [SerializeField] TextMeshProUGUI nameDisplayText;

    [Header("Text Speed")][SerializeField] float textSpeed;

    [Header("Button Layouts")]
    [SerializeField] GameObject buttonsHolder;

    [SerializeField] Button buttonCompliment;

    [SerializeField] Button buttonEducated;

    [SerializeField] Button buttonNeutral;

    [SerializeField] Button buttonRude;

    [SerializeField] Button buttonInsult;

    [SerializeField] Button buttonIgnore;

    [SerializeField] Button buttonHellWrath;

    [SerializeField] Button buttonHellLust;

    [SerializeField] Button buttonHellGluttony;

    [SerializeField] Button buttonHellGreed;

    [SerializeField] Button buttonHellSloth;

    [SerializeField] Button buttonHellEnvy;

    [SerializeField] Button buttonHellPride;

    [SerializeField] Button buttonHeaven;

    [SerializeField] Button buttonRandom;

    [Header("Sounds")]
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip bellSound;

    [SerializeField] AudioClip talkingSound;

    int index;

    int stress;

    Vector3 originalPosition = new Vector3(3.73999f, -5f, 0.43f);

    [HideInInspector] public GameObject npcObject;

    [HideInInspector] public Animator npcAnimator;


    private void Start()
    {
        buttonsHolder.SetActive(false);
    }

    public void Update()
    {
        //if the player clicks on the left mouse
        if (Input.GetMouseButtonDown(0) && Time.timeScale == 1)
        {
            //if the dialogue text is the same as the one on the UI
            if (displayText.text != dialogues[index])
            {
                audioSource.Stop();
                //we stop all coroutines (the writing of the text)
                StopAllCoroutines();

                buttonsHolder.SetActive(true);

                //and set the dialogue to be the same (one more click on the screen and the next line will play
                displayText.text = dialogues[index];
            }
        }
    }

    // this is for when we start the dialogue
    public IEnumerator StartDialogue()
    {
        stress = 0;

        buttonsHolder.SetActive(false);

        buttonCompliment.gameObject.SetActive(true);

        buttonEducated.gameObject.SetActive(true);

        buttonNeutral.gameObject.SetActive(true);

        buttonRude.gameObject.SetActive(true);

        buttonInsult.gameObject.SetActive(true);

        buttonIgnore.gameObject.SetActive(true);

        TakeButtonsPurpose();

        GiveButtonsPurpose();

        npcObject = Instantiate(characterModel, originalPosition, characterModel.transform.rotation);

        npcAnimator = npcObject.GetComponent<Animator>();

        nameDisplayText.text = characterName;

        //we put the display text to nothing
        displayText.text = "";

        npcAnimator.Play("GoingUp");

        yield return new WaitForSeconds(.1f);

        audioSource.Stop();

        audioSource.PlayOneShot(bellSound);

        yield return new WaitForSeconds(1);

        audioSource.Stop();

        //and we start the corotine for the writing of the text
        NextLine(0);

        yield break;
    }

    //we write the text
    IEnumerator TypeLine()
    {
        foreach (char c in dialogues[index].ToCharArray())
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(talkingSound);
            }
            displayText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        audioSource.Stop();

        buttonsHolder.SetActive(true);

        yield break;
    }

    //we get the next line, if its still less than the dialogue length we change the sprite again, add 1 to the index and start the coroutine again
    void NextLine(int code)
    {
        index = code;
        displayText.text = "";
        StartCoroutine(TypeLine());
    }

    void GiveButtonsPurpose()
    {
        buttonCompliment.onClick.AddListener(ComplimentButton);

        buttonEducated.onClick.AddListener(EducatedButton);

        buttonNeutral.onClick.AddListener(NeutralButton);

        buttonRude.onClick.AddListener(RudeButton);

        buttonInsult.onClick.AddListener(InsultButton);

        buttonIgnore.onClick.AddListener(IgnoreButton);

        buttonHeaven.onClick.AddListener(SendToHeaven);

        buttonHellWrath.onClick.AddListener(SendToWrath);

        buttonHellLust.onClick.AddListener(SendToLust);

        buttonHellGluttony.onClick.AddListener(SendToGluttony);

        buttonHellGreed.onClick.AddListener(SendToGreed);

        buttonHellSloth.onClick.AddListener(SendToSloth);

        buttonHellEnvy.onClick.AddListener(SendToEnvy);

        buttonHellPride.onClick.AddListener(SendToPride);

        buttonRandom.onClick.AddListener(SendToRandom);
    }

    public void TakeButtonsPurpose()
    {
        buttonCompliment.onClick.RemoveAllListeners();

        buttonEducated.onClick.RemoveAllListeners();

        buttonNeutral.onClick.RemoveAllListeners();

        buttonRude.onClick.RemoveAllListeners();

        buttonInsult.onClick.RemoveAllListeners();

        buttonIgnore.onClick.RemoveAllListeners();

        buttonHeaven.onClick.RemoveAllListeners();

        buttonHellWrath.onClick.RemoveAllListeners();

        buttonHellLust.onClick.RemoveAllListeners();

        buttonHellGluttony.onClick.RemoveAllListeners();

        buttonHellGreed.onClick.RemoveAllListeners();

        buttonHellSloth.onClick.RemoveAllListeners();

        buttonHellEnvy.onClick.RemoveAllListeners();

        buttonHellPride.onClick.RemoveAllListeners();

        buttonRandom.onClick.RemoveAllListeners();
    }

    void ComplimentButton()
    {
        if (stress < 3)
        {
            buttonCompliment.gameObject.SetActive(false);

            buttonsHolder.SetActive(false);

            stress += complimentLevel;

            NextLine(1);
        }
        else
        {
            NextLine(7);
        }
    }

    void EducatedButton()
    {
        if (stress < 3)
        {
            buttonEducated.gameObject.SetActive(false);

            buttonsHolder.SetActive(false);

            stress += educatedLevel;

            NextLine(2);
        }
        else
        {
            NextLine(7);
        }
    }

    void NeutralButton()
    {
        if (stress < 3)
        {
            buttonNeutral.gameObject.SetActive(false);

            buttonsHolder.SetActive(false);

            stress += neutralLevel;

            NextLine(3);
        }
        else
        {
            NextLine(7);
        }
    }

    void RudeButton()
    {
        if (stress < 3)
        {
            buttonRude.gameObject.SetActive(false);

            buttonsHolder.SetActive(false);

            stress += rudeLevel;

            NextLine(4);
        }
        else
        {
            NextLine(7);
        }
    }

    void InsultButton()
    {
        if (stress < 3)
        {
            buttonInsult.gameObject.SetActive(false);

            buttonsHolder.SetActive(false);

            stress += insultLevel;

            NextLine(5);

        }
        else
        {
            NextLine(7);
        }
    }

    void IgnoreButton()
    {
        if (stress < 3)
        {
            buttonIgnore.gameObject.SetActive(false);

            buttonsHolder.SetActive(false);

            stress += ignoreLevel;

            NextLine(6);
        }
        else
        {
            NextLine(7);
        }
    }

    void SendToHeaven()
    {
        TakeButtonsPurpose();

        GameManager.Instance.SendToHeaven(isGoodPerson, wrathLevel);
    }

    void SendToWrath()
    {
        TakeButtonsPurpose();

        GameManager.Instance.SendToWrath(wrathLevel);
    }

    void SendToLust()
    {
        TakeButtonsPurpose();

        GameManager.Instance.SendToLust(lustLevel);
    }

    void SendToGluttony()
    {
        TakeButtonsPurpose();

        GameManager.Instance.SendToGluttony(gluttonyLevel);
    }

    void SendToGreed()
    {
        TakeButtonsPurpose();

        GameManager.Instance.SendToGreed(greedLevel);
    }

    void SendToSloth()
    {
        TakeButtonsPurpose();

        GameManager.Instance.SendToSloth(slothLevel);
    }

    void SendToEnvy()
    {
        TakeButtonsPurpose();

        GameManager.Instance.SendToEnvy(envyLevel);
    }

    void SendToPride()
    {
        TakeButtonsPurpose();

        GameManager.Instance.SendToPride(prideLevel);
    }

    void SendToRandom()
    {
        int randomNumber = Random.Range(1, 8);

        switch(randomNumber)
        {
            case 1:
                SendToWrath();
                break;

            case 2:
                SendToLust();
                break;

            case 3:
                SendToGluttony();
                break;

            case 4:
                SendToGreed();
                break;

            case 5:
                SendToSloth();
                break;

            case 6:
                SendToEnvy();
                break;

            case 7:
                SendToPride();
                break;

            default:
                break;
        }
    }
}

