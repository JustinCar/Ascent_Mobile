using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class GameCenter : MonoBehaviour
{
    public bool loginSuccessful;

    string MainLeaderboardID = "Main Leaderboard";
    string EssenceLeaderboardID = "Essence Leaderboard";
    string HighestFloorLeaderboardID = "Highest Floor Leaderboard";

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameCenter");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        AuthenticateUser();
    }

    void AuthenticateUser()
    {
        Social.localUser.Authenticate((bool success) => 
        { 
            if(success)
            {
                loginSuccessful = true;
                Debug.Log("Authentication success");
            }
            else
            {
                Debug.Log("Authentication fail");
            }
        });
    }

    public void showLeaderBoard() 
    {
        Social.ShowLeaderboardUI();
    }

    public void UpdateEssenceLeaderBoard(int myScore)
    {
        if(loginSuccessful)
        {
            Social.ReportScore(myScore, EssenceLeaderboardID, (bool success) => {
            if(success)
                Debug.Log("Upload success");
                // handle success or failure
                });
        }
        else
        {
            Social.localUser.Authenticate((bool success) => 
            {
                if(success)
                {
                    loginSuccessful = true;
                    Social.ReportScore(myScore, EssenceLeaderboardID, (bool successful) => {
                    // handle success or failure
                });
                }
                else
                {
                    Debug.Log("Upload fail");
                }
                    // handle success or failure
            });
        }
    }

    public void UpdateFloorLeaderBoard(int myScore)
    {
        if(loginSuccessful)
        {
            Social.ReportScore(myScore, HighestFloorLeaderboardID, (bool success) => {
            if(success)
                Debug.Log("Upload success");
                // handle success or failure
                });
        }
        else
        {
            Social.localUser.Authenticate((bool success) => 
            {
                if(success)
                {
                    loginSuccessful = true;
                    Social.ReportScore(myScore, HighestFloorLeaderboardID, (bool successful) => {
                    // handle success or failure
                });
                }
                else
                {
                    Debug.Log("Upload fail");
                }
                    // handle success or failure
            });
        }
    }
}

