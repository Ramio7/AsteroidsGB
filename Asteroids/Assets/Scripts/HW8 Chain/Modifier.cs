namespace RRRStudyProject
{
    public class Modifier
    {
        protected Unit _unit;
        protected Modifier Next;

        public Modifier(Unit unit)
        {
            _unit = unit;
        }

        public void Add(Modifier newModifier)
        {
            if (Next != null) Next.Add(newModifier);
            else Next = newModifier;
        }

        public virtual void Handle() => Next?.Handle();
    }
}