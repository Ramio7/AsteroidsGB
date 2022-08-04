using System;
using System.Collections.Generic;
using UnityEngine;

namespace RRRStudyProject
{
    [Serializable]
    public struct DictionarySurrogated
    {
        public string[] objectNames;
        public ObjectPool[] objectPools;

        public DictionarySurrogated(Dictionary<string, ObjectPool> poolsDictionary)
        {
            objectNames = new string[poolsDictionary.Count];
            objectPools = new ObjectPool[objectNames.Length];
            for (int i = 0; i < poolsDictionary.Count; i++)
            {
                poolsDictionary.Keys.CopyTo(objectNames, i);
                poolsDictionary.Values.CopyTo(objectPools, i);
            }
        }

        public Type GetDataContractType(Type type)
        {
            if (typeof(ViewServices).IsAssignableFrom(type))
            {
                return typeof(DictionarySurrogated);
            }
            return type;
        }

        public void GetDictionaryToSerialize(Dictionary<string, ObjectPool> poolsDictionary)
        {
            foreach (string objectName in poolsDictionary.Keys)
            {
                Array.Resize(ref objectNames, objectNames.Length + 1);
                objectNames[objectNames.Length - 1] = objectName;
            }

            foreach (ObjectPool objectPool in poolsDictionary.Values)
            {
                Array.Resize(ref objectPools, objectPools.Length + 1);
                objectPools[objectPools.Length - 1] = objectPool;
            }
        }

        public Dictionary<string, ObjectPool> GetDeserializedDictionary()
        {
            Dictionary<string, ObjectPool> deserializedDictionary = new Dictionary<string, ObjectPool>();
            for (int i = 0; i < objectNames.Length; i++) deserializedDictionary.Add(objectNames[i], objectPools[i]);
            return deserializedDictionary;
        }
    }
}