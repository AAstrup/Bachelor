namespace Bachelor
{
    public class LinkedTreeElement
    {
        public LinkedTreeElement parent;
        public int value;

        public LinkedTreeElement(int value)
        {
            this.value = value;
        }
        public LinkedTreeElement(LinkedTreeElement parent,int value)
        {
            this.value = value;
            this.parent = parent;
        }
    }
}