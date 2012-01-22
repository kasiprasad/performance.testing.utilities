using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.WebTesting;

namespace Performance.Testing.Fluent.WebTesting.Framework
{
    public abstract class BaseWebTest : WebTest
    {
        protected BaseWebTest()
        {
            PreAuthenticate = true;
        }

        public abstract override IEnumerator<WebTestRequest> GetRequestEnumerator();
    }
}