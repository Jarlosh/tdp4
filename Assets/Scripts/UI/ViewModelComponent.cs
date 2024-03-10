using UnityEngine;

namespace TDPF.UI
{
    public abstract class ViewModelComponent<T>: MonoBehaviour
    {
        public T Model { get; protected set; }

        public bool IsVisible => Parent.activeSelf;

        protected virtual GameObject Parent => gameObject;

        public virtual void ShowOrUpdate(T data)
        {
            if(IsVisible)
            {
                ClearData();
            }

            if (!IsVisible)
            {
                SetVisible(true);
            }
            Model = data;
            OnDataSet();
        }

        /// <summary>
        /// Sets data BUT don't change isVisible
        /// </summary>
        public void SetData(T data)
        {
            ClearData();
            Model = data;
            OnDataSet();
        }
        
        protected abstract void OnDataSet();

        public virtual void Hide()
        {
            if(IsVisible)
            {
                ClearData();
                SetVisible(false);
            }
        }

        private void SetVisible(bool state)
        {
            Parent.SetActive(state);
        }

        public virtual void ClearData()
        {
            Model = default;
        }
    }
}