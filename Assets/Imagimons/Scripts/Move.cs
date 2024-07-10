using UnityEngine;

namespace ImagimonTheGame
{
    public class Move
    {
        public MoveBase Base { get; private set; }
        public int PP { get; private set; }

        public Move(MoveBase iBase, int pp)
        {
            Base = iBase;
            PP = pp; // Initialize PP with the provided parameter
        }

        // Optionally, add methods or properties as needed
        public void DecreasePP(int amount)
        {
            PP -= amount;
            if (PP < 0)
            {
                PP = 0;
            }
        }
    }
}
