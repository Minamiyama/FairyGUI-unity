using System.Collections.Generic;
using System.IO;

namespace FairyAnalyzer
{
    public class FileHelper
    {
        /// <summary>
        /// 获取指定目录下的所有子文件夹
        /// </summary>
        /// <param name="_info"></param>
        /// <param name="_list"></param>
        public static void GetAllDirBySub(FileSystemInfo _info, List<DirectoryInfo> _list)
        {
            if (!_info.Exists)
                return;
            DirectoryInfo dir = _info as DirectoryInfo;
            //不是目录 
            if (dir == null)
                return;
            FileSystemInfo[] files = dir.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                DirectoryInfo dirInfo = files[i] as DirectoryInfo;

                if (null != dirInfo)
                {
                    _list.Add(dirInfo);
                }
            }
        }
    }
}