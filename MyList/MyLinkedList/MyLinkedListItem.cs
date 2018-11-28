namespace MyLinkedList
{
    internal class MyLinkedListItem<T>
    {
        public MyLinkedListItem(T value)
        {
            this.Value = value;
        }

        public MyLinkedListItem<T> Next
        {
            get;
            set;
        }

        public MyLinkedListItem<T> Prev
        {
            get;
            set;
        }

        public T Value
        {
            get;
            set;
        }
    }
}
