using System;
using System.IO;
using System.Diagnostics;
using System.Runtime;
using System.Net;

namespace MCNFTS
{
    class Program
    {
        static string ProgrmPath;
        static string NftsName,yesorno;

        static string osid = System.Environment.OSVersion.Platform.ToString();
        static int sz = Getddnum();
        static string[] Dname = new string[sz];  //Drivername数组
        static int Drivernum=-1;
        static void Main(string[] args)
        {
            ProgrmPath = Directory.GetCurrentDirectory();

                start();
                return;
   
        }
        static int Getddnum()
        {
            DriveInfo[] allDD = DriveInfo.GetDrives();
            return allDD.Length;

        }

        static void start()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen; //
            Console.WriteLine("         Bryan MacOS NTFS 写入权限获取工具");
            Console.WriteLine("                   ver1.0");
            Console.WriteLine(@"    https://github.com/BryanXBY/NFTS-For-Mac");
            Console.WriteLine("                  本机共有磁盘数："+sz.ToString());
            Console.ResetColor(); //将控制台的前景色和背景色设为默认值
            Console.WriteLine(" ");
            Console.WriteLine("==========================================================");
            Console.Write("请输入NTFS的");
            Console.ForegroundColor = ConsoleColor.Cyan; //
            Console.Write("磁盘名称（卷标）");
            Console.ResetColor();
            Console.WriteLine("添加对应磁盘的写入权限。\n如果你不知道卷标名称");
            Console.Write("可以输入");
            Console.ForegroundColor = ConsoleColor.Red; //
            Console.Write("help");
            Console.ResetColor();
            Console.Write("以查看本机系统内的所有磁盘卷标。");
            Console.WriteLine(" ");
            Console.WriteLine("输入exit退出程序");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("直接按回车可以自动添加已插入U盘的权限，但可能重复添加。");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed; //
            Console.WriteLine("警告:如果你的卷标名为help的话，请输入#help来将help作为卷标!");
            Console.ResetColor();
            Console.WriteLine("==========================================================");
            Console.WriteLine(" ");
            Console.Write("请输入：");
            Console.ForegroundColor = ConsoleColor.Blue; //
            NftsName = Console.ReadLine();
            Console.WriteLine(" ");
            Console.ResetColor();
            if (NftsName == "help" || NftsName == "/help" || NftsName == @"\help")
            {
                Console.ForegroundColor = ConsoleColor.Blue; 
                Console.WriteLine("                 卷标查看");
                Console.WriteLine(" ");
                Console.ResetColor();
                Console.WriteLine("程序运行目录为：");
                Console.WriteLine(ProgrmPath);
                Console.WriteLine(" ");
                Console.WriteLine("系统存在下列卷标(黄色字体部分)：");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                GetDrive();
                Console.ResetColor();
                Console.ReadKey();
                    start();
                    return;
            }
            if (NftsName == "exit")
            {
                exit();
                return;

            }
            if (NftsName == "")
            {
                auto();
                return;
            }
            else
            {
                if (NftsName == "#help")
                {
                    help2();
                    return;
                }
                else

                {
                    if(NftsName !="")
                    {
                        if (osid == "Win32NT")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed; //
                            Console.WriteLine("\n\n\n\n当前系统并不需要本程序，本程序只针对OSX和Linux！\n\n\n\n");
                            Console.ResetColor();
                            Console.ReadKey();
                            start();
                            return;
                        }
                        string a1 = "LABEL=" + NftsName + " none ntfs rw,auto,nobrowse";
                        JBlogin(a1);

                    }
                    Console.ReadKey();
                    start();
                    return;
                }

            }


        }

        static void JBlogin(string sss)
        {
            Console.ForegroundColor = ConsoleColor.Blue; //
            Console.WriteLine("                 激活权限");
            Console.WriteLine(" ");
            Console.ResetColor();
            Console.WriteLine("正在激活卷标：" + NftsName + "的写入权限！");
            Console.ReadKey();
            bool okme = false;
            bool fileok = File.Exists(ProgrmPath + "/log");
            string rwlist = sss;
            if (fileok == true)
            {
                //文档存在
                string text = System.IO.File.ReadAllText(ProgrmPath + "/log");
                int allnum = text.IndexOf(rwlist);
                if (allnum != -1)
                {
                    okme = true;
                }
                else
                {
                    okme = false;
                }
            }

            if (okme == true)
            {
                Console.WriteLine("已经为" + NftsName + "添加过写入权限！");
                Console.ReadKey();
            } else
            {

                if (fileok == true)
                {
                    string rss = System.IO.File.ReadAllText(ProgrmPath + "/log");
                    rss = rss + System.Environment.OSVersion.Platform.ToString(); 
                    rss= rss + rwlist;
                    byte[] data = System.Text.Encoding.Default.GetBytes(rwlist);
                    FileStream fs = new FileStream(@"/etc/fstab", FileMode.Create);
                    fs.Write(data, 0, data.Length);
                    fs.Close();
                    byte[] data2 = System.Text.Encoding.Default.GetBytes(rss);
                    FileStream log = new FileStream(ProgrmPath + "/log", FileMode.Open);
                    log.Write(data2, 0, data2.Length);
                    log.Close();
                    Console.WriteLine("激活"+NftsName+"权限完成！");
                   

                }
                else
                {
                    byte[] data = System.Text.Encoding.Default.GetBytes(rwlist);
                    FileStream fs = new FileStream(@"/etc/fstab", FileMode.Create);
                    fs.Write(data, 0, data.Length);
                    fs.Close();
                    FileStream log = new FileStream(ProgrmPath + "/log", FileMode.Create);
                    log.Write(data, 0, data.Length);
                    log.Close();
                    Console.WriteLine("激活" + NftsName + "权限完成！");

                }



            }

            return;
        }
        static void help2()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed; //
            Console.WriteLine("检测到输入了#help!");
            Console.WriteLine("请问你实际想要输入的是help还是#help");
            Console.ResetColor();
            Console.WriteLine("输入Y以选择输入help,输入N以选择输入#help");
            yesorno = Console.ReadLine();
            Console.WriteLine(yesorno);
            if (yesorno == "Y" || yesorno == "y")
            {
                NftsName = "help";
                string a1 = "LABEL=" + NftsName + " none ntfs rw,auto,nobrowse";
                JBlogin(a1);
                return;

            }
            if (yesorno == "N" || yesorno == "n")
            {
                NftsName = "#help";
                string a1 = "LABEL=" + NftsName + " none ntfs rw,auto,nobrowse";
                JBlogin(a1);
                return;
            }
            if (yesorno != "Y" || yesorno != "N" || yesorno != "y" || yesorno != "n")
            {
                help2();
                return;
            }
           
            
        }

        static string listdoit()
        {
            
            if (osid == "Win32NT")
            {
                return "error";
            }
            Console.WriteLine("工作模式：" + osid);
            string shell = ProgrmPath+"/list.sh";
            Process process = new Process();
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.ErrorDialog = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = shell;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.WorkingDirectory = ProgrmPath;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string aaa = output;
            process.WaitForExit();
            process.Close();
            return aaa;
        }
        static void editNO()
        { 
        
        
        
        }
        static void exit()
        {
            Console.WriteLine("输入Y退出程序，输入N返回主菜单,再次输入exit将把exit作为卷标名。");
            Console.Write("请输入：");
            string cintxit = Console.ReadLine();
            if (cintxit == "Y" || cintxit == "y")
            {
                over();
                return;
            }
            if (cintxit == "N" || cintxit == "n")
            {
                start();
                return;
            }
            if (cintxit == "exit")
            {
                NftsName = "exit";

                string a1 = "LABEL=" + NftsName + " none ntfs rw,auto,nobrowse";
                JBlogin(a1);
                return;
            }
        }

        static void auto()
        {
            Console.WriteLine("自动处理中！");
            string a1=" ";
            string osid = System.Environment.OSVersion.Platform.ToString();
            if (osid == "Win32NT")
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; //
                Console.WriteLine("\n\n\n\n当前系统并不需要本程序，本程序只针对OSX和Linux！\n\n\n\n");
                Console.ResetColor();
                Console.ReadKey(); 
                start();
                return;
            }
            GetDrive();

            for (int counter = 0; counter <= Drivernum; counter++)
            {
                Console.WriteLine("正在处理磁盘>id:" + counter.ToString() + "  name:" + Dname[counter]);
                if (Dname[counter] == "该磁盘没有卷标")
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("磁盘ID：" + counter.ToString() + "处理失败，请设置一个卷标后再试。");
                    Console.ResetColor();
                }
                if (Dname[counter] != "该磁盘没有卷标")
                {
                    if (a1 == " ")
                    {
                        a1 = "LABEL=" + Dname[counter] + " none ntfs rw,auto,nobrowse";
                    }
                    else
                    { 
                        a1= a1+ System.Environment.NewLine.ToString()+ "LABEL=" + Dname[counter] + " none ntfs rw,auto,nobrowse";
                    }
                }

            }

            JBlogin(a1);
            Console.WriteLine("\n\n\n\n任务处理完成！\n\n\n\n");
            Console.ReadKey();
            Console.WriteLine("执行任务记录如下：");
            Console.WriteLine(a1);
            Console.WriteLine("按任意键回到主菜单");
            Console.ReadKey();
            start();
            return;

        }
        /// <summary>
        /// 获取磁盘信息
        /// </summary>
        static void GetDrive()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  Drive type: {0}", d.DriveType);
                Console.ResetColor();
                if (d.IsReady == true)
                {
                    string ddname = d.VolumeLabel.ToString();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("  Volume label（卷标）: {0}", d.VolumeLabel);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("  File system（磁盘格式）: {0}", d.DriveFormat);
                    if (d.DriveFormat.ToString()=="NTFS")
                    {
                        Drivernum = Drivernum + 1;
                        if (ddname == "")
                        {
                            ddname = "该磁盘没有卷标";
                            Dname[Drivernum] = ddname;
                        }
                        Dname[Drivernum] = ddname ;

                    }
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(
                        "  Available space to current user:{0, 15} bytes",
                        d.AvailableFreeSpace);

                    Console.WriteLine(
                        "  Total available space:          {0, 15} bytes",
                        d.TotalFreeSpace);

                    Console.WriteLine(
                        "  Total size of drive:            {0, 15} bytes ",
                        d.TotalSize);
                    Console.ResetColor();
                }
              
            }
            if (Drivernum >= 0)
            {
                int allnum = Drivernum + 1;
                Console.WriteLine("一共找到：" + allnum.ToString() + "个NTFS的磁盘，分别如下：");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                for (int counter = 0; counter <= Drivernum ; counter++)
                {
                    Console.WriteLine(">id:"+counter.ToString()+"  name:"+Dname[counter]);
                }
                Console.ResetColor();
            }
        }


        //没有下文后 程序会退出，所以放最后面！
        static void over()
        {
            Console.WriteLine("Good Bye!");

        }


    }
}
