using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace DDGS
{
    internal class Utils
    {
        static string[] USERAGENTS = new string[4] {
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 14_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36",
        };

        internal static string GetUA()
        {
            Random rd = new Random();
            return USERAGENTS[rd.Next(USERAGENTS.Length)];
        }

        internal static string extractVQD(string html_bytes)
        {
            string c1, c2;
            int start, end;
            try
            {
                c1 = "vqd=\""; c2 = "\"";
                start = html_bytes.IndexOf(c1) + c1.Length;
                end = html_bytes.IndexOf(c2, start);
                Debug.Assert(start != -1 && end != -1);
                return html_bytes.Substring(start, end - start);
            }
            catch (Exception ex)
            {

            }
            try
            {
                c1 = "vqd="; c2 = "&";
                start = html_bytes.IndexOf(c1) + c1.Length;
                end = html_bytes.IndexOf(c2, start);
                Debug.Assert(start != -1 && end != -1);
                return html_bytes.Substring(start, end - start);
            }
            catch (Exception ex)
            {

            }
            try
            {
                c1 = "vqd='"; c2 = "'";
                start = html_bytes.IndexOf(c1) + c1.Length;
                end = html_bytes.IndexOf(c2, start);
                Debug.Assert(start != -1 && end != -1);
                return html_bytes.Substring(start, end - start);
            }
            catch (Exception ex)
            {

            }
            throw new Exception("extractVQD fail");
        }

        static Regex reg = new Regex("<.*?>");
        internal static string normalize(string text)
        {
            text = reg.Replace(text, "");
            text = Uri.UnescapeDataString(text);
            return text;
        }

        internal static string normalizeUrl(string text)
        {
            text = text.Substring(1, text.Length - 2);
            text = text.Replace(" ", "+");
            return text;
        }
    }
}
