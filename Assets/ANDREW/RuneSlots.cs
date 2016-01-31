using UnityEngine;
using System.Collections;

public class RuneSlots : MonoBehaviour {

    public const int MAX_INGREDIENTS = 12;
    public const int MAX_CANDLES = 6;
    public const int MAX_OBELISKS = 4;

    private const int WHITE = 3;
    private const int RED = 2;
    private const int GREEN = 1;
    private const int BLUE = 0;

    private const int SHIELD = 0;
    private const int BANISH = 1;
    private const int ATTACK = 2;

    private const int SPHERE = 0;
    private const int CUBE = 1;

    private const int CLOCKWISE = 0;
    private const int CCLOCKWISE = 1;
    private const int VARIED = 2;

    //Circle attributes
    public int numPoints;
    public int numRings;
    public bool[] inscribedRings;
    public int colorID;

    //Pre Activation
    public static RuneSlots S;
    public int[] runeIds = new int[9];
    private int[] correctRunes = new int[9];
    public int activeRune = 0;
    public Sprite[] runeTextures = new Sprite[8];

    public bool circleActive = false;

    //Post Activation
    public int focalSymbol = 0;
    public int[] ingredientIds = new int[MAX_INGREDIENTS];
    public bool[] obeliskFaceTowards = new bool[MAX_OBELISKS];
    public int numObelisks = 0;
    public int numCandles = 0;
    public int numIngredients = 0;
    public bool[] ringClockwise;
    public int particleShape; //0 - sphere, 1 - cube

    public int openMenu = 0;


    void Awake()
    {
        S = this;
    }

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 9; i++)
            runeIds[i] = -1;

        do
        {
            numPoints = 3 + Random.Range(0, 8);
        } while (numPoints == 7);
        numRings = 2 + Random.Range(0, 2);
        inscribedRings = new bool[numRings];
        ringClockwise = new bool[numRings];
        for (int i = 0; i < inscribedRings.Length; i++)
            inscribedRings[i] = flipCoin();
        postRandomize();
        colorID = Random.Range(0, 4);

        CalcCorrectRunes();
    }
	
	// Update is called once per frame
	void Update () {
	    if (!circleActive)
        {
            //Check to see if rune combination is the correct one.
            if (VerifyRunes())
                circleActive = true;
        }
        else
        {

        }
	}

    public void setActiveRune(int rune)
    {
        runeIds[activeRune] = rune;
        nextActiveRune();
    }

    public void setOpenMenu(int menu)
    {
        openMenu = menu;
    }

    public void prevActiveRune()
    {
        activeRune -= 1;
        if (activeRune < 0)
            activeRune = runeIds.Length - 1;
    }

    public void nextActiveRune()
    {
        activeRune = (activeRune + 1) % runeIds.Length;
    }

    //Returns whether activation was a success (fails if ritual is not configured correctly!)
    public bool activateRitual(int id, string incantation)
    {
        bool doublePotency = false; //Ritual should be twice as strong.

        //Check Incantation
        if (!VerifyIncantation(id, incantation)) {
            postMessage("Invalid Incantation.");
            return false;
        }

        if (id == SHIELD)
        {
            int focalPenalty = 0;
            if (focalSymbol <= 3) //Circle symbol
                focalPenalty = -1;

            //Check Candles
            if ((numCandles == 3+focalPenalty && colorID == BLUE)
                || (numCandles == 2+focalPenalty && colorID == GREEN)
                || (numCandles == 1 + focalPenalty && colorID == RED)
                || (colorID == WHITE)) { 
                //candles okay!
            } else
            {
                postMessage("Invalid Conjuration Circle Configuration.");
                return false;
            }

            //Check Obelisks
            if ((numObelisks == 2+focalPenalty) || (numObelisks == 3+focalPenalty && focalSymbol == 8))
            {
                for (int i = 0; i < numObelisks; i++)
                {
                    if (!obeliskFaceTowards[i])
                    {
                        postMessage("Invalid Conjuration Circle Configuration.");
                        return false;
                    }
                }
                //obelisks okay!
                if (numObelisks == 3 + focalPenalty && focalSymbol == 8)
                    doublePotency = true;
            } else
            {
                postMessage("Invalid Conjuration Circle Configuration.");
                return false;
            }

            //Check Ingredients
            int feathersRequired = 2;
            if (particleShape == SPHERE)
                feathersRequired -= 1;
            int ginsengRequired = 1;
            if (RotationType() == VARIED)
                ginsengRequired += 1;

            int featherCount = 0;
            int ginsengCount = 0;
            int eggCount = 0;
            int otherCount = 0;
            for(int i=0; i<numIngredients; i++)
            {
                if (ingredientIds[i] == 0) ginsengCount++;
                else if (ingredientIds[i] == 4) featherCount++;
                else if (ingredientIds[i] == 5) eggCount++;
                else otherCount++;
            }

            if (otherCount == 0 && featherCount == feathersRequired && ginsengCount == ginsengRequired
                && (eggCount == 1 || (eggCount == 2 && focalSymbol == 8))) {
                //ingredients okay!
                if (eggCount == 1)
                    doublePotency = false;
            } else
            {
                postMessage("Invalid Ingredients.");
                return false;
            }

            //TODO: Perform shielding ritual!
            return true;
        }
        else if (id == BANISH)
        {
            int focalPenalty = 0;
            int rotation = RotationType();
            if (rotation == CLOCKWISE)
                focalPenalty -= 1;
            else if (rotation == CCLOCKWISE)
                focalPenalty += 1;

            //Check Candles
            if (((numCandles == 1 + focalPenalty || (focalSymbol == 7 && numCandles == 1 + focalPenalty + 1)) && colorID == BLUE)
                || ((numCandles == 2 + focalPenalty || (focalSymbol == 7 && numCandles == 2 + focalPenalty + 1)) && colorID == GREEN)
                || ((numCandles == 3 + focalPenalty || (focalSymbol == 7 && numCandles == 3 + focalPenalty + 1)) && colorID == RED)
                || (colorID == WHITE))
            {
                //candles okay!
                if (((focalSymbol == 7 && numCandles == 1 + focalPenalty + 1) && colorID == BLUE)
                || ((focalSymbol == 7 && numCandles == 2 + focalPenalty + 1) && colorID == GREEN)
                || ((focalSymbol == 7 && numCandles == 3 + focalPenalty + 1) && colorID == RED)
                || (focalSymbol == 7 && numCandles == 1 && colorID == WHITE))
                    doublePotency = true;
            }
            else
            {
                postMessage("Invalid Conjuration Circle Configuration.");
                return false;
            }

            //Check Obelisks
            if (numObelisks != 0)
            {
                postMessage("Invalid Conjuration Circle Configuration.");
                return false;
            }

            //Check Ingredients
            int feathersRequired = 2;
            if (particleShape == SPHERE)
                feathersRequired += 1;
            int legsRequired = 2;
            if (numCandles % 2 == 0)
                legsRequired -= 1;
            else
                legsRequired += 1;

            int featherCount = 0;
            int legsCount = 0;
            int otherCount = 0;
            for (int i = 0; i < numIngredients; i++)
            {
                if (ingredientIds[i] == 1) legsCount++;
                else if (ingredientIds[i] == 4) featherCount++;
                else otherCount++;
            }

            if (otherCount == 0 && legsCount == legsRequired
                && (featherCount == feathersRequired || (featherCount == feathersRequired+2 && focalSymbol == 7))) {
                //ingredients okay!
                if (featherCount == feathersRequired)
                    doublePotency = false;
            }
            else
            {
                postMessage("Invalid Ingredients.");
                return false;
            }

            //TODO: Perform banishing ritual!
            return true;
        }
        else if (id == ATTACK)
        {
            //Check Ingredients
            int rotation = RotationType();
            int leavesRequired = 3;
            if (colorID == GREEN)
                leavesRequired += 1;
            else if (colorID == RED)
                leavesRequired -= 1;
            if (rotation == CLOCKWISE)
                leavesRequired += 1;
            int bloodRequired = 1;
            if (colorID == RED)
                bloodRequired += 1;
            if (rotation == CCLOCKWISE)
                bloodRequired += 1;
            if (particleShape == CUBE)
                bloodRequired += 1;

            int leafCount = 0;
            int bloodCount = 0;
            int otherCount = 0;
            for (int i = 0; i < numIngredients; i++)
            {
                if (ingredientIds[i] == 2) leafCount++;
                else if (ingredientIds[i] == 3) bloodCount++;
                else otherCount++;
            }

            if (otherCount == 0 && leafCount == leavesRequired
                && (bloodCount == bloodRequired || (bloodCount == bloodRequired+2 && focalSymbol == 5)))
            {
                //ingredients okay!
                if (bloodCount == bloodRequired + 2)
                    doublePotency = true;
            }
            else
            {
                postMessage("Invalid Ingredients.");
                return false;
            }

            int focalPenalty = 0;
            if (leafCount % 2 == 0)
                focalPenalty = 1;

            //Check Candles
            if (numCandles == 0 + focalPenalty)
            {
                //candles okay!
            }
            else
            {
                postMessage("Invalid Conjuration Circle Configuration.");
                return false;
            }

            //Check Obelisks
            if ((numObelisks == 2 - focalPenalty) || (numObelisks == 3 - focalPenalty && focalSymbol == 5))
            {
                for (int i = 0; i < numObelisks; i++)
                {
                    if (obeliskFaceTowards[i])
                    {
                        postMessage("Invalid Conjuration Circle Configuration.");
                        return false;
                    }
                }
                //obelisks okay!
                if (numObelisks == 2 - focalPenalty)
                    doublePotency = false;
            }
            else
            {
                postMessage("Invalid Conjuration Circle Configuration.");
                return false;
            }

            //TODO: Perform attack ritual!
            return true;
        }
        return false;
    }

    public void postMessage(string msg)
    {
        //TODO??
    }

    public void postRandomize()
    {
        focalSymbol = Random.Range(0, 9);
        colorID = Random.Range(0, 3);
        particleShape = Random.Range(0, 2);
        int spinStyle = Random.Range(0, 3);
        for (int i = 0; i < ringClockwise.Length; i++)
        {
            if (spinStyle == 0)
                ringClockwise[i] = false;
            else if (spinStyle == 1)
                ringClockwise[i] = true;
            else
                ringClockwise[i] = flipCoin();
        }
    }

    private bool VerifyRunes()
    {
        for(int i=0;i<runeIds.Length;i++)
        {
            if (runeIds[i] != correctRunes[i])
                return false;
        }
        return true;
    }

    private int RotationType()
    {
        bool allClockwise = true;
        bool allCClockwise = true;
        for(int i=0;i<ringClockwise.Length;i++)
        {
            if (ringClockwise[i] == false)
                allClockwise = false;
            else
                allCClockwise = false;
        }

        if (allClockwise) return CLOCKWISE;
        else if (allCClockwise) return CCLOCKWISE;
        else return VARIED;
    }

    private void CalcCorrectRunes()
    {
        if (colorID == WHITE) {
            correctRunes[0] = 7;
            correctRunes[1] = 2;
            correctRunes[2] = 5;
        }
        else if (colorID == RED)
        {
            correctRunes[0] = 6;
            correctRunes[1] = 3;
            correctRunes[2] = 7;
        }
        else if (colorID == GREEN)
        {
            correctRunes[0] = 0;
            correctRunes[1] = 4;
            correctRunes[2] = 3;
        }
        else
        {
            correctRunes[0] = 5;
            correctRunes[1] = 0;
            correctRunes[2] = 1;
        }

        int numInscribed = 0;
        for(int i=0; i<inscribedRings.Length; i++)
        {
            if (inscribedRings[i])
                numInscribed++;
        }

        if (numInscribed == 0)
        {
            correctRunes[3] = 0;
            correctRunes[4] = 2;
            correctRunes[5] = 0;
        } else if (numInscribed == 1)
        {
            correctRunes[3] = 3;
            correctRunes[4] = 2;
            correctRunes[5] = 1;
        } else if (numInscribed == 2)
        {
            correctRunes[3] = 5;
            correctRunes[4] = 4;
            correctRunes[5] = 6;
        } else
        {
            correctRunes[3] = 7;
            correctRunes[4] = 2;
            correctRunes[5] = 7;
        }

        switch(numPoints)
        {
            case 3:
                correctRunes[6] = 7;
                correctRunes[7] = 6;
                correctRunes[8] = 5;
                break;
            case 4:
                correctRunes[6] = 4;
                correctRunes[7] = 3;
                correctRunes[8] = 2;
                break;
            case 5:
                correctRunes[6] = 1;
                correctRunes[7] = 0;
                correctRunes[8] = 7;
                break;
            case 6:
                correctRunes[6] = 6;
                correctRunes[7] = 5;
                correctRunes[8] = 4;
                break;
            case 7:
                correctRunes[6] = 3;
                correctRunes[7] = 2;
                correctRunes[8] = 1;
                break;
            case 8:
                correctRunes[6] = 0;
                correctRunes[7] = 7;
                correctRunes[8] = 6;
                break;
            case 9:
                correctRunes[6] = 5;
                correctRunes[7] = 4;
                correctRunes[8] = 3;
                break;
            default:
                correctRunes[6] = 2;
                correctRunes[7] = 1;
                correctRunes[8] = 0;
                break;
        }
    }

    private bool VerifyIncantation(int id, string incantation)
    {
        string filtered = incantation.ToLower();
        if (id == SHIELD)
        {
            switch(focalSymbol)
            {
                case 0: if (filtered == "ihalotua") return true; break;
                case 1: if (filtered == "phoraqur") return true; break;
                case 2: if (filtered == "lethodar") return true; break;
                case 3: if (filtered == "dolibix") return true; break;
                case 4: if (filtered == "donumeo") return true; break;
                case 5: if (filtered == "holasil") return true; break;
                case 6: if (filtered == "akular") return true; break;
                case 7: if (filtered == "ygosanyo") return true; break;
                case 8: if (filtered == "somatha") return true; break;
            }
        }
        else if (id == BANISH)
        {
            switch (focalSymbol)
            {
                case 0: if (filtered == "athomiqu") return true; break;
                case 1: if (filtered == "shlotam") return true; break;
                case 2: if (filtered == "seororel") return true; break;
                case 3: if (filtered == "tasarak") return true; break;
                case 4: if (filtered == "guthaatel") return true; break;
                case 5: if (filtered == "krorolyb") return true; break;
                case 6: if (filtered == "unithocl") return true; break;
                case 7: if (filtered == "madulic") return true; break;
                case 8: if (filtered == "pytrores") return true; break;
            }
        }
        else
        {
            switch (focalSymbol)
            {
                case 0: if (filtered == "xe'actois") return true; break;
                case 1: if (filtered == "it'amalism") return true; break;
                case 2: if (filtered == "er'aresi") return true; break;
                case 3: if (filtered == "kil-chandhu") return true; break;
                case 4: if (filtered == "far'ilursi") return true; break;
                case 5: if (filtered == "ybais-ilul") return true; break;
                case 6: if (filtered == "dalc'itep") return true; break;
                case 7: if (filtered == "oth-ephoggg") return true; break;
                case 8: if (filtered == "yr'poraz") return true; break;
            }
        }
        return false;
    }

    public static bool flipCoin()
    {
        int decide = Random.Range(0, 2);
        if (decide == 0)
            return false;
        else
            return true;
    }
}
