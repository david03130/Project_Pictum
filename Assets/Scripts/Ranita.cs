using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ranita : MonoBehaviour
{
    public DetectionZone player;

    private void Awake()
    {
        player = GetComponent<DetectionZone>();
    }

    void Update()
    {
        if (player.detectedColliders.Count > 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
