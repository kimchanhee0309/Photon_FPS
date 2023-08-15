using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System.Linq;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Scoreboard : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPrefab;
    public CanvasGroup canvasGroup;

    Dictionary<Player, ScoreboardItem> scoreboardItems = new Dictionary<Player, ScoreboardItem>();

    public static Scoreboard instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            AddScoreboardItem(player);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddScoreboardItem(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemoveScoreboardItem(otherPlayer);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (scoreboardItems.TryGetValue(targetPlayer, out ScoreboardItem item))
        {
            item.UpdateStats();
            UpdateScoreboardOrder();
        }
    }

    void AddScoreboardItem(Player player)
    {
        ScoreboardItem item = Instantiate(scoreboardItemPrefab, container).GetComponent<ScoreboardItem>();
        item.Initialize(player);
        scoreboardItems[player] = item;
        UpdateScoreboardOrder();
    }

    void RemoveScoreboardItem(Player player)
    {
        if (scoreboardItems.TryGetValue(player, out ScoreboardItem item))
        {
            Destroy(item.gameObject);
            scoreboardItems.Remove(player);
            UpdateScoreboardOrder();
        }
    }

    void UpdateScoreboardOrder()
    {
        List<Player> sortedPlayers = scoreboardItems.Keys.ToList();
        sortedPlayers.Sort((p1, p2) => GetKills(p2) - GetKills(p1));

        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            scoreboardItems[sortedPlayers[i]].transform.SetSiblingIndex(i);
        }
    }

    int GetKills(Player player)
    {
        if (player.CustomProperties.TryGetValue("kills", out object kills))
        {
            return (int)kills;
        }
        return 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            canvasGroup.alpha = 1;
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            canvasGroup.alpha = 0;
        }
    }
}
