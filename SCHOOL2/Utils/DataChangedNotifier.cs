namespace SCHOOL2.Utils
{
    public class DataChangedNotifier
    {
        public static event Action OnDataChanged;

        public static void NotifyDataChanged()
        {
            OnDataChanged?.Invoke();
        }
    }

}
