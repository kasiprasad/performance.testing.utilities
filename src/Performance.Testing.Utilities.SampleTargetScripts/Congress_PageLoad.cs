using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.WebTesting;
using Performance.Testing.Fluent.WebTesting.Framework;

namespace Performance.Testing.Utilities.SampleTargetScripts
{
    public class Congress_PageLoad : WebTest
    {
        public override IEnumerator<WebTestRequest> GetRequestEnumerator()
        {
            var request = FluentRequest.Create("http://www.100-congress.com");
            yield return request;
            request = null;
        }
    }
}