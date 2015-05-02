/*
 * Created by SharpDevelop.
 * User: Gerald
 * Date: 2015/5/2
 * Time: 20:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Web;

namespace URLFetcher
{
	/// <summary>
	/// A simple HTTP request wrapper with cookie support.
	/// </summary>
	public class Fetcher
	{
		public Fetcher()
		{
			cookies = new CookieContainer();
		}
		CookieContainer cookies;
		public string UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36 OPR/26.0.1656.60";

		
		HttpWebRequest getRequest(string url) {
			var request = (HttpWebRequest) WebRequest.Create(url);
			request.CookieContainer = cookies;
			request.UserAgent = UserAgent;
			return request;
		}
		
		Result finishRequest(HttpWebRequest request) {
			request.Method = "GET";
			return new Result(request);
		}
		
		Result finishRequest(HttpWebRequest request, byte[] data) {
			request.Method = "POST";
			request.ContentLength = data.Length;
			var reqStream = request.GetRequestStream();
			reqStream.Write(data, 0, data.Length);
			reqStream.Close();
			return new Result(request);
		}
		
		Result finishRequest(HttpWebRequest request, string data) {
			return finishRequest(request, Encoding.UTF8.GetBytes(data));
		}
		
		public Result fetch(string url) {
			return finishRequest(getRequest(url));
		}
		
		public Result fetch(string url, string data) {
			return finishRequest(getRequest(url), data);
		}
		
		public Result fetch(string url, Dictionary<string, string> data) {
			var request = getRequest(url);
			request.ContentType = "application/x-www-form-urlencoded";
			var list = new List<string>();
			foreach(KeyValuePair<string, string> e in data)
				list.Add(string.Format("{0}={1}",
				                       HttpUtility.UrlEncode(e.Key, Encoding.UTF8),
				                       HttpUtility.UrlEncode(e.Value, Encoding.UTF8)));
			return finishRequest(request, string.Join("&", list.ToArray()));
		}
	}
}
