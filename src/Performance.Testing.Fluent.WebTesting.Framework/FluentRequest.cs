using System;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.WebTesting;
using Newtonsoft.Json;

namespace Performance.Testing.Fluent.WebTesting.Framework
{
    /// <summary>
    /// Allows for fluently creating/configuring a WebTestRequest
    /// </summary>
    public static class FluentRequest
    {
        /// <summary>
        /// Creates a Web Test Request using the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parseDependentRequests">if set to <c>true</c> [set's the Web Test Request to parse dependent requests].</param>
        /// <param name="followRedirects">if set to <c>true</c> [set's the Web Test Request to follow redirects].</param>
        /// <returns></returns>
        public static WebTestRequest Create(string url, bool parseDependentRequests = false,
                                            bool followRedirects = false)
        {
            var request = new WebTestRequest(url)
                              {
                                  Method = HTTPMethodType.GET.ToString(),
                                  ParseDependentRequests = parseDependentRequests,
                                  FollowRedirects = followRedirects
                              };

            return request
                .WithHeader("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7")
                .WithHeader("Pragma", "no-cache");
        }


        /// <summary>
        /// Set's the Web Test Request with a an HTTP Header - Content-Type text/json
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static WebTestRequest AsJson(this WebTestRequest request)
        {
            return request
                .WithHeader("Content-Type", "text/json");
        }

        /// <summary>
        /// Set's the Web Test Request with a an HTTP Header - x-requested-with XMLHttpRequest
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static WebTestRequest AsAJAX(this WebTestRequest request)
        {
            return request
                .WithHeader("x-requested-with", "XMLHttpRequest");
        }

        /// <summary>
        /// Set's the Web Test Request with a an HTTP Header
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static WebTestRequest WithHeader(this WebTestRequest request, string name, string value)
        {
            if (request.Headers.Contains(name))
                request.Headers.First(h => h.Name == name).Value = value;
            else
                request.Headers.Add(new WebTestRequestHeader(name, value));
            
            return request;
        }

        /// <summary>
        /// Set's the Web Test Request with a Query String Parameter
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static WebTestRequest WithQueryStringParameter(this WebTestRequest request, string name, string value)
        {
            request.QueryStringParameters.Add(name, value);
            return request;
        }

        /// <summary>
        /// Set's the Web Test Request with a Form Post Parameter
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="urlEncode">if set to <c>true</c> [URL encode].</param>
        /// <returns></returns>
        public static WebTestRequest WithFormPostParameter(this WebTestRequest request, string name, string value,
                                                           bool urlEncode = false)
        {
            var body = request.Body as FormPostHttpBody;

            if (body == null)
                throw new Exception("You must have the Request's Method set to POST. Use POST() before attempting to add form post parameters");

            body.FormPostParameters.Add(name, value, urlEncode);
            return request;
        }

        /// <summary>
        /// Set's the Web Test Request with a Cookie
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static WebTestRequest WithCookie(this WebTestRequest request, string name, string value,
                                                string path = "/")
        {
            request.Cookies.Add(new Cookie(name, value, path));
            return request;
        }

        /// <summary>
        /// Sets the Web Request to use StringHttpBody application/json content with a serialized JSON representation of the provided T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">The web test request.</param>
        /// <param name="item">The item to be serialized as JSON.</param>
        /// <returns></returns>
        public static WebTestRequest WithJson<T>(this WebTestRequest request, T item)
        {
            if(request.Method != HTTPMethodType.POST.ToString())
                throw new Exception("You use WithJson<T>(T item) without setting the Request to Method POST. Please use the POST() method first.");

            request.Body = new StringHttpBody()
            {
                BodyString = JsonConvert.SerializeObject(item),
                InsertByteOrderMark = false,
                ContentType = "application/json"
            };

            return request
                .AsJson();
        }
        
        #region "HTTP Method Related Functionality"

        /// <summary>
        /// Set's the HTTP Method of the specified Web Request
        /// </summary>
        /// <param name="request">The web test request.</param>
        /// <param name="methodType">HTTP Metho Type.</param>
        /// <returns></returns>
        public static WebTestRequest Method(this WebTestRequest request, HTTPMethodType methodType)
        {
            request.Method = methodType.ToString();

            if (methodType != HTTPMethodType.POST)
                return request;


            request.Body = new FormPostHttpBody();
            return request
                .WithHeader("Content-Type", "application/x-www-form-urlencoded");
        }

        /// <summary>
        /// Set's the HTTP Method of the specified Web Request to POST
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static WebTestRequest POST(this WebTestRequest request)
        {
            return request.Method(HTTPMethodType.POST);
        }


        public static WebTestRequest GET(this WebTestRequest request)
        {
            return request.Method(HTTPMethodType.GET);
        }


        #endregion
    }
}
