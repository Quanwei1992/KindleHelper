using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace KindleHelper.Plugin.Interface
{
    public interface IPlugin
    {
        /// <summary>
        /// 返回插件名称
        /// </summary>
        /// <returns>插件名称</returns>
        string Show();
        /// <summary>
        /// 显示窗体
        /// </summary>
        void ShowForm();
        /// <summary>
        /// 以对话框方式显示窗体
        /// </summary>
        void ShowFormAsDialog();
        /// <summary>
        /// 返回插件版本
        /// </summary>
        /// <returns>插件版本</returns>
        string version();
        /// <summary>
        /// 运行特定方法并返回结果
        /// </summary>
        /// <returns>结果</returns>
        object getresult();
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <returns>结果</returns>
        object run();
        /// <summary>
        /// 获取包含内部运行时间数据的对象
        /// </summary>
        /// <returns>包含内部运行时间数据的对象</returns>
        Stopwatch Watch();
        /// <summary>
        /// 确定是否需要以对话框方式显示窗体
        /// </summary>
        /// <returns>结果</returns>
        bool IsNotNeedToShowAsDialog();
    }
}
