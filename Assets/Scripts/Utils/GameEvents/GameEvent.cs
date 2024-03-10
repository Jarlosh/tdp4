using System;

namespace Utils.GameEvents
{
    public interface IGameEvent
    {
        event Action EventOnInvoked;
    }
    
    public interface IGameEvent<out T>
    {
        event Action<T> EventOnInvoked;
    }
    
    public interface IGameEvent<out T1, out T2>
    {
        event Action<T1, T2> EventOnInvoked;
    }
    
    public class GameEvent: IGameEvent
    {
        public event Action EventOnInvoked;
        public void Invoke() => EventOnInvoked?.Invoke();
    }
    
    public class GameEvent<T>: IGameEvent<T>
    {
        public event Action<T> EventOnInvoked;
        public void Invoke(T item) => EventOnInvoked?.Invoke(item);
    }
    
    public class GameEvent<T1, T2>: IGameEvent<T1, T2>
    {
        public event Action<T1, T2> EventOnInvoked;
        public void Invoke(T1 i1, T2 i2) => EventOnInvoked?.Invoke(i1, i2);
    }
    
    public static class GameEventExtensions
    {
        public static void Subscribe(this IGameEvent gameEvent, Action action) 
            => gameEvent.EventOnInvoked += action;

        public static void Unsubscribe(this IGameEvent gameEvent, Action action) 
            => gameEvent.EventOnInvoked -= action;
    
        public static void Subscribe<T>(this IGameEvent<T> gameEvent, Action<T> action) 
            => gameEvent.EventOnInvoked += action;

        public static void Unsubscribe<T>(this IGameEvent<T> gameEvent, Action<T> action) 
            => gameEvent.EventOnInvoked -= action;

        public static void Subscribe<T1, T2>(this IGameEvent<T1, T2> gameEvent,Action<T1, T2> action) 
            => gameEvent.EventOnInvoked += action;

        public static void Unsubscribe<T1, T2>(this IGameEvent<T1, T2> gameEvent, Action<T1, T2> action) 
            => gameEvent.EventOnInvoked -= action;
    }
}