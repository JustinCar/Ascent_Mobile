using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject levelGenerator;
    LevelGenerator generatorScript;
    public List<GameObject> levelComponents;
    GameObject level;
    public GameObject floorNumberTxtObject;
    Text floorNumberTxt;

    public int levelLengthIncreaseAmount;
    public float enemyhealthMultiplierIncreaseAmount;

    int levelLength = 5; // Length in number of rooms along main path
    int levelDirection; // either 1, 2, 3 or 4
    public float enemyHealthMultiplier = 0;
    public int floorNumber = 1;
    public int biome; // 1 == Desert, 2 == Forest, 3 == Ice, 4 == Void

    // Start is called before the first frame update
    void Start()
    {
        levelDirection = Random.Range(1, 4);
        level = Instantiate (levelGenerator);
        generatorScript = level.GetComponent<LevelGenerator>();
        generatorScript.numberOfRooms = levelLength;
        generatorScript.mainPathDirection = levelDirection;
        biome = Random.Range(1, 5); // 1 == Desert, 2 == Forest, 3 == Ice, 4 == Void 
        generatorScript.biome = biome;
        floorNumberTxtObject.SetActive(true);
        floorNumberTxt = floorNumberTxtObject.GetComponent<Text>();

        switch (biome) 
        {
            case 1:
            floorNumberTxt.text = "FLOOR " + floorNumber + "\n" + "WASTES" + "\n" + "FIRE DAMAGE INCREASED" + "\n" + "ICE DAMAGE DECREASED";  
            floorNumberTxt.color = Color.red;  
            break;
            case 2:
            floorNumberTxt.text = "FLOOR " + floorNumber + "\n" + "WILDS" + "\n" + "POISON DAMAGE INCREASED" + "\n" + "VOID DAMAGE DECREASED";
            floorNumberTxt.color = Color.green;  
            break;
            case 3:
            floorNumberTxt.text = "FLOOR " + floorNumber + "\n" + "TUNDRA" + "\n" + "ICE DAMAGE INCREASED" + "\n" + "FIRE DAMAGE DECREASED";  
            floorNumberTxt.color = Color.cyan;
            break;
            case 4:
            floorNumberTxt.text = "FLOOR " + floorNumber + "\n" + "THE VOID" + "\n" + "VOID DAMAGE INCREASED" + "\n" + "POISON DAMAGE DECREASED";  
            floorNumberTxt.color = Color.magenta;
            break;
            default:
            floorNumberTxt.text = "biome error";  
            break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStartPos() 
    {
        player.transform.position = generatorScript.startRoom.transform.position;
    }

    // Level completed, new level is generated
    public void levelCompleted() 
    {
        foreach (GameObject obj in levelComponents) 
        {
            Destroy(obj);
        }
        Destroy(level);

        levelDirection = Random.Range(1, 4);

        levelLength += levelLengthIncreaseAmount;
        enemyHealthMultiplier += enemyhealthMultiplierIncreaseAmount;
        floorNumber++;

        level = Instantiate (levelGenerator);
        generatorScript = level.GetComponent<LevelGenerator>();
        generatorScript.numberOfRooms = levelLength;
        generatorScript.mainPathDirection = levelDirection;
        biome = Random.Range(1, 5); // 1 == Desert, 2 == Forest, 3 == Ice, 4 == Void 
        generatorScript.biome = biome;

        floorNumberTxtObject.SetActive(true);
        switch (biome) 
        {
            case 1:
            floorNumberTxt.text = "FLOOR " + floorNumber + "\n" + "WASTES" + "\n" + "FIRE DAMAGE INCREASED" + "\n" + "ICE DAMAGE DECREASED";  
            break;
            case 2:
            floorNumberTxt.text = "FLOOR " + floorNumber + "\n" + "WILDS" + "\n" + "POISON DAMAGE INCREASED" + "\n" + "VOID DAMAGE DECREASED";
            floorNumberTxt.color = Color.green;  
            break;
            case 3:
            floorNumberTxt.text = "FLOOR " + floorNumber + "\n" + "TUNDRA" + "\n" + "ICE DAMAGE INCREASED" + "\n" + "FIRE DAMAGE DECREASED";  
            floorNumberTxt.color = Color.cyan;
            break;
            case 4:
            floorNumberTxt.text = "FLOOR " + floorNumber + "\n" + "THE VOID" + "\n" + "VOID DAMAGE INCREASED" + "\n" + "POISON DAMAGE DECREASED";  
            floorNumberTxt.color = Color.magenta;
            break;
            default:
            floorNumberTxt.text = "biome error";  
            break;
        }
    }

    // Call when something goes wrong with level generation
    public void resetLevel() 
    {
        Debug.Log("========================RESET LEVEL=====================");
        foreach (GameObject obj in levelComponents) 
        {
            Destroy(obj);
        }
        Destroy(level);

        levelDirection = Random.Range(1, 4);

        level = Instantiate (levelGenerator);
        generatorScript = level.GetComponent<LevelGenerator>();
        generatorScript.numberOfRooms = levelLength;
        generatorScript.mainPathDirection = levelDirection;
        biome = Random.Range(1, 5); // 1 == Desert, 2 == Forest, 3 == Ice, 4 == Void 
        generatorScript.biome = biome;

        floorNumberTxtObject.SetActive(true);

        switch (biome) 
        {
            case 1:
            floorNumberTxt.text = "FLOOR " + floorNumber + "\n" + "WASTES" + "\n" + "FIRE DAMAGE INCREASED" + "\n" + "ICE DAMAGE DECREASED";  
            break;
            case 2:
            floorNumberTxt.text = "FLOOR " + floorNumber + "\n" + "WILDS" + "\n" + "POISON DAMAGE INCREASED" + "\n" + "VOID DAMAGE DECREASED";
            floorNumberTxt.color = Color.green;  
            break;
            case 3:
            floorNumberTxt.text = "FLOOR " + floorNumber + "\n" + "TUNDRA" + "\n" + "ICE DAMAGE INCREASED" + "\n" + "FIRE DAMAGE DECREASED";  
            floorNumberTxt.color = Color.cyan;
            break;
            case 4:
            floorNumberTxt.text = "FLOOR " + floorNumber + "\n" + "THE VOID" + "\n" + "VOID DAMAGE INCREASED" + "\n" + "POISON DAMAGE DECREASED";  
            floorNumberTxt.color = Color.magenta;
            break;
            default:
            floorNumberTxt.text = "biome error";  
            break;
        }
    }
}
