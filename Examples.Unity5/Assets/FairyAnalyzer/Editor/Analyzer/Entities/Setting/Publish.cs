using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FairyAnalyzer.Setting
{
    /// <summary>
    /// 代码生成设置
    /// </summary>
    public class CodeGeneration
    {
        /// <summary>
        /// 类名前缀
        /// </summary>
        public string classNamePrefix { get; set; }

        /// <summary>
        /// 代码输出路径
        /// </summary>
        public string codePath { get; set; }

        /// <summary>
        /// 代码类型
        /// </summary>
        public string codeType { get; set; }

        /// <summary>
        /// 是否使用名称获取成员对象
        /// </summary>
        public bool getMemberByName { get; set; }

        /// <summary>
        /// 不生成使用默认名称的成员
        /// </summary>
        public bool ignoreNoname { get; set; }

        /// <summary>
        /// 成员名称前缀
        /// </summary>
        public string memberNamePrefix { get; set; }

        /// <summary>
        /// 包名称
        /// </summary>
        public string packageName { get; set; }
    }

    /// <summary>
    /// FairyGUI Editor的全局发布设置
    /// </summary>
    public class Publish
    {
        public CodeGeneration codeGeneration { get; set; }
        public bool compressDesc { get; set; }
        public string fileExtension { get; set; }
        public int packageCount { get; set; }
        public string path { get; set; }
    }
}
