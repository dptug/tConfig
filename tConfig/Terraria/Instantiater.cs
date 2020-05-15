using System.Linq.Expressions;
using System.Reflection;

namespace Terraria
{
	public static class Instantiater
	{
		public delegate T Constructor<T>(params object[] Params);

		public static Constructor<T> GetActivator<T>(ConstructorInfo CI)
		{
			ParameterInfo[] parameters = CI.GetParameters();
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]), "Params");
			Expression[] array = new Expression[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				Expression expression = Expression.ArrayIndex(parameterExpression, Expression.Constant(i));
				array[i] = Expression.Convert(expression, parameters[i].ParameterType);
			}
			return (Constructor<T>)Expression.Lambda(typeof(Constructor<T>), Expression.New(CI, array), parameterExpression).Compile();
		}

		public static T Create<T>(ConstructorInfo CI, params object[] Params)
		{
			return GetActivator<T>(CI)(Params);
		}
	}
}
