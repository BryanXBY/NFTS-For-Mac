using System;
using System.Collections.Generic;
using System.Text;

namespace TextTools
{
    class Tools
    {
        /// <summary>
        /// 取文本左边内容
        /// </summary>
        /// <param name="str">文本</param>
        /// <param name="s">标识符</param>
        /// <returns>左边内容</returns>
        public static string GetLeft(string str, string s)
        {
            string temp = str.Substring(0, str.IndexOf(s));
            return temp;
        }


        /// <summary>
        /// 取文本右边内容
        /// </summary>
        /// <param name="str">文本</param>
        /// <param name="s">标识符</param>
        /// <returns>右边内容</returns>
        public static string GetRight(string str, string s)
        {
            string temp = str.Substring(str.IndexOf(s), str.Length - str.Substring(0, str.IndexOf(s)).Length);
            return temp;
        }

        /// <summary>
        /// 取文本中间内容
        /// </summary>
        /// <param name="str">原文本</param>
        /// <param name="leftstr">左边文本</param>
        /// <param name="rightstr">右边文本</param>
        /// <returns>返回中间文本内容</returns>
        public static string Between(string str, string leftstr, string rightstr)
        {
            int i = str.IndexOf(leftstr) + leftstr.Length;
            string temp = str.Substring(i, str.IndexOf(rightstr, i) - i);
            return temp;
        }


        /// <summary>
        /// 取文本中间到List集合
        /// </summary>
        /// <param name="str">文本字符串</param>
        /// <param name="leftstr">左边文本</param>
        /// <param name="rightstr">右边文本</param>
        /// <returns>List集合</returns>
        public List<string> BetweenArr(string str, string leftstr, string rightstr)
        {
            List<string> list = new List<string>();
            int leftIndex = str.IndexOf(leftstr);//左文本起始位置
            int leftlength = leftstr.Length;//左文本长度
            int rightIndex = 0;
            string temp = "";
            while (leftIndex != -1)
            {
                rightIndex = str.IndexOf(rightstr, leftIndex + leftlength);
                if (rightIndex == -1)
                {
                    break;
                }
                temp = str.Substring(leftIndex + leftlength, rightIndex - leftIndex - leftlength);
                list.Add(temp);
                leftIndex = str.IndexOf(leftstr, rightIndex + 1);
            }
            return list;
        }


        /// <summary>
        /// 指定文本倒序
        /// </summary>
        /// <param name="str">文本</param>
        /// <returns>倒序文本</returns>
        public static string StrReverse(string str)
        {
            char[] chars = str.ToCharArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < chars.Length; i++)
            {
                sb.Append(chars[chars.Length - 1 - i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 取指定文本
        /// </summary>
        /// <param name="a">源文本</param>
        /// <param name="num">第几个字符</param>
        /// <returns></returns>
        public static string GetStringOfNum (string a,int num)
        {
            a = a.Substring(0, num);
            return a;

        }

    }
}