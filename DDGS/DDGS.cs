using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace DDGS
{
    public class DDGS
    {
        #region Singleton Mode
        private static DDGS uniqueInstance;
        private static readonly object locker = new object();

        public static DDGS GetInstance()
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new DDGS();
                    }
                }
            }
            return uniqueInstance;
        }
        public static DDGS Default => GetInstance();
        #endregion

        #region Constructor
        public DDGS()
        {
            client = new HttpClient(new HttpClientHandler()) { Timeout = new TimeSpan(0, 0, 10) };
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json, text/javascript, */*; q=0.01");
            client.DefaultRequestHeaders.UserAgent.ParseAdd(Utils.GetUA());
            client.DefaultRequestHeaders.Referrer = new Uri("https://duckduckgo.com/");
        }

        public DDGS(HttpClient client)
        {
            this.client = client;
        }
        #endregion

        #region Private
        private HttpClient client;

        private string getVQD(string keywords)
        {
            var body = new FormUrlEncodedContent(new Dictionary<string, string>() { { "q", keywords } });
            var res = client.PostAsync("https://duckduckgo.com", body).Result;
            var content = res.Content.ReadAsStringAsync().Result;
            return Utils.extractVQD(content);
        }
        #endregion

        /// <summary>
        /// DuckDuckGo text search generator. Query params: https://duckduckgo.com/params
        /// </summary>
        /// <param name="keywords">keywords for query.</param>
        /// <param name="region">wt-wt, us-en, uk-en, ru-ru, etc. Defaults to "cn-zh".</param>
        /// <param name="safesearch">on, moderate, off. Defaults to "moderate".</param>
        /// <param name="timelimit">d, w, m, y. Defaults to null.</param>
        /// <param name="max_results">max number of results. If null, returns results only from the first response. Defaults to null.</param>
        /// <returns></returns>
        public List<DDGSearchResult> Text(
            string keywords,
            string region = "cn-zh",
            string safesearch = "moderate",
            string timelimit = null,
            int? max_results = null
            )
        {
            if (keywords == null)
                throw new ArgumentNullException("keywords is null");

            var vqd = getVQD(keywords);
            if (vqd == null || vqd == "")
                throw new ArgumentNullException("vqd is empty");

            var query = new Dictionary<string, string>();
            query.Add("q", Uri.EscapeDataString(keywords));
            query.Add("kl", region);
            query.Add("l", region);
            query.Add("s", "0");
            query.Add("df", timelimit);
            query.Add("vqd", vqd);
            query.Add("o", "json");
            query.Add("sp", "0");

            switch(safesearch)
            {
                case "moderate":
                    query.Add("ex", "-1");
                    break;
                case "off":
                    query.Add("ex", "-2");
                    break;
                case "on":
                    query.Add("p", "1");
                    break;
            }

            var url = "https://links.duckduckgo.com/d.js" + UrlHelper.BuildQuery(query);

            var resp = client.GetAsync(url).Result;
            if (!resp.IsSuccessStatusCode)
                throw new Exception($"http code: {resp.StatusCode}");
            var raw = resp.Content.ReadAsStringAsync().Result;

            var json = JsonConvert.DeserializeObject<DDGSearchResDto>(raw);

            var page_data = json.Results;

            List<DDGSearchResult> res = new List<DDGSearchResult>();
            foreach (var row in page_data)
            {
                var href = row.U;
                if (href != null && href != $"http://www.google.com/search?q={keywords}")
                {
                    var body = row.A;
                    body = Utils.normalize(body);
                    if (body == null || body == "")
                        continue;
                    res.Add(new DDGSearchResult()
                    {
                        Title = Utils.normalize(row.T),
                        Href = Utils.normalizeUrl(href),
                        Body = body
                    });
                }
            }
            return res;
        }
    }
}
