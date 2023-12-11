using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> usernames;

    [SerializeField]
    private List<TextMeshProUGUI> times;

    private string publicLeaderboardKey = "e1f0d51a127b3781a29880af4e57a8ed8465104bd73ea113c8b84d03511a7e8a";

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < usernames.Count) ? msg.Length : usernames.Count;
            for (int i = 0;i < loopLength; i++)
            {
                usernames[i].text = msg[i].Username;
                times[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboard(string username, float times)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, (int)times, ((msg) =>
        {
            GetLeaderboard();
        }));
    }

    // Start is called before the first frame update
    void Start()
    {
        GetLeaderboard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabCompletedRuns()
    {

    }
}
