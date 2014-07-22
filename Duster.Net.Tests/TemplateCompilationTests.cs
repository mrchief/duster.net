using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Duster.Net.Tests
{
    [TestClass]
    public class TemplateCompilationTests
    {
        [TestMethod]
        public void CanCompileHelloWorldTemplate()
        {
            const string helloWorldMarkup = "<div>Hello world!</div>";
            var compiled = DustCompiler.Compile("hw", helloWorldMarkup);
            Assert.AreEqual(@"(function(){dust.register(""hw"",body_0);function body_0(chk,ctx){return chk.write(""<div>Hello world!</div>"");}return body_0;})();", compiled);
        }

		[TestMethod]
		public void CanCompileMultiLineTemplate()
		{
			const string helloWorldMarkup = "<div>\r\n\t\tHello\t world!\r\n</div>";
			var compiled = DustCompiler.Compile("hw", helloWorldMarkup);
			Assert.AreEqual(@"(function(){dust.register(""hw"",body_0);function body_0(chk,ctx){return chk.write(""<div>Hello world!</div>"");}return body_0;})();", compiled);
		}
    }
}
