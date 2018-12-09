namespace Common.Entities
{
    public class GeoPosition
    {
        public double X
        {
            get;
            set;
        }

        public double Y
        {
            get;
            set;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() 
                   ^ this.Y.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            
            var geoPosition = obj as GeoPosition;

            if (geoPosition != null)
            {
                return this.X.Equals(geoPosition.X) 
                       && this.Y.Equals(geoPosition.Y);
            }

            return false;
        }
    }
}
