using MVVM.Model;
using UnityEngine;

namespace MVVM.View
{
    public abstract class BaseView<T>  : MonoBehaviour where T : struct
    {
        /// <summary>
        /// 判断当前是否需要更新页面
        /// </summary>
        /// <returns></returns>
        public abstract bool NeedRefreshView();

        /// <summary>
        /// 数据刷新页面
        /// </summary>
        /// <param name="newModel"></param>
        /// <param name="oldModel"></param>
        public virtual void OnRefreshView(T newModel,T oldModel) { }
    }
}