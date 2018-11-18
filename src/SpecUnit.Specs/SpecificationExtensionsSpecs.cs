using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace SpecUnit.Specs
{
    [Concern(typeof(SpecificationExtensions))]
    public class when_expressing_assertions_as_BDD_style_extension_methods
    {
        [Specification]
        public void should_allow__ShouldBeFalse__to_be_used_in_place_of__Assert_IsFalse__()
        {
            false.ShouldBeFalse();
        }

        [Specification]
        public void should_allow__ShouldBeTrue__to_be_used_in_place_of__Assert_IsTrue__()
        {
            true.ShouldBeTrue();
        }

        [Specification]
        public void should_allow__ShouldEqual__to_be_used_in_place_of__Assert_AreEqual__()
        {
            string s1 = "some string";
            string s2 = s1;

            s2.ShouldEqual(s1);
        }

        [Specification]
        public void should_allow__ShouldNotEqual__to_be_used_in_place_of__Assert_AreNotEqual__()
        {
            string s1 = "some string";
            string s2 = "other string";

            s2.ShouldNotEqual(s1);
        }

        [Specification]
        public void should_allow__ShouldBeNull__to_be_used_in_place_of__Assert_IsNull__()
        {
            object nullObject = null;
            nullObject.ShouldBeNull();
        }

        [Specification]
        public void should_allow__ShouldNotBeNull__to_be_used_in_place_of__Assert_IsNotNull__()
        {
            object o = new object();
            o.ShouldNotBeNull();
        }

        [Specification]
        public void should_allow__ShouldBeTheSameAs__to_be_used_in_place_of__Assert_AreSame__()
        {
            object o1 = new object();
            object o2 = o1;

            o2.ShouldBeTheSameAs(o1);
        }

        [Specification]
        public void should_allow__ShouldNotBeTheSameAs__to_be_used_in_place_of_Assert_AreNotSame__()
        {
            object o1 = new object();
            object o2 = new object();

            o2.ShouldNotBeTheSameAs(o1);
        }

        [Obsolete("Use ShouldBe() method instead. This method will be deleted in a future revision.")]
        [Specification]
        public void should_allow__ShouldBeOfType__to_be_used_in_place_of__Assert_IsInstanceOfType__()
        {
            "some string".ShouldBeOfType(typeof(String));
        }

        [Specification]
        public void should_allow__ShouldBe__to_be_used_in_place_of__Assert_IsInstanceOfType__()
        {
            "some string".ShouldBe(typeof(string));
        }

        [Specification]
        public void should_allow__ShouldNotBeOfType__to_be_used_in_place_of__Assert_IsNotInstanceOfType__()
        {
            "some string".ShouldNotBeOfType(typeof(int));
        }

        [Specification]
        public void should_allow__ShouldContain__to_be_used_in_place_of__Assert_Contains__()
        {
            object o = new object();

            List<object> objects = new List<object>();
            objects.Add(new object());
            objects.Add(o);
            objects.Add(new object());

            objects.ShouldContain(o);
        }

        [Specification]
        public void should_allow__ShouldNotContain__to_be_used_in_place_of__Assert_NotContains__()
        {
            List<object> objects = new List<object>();
            objects.Add(new object());
            objects.Add(new object());

            objects.ShouldNotContain(new object());
        }

        [Specification]
        public void should_allow__ShouldBeEmpty__to_be_used_on_a_collection_in_place_of__Assert_IsEmpty__()
        {
            ArrayList objects = new ArrayList();
            objects.ShouldBeEmpty();
        }

        [Specification]
        public void should_allow__ShouldBeEmpty__to_be_used_on_a_string_in_place_of__Assert_IsEmpty__()
        {
            string.Empty.ShouldBeEmpty();
        }

        [Specification]
        public void should_allow__ShouldNotBeEmpty__to_be_used_on_a_string_in_place_of__Assert_IsNotEmpty__()
        {
            "some string".ShouldNotBeEmpty();
        }

        [Specification]
        public void should_allow__ShouldNotBeEmpty__to_be_used_on_a_collection_in_place_of__Assert_IsNotEmpty__()
        {
            ArrayList objects = new ArrayList();
            objects.Add(new object());

            objects.ShouldNotBeEmpty();
        }

        [Specification]
        public void should_allow__ShouldBeGreaterThan__to_be_used_in_place_of__Assert_Greater__()
        {
            2.ShouldBeGreaterThan(1);
        }

        [Specification]
        public void should_allow__ShouldBeLessThan__to_be_used_in_place_of__Assert_Less__()
        {
            1.ShouldBeLessThan(2);
        }

        [Specification]
        public void should_allow__ShouldContain__to_be_used_in_place_of__StringAssert_Contains__()
        {
            "some string".ShouldContain("me");
        }

        [Specification]
        public void should_allow__ShouldNotContain__to_be_used_in_place_of__StringAssert_NotContains__()
        {
            "some string".ShouldNotContain("xx");
        }

        [Specification]
        public void should_raise_an_assertion_exception_when__ShouldNotContain__fails()
        {
            typeof(AssertionException).ShouldBeThrownBy(
                delegate
                    {
                        "some string".ShouldNotContain("me");
                    })
                    .ShouldContainErrorMessage("\"some string\" should not contain \"me\".");
        }

        [Specification]
        public void should_allow__ShouldBeEqualIgnoringCase__to_be_used_in_place_of__StringAssert_AreEqualIgnoringCase__()
        {
            "some string".ShouldBeEqualIgnoringCase("Some String");
        }

        [Specification]
        public void should_allow__ShouldStartWith__to_be_used_in_place_of__StringAssert_StartsWith__()
        {
            "some string".ShouldStartWith("so");
        }

        [Specification]
        public void should_allow__ShouldEndWith__to_be_used_in_place_of__StringAssert_EndsWith__()
        {
            "some string".ShouldEndWith("ng");
        }

        [Specification]
        public void should_allow__ShouldBeSurroundedWith__to_be_used_in_place_of__StringAssert_StartsWith__and__StringAssert_EndsWith__()
        {
            "!some string/!".ShouldBeSurroundedWith("!", "/!");
        }

        [Specification]
        public void should_allow__ShouldBeSurroundedWith__with_a_single_delimiter_to_be_used_in_place_of__StringAssert_StartsWith__and__StringAssert_EndsWith__()
        {
            "!some string!".ShouldBeSurroundedWith("!");
        }

        [Specification]
        public void should_allow__ShouldBeThrownBy__to_be_used_in_place_of__ExpectedExceptionAttribute__()
        {
            typeof(ApplicationException).ShouldBeThrownBy(
                 delegate
                 {
                     throw new ApplicationException("An error.");
                 }).ShouldContainErrorMessage("An error.");
        }

        [Specification]
        public void should_allow__ShouldContainErrorMessage__to_be_used_on_an_exception_in_place_of__StringAssert_Contains__()
        {
            new Exception("An error occurred.").ShouldContainErrorMessage("n error");
        }
    }

    [Concern(typeof(SpecificationExtensions), "GetException extention")]
    public class when_a_delegate_throws_an_exception
    {
        private MethodThatThrows _method;
        private Exception _exception;

        [Context]
        public void Context()
        {
            _method = delegate { throw new ApplicationException(); };

            _exception = _method.GetException();
        }

        [Observation]
        public void should_retrieve_the_exception()
        {
            _exception.ShouldBeOfType(typeof(ApplicationException));
        }
    }

    [Concern(typeof(SpecificationExtensions))]
    public class when_the_ShouldBeThrownBy_assertion_is_not_satisfied
    {
        //[ExpectedException(typeof(AssertionException))]
        [Specification]
        public void should_fail_the_test()
        {
            Assert.Throws<AssertionException>(() =>
            typeof(ApplicationException).ShouldBeThrownBy(delegate { var i = 0; }));
        }
    }
}