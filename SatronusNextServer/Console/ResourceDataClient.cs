using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public static class ResourceDataClient
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task SetBearer(string accessToken)
        {
            httpClient.SetBearerToken(accessToken);
        }

        public static async Task SendFileToServer(string fileFullPath)
        {
            FileInfo fi = new FileInfo(fileFullPath);
            string fileName = fi.Name;
            byte[] fileContents = File.ReadAllBytes(fi.FullName);
            Uri webService = new Uri(@"http://localhost:5001/Home/UploadFile");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, webService);
            requestMessage.Headers.ExpectContinue = false;

            MultipartFormDataContent multiPartContent = new MultipartFormDataContent("Upload File");
            ByteArrayContent byteArrayContent = new ByteArrayContent(fileContents);
            byteArrayContent.Headers.Add("Content-Type", "application/octet-stream");
            multiPartContent.Add(byteArrayContent, "file", fileName);
            requestMessage.Content = multiPartContent;

            try
            {
                Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
                HttpResponseMessage httpResponse = httpRequest.Result;
                HttpStatusCode statusCode = httpResponse.StatusCode;
                HttpContent responseContent = httpResponse.Content;

                if (responseContent != null)
                {
                    Task<String> stringContentsTask = responseContent.ReadAsStringAsync();
                    String stringContents = stringContentsTask.Result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task DownloadFile(string fileName)
        {
            using (var result = await httpClient.GetAsync("http://localhost:5001/Home/Download?filename=" + fileName))
            {
                if (result.IsSuccessStatusCode)
                {
                    byte[] bt = new byte[Convert.ToInt32(result.Content.Headers.ContentLength)];
                    bt = await result.Content.ReadAsByteArrayAsync();
                    ByteArrayContent bytes = new ByteArrayContent(bt);
                    MultipartFormDataContent content = new MultipartFormDataContent();
                    content.Add(bytes, "file", result.Content.Headers.ContentDisposition.FileName);

                    File.WriteAllBytes("E:\\Lx\\" + result.Content.Headers.ContentDisposition.FileName, bt);
                }
            }
        }
    }
}
