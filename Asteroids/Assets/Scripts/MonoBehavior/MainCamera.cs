using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RRRStudyProject
{
    public class MainCamera : MonoBehaviour
    {
        MainGame game;

        Player player;
        Vector3 playerPosition;

        private void Start()
        {
            game = FindObjectOfType<MainGame>();
            if (!FindObjectOfType<Player>())
            {
                Camera.main.gameObject.transform.position = new Vector3(0, 0, 10);
                throw new System.Exception("Can't find Player");
            }
            player = FindObjectOfType<Player>();
        }

        private void Update()
        {
            playerPosition = player.gameObject.transform.position;
            transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
            if (player == null) player = game.playerObject.GetComponent<Player>();
        }
    }
}