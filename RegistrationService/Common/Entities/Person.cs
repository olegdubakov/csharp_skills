namespace Common.Entities
{
    public class Person
    {
        private string _name;
        private string _lastName;

        public string Name
        {
            get => this._name;
            set => this._name = value?.Trim() ?? string.Empty;
        }

        public string LastName
        {
            get => this._lastName;
            set => this._lastName = value?.Trim() ?? string.Empty;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^ this.LastName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var person = obj as Person;

            if (person != null)
            {
                if (this._name.Equals(person.Name)
                    && this._lastName.Equals(person.LastName))
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
