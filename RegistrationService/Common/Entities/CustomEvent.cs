namespace Common.Entities
{
    using System;

    public class CustomEvent
    {
        public string Name
        {
            get;
            set;
        }

        public DateTime EventDate
        {
            get; 
            set;
        }

        public GeoPosition Position
        {
            get;
            set;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() 
                ^ this.Position.GetHashCode()
                ^ this.EventDate.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var customEvent = obj as CustomEvent;

            if (customEvent != null)
            {
                if (this.Name.Equals(customEvent.Name)
                    && this.Position.Equals(customEvent.Position)
                    && this.EventDate.Equals(customEvent.EventDate))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
