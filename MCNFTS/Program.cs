using System;
using System.IO;
using System.Diagnostics;
using System.Runtime;

namespace MCNFTS
{
    class Program
    {
        static string ProgrmPath;
        static string NftsName,yesorno;

        static void Main(string[] args)
        {
            ProgrmPath = Directory.GetCurrentDirectory();
            string username = System.Environment.UserName.ToString();
            bool first = true;
            string userpd = ProgrmPath +"/start.txt";
            first = File.Exists(userpd);
            if (first == true)
            {
                //非首次运行直接启动
                start();
                return;
            }
            if (first == false)
            {
                //首次运行生成启动脚本
                Console.WriteLine("初次运行会将在程序根目录生成一个启动命令的记事本，名称为start.txt");
                Console.WriteLine("你可以打开终端，并复制里面的内容来启动，以防权限不足！");
                Console.ReadKey();
                Console.WriteLine(@"本程序完全免费,发布于https://github.com/BryanXBY/NFTS-For-Mac");
                string meg = "cd " + ProgrmPath + System.Environment.NewLine.ToString();
                meg = meg+ "sudo chmod -x list.sh" + System.Environment.NewLine.ToString();
                meg = meg + "sudo chmod 777 list.sh" + System.Environment.NewLine.ToString();
                meg = meg + "sudo chmod - x MCNFTS" + System.Environment.NewLine.ToString();
                meg = meg + "sudo chmod 777 MCNFTS" + System.Environment.NewLine.ToString();
                meg = meg + "sudo ./MCNFTS" ;
                byte[] data = System.Text.Encoding.Default.GetBytes(meg);
                FileStream fs = new FileStream(ProgrmPath+"/start.txt", FileMode.Create);
                fs.Write(data, 0, data.Length);
                fs.Close();
                start();
                return;
            }
            
           
        }

        static void start()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen; //
            Console.WriteLine("         Bryan MacOS NTFS 写入权限获取工具");
            Console.WriteLine("                   ver1.0");
            Console.WriteLine(@"    https://github.com/BryanXBY/NFTS-For-Mac");
            Console.ResetColor(); //将控制台的前景色和背景色设为默认值
            Console.WriteLine(" ");
            Console.WriteLine("==========================================================");
            Console.Write("请输入NFTS的");
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
                string msgout = listdoit();
                if (msgout != "error")
                {
                    Console.WriteLine(msgout);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("如果你有看到类似No such file or directory的提示！");
                    Console.WriteLine("这说明载入shell出错，请将程序目录下的list.sh复制到:" + ProgrmPath + " / 下。\n或者复制程序目录下的start.txt，然后在终端中粘贴并按回车使用！");
                    Console.ResetColor();
                }
                else { 
                Console.WriteLine("当前系统并不需要本程序即可支持NFTS格式");
                }
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
                    JBlogin();
                    return;
                }

            }


        }

        static void JBlogin()
        {
            Console.ForegroundColor = ConsoleColor.Blue; //
            Console.WriteLine("                 激活权限");
            Console.WriteLine(" ");
            Console.ResetColor();
            Console.WriteLine("正在激活卷标：" + NftsName + "的写入权限！");
            Console.ReadKey();
            bool okme = false;
            bool fileok = File.Exists(ProgrmPath + "/log");
            string rwlist = "LABEL=" + NftsName + " none ntfs rw,auto,nobrowse";
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
                    Console.WriteLine("激活权限完成！");
                    Console.ReadKey();

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
                    Console.WriteLine("激活权限完成！");
                    Console.ReadKey();

                }



            }


            start();
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
                JBlogin();
                return;

            }
            if (yesorno == "N" || yesorno == "n")
            {
                NftsName = "#help";
                JBlogin();
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
            string osid = System.Environment.OSVersion.Platform.ToString();
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
                JBlogin();
                return;
            }
        }

        static void auto()
        {
            Console.WriteLine("自动处理中！");
            string diskmenu = listdoit();
            if (diskmenu == "error")
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; //
                Console.WriteLine("\n\n\n\n当前系统并不需要本程序，本程序只针对OSX和Linux！\n\n\n\n");
                Console.ResetColor();
                Console.ReadKey();
                start();
                return;
            }
            diskmenu = diskmenu.Replace("Macintosh HD", "");
            if (diskmenu != "")
            {
                int allnum = diskmenu.IndexOf(" ");
                if (allnum == -1)
                {
                    Console.WriteLine("发现磁盘：" + diskmenu);
                    Console.ReadKey();
                    diskmenu= diskmenu.Replace(System.Environment.NewLine.ToString(), "");
                    NftsName = diskmenu;
                    if (diskmenu != "")
                    {
                        JBlogin();
                    }
                    else 

                    {
                        Console.WriteLine("请插入U盘后重试");
                        Console.ReadKey();
                    }

                    return;
                }if (allnum != -1)
                {
                    Console.WriteLine("多个磁盘请人工操作，以免录入出错！");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("如果你有看到类似No such file or directory的提示！");
                    Console.WriteLine("这说明载入shell出错，请将程序目录下的list.sh复制到:" + ProgrmPath + " / 下。\n或者复制程序目录下的start.txt，然后在终端中粘贴并按回车使用！");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.WriteLine(diskmenu);
                    start();
                    return;
                }


            }
            else
            {
                Console.WriteLine("未检测到除了Macintosh HD外的其它磁盘");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("如果你有看到类似No such file or directory的提示！");
                Console.WriteLine("这说明载入shell出错，请将程序目录下的list.sh复制到:" + ProgrmPath + " / 下。\n或者复制程序目录下的start.txt，然后在终端中粘贴并按回车使用！");
                Console.ResetColor();
                Console.ReadKey();
            }
            start();
            return;

        }

        //没有下文后 程序会退出，所以放最后面！
        static void over()
        {
            Console.WriteLine("Good Bye!");

        }


    }
}
