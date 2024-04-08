using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFather : MonoBehaviour
{
    /*because there's 7 dialogues, the array will be 7, each of them corresponding to a different dialogue
    0 - First Dialogue 1 - Complimented Dialogue 2 - Educated Dialogue 3 - Neutral Dialogue 4 - Rude Dialogue 5 - Insult Dialogue 6 - Say Nothing 7 - Stressed Dialogue
    */
    [Header("Dialogues")][TextArea] public string[] dialogues = new string[8];

    public bool isGoodPerson;

    [Header("StressBarLevels")]
    public int complimentLevel;

    public int educatedLevel;

    public int neutralLevel;

    public int rudeLevel;

    public int insultLevel;

    public int ignoreLevel;

    [Header("HellLevels")]
    public int wrathLevel;

    public int lustLevel;

    public int gluttonyLevel;

    public int greedLevel;

    public int slothLevel;

    public int envyLevel;

    public int prideLevel;
}
