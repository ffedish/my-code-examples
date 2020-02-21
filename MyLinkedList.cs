using System;
using System.Collections;
using System.Collections.Generic;

namespace New_Semestr
{
    public class MyLinkedListNode<T>
    {
        public MyLinkedList<T> List;

        public MyLinkedListNode<T> Previous;
        public MyLinkedListNode<T> Next;

        public T Value;

        public MyLinkedListNode(T Value)
        {
            this.Value = Value;
        }
        
        public override string ToString()
        {
            return Value.ToString();
        }
    }
    public class MyLinkedList<T> : ICollection<T>, ICollection, IEnumerable<T>, IEnumerable
    {
        public int Count { get; set; }
        public MyLinkedListNode<T> First = null;
        public MyLinkedListNode<T> Last = null;

        private void InicializeFirstElement(T Value) 
        {
            MyLinkedListNode<T> Insert = new MyLinkedListNode<T>(Value);

            InicializeFirstElement(Insert);
        }

        private void InicializeFirstElement(MyLinkedListNode<T> Value) 
        {
            First = Value;
            Last = Value;
            Value.List = this;
            Count++;
        }

        public void AddFirst(T Value) 
        {
            MyLinkedListNode<T> Inert = new MyLinkedListNode<T>(Value);

            if (First == null)
            {
                InicializeFirstElement(Value);
                return;
            }

            AddFirst(Inert);
        }

        public void AddFirst(MyLinkedListNode<T> Value) 
        {
            if (Value.List == this)
                throw new InvalidOperationException();

            if (First == null)
            {
                InicializeFirstElement(Value);
                return;
            }

            Count++;
            Value.List = this;
            First.Previous = Value;
            Value.Next = First;
            First = Value;
        }

        public void AddLast(MyLinkedListNode<T> Value)
        {
            if (Value.List == this)
                throw new InvalidOperationException();

            if (Last == null)
            {
                InicializeFirstElement(Value);
                return;
            }

            Count++;
            Value.List = this;
            Last.Next = Value;
            Value.Previous = Last;
            Last = Value;
        }

        public void AddLast(T Value)
        {
            var Insert = new MyLinkedListNode<T>(Value);

            AddLast(Insert);
        }

        public void RemoveLast()
        {
            if (Last != null)
            {
                Count--;
                Last.List = null;
            }

            Last = Last?.Previous;

            if (Last != null)
            {
                Last.Next.Previous = null;
                Last.Next = null;
            }
        }

        public void RemoveFirst()
        {
            if (First != null)
            {
                Count--;
                First.List = null;
            }

            First = First?.Next;

            if (First != null)
            {
                First.Previous.Next = null;
                First.Previous = null;
            }
        }

        public MyLinkedListNode<T> Find(T Value)
        {
            var curr = First;

            while (curr != null)
            {
                if (curr.Value.Equals(Value))
                    return curr;

                curr = curr.Next;
            }

            return null;
        }

        public MyLinkedListNode<T> FindLast(T Value)
        {
            var curr = Last;

            while (curr != null)
            {
                if (curr.Value.Equals(Value))
                    return curr;

                curr = curr.Previous;
            }

            return null;
        }

        public void AddAfter(MyLinkedListNode<T> Current, T Value)
        {
            var Insert = new MyLinkedListNode<T>(Value);

            AddAfter(Current, Insert);
        }

        public void AddAfter(MyLinkedListNode<T> Current, MyLinkedListNode<T> Insert)
        {
            var currNext = Current.Next;

            if (Insert.List == this)
                throw new InvalidOperationException();

            if (currNext == null)
            {
                AddLast(Insert);
                return;
            }

            Count++;
            Insert.List = this;
            Current.Next = Insert;
            Insert.Previous = Current;
            Insert.Next = currNext;
            currNext.Previous = Insert;
        }

        public void AddBefore(MyLinkedListNode<T> Current, T Value)
        {
            var Insert = new MyLinkedListNode<T>(Value);

            AddBefore(Current, Insert);
        }

        public void AddBefore(MyLinkedListNode<T> Current, MyLinkedListNode<T> Insert)
        {
            var currPrevious = Current.Previous;

            if (Insert.List == this)
                throw new InvalidOperationException();

            if (currPrevious == null)
            {
                AddFirst(Insert);
                return;
            }

            Count++;
            Insert.List = this;
            Current.Previous = Insert;
            Insert.Next = Current;
            Insert.Previous = currPrevious;
            currPrevious.Next = Insert;
        }

        public void Remove(MyLinkedListNode<T> Insert)
        {
            if (Insert.List != this)
                throw new ArgumentException();

            Count--;
            Insert.List = null;
            if (Insert == Last)
                Last = Insert.Previous;

            if (Insert == First)
                First = Insert.Next;
              

            if (Insert.Next == null && Insert.Previous == null)
                Clear();

            else if (Insert.Next == null)
                Insert.Previous.Next = null;

            else if (Insert.Previous == null)
                Insert.Next.Previous = null;

            else
            {
                Insert.Next.Previous = Insert.Previous;
                Insert.Previous.Next = Insert.Next;
            }
        }

        public void CopyTo(Array array, int index)
        {
            var Current = First;
            
            while (Current != null)
            {
                array.SetValue(Current.Value, index);

                index++;
                Current = Current.Next;
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var Current = First;

            while (Current != null)
            {
                array.SetValue(Current.Value, arrayIndex);

                arrayIndex++;
                Current = Current.Next;
            }
        }

        public MyLinkedList(){ }

        public MyLinkedList(T[] Values)
        {
            foreach (var i in Values) 
                Add(i);
        }

        public void Add(T item)
        {
            var Insert = new MyLinkedListNode<T>(item);

            if (Last == null)
            {
                InicializeFirstElement(Insert);
                return;
            }

            AddLast(Insert);
        }

        public void Clear()
        {
            var start = First;

            while (start!=null)
            {
                start.List = null;
                start = start.Next;
            }

            First = null;
            Last = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            var curr = First;

            while (curr != null)
            {
                if (curr.Value.Equals(item))
                    return true;
                curr = curr.Next;
            }

            return false;
        }

        public bool Remove(T item)
        {
            var deleted = Find(item);
            Remove(deleted);

            return deleted!=null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyListEnumerator(this);
        }

        class MyListEnumerator : IEnumerator, IEnumerator<T>
        {
            MyLinkedList<T> Enumeration = null;

            MyLinkedListNode<T> Curr=null;
            MyLinkedListNode<T> StartPosition = null;

            public MyListEnumerator(MyLinkedList<T> Insert)
            {
                if (Insert == null)
                    throw new NullReferenceException();

                Enumeration = Insert;
                StartPosition = Insert.First;
            }

            public T Current
            {
                get
                {
                    return Curr.Value;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Curr.Value;
                }
            }

            public void Dispose() { }

            public bool MoveNext()
            {
                if (Curr == null)
                    Curr = StartPosition;
                else
                    Curr = Curr.Next;

                return Curr!= null;
            }

            public void Reset()
            {
                Curr = StartPosition;
            }
        }

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();
    }
}
