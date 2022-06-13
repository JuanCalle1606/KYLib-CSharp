using System;
using KYLib.Utils;
using Xunit;
// ReSharper disable VariableCanBeNotNullable
namespace KYLibTest.Utils;

public class EnsureTest
{
	#region NotNull
	
	[Fact]
	public void NotNull_True()
	{
		var a = new {};
		var b = 10;
		var c = 10f;
		string? o = "Hello";
		
		Assert.True(Ensure.NotNull(a,null));
		Assert.True(Ensure.NotNull(b, null));
		Assert.True(Ensure.NotNull(c, null));
		Assert.True(Ensure.NotNull(o, null));
	}

	[Fact]
	public void NotNull_With_Error()
	{
		object? a = null;
		int? b = null;
		double? c = null;
		string? d = null;

		Assert.Throws<ArgumentNullException>((Action)(()=> Ensure.NotNull(a, null)));
		Assert.Throws<ArgumentNullException>((Action)(() => Ensure.NotNull(b, null)));
		Assert.Throws<ArgumentNullException>((Action)(() => Ensure.NotNull(c, null)));
		Assert.Throws<ArgumentNullException>((Action)(() => Ensure.NotNull(d, null)));
	}

	[Fact]
	public void NotNull_Without_Error()
	{
		object? a = null;
		int? b = null;
		double? c = null;
		string? d = null;

		Assert.False(Ensure.NotNull(a, null, false));
		Assert.False(Ensure.NotNull(b, null, false));
		Assert.False(Ensure.NotNull(c, null, false));
		Assert.False(Ensure.NotNull(d, null, false));
	}

	#endregion
}