using System.Collections;
using System;

namespace RRRStudyProject
{
    public sealed class ListExecuteObject : IEnumerator, IEnumerable
    {
        private IExecute[] _interactiveObjects;
        private int _index = -1;

        public IExecute this[int curr]
        {
            get => _interactiveObjects[curr];
            private set => _interactiveObjects[curr] = value;
        }

        public object Current => _interactiveObjects[_index];

        public int Length => _interactiveObjects.Length;

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
            _index = -1;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

	   public void AddExecuteObj(IExecute execute)
		{
			if (_interactiveObjects == null)
			{
				_interactiveObjects = new[] {execute};
				return;
			}
			
			Array.Resize(ref _interactiveObjects, Length + 1);
			_interactiveObjects[Length - 1] = execute;
		}

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
