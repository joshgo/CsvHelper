﻿// Copyright 2009-2012 Josh Close
// This file is a part of CsvHelper and is licensed under the MS-PL
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html
// http://csvhelper.com
using System;
using System.IO;
using CsvHelper.Configuration;
#if WINRT_4_5
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace CsvHelper.Tests
{
	[TestClass]
	public class CsvReaderConstructorTests
	{
		[TestMethod]
		public void InvalidParameterTest()
		{
			try
			{
				new CsvReader( new TestParser() );
				Assert.Fail();
			}
			catch( CsvConfigurationException )
			{
			}
		}

		[TestMethod]
		public void EnsureInternalsAreSetupCorrectlyWhenPassingTextReaderTest()
		{
			using( var stream = new MemoryStream() )
			using( var reader = new StreamReader( stream ) )
			using( var csv = new CsvReader( reader ) )
			{
				Assert.AreSame( csv.Configuration, csv.Parser.Configuration );
			}
		}

		[TestMethod]
		public void EnsureInternalsAreSetupCorrectlyWhenPassingTextReaderAndConfigurationTest()
		{
			using( var stream = new MemoryStream() )
			using( var reader = new StreamReader( stream ) )
			using( var csv = new CsvReader( reader, new CsvConfiguration() ) )
			{
				Assert.AreSame( csv.Configuration, csv.Parser.Configuration );
			}
		}

		[TestMethod]
		public void EnsureInternalsAreSetupCorrectlyWhenPassingParserTest()
		{
			using( var stream = new MemoryStream() )
			using( var reader = new StreamReader( stream ) )
			{
				var parser = new CsvParser( reader );

				using( var csv = new CsvReader( parser ) )
				{
					Assert.AreSame( csv.Configuration, csv.Parser.Configuration );
					Assert.AreSame( parser, csv.Parser );
				}
			}
		}

		private class TestParser : ICsvParser
		{
			public void Dispose()
			{
				throw new NotImplementedException();
			}

			public CsvConfiguration Configuration { get; private set; }

			public int FieldCount
			{
				get { throw new NotImplementedException(); }
			}

			public string[] Read()
			{
				throw new NotImplementedException();
			}

			public long CharPosition
			{
				get { throw new NotImplementedException(); }
			}

			public long BytePosition { get; private set; }

			public int Row
			{
				get { throw new NotImplementedException(); }
			}
		}
	}
}
