namespace ObserverSystem
{
    public interface INotifyListener<in T>
    {
        public void ReceiveNotification(T state);
    }

    public interface INotifyListener
    {
        public void ReceiveNotification();
    }
}