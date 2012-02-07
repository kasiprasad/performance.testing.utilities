﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.WebTesting;
using Performance.Testing.Fluent.WebTesting.Framework;

namespace Performance.Testing.Utilities.SampleTargetScripts
{
    public class Version : WebTest
    {
        public override IEnumerator<WebTestRequest> GetRequestEnumerator()
        {
            var request = FluentRequest.Create("http://www.ventonit.com/version.txt");
            yield return request;
            request = null;
        }
    }
}
