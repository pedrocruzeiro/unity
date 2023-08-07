using System.Runtime.Serialization;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class LeaderBoard : MonoBehaviour
{

    [SerializeField] private List<TextMeshProUGUI> names;

    [SerializeField] private List<TextMeshProUGUI> scores;

    [SerializeField] private GameObject newHighScoreUI;

    [SerializeField] private TextMeshProUGUI inputName;

    [SerializeField] private TextMeshProUGUI highScore;

    [SerializeField] private float points = 0;

    private bool newHighScore = false;

    private bool newHighScoreAdded = false;

    private string publicLeaderBoardKey = "8246848006d79c5d6656b9039647b9c8e67a940df2554365036a58f17dc5cbd0";
    // Start is called before the first frame update
    void Start()
    {
    }

    public void GetLeaderBoard(){
        LeaderboardCreator.GetLeaderboard(publicLeaderBoardKey,((msg) => 
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for(int i = 0; i < loopLength; i++){
                if((int)points > msg[i].Score){
                    newHighScore = true;
                }
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }

                Debug.Log("Msg length: " + msg.Length);
                Debug.Log("NewHighScore: " + newHighScore);
                Debug.Log("Names Count: " + names.Count);
            if(!newHighScoreAdded && (newHighScore || msg.Length == 0 || (msg.Length < names.Count))){

                newHighScoreUI.SetActive(true);
                highScore.SetText("" + points);
            }
        }));
    }

    public void gameOverPointsEvent(float points){
       this.points = points;
        GetLeaderBoard();
    }

    public void setLeaderBoardEntry(){
        var username = inputName.text;
        if (username.Length > 8){
            inputName.text.Substring(0,8);
        }
       Debug.Log("username: " + username);
       LeaderboardCreator.UploadNewEntry(publicLeaderBoardKey,username,(int)points, ((msg) => 
       {
        GetLeaderBoard();
       }
        ));
        newHighScoreAdded = true;
        newHighScoreUI.SetActive(false);
        newHighScore = false;
        LeaderboardCreator.ResetPlayer();
    }
}
