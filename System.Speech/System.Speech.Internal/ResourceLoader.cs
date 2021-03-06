using System.IO;
using System.Net;

namespace System.Speech.Internal
{
	internal class ResourceLoader
	{
		internal Stream LoadFile(Uri uri, out string mimeType, out Uri baseUri, out string localPath)
		{
			localPath = null;
			Stream stream = null;
			if (!uri.IsAbsoluteUri || uri.IsFile)
			{
				string text = uri.IsAbsoluteUri ? uri.LocalPath : uri.OriginalString;
				try
				{
					stream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read);
				}
				catch
				{
					if (Directory.Exists(text))
					{
						throw new InvalidOperationException(SR.Get(SRID.CannotReadFromDirectory, text));
					}
					throw;
				}
				baseUri = null;
			}
			else
			{
				try
				{
					stream = DownloadData(uri, out baseUri);
				}
				catch (WebException ex)
				{
					throw new IOException(ex.Message, ex);
				}
			}
			mimeType = null;
			return stream;
		}

		internal void UnloadFile(string localPath)
		{
		}

		internal Stream LoadFile(Uri uri, out string localPath, out Uri redirectedUri)
		{
			string mimeType;
			return LoadFile(uri, out mimeType, out redirectedUri, out localPath);
		}

		private static Stream DownloadData(Uri uri, out Uri redirectedUri)
		{
			WebRequest webRequest = WebRequest.Create(uri);
			webRequest.Credentials = CredentialCache.DefaultCredentials;
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse())
			{
				using (httpWebResponse.GetResponseStream())
				{
					redirectedUri = httpWebResponse.ResponseUri;
					using (WebClient webClient = new WebClient())
					{
						webClient.UseDefaultCredentials = true;
						return new MemoryStream(webClient.DownloadData(redirectedUri));
					}
				}
			}
		}
	}
}
