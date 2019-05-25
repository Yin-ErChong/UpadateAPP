using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpadateAPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            OldFilePath.Text = @"BiHu.BaoXian.Service\lib\artificial";
        }

        private void BeginUpadate_Click(object sender, EventArgs e)
        {
            try
            {
                FileHelp fileHelp = new FileHelp();
                List<string> filePathList = fileHelp.GetFileNamePath(OldFilePath.Text);


                foreach (string item in filePathList)
                {
                    MessageBox.Show(item);
                    DirectoryInfo DirNew = new DirectoryInfo(NewFilePath.Text);
                    if (DirNew.GetFiles().Count() < 0)
                    {
                        MessageBox.Show("读取更新文件为空");
                        return;
                    }
                    foreach (var file in DirNew.GetFiles())
                    {
                        File.Copy(file.FullName, item+ @"\"+file.Name, true);
                    }
                }
                MessageBox.Show("覆盖完成");
            }
            catch (Exception ex)
            {

                MessageBox.Show("覆盖发生异常"+ ex.ToString());
            }

        }
        /// <summary>
        /// 复制文件到指定目录并重命名
        /// </summary>
        /// <param name="sourcePaths">要复制的文件路径集合</param>
        /// <param name="targetDir">目标目录</param>
        /// <returns>Item1:对应路径，Item2:失败文件路径</returns>
        public static Tuple<Dictionary<string, string>, List<string>> CopyFileToDir(List<string> sourcePaths, string targetDir)
        {
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }
            var errorFiles = new List<string>();
            var saveDirs = new Dictionary<string, string>();
            sourcePaths.ForEach(item =>
            {
                //路径不存在或者路径已存在则失败
                if (!File.Exists(item) || saveDirs.ContainsKey(item))
                {
                    errorFiles.Add(item);
                }
                else
                {
                    var saveName = Guid.NewGuid() + Path.GetExtension(item);
                    var savePath = Path.Combine(targetDir, saveName);
                    File.Copy(item, savePath);
                    saveDirs.Add(item, savePath);
                }
            });
            var result = new Tuple<Dictionary<string, string>, List<string>>(saveDirs, errorFiles);
            return result;
        }
    }
}
