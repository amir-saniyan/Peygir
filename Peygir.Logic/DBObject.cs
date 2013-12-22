namespace Peygir.Logic
{
    public abstract class DBObject
    {
        public const int InvalidID = -1;

        public int ID { get; protected set; }

        public abstract void Add();

        public abstract void Update();

        public abstract void Delete();

        protected DBObject()
        {
            ID = InvalidID;
        }
    }
}
