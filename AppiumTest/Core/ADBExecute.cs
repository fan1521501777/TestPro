using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumTest.Core
{
    public class ADBExecute
    {
        /// <summary>
        /// 截屏
        /// </summary>
        /// <param name="filePath">本地文件路径</param>
        /// <param name="deviceName">设备ID</param>
        /// <returns>返回设备的版本</returns>
        public static string GetScreenshot(string filePath, string deviceName = "")
        {
            if (!string.IsNullOrEmpty(deviceName.Trim()))
            {
                deviceName = " -s " + deviceName;
            }
            string cmd = @"adb {0} shell /system/bin/screencap -p /sdcard/screenshot.png";//（保存到SDCard）
            cmd = string.Format(cmd, deviceName);
            cmd = ExecuteDos(cmd);
            if (!string.IsNullOrEmpty(cmd))
            {
                return cmd;
            }
            cmd = @"adb {0} pull /sdcard/screenshot.png {1}";//（保存到电脑）
            cmd = string.Format(cmd, deviceName, filePath);
            cmd = ExecuteDos(cmd);
            return cmd;
        }

        /// <summary>
        /// 使用ADB发送点击事件
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="deviceName">移动端设备序列号</param>
        public static void ADBTap(int x, int y, string deviceName = "")
        {
            string cmd = @" shell input tap {1} {2}";
            if (string.IsNullOrEmpty(deviceName.Trim()))
                cmd = string.Format(cmd, deviceName, x, y);
            else
                cmd = string.Format(cmd, " -s " + deviceName, x, y);
            ExecuteDos(cmd);
        }

        #region 执行DOS
        public static string ExecuteDos(string dosCommand)
        {
            return Execute(dosCommand, 1000);
        }

        /// <summary>  
        /// 执行DOS命令，返回DOS命令的输出  
        /// </summary>  
        /// <param name="dosCommand">dos命令</param>  
        /// <param name="milliseconds">等待命令执行的时间（单位：毫秒），  
        /// 如果设定为0，则无限等待</param>  
        /// <returns>返回DOS命令的输出</returns>  
        public static string Execute(string command, int seconds)
        {
            string output = ""; //输出字符串  
            if (command != null && !command.Equals(""))
            {
                //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                Process process = new Process();//创建进程对象  
                                                //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                ProcessStartInfo startInfo = new ProcessStartInfo();
                //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                startInfo.FileName = "cmd.exe";//设定需要执行的命令  
                //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出  
                //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动  
                //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                startInfo.RedirectStandardInput = false;//不重定向输入  
                //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                startInfo.RedirectStandardOutput = true; //重定向输出  
                //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                startInfo.CreateNoWindow = true;//不创建窗口  
                ////Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                process.StartInfo = startInfo;
                //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                try
                {
                    //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                    if (process.Start())//开始进程  
                    {
                        //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                        if (seconds == 0)
                        {
                            //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                            process.WaitForExit();//这里无限等待进程结束  
                        }
                        else
                        {
                            //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                            process.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒  
                        }
                        //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出  
                    }
                }
                catch
                {
                    //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                }
                finally
                {
                    //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                    if (process != null)
                    {
                        //Console.WriteLine("命令:" + command + DateTime.Now.ToString("  HH:mm:ss.fff"));
                        process.Close();
                    }
                }
            }
            return output;
        }
        #endregion
    }
}
