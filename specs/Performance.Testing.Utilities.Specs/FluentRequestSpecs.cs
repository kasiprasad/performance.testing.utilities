using System;
using System.Linq;
using Machine.Specifications;
using Microsoft.VisualStudio.TestTools.WebTesting;
using Performance.Testing.Fluent.WebTesting.Framework;

namespace Performance.Testing.Utilities.Specs.FluentRequestSpecs
{
    public class When_creating_a_request
    {
        static WebTestRequest sut;
        static string url = "http://www.google.com/";

        Because of = () => sut = FluentRequest.Create(url);

        It should_set_the_url_to_the_url_provided = () => sut.Url.ShouldEqual(url);
        It should_have_an_accept_charset_header = () => sut.Headers.First(h => h.Name == "Accept-Charset").Value.ShouldEqual("ISO-8859-1,utf-8;q=0.7,*;q=0.7");
        It should_have_a_pragma_nocache_header = () => sut.Headers.First(h => h.Name == "Pragma").Value.ShouldEqual("no-cache");
        It should_set_the_request_method_to_GET = () => sut.Method.ShouldEqual(HTTPMethodType.GET.ToString());
    }

    public class When_attempting_to_add_a_form_post_parameter_to_a_request_which_is_not_of_method_type_POST
    {
        static string url = "http://www.google.com/";

        static Exception sut;

        Because of = () => sut = Catch.Exception(() => FluentRequest.Create(url)
                                                     .WithFormPostParameter("anything", "anyvalue"));

        It should_throw_an_exception_indicating_that_the_requests_method_was_not_POST = () =>
            {
                sut.ShouldNotBeNull();
                sut.Message.ShouldContain("You must have the Request's Method set to POST");
            };
    }

    public class When_setting_a_request_with_json
    {
        Because of = () => sut = FluentRequest.Create("http://www.google.com")
                                     .POST()
                                     .WithJson(new { Name = "Kasi Prasad", City = "New York" });

        It should_have_a_content_type_header_set_to_application_json =
            () => sut.Headers.First(h => h.Name == "Content-Type").Value.ShouldEqual("application/json");

        It should_have_a_string_http_body = () => sut.Body.ShouldBeOfType<StringHttpBody>();

        It should_have_a_body_with_content_type_set_to_application_json = () => sut.Body.ContentType.ShouldEqual("application/json");

        It should_have_a_serialized_json_string_as_its_body_content =
            () => (sut.Body as StringHttpBody).BodyString
                .ShouldEqual(@"{""Name"":""Kasi Prasad"",""City"":""New York""}");

        static WebTestRequest sut;
    }

    public class When_attempting_to_set_a_request_with_json_which_has_not_been_set_to_post
    {
        Because of = () => exception = Catch.Exception(() => FluentRequest.Create("http://www.google.com/").WithJson(new { Anything = "Anything" }));

        It should_throw_an_exception_indicating_that_you_must_use_the_POST_method_first = () =>
            {
                exception.ShouldNotBeNull();
                exception.Message.ShouldContain("Please use the POST() method first.");
            };

        static Exception exception;
            

    }
}

