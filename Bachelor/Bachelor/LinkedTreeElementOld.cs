namespace Bachelor
{
    public class LinkedTreeElementOld
    {
        public LinkedTreeElementOld parent;
        public int value;

        public LinkedTreeElementOld(int value)
        {
            this.value = value;
        }
        public LinkedTreeElementOld(LinkedTreeElementOld parent,int value)
        {
            this.value = value;
            this.parent = parent;
        }
    }
}