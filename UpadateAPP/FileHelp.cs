using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpadateAPP
{
    public class FileHelp
    {
        string RootPath = System.IO.Directory.GetCurrentDirectory();
        public FileHelp()
        {
            Dir = new DirectoryInfo(RootPath);
        }
        public FileHelp(string path)
        {
            RootPath = path;
            Dir = new DirectoryInfo(RootPath);
        }
        DirectoryInfo Dir;
        List<string> pathList = new List<string>();

        public List<string> GetFileNamePath(string filename)
        {
            return GetFileNamePathRecursive(filename, Dir.FullName);
        }
        /// <summary>
        /// 递归获取复合条件的目录List
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<string> GetFileNamePathRecursive(string filename, string path)
        {
            DirectoryInfo DirCurrent = new DirectoryInfo(path);
            if (DirCurrent.GetDirectories().Count()<1)
            {
                return pathList;
            }
            else
            {
                foreach (DirectoryInfo item in DirCurrent.GetDirectories())
                {
                    if (item.FullName.EndsWith(filename))
                    {
                        pathList.Add(item.FullName);
                    }
                    GetFileNamePathRecursive(filename, item.FullName);
                }
                return pathList;
            }

        }
    }
}
