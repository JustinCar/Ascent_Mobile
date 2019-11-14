using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleChoice : MonoBehaviour
{
    enum style {BOW, MARTIAL, SWORD};
    public Button bowBtn;
    public Button martialBtn;
    public Button swordBtn;

    style fightingStyle;
    public  GameJustStarted justStartedScript;

    public void bowSelected()
    {
        ColorBlock coloursBow = bowBtn.colors;
        coloursBow.normalColor = Color.yellow;
        bowBtn.colors = coloursBow;

        ColorBlock coloursMartial = martialBtn.colors;
        coloursMartial.normalColor = Color.black;
        martialBtn.colors = coloursMartial;

        ColorBlock coloursSword = swordBtn.colors;
        coloursSword.normalColor = Color.black;
        swordBtn.colors = coloursSword;

        fightingStyle = style.BOW;
    }

    public void martialSelected()
    {
        ColorBlock coloursBow = bowBtn.colors;
        coloursBow.normalColor = Color.black;
        bowBtn.colors = coloursBow;

        ColorBlock coloursMartial = martialBtn.colors;
        coloursMartial.normalColor = Color.yellow;
        martialBtn.colors = coloursMartial;

        ColorBlock coloursSword = swordBtn.colors;
        coloursSword.normalColor = Color.black;
        swordBtn.colors = coloursSword;

        fightingStyle = style.MARTIAL;
    }

    public void swordSelected()
    {
        ColorBlock coloursBow = bowBtn.colors;
        coloursBow.normalColor = Color.black;
        bowBtn.colors = coloursBow;

        ColorBlock coloursMartial = martialBtn.colors;
        coloursMartial.normalColor = Color.black;
        martialBtn.colors = coloursMartial;

        ColorBlock coloursSword = swordBtn.colors;
        coloursSword.normalColor = Color.yellow;
        swordBtn.colors = coloursSword;

        fightingStyle = style.SWORD;
    }

    public void saveStyle() 
    {
        justStartedScript.justStarted = false;
        switch (fightingStyle) 
        {
            case style.BOW :
            SaveLoadManager.SetFightingStyle(0);
            Debug.Log("FIGHTING STYLE = BOW");
            FaceBookEvents.LogLevelStartedEvent("BOW");
            break;

            case style.MARTIAL :
            SaveLoadManager.SetFightingStyle(1);
            Debug.Log("FIGHTING STYLE = MARTIAL");
            FaceBookEvents.LogLevelStartedEvent("MARTIAL");
            break;

            case style.SWORD :
            SaveLoadManager.SetFightingStyle(2);
            Debug.Log("FIGHTING STYLE = SWORD");
            FaceBookEvents.LogLevelStartedEvent("SWORD");
            break;
        }
    }
}
