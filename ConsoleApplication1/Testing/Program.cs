using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class Program
{
	public static void Main()
	{
		List<string> list = new List<string> { "1", "2", "3" };
		Utils utils = new Utils();
		A test = new A();
		test.tt = new List<string> { "1", "2" };
		Type type = test.GetType();
		PropertyInfo[] propertyInfos = type.GetProperties();
		foreach(PropertyInfo p in propertyInfos)
        {
			dynamic ab  = utils.ChangeType(test.tt, typeof(List<string>));
			var arr = Array.CreateInstance(ab.GetType(), ab.Count);
			Array.Copy(ab, arr, ab.Count);
			p.SetValue(test, arr);
        }
		var obj = utils.ChangeType(list, typeof(List<int>));
		Console.WriteLine("Done!");
	}

	public class A
    {
		public List<string> tt { get; set; }
		 
	}
}

public class Utils
{
	public object ChangeType(object obj, Type type)
	{
		
		if (IsList(obj))
		{
			List<object> objs = ((IEnumerable)obj).Cast<object>().ToList();
			Type containedType = type.GenericTypeArguments.First();
			return objs.Select(item => Convert.ChangeType(item, containedType)).ToList();
		}
		return Convert.ChangeType(obj, type);
	}

	public bool IsList(object o)
	{
		if (o == null) return false;
		return o is IList;
	}
}