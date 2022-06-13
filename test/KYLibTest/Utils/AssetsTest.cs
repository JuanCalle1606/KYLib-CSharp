using KYLib.Utils;
using Xunit;
#pragma warning disable CS1718
// ReSharper disable EqualExpressionComparison
namespace KYLibTest.Utils;

public class AssetsTest
{
	[Fact]
	public void Equality_Without_Nullable()
	{
		#region Config

		Assets a = new Assets("src");
		Assets b = new Assets("src/code");
		Assets c = a.GetAssets("code");
		bool operatorA;
		bool operatorB;
		bool operatorC;
		bool methodA;
		bool methodB;
		bool methodC;

		#endregion
		#region A
		operatorA = a == a;
		operatorB = a == b;
		operatorC = a == c;
		methodA = a.Equals(a);
		methodB = a.Equals(b);
		methodC = a.Equals(c);
		
		Assert.True(operatorA);
		Assert.False(operatorB);
		Assert.False(operatorC);
		Assert.True(methodA);
		Assert.False(methodB);
		Assert.False(methodC);
		#endregion
		#region B

		operatorA = b == a;
		operatorB = b == b;
		operatorC = b == c;
		methodA = b.Equals(a);
		methodB = b.Equals(b);
		methodC = b.Equals(c);

		Assert.False(operatorA);
		Assert.True(operatorB);
		Assert.True(operatorC);
		Assert.False(methodA);
		Assert.True(methodB);
		Assert.True(methodC);

		#endregion
		#region C

		operatorA = c == a;
		operatorB = c == b;
		operatorC = c == c;
		methodA = c.Equals(a);
		methodB = c.Equals(b);
		methodC = c.Equals(c);

		Assert.False(operatorA);
		Assert.True(operatorB);
		Assert.True(operatorC);
		Assert.False(methodA);
		Assert.True(methodB);
		Assert.True(methodC);

		#endregion
	}

	[Fact]
	public void Inequality_Without_Nullable()
	{
		#region Config

		Assets a = new Assets("src");
		Assets b = new Assets("src/code");
		Assets c = a.GetAssets("code");
		bool operatorA;
		bool operatorB;
		bool operatorC;

		#endregion
		#region A

		operatorA = a != a;
		operatorB = a != b;
		operatorC = a != c;

		Assert.False(operatorA);
		Assert.True(operatorB);
		Assert.True(operatorC);

		#endregion
		#region B

		operatorA = b != a;
		operatorB = b != b;
		operatorC = b != c;

		Assert.True(operatorA);
		Assert.False(operatorB);
		Assert.False(operatorC);

		#endregion
		#region C

		operatorA = c != a;
		operatorB = c != b;
		operatorC = c != c;

		Assert.True(operatorA);
		Assert.False(operatorB);
		Assert.False(operatorC);

		#endregion
	}
	
}