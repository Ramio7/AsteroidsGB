using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RRRStudyProject
{
    public class ListAbility : IExecute
    {
        private List<IAbility> _abilities;
        private CommandInput _input;

        public ListAbility(List<IAbility> abilities, CommandInput commandInput)
        {
            _abilities = abilities;
            _input = commandInput;
            Object.FindObjectOfType<MainGame>().interactiveObjects.AddExecuteObj(this);
        }

        public IAbility this[int index] { get => _abilities[index]; set => _abilities[index] = value; }

        public string this[Target index]
        {
            get
            {
                var ability = _abilities.FirstOrDefault(a => a.Target == index);
                return ability == null ? "Not supported" : ability.ToString();
            }
        }

        public int MaxDamage => _abilities.Select(a => (int)a.Damage).Max();

        public IEnumerable<IAbility> GetAbility()
        {
            while (true)
            {
                yield return _abilities[Random.Range(0, _abilities.Count)];
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _abilities.Count; i++)
            {
                yield return _abilities[i];
            }
        }

        public IEnumerable<IAbility> GetAbility(DamageType index)
        {
            foreach (IAbility ability in _abilities)
            {
                if (ability.DamageType == index)
                {
                    yield return ability;
                }
            }
        }

        private void UseAbility() //refactor ability use with observer
        {
            if (_input.useFirstAbility.KeyDown) Debug.Log(this[0].Name);
            if (_input.useSecondAbility.KeyDown) Debug.Log(this[1].Name);
            if (_input.useThirdAbility.KeyDown) Debug.Log(this[2].Name);
            if (_input.useFourthAbility.KeyDown) Debug.Log(this[3].Name);
        }

        public void Update()
        {
            UseAbility();
        }
    }
}