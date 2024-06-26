using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public MatchingSetting MatchingSetting;
    public static GameManager Singleton;
    private static string info;
    private Dictionary<string, Player> players = new Dictionary<string, Player>();
    private void Awake()
    {
        Singleton = this;
    }

    public void RegisterPlayer(string name, Player player)
    {
        player.transform.name = name;
        players.Add(name, player);
    }

    public void UnregisterPlayer(string name)
    {
        players.Remove(name);
    }


    public Player GetPlayer(string name)
    {
        return players[name];
    }

}