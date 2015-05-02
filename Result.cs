/*
 * Created by SharpDevelop.
 * User: Gerald
 * Date: 2015/5/2
 * Time: 22:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.IO;
using System.Text;

namespace URLFetcher
{
	/// <summary>
	/// Result of Fetcher.fetch.
	/// </summary>
	public class Result {
		public Result(HttpWebRequest request) {
			response = (HttpWebResponse) request.GetResponse();
			var reader = new StreamReader(response.GetResponseStream());
			_status = (int) response.StatusCode;
			_encoding = response.ContentEncoding.Length > 0 ? response.ContentEncoding : "utf-8";
			_content = new char[response.ContentLength];
			long count = response.ContentLength;
			int ret;
			length = 0;
			while(count > 0) {
				ret = count > 4096 ? 4096 : (int) count;
				ret = reader.Read(_content, length, ret);
				if(ret <= 0) break;
				length += ret;
				count -= ret;
			}
		}
		readonly HttpWebResponse response;
		readonly char[] _content;
		int _status;
		int length;
		readonly string _encoding;
		
		public int status {
			get { return _status; }
		}
		
		public string encoding {
			get { return _encoding; }
		}
		
		public char[] content {
			get { return _content; }
		}
		
		public string text {
			get { return getText(); }
		}
		
		string getText() {
			try {
				Encoding enc = Encoding.GetEncoding(_encoding);
				return enc.GetString(enc.GetBytes(_content));
			} catch {
				return null;
			}
		}
	}
}
