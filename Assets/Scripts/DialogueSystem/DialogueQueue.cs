using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Utils.GameEvents;
using Zenject;

namespace TDPF.FuckUp.DialogueSystem
{
    public interface IDialogueItem
    {
        CharacterItem Character { get; }
        string Text { get; }
        ItemSettings Settings { get; }
    }

    public interface IDialogueProcessor
    {
        void Process(DialogueController controller);
    }
    
    public interface IDialogueNode
    {
    }
    
    [UsedImplicitly]
    public class DialogueQueue
    {
        private readonly Queue<IDialogueProcessor> _queue = new();
        
        private readonly GameEvent<IDialogueNode> _onEnqueue = new();
        public IGameEvent<IDialogueNode> OnEnqueue => _onEnqueue;

        public int Count => _queue.Count;
        public bool Any => Count > 0;

        public IDialogueProcessor Dequeue()
        {
            return _queue.Dequeue();
        }

        public void Enqueue(IDialogueItem item)
        {
            _queue.Enqueue(new ShowProcessor(item));
            _onEnqueue?.Invoke(null);
        }

        public void Enqueue(Action callback)
        {
            _queue.Enqueue(MakeCallbackProcessor(callback));
            _onEnqueue?.Invoke(null);
        }

        private IEnumerable<IDialogueProcessor> ToProcessors(IDialogueNode node)
        {
            return node switch
            {
                DialogueStack stack => MakeStackProcessors(stack),
                IDialogueItem item => MakeShowProcessor(item),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private IEnumerable<IDialogueProcessor> MakeStackProcessors(DialogueStack stack)
        {
            foreach (var item in stack.GetItems())
            {
                yield return new ShowProcessor(item);
            }
        }

        private IEnumerable<IDialogueProcessor> MakeShowProcessor(IDialogueItem item)
        {
            yield return new ShowProcessor(item);
        }
        
        private IDialogueProcessor MakeCallbackProcessor(in Action action)
        {
            return new ActionProcessor(action);
        }
        
        private record ShowProcessor(IDialogueItem Item): IDialogueProcessor
        {
            public void Process(DialogueController controller) => controller.Play(Item);
        }

        private record ActionProcessor(in Action<DialogueController> Delegate): IDialogueProcessor
        {
            public ActionProcessor(Action action): this(Wrap(action))
            {
            }
            
            private static Action<DialogueController> Wrap(Action action)
            {
                return Callback;
                void Callback(DialogueController c)
                {
                    action?.Invoke();
                    c.Complete();
                }
            }

            private void JustComplete(DialogueController controller)
            {
                controller.Complete();
            }
            
            public void Process(DialogueController controller)
            {
                Delegate(controller);
            }
        }
    }
}