using System;
namespace GrapchLibrary
{
    interface IGrapchHelper
    {
        /// <summary>
        /// 释放资源
        /// </summary>
        void Dispose();  
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        void Draw(int height, int width);
        /// <summary>
        /// 绘制对象
        /// </summary>
        System.Drawing.Graphics G { get; set; }
        /// <summary>
        /// 图片文件路径
        /// </summary>
        string Filepath { get; set; }
    }
}
