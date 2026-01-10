namespace MVVM.Model
{
    /// <summary>
    /// 基础数据类
    /// </summary>
    public abstract class BaseModel<T>  where T : struct
    {
        protected bool _enable = false;
        protected T _model;
        // protected T _modelCache;
        public event ModelEvent<T> onValueChanged;

        /// <summary>
        /// 数据是否激活
        /// </summary>
        public bool Enable
        {
            get => _enable;
            set => _enable = value;
        }

        public T Model => _model;

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="model"></param>
        public void SetModel(T model)
        {
            if(!_enable)
                return;
            if(!NeedUpdateValue(model, _model))
                return;//数据不需要更新
            //通知更新数据
            onValueChanged?.Invoke(model, _model);
            _model = model;
        }


        /// <summary>
        /// 判断是否需要更新数据
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        protected abstract bool NeedUpdateValue(T newValue, T oldValue);
    }
    public delegate void ModelEvent<in T>(T oldValue, T newValue) where T : struct;

}