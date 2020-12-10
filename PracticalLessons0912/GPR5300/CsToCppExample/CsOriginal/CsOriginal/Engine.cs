using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsCalc
{
    class Engine
    {
        int size;
        Vector2Int playerPosition;

        public Engine(int size)
        {
            this.size = size;
        }

        public void Move(int dx, int dy)
        {
            playerPosition.x = (playerPosition.x + dx + size) % size;
            playerPosition.y = (playerPosition.y + dy + size) % size;
        }

        public string Draw()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    sb.Append((x == playerPosition.x && y == playerPosition.y) ? "O" : "_");
                }
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

    }

    public struct Vector2Int
    {
        public int x;
        public int y;

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2Int operator + (Vector2Int lhs, Vector2Int rhs)
        {
            return new Vector2Int(lhs.x + rhs.x, lhs.y + rhs.y);
        }
    }
}
