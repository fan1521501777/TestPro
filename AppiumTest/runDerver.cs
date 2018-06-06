using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AppiumTest
{
    public partial class runDerver : Form
    {
        AndroidDriver<IWebElement> appiumDriver = null;
        /// <summary>
        /// 端口占用列表
        /// </summary>
        public static List<int> Ports = new List<int>();
        /// <summary>
        /// 设备ID与端口映射表
        /// </summary>
        public static Dictionary<string, List<int>> AppiumPorts = new Dictionary<string, List<int>>();
        public runDerver()
        {
            InitializeComponent();
        }

        private void btnStrat_Click(object sender, EventArgs e)
        {
            DesiredCapabilities capability = new DesiredCapabilities();

            //capability.SetCapability("appPackage", "com.tencent.mm");
            //capability.SetCapability("appActivity", ".ui.LauncherUI");
            //capability.SetCapability(MobileCapabilityType.AutomationName, "uiautomator2");
            //capability.SetCapability("noReset", true);
            //capability.SetCapability("udid", "4e25335d");
            //capability.SetCapability(MobileCapabilityType.DeviceName, "device");
            //capability.SetCapability("newCommandTimeout", "1800");
            //capability.SetCapability("platformName", "Android");
            //capability.SetCapability("autoLaunch", "true");
            //capability.SetCapability("androidDeviceReadyTimeout", "3000");
            //capability.SetCapability("unicodeKeyboard", "true");
            //capability.SetCapability("resetKeyboard", "true");
            //capability.SetCapability("deviceReadyTimeout", "3000");
            ////capability.SetCapability("uiautomator", "uiautomator2");
            //capability.SetCapability("uiautomator", "appium");
            ////capability.SetCapability("platformVersion", "17");

            // ADB获取当前运行程序的appPackage与appActivity命令：adb shell dumpsys activity | findstr "mFocusedActivity"
            capability.SetCapability("appPackage", "com.greatwall.nydzy_m");//APP名 com.tencent.mm IOS  com.tencent.xin
            capability.SetCapability("appActivity", "com.greatwall.nydzy_m.MainActivity");
            capability.SetCapability("newCommandTimeout", 2200);
            capability.SetCapability("automationName", "uiautomator2");//uiautomator2  Appium
            capability.SetCapability("noReset", true);
            capability.SetCapability("udid", "98895a423635584f51");// 98895a423635584f51  MDGDU16406003944
            capability.SetCapability("deviceName", "device");
            capability.SetCapability("platformName", "Android");
            capability.SetCapability("autoLaunch", true);
            capability.SetCapability("androidDeviceReadyTimeout", "10000");
            capability.SetCapability("unicodeKeyboard", true);
            capability.SetCapability("resetKeyboard", true);
            //capability.SetCapability("uiautomator", "uiautomator2");
            capability.SetCapability("deviceReadyTimeout", 10000);
            capability.SetCapability("platformVersion", "7");
            capability.SetCapability(AndroidMobileCapabilityType.IgnoreUnimportantViews, true);
            //ChromeOptions options = new ChromeOptions();
            //options.AddAdditionalCapability("androidProcess", "com.tencent.mm:tools");
            //capability.SetCapability(ChromeOptions.Capability, options);

            DesiredCapabilities option = new DesiredCapabilities();
            option.SetCapability("androidProcess", "com.tencent.mm:tools");
            capability.SetCapability(ChromeOptions.Capability, option.ToDictionary());

            string url = FormatAppiumUrl("http://127.0.0.1:4724/wd/hub", "98895a423635584f51");//本机
            //string url = FormatAppiumUrl("http://10.3.250.216:4723/wd/hub", "0e31f9fc431a5cc807acba1b67491413a177941a");//苹果MAC 远程IP

            appiumDriver = new AndroidDriver<IWebElement>(new Uri(url), capability, TimeSpan.FromMinutes(1));
        }

        /// <summary>
        /// 格式化Appium地址，对于本地地址，会自动启动新的appium服务，并改变地址
        /// </summary>
        /// <param name="strBaseUrrl">Appium服务器地址</param>
        /// <param name="udid">设备编号</param>
        /// <returns>Appium服务地址</returns>
        private string FormatAppiumUrl(string strBaseUrrl, string udid)
        {
            if (!string.IsNullOrEmpty(strBaseUrrl))
            {
                if (strBaseUrrl.ToLower().StartsWith("http://127.0.0.1") || strBaseUrrl.ToLower().StartsWith("http://localhost"))
                {
                    if (!string.IsNullOrEmpty(txtAppiumPath.Text) && Directory.Exists(txtAppiumPath.Text))
                    {
                        int iAppiumPort;
                        string cmd = Path.Combine(txtAppiumPath.Text, @"resources\app\node_modules\appium\build\lib\") + GetAppiumArguments(udid, out iAppiumPort);
                        Process process = new Process();
                        process.StartInfo.FileName = txtNodePath.Text;
                        process.StartInfo.Arguments = cmd;
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.UseShellExecute = false;
                        try
                        {
                            process.Start();
                            strBaseUrrl = string.Format("http://127.0.0.1:{0}/wd/hub", iAppiumPort);

                            bool isLoad = WaitAppiumServerLoaded(iAppiumPort, 60000);
                        }
                        catch { }
                    }
                }
            }
            Thread.Sleep(1000);
            return strBaseUrrl;
        }

        /// <summary>
        /// 获取Appium服务器启动参数，包括端口信息，启动入口等信息
        /// </summary>
        /// <param name="udid">设备编号</param>
        /// <param name="iAppiumPort">Appium服务器端口</param>
        /// <returns>Appium服务器启动参数</returns> 
        private string GetAppiumArguments(string udid, out int iAppiumPort)
        {
            iAppiumPort = GetUsablePort(Ports, 4723);
            int iSelendroidPort = GetUsablePort(Ports, iAppiumPort + 1);
            int iChromedriverPort = GetUsablePort(Ports, iSelendroidPort + 1);
            int webdriveragentPort = GetUsablePort(Ports, iChromedriverPort + 1);
            //int iBootstrapPort = GetUsablePort(Ports, iSelendroidPort + 1);
            //int iBootstrapPort = Framework.Helper.AndroidHelper.GetUsablePort(udid, 4724);
            int iBootstrapPort = 4724;
            lock (AppiumPorts)
            {
                AppiumPorts[udid] = new List<int>();
                AppiumPorts[udid].Add(iAppiumPort);
                AppiumPorts[udid].Add(iSelendroidPort);
                AppiumPorts[udid].Add(iChromedriverPort);
                AppiumPorts[udid].Add(webdriveragentPort);
            }
            lock (Ports)
            {
                Ports.AddRange(AppiumPorts[udid]);
            }
            string strArguments = string.Format(@"main.js  --address 127.0.0.1 --port {0} --bootstrap-port {1} --selendroid-port {2} --chromedriver-port {3} --webdriveragent-port {4} --platform-name Android  --platform-version 21 --automation-name Appium --log-no-color --log d:/appium.log", new object[] { iAppiumPort, iBootstrapPort, iSelendroidPort, iChromedriverPort, webdriveragentPort });
            return strArguments;
        }

        public int GetUsablePort(List<int> usedPorts, int StartPort = 1, int EndPort = 65535)
        {
            int iPort = StartPort;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            List<int> ipEndPoints = ipProperties.GetActiveTcpListeners().Select<IPEndPoint, int>(p => p.Port).ToList();
            ipEndPoints.AddRange(ipProperties.GetActiveUdpListeners().Select<IPEndPoint, int>(p => p.Port));
            for (iPort = StartPort; iPort <= EndPort; iPort++)
            {
                if (!ipEndPoints.Contains(iPort) && !usedPorts.Contains(iPort))
                {
                    return iPort;
                }
            }

            return -1;
        }
        /// <summary>
        /// 等待Appium服务器启动完成
        /// </summary>
        /// <param name="iAppiumPort">Appium服务占用端口</param>
        /// <param name="iTimeout">超时时间（毫秒）</param>
        /// <returns>等待结果，在超时时间内为true否则为false</returns>
        private bool WaitAppiumServerLoaded(int iAppiumPort, int iTimeout)
        {
            DateTime dtStart = DateTime.Now;
            while ((DateTime.Now - dtStart).TotalMilliseconds <= iTimeout)
            {
                if (PortInUse(iAppiumPort))
                {
                    return true;
                }
                Thread.Sleep(200);
            }
            return false;
        }

        public bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            List<int> ipEndPoints = ipProperties.GetActiveTcpListeners().Select<IPEndPoint, int>(p => p.Port).ToList();
            ipEndPoints.AddRange(ipProperties.GetActiveUdpListeners().Select<IPEndPoint, int>(p => p.Port));
            if (ipEndPoints.Contains(port))
            {
                inUse = true;
            }

            return inUse;
        }

        private void btnGetPageSource_Click(object sender, EventArgs e)
        {

            if (appiumDriver != null)
            {

                rtxtPageSource.AppendText(FormatXml(appiumDriver.PageSource));
            }
        }

        private string FormatXml(string sUnformattedXml)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sUnformattedXml);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xtw.Indentation = 1;
                xtw.IndentChar = ' ';
                xd.WriteTo(xtw);
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
            }
            return sb.ToString();
        }
    }
}
