using UnityEngine;

namespace ImagimonTheGame
{
    [CreateAssetMenu(fileName = "Move", menuName = "Imagimon/Create new move")]
    public class MoveBase : ScriptableObject
    {
        public enum MoveType
        {
            Physical,
            Special
        }

        [SerializeField] string name;

        [TextArea]
        [SerializeField] string description;

        [SerializeField] ImagimonType type;
        [SerializeField] int power;
        [SerializeField] int accuracy;
        [SerializeField] int pp;

        public MoveBase(string name)
        {
            this.name = name;
            description = "Move description";
            type = ImagimonType.Normal;
            power = 50;
            accuracy = 100;
            pp = 10;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public ImagimonType Type
        {
            get { return type; }
            set { type = value; }
        }

        public int Power
        {
            get { return power; }
            set { power = value; }
        }

        public int Accuracy
        {
            get { return accuracy; }
            set { accuracy = value; }
        }

        public int PP
        {
            get { return pp; }
            set { pp = value; }
        }
    }
}
