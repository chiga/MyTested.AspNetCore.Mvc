﻿namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.ViewTests
{
    using System.Collections.Generic;
    using System.Net;
    using Exceptions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.ViewEngines;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ViewComponentTestBuilderTests
    {
        [Fact]
        public void WithStatusCodeAsIntShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
                .WithStatusCode(500);
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
                .WithStatusCode(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public void WithStatusCodeShouldThrowExceptionWhenActionReturnsWrongStatusCode()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CustomViewComponentResult())
                        .ShouldReturn()
                        .ViewComponent()
                        .WithStatusCode(HttpStatusCode.NotFound);
                },
                "When calling CustomViewComponentResult action in MvcController expected view component result to have 404 (NotFound) status code, but instead received 500 (InternalServerError).");
        }
        
        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithString()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
                .WithContentType(ContentType.ApplicationXml);
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
                .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueConstant()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
                .WithContentType(ContentType.ApplicationXml);
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValue()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CustomViewComponentResult())
                        .ShouldReturn()
                        .ViewComponent()
                        .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson));
                },
                "When calling CustomViewComponentResult action in MvcController expected view component result ContentType to be 'application/json', but instead received 'application/xml'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValue()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CustomViewComponentResult())
                        .ShouldReturn()
                        .ViewComponent()
                        .WithContentType((MediaTypeHeaderValue)null);
                },
                "When calling CustomViewComponentResult action in MvcController expected view component result ContentType to be null, but instead received 'application/xml'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValueAndNullActual()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent()
                .WithContentType((MediaTypeHeaderValue)null);
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValueAndNullActual()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ViewComponentResultByName())
                        .ShouldReturn()
                        .ViewComponent()
                        .WithContentType(new MediaTypeHeaderValue(TestObjectFactory.MediaType));
                },
                "When calling ViewComponentResultByName action in MvcController expected view component result ContentType to be 'application/json', but instead received null.");
        }

        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithValidViewEngine()
        {
            var viewEngine = TestObjectFactory.GetViewEngine();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentWithViewEngine(viewEngine))
                .ShouldReturn()
                .ViewComponent()
                .WithViewEngine(viewEngine);
        }

        [Fact]
        public void WithViewEngineShouldNotThrowExceptionWithNullViewEngine()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent()
                .WithViewEngine(null);
        }

        [Fact]
        public void WithViewEngineShouldThrowExceptionWithInvalidViewEngine()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .WithoutValidation()
                        .Calling(c => c.ViewComponentWithViewEngine(null))
                        .ShouldReturn()
                        .ViewComponent()
                        .WithViewEngine(new CustomViewEngine());
                },
                "When calling ViewComponentWithViewEngine action in MvcController expected view component result ViewEngine to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithValidViewEngine()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentWithViewEngine(new CustomViewEngine()))
                .ShouldReturn()
                .ViewComponent()
                .WithViewEngineOfType<CustomViewEngine>();
        }

        [Fact]
        public void WithViewEngineOfTypeShouldThrowExceptionWithInvalidViewEngine()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ViewComponentWithViewEngine(new CustomViewEngine()))
                        .ShouldReturn()
                        .ViewComponent()
                        .WithViewEngineOfType<IViewEngine>();
                },
                "When calling ViewComponentWithViewEngine action in MvcController expected view component result ViewEngine to be of IViewEngine type, but instead received CustomViewEngine.");
        }

        [Fact]
        public void WithViewEngineOfTypeShouldNotThrowExceptionWithNullViewEngine()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ViewComponentResultByName())
                        .ShouldReturn()
                        .ViewComponent()
                        .WithViewEngineOfType<CustomViewEngine>();
                },
                "When calling ViewComponentResultByName action in MvcController expected view component result ViewEngine to be of CustomViewEngine type, but instead received null.");
        }

        [Fact]
        public void WithArgumentsShouldThrowExceptionWithIncorrectArgumentsType()
        {
            Test.AssertException<ViewResultAssertionException>(
                   () =>
                   {
                       MyMvc
                           .Controller<MvcController>()
                           .Calling(c => c.ViewComponentWithIncorrectArguments())
                           .ShouldReturn()
                           .ViewComponent();
                   },
                   "When calling ViewComponentWithIncorrectArguments action in MvcController expected view component result Arguments to be array of objects, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void WithArgumentShouldNotThrowExceptionWithCorrectArgument()
        {
            var responseModels = TestObjectFactory.GetListOfResponseModels();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent()
                .WithArgument(responseModels);
        }
        
        [Fact]
        public void WithArgumentShouldThrowExceptionWithIncorrectArgument()
        {
            Test.AssertException<ViewResultAssertionException>(
                   () =>
                   {
                       MyMvc
                           .Controller<MvcController>()
                           .Calling(c => c.ViewComponentResultByType())
                           .ShouldReturn()
                           .ViewComponent()
                           .WithArgument(1);
                   },
                   "When calling ViewComponentResultByType action in MvcController expected view component result with at least one argument to be the given one, but none was found.");
        }

        [Fact]
        public void WithArgumentShouldNotThrowExceptionWithCorrectArgumentOfType()
        {
            var responseModels = TestObjectFactory.GetListOfResponseModels();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent()
                .WithArgumentOfType<List<ResponseModel>>();
        }

        [Fact]
        public void WithArgumentShouldThrowExceptionWithIncorrectArgumentOfType()
        {
            Test.AssertException<ViewResultAssertionException>(
                   () =>
                   {
                       MyMvc
                           .Controller<MvcController>()
                           .Calling(c => c.ViewComponentResultByType())
                           .ShouldReturn()
                           .ViewComponent()
                           .WithArgumentOfType<int>();
                   },
                   "When calling ViewComponentResultByType action in MvcController expected view component result with at least one argument to be of Int32 type, but none was found.");
        }

        [Fact]
        public void WithArgumentsShouldNotThrowExceptionWithCorrectArguments()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent()
                .WithArguments(1, "text");
        }
        
        [Fact]
        public void WithArgumentsShouldThrowExceptionWithIncorrectArgumentsCount()
        {
            Test.AssertException<ViewResultAssertionException>(
                   () =>
                   {
                       MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ViewComponentResultByName())
                        .ShouldReturn()
                        .ViewComponent()
                        .WithArguments(1, "text", 15);
                   },
                   "When calling ViewComponentResultByName action in MvcController expected view component result Arguments to have 3 items, but in fact found 2.");
        }

        [Fact]
        public void WithArgumentsShouldThrowExceptionWithIncorrectArguments()
        {
            Test.AssertException<ViewResultAssertionException>(
                   () =>
                   {
                       MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ViewComponentResultByName())
                        .ShouldReturn()
                        .ViewComponent()
                        .WithArguments(1, "incorrect");
                   },
                   "When calling ViewComponentResultByName action in MvcController expected view component result to have argument on position 1 equal to the given one on the same position, but in fact it was different.");
        }

        [Fact]
        public void WithArgumentsShouldThrowExceptionWithCorrectArgumentsAsEnumerable()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent()
                .WithArguments(new List<object> { 1, "text" });
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent()
                .WithContentType(ContentType.ApplicationXml)
                .AndAlso()
                .WithStatusCode(500);
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            var actionResult = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent()
                .AndProvideTheActionResult();

            Assert.NotNull(actionResult);
            Assert.IsAssignableFrom<ViewComponentResult>(actionResult);
        }
    }
}
