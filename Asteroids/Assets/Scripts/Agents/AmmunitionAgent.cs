using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RRRStudyProject
{
    public class AmmunitionAgent : IEnumerable, IEnumerator
    {
        List<GameObject> _ammunitionPrefabs = new List<GameObject>();

        private int _index = 0;

        public List<GameObject> AmmunitionPrefabs { get => _ammunitionPrefabs; set => _ammunitionPrefabs = value; }

        public object Current => _ammunitionPrefabs[_index];
        public int Length => _ammunitionPrefabs.Capacity;
        public int Index { get => _index; set => _index = value; }

        public void InitiateAmmunitionList(GameObject[] ammunitions)
        {
            for (int i = 0; i < ammunitions.Length; i++) _ammunitionPrefabs.Add(ammunitions[i]);
        }

        public Ammunition[] ExtractAmmunitionClasses()
        {
            Ammunition[] result = new Ammunition[AmmunitionPrefabs.Count];
            for (int i = 0; i < AmmunitionPrefabs.Count; i++)
            {
                if (AmmunitionPrefabs[i].TryGetComponent(out Ammunition ammo))
                {
                    ammo.Data = new AmmunitionData(AmmunitionPrefabs[i].name);
                    result[i] = ammo;
                }
            }
            if (result != null) return result;
            throw new System.Exception("No ammo founded");
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (_index == Length - 1)
            {
                Reset();
                return false;
            }
            
            _index++;
            return true;
        }

        public void Reset()
        {
            _index = 0;
        }
    }
}