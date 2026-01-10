using System;
using MVVM.Model;
using UnityEngine;

namespace MVVM.View
{
    /// <summary>
    /// 基础数据管理类，主要处理数据和前端的通讯
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseViewModel<T> : MonoBehaviour where T : struct
    {
        private BaseModel<T> _model = null;
        [SerializeField]
        private BaseView<T> _view;

        public T Model
        {
            get
            {
                if(_model == null)
                    return default(T);
                return _model.Model;
            }
            set
            {
                if(_model == null)
                    return;
                _model.SetModel(value);
            }
        }

        private void OnEnable()
        {
            //显示时主动刷新一次页面
            _view?.OnRefreshView(_model.Model,_model.Model);
        }

        /// <summary>
        /// 绑定model
        /// </summary>
        /// <param name="model"></param>
        public void BindModel(BaseModel<T> model)
        {
            if (_model != null)
                UnBindModel();
            _model = model;
            _model.onValueChanged += OnSetModel;
        }

        /// <summary>
        /// 取消绑定model
        /// </summary>
        public void UnBindModel()
        {
            if(_model == null)
                return;
            _model.onValueChanged -= OnSetModel;
            _model = null;
        }

        protected virtual void OnDestroy()
        {
            UnBindModel();
        }

        protected virtual void OnSetModel(T newModel,T oldModel)
        {
            if(_view == null)
                return;
            var needRefreshView = _view.NeedRefreshView();
            if(needRefreshView)
                _view.OnRefreshView(newModel, oldModel);
        }
    }
}