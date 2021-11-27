namespace ObserverSystem
{
    public interface INotifyObserver<out T>
    {
        public void AddListener(INotifyListener<T> notifier);

        public void RemoveListener(INotifyListener<T> notifier);
    }

    public interface INotifyObserver
    {
        public void AddListener(INotifyListener notifier);

        public void RemoveListener(INotifyListener notifier);
    }
}