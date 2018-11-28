using System;

namespace MyLinkedList
{
    public class MyLinkedList<T>
    {
        private MyLinkedListItem<T> _root;

        public MyLinkedList()
        {
            this._root = null;
        }

        public void Add(T item)
        {
            try
            {
                var last = this.GetLast();
                var newItem = new MyLinkedListItem<T>(item);

                this.SetNextNodeOrRoot(last, newItem);
                this.SetPrevNode(newItem, last);
            }
            catch (NullReferenceException)
            {
                this.SetRoot(new MyLinkedListItem<T>(item));
            }
        }

        public void AddAfter(T existing, T newElement)
        {
            try
            {
                var existingItem = this.Find(existing);
                var nextItem = existingItem.Next;
                var newItem = new MyLinkedListItem<T>(newElement);

                this.SetNextNodeOrRoot(newItem, nextItem);
                this.SetPrevNode(newItem, existingItem);

                this.SetNextNodeOrRoot(existingItem, newItem);
                this.SetPrevNode(nextItem, newItem);
            }
            catch (NullReferenceException)
            {
                throw new InvalidOperationException();
            }
        }

        public void Remove(T item)
        {
            try
            {
                var existingItem = this.Find(item);
                var next = existingItem.Next;
                var prev = existingItem.Prev;

                this.SetNextNodeOrRoot(prev, next);
                this.SetPrevNode(next, prev);
            }
            catch (NullReferenceException)
            {
                throw new InvalidOperationException();
            }
        }

        public override string ToString()
        {
            var temp = this._root;
            var result = string.Empty;

            while (temp != null)
            {
                result = $"{result}{temp.Value};";
                temp = temp.Next;
            } 

            return result;
        }

        private void SetRoot(MyLinkedListItem<T> item)
        {
            this._root = item;
        }

        private MyLinkedListItem<T> Find(T item)
        {
            var temp = this._root;

            while (!temp.Value.Equals(item))
            {
                temp = temp.Next;
            }

            return temp;
        }
        private MyLinkedListItem<T> GetLast()
        {
            var temp = this._root;

            while (temp.Next != null)
            {
                temp = temp.Next;
            }

            return temp;
        }

        private void SetNextNodeOrRoot(MyLinkedListItem<T> item, MyLinkedListItem<T> next)
        {
            try
            {
                item.Next = next;
            }
            catch (NullReferenceException)
            {
                this._root = next;
            }
        }
        private void SetPrevNode(MyLinkedListItem<T> item, MyLinkedListItem<T> prev)
        {
            try
            {
                item.Prev = prev;
            }
            catch (NullReferenceException)
            {
            }
        }
    }
}
