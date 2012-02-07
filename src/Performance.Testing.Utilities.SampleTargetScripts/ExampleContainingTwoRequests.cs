using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.WebTesting;
using Performance.Testing.Fluent.WebTesting.Framework;

namespace Performance.Testing.Utilities.SampleTargetScripts
{
    public class ExampleContainingTwoRequests : WebTest
    {
        public override IEnumerator<WebTestRequest> GetRequestEnumerator()
        {
            var request1 = FluentRequest.Create("http://www.google.com");
            yield return request1;

            var request2 = FluentRequest.Create("http://www.google.com?q=something")
                .POST();
            yield return request2;

        }
    }
}