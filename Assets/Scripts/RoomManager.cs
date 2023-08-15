using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    void Awake()
    {
        if (Instance) //checks if another RoomManager exists
        {
            Destroy(gameObject); //there can only be one
            return;
        }
        DontDestroyOnLoad(gameObject); //I am the only one...
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Map_v1")
        {
            // Check if a PhotonView with the same ID already exists
            PhotonView existingPhotonView = PhotonView.Find(999); // Change 999 to your actual PhotonView ID
            if (existingPhotonView != null)
            {
                // Destroy the existing PhotonView
                PhotonNetwork.Destroy(existingPhotonView);
            }

            // Instantiate the new PhotonView
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }
}
