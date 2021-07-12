using System;

namespace Domain
{
    public class Item
    {
        public int Index { get; set; }
        public float Weight { get; set; }
        public float Cost { get; set; }
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Item item = (Item)obj;
                return (this.Cost == item.Cost) && (this.Index == item.Index) && (this.Weight == item.Weight);
            }
        }
    }
}
