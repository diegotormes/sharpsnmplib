﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Lextm.SharpSnmpLib.Mib;
using System.IO;

namespace Lextm.SharpSnmpLib.Tests
{
    [TestFixture]
    class TestTextualConvention
    {
        [Test]
        public void Test1()
        {
            const string test =
@"someObjectName ::= TEXTUAL-CONVENTION
    STATUS current
    DESCRIPTION ""A multi line
        description""
    SYNTAX INTEGER {
        first(0),
        second(100)
        }";
            Lexer lexer = new Lexer();
            StringReader reader = new StringReader(test);
            lexer.Parse(reader);
            string name = lexer.NextSymbol.ToString();
            lexer.NextSymbol.Expect(Symbol.Assign);
            lexer.NextSymbol.Expect(Symbol.TextualConvention);
            var tc = new TextualConvention(string.Empty, name, lexer);
            Assert.AreEqual(name, tc.Name);
            Assert.IsNull(tc.Reference);
            Assert.AreEqual(typeof(Integer), tc.Syntax.GetType());
            Assert.AreEqual("second", (tc.Syntax as Integer)[100]);
        }
    }
}