using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.WebTesting;
using Performance.Testing.Fluent.WebTesting.Framework;

namespace Performance.Testing.Utilities.SampleTargetScripts
{
    public class Google_Home_PageLoad : BaseWebTest
    {
        public override IEnumerator<WebTestRequest> GetRequestEnumerator()
        {
            var request = FluentRequest.Create("http://www.google.com");
            yield return request;
            request = null;
        }
    }
}