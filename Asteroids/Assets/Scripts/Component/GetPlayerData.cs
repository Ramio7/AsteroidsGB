using UnityEngine;

namespace RRRStudyProject
{
    public static class GetPlayerData
    {
        public static Vector2 GetPlayerPosition()
        {
            Player _player = Object.FindObjectOfType<Player>();
            float xCoordinate = 0;
            float yCoordinate = 0;
            if (!_player)
            {
                return new Vector2(xCoordinate, yCoordinate);
            }
            xCoordinate = _player.transform.position.x;
            yCoordinate = _player.transform.position.y;
            return new Vector2(xCoordinate, yCoordinate);
        }

        public static Vector2 GenerateObjectStartPosition()
        {
            Vector2 playerPosition = GetPlayerPosition();
            float xAxisPosition = playerPosition.x + Random.Range(-100, 100);
            float yAxisPosition = playerPosition.y + Random.Range(-100, 100);
            return new Vector2(xAxisPosition, yAxisPosition);
        }

        public static Vector2 GenerateWaypointNearPlayer()
        {
            Vector2 playerPosition = GetPlayerPosition();
            float xAxisPosition = playerPosition.x + Random.Range(-10, 10);
            float yAxisPosition = playerPosition.y + Random.Range(-10, 10);
            return new Vector2(xAxisPosition, yAxisPosition);
        }
    }
}