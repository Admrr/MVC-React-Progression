using System;
using System.Linq.Expressions;

namespace Practice4.Filtering
{

    public class Filter
    {
        public int Kind { get; set; }
        public string Attribute { get; set; }
        public object Value { get; set; }
        // We add F1 and F2 so that we can use context filtering.
        public Filter F1 { get; set; }
        public Filter F2 { get; set; }

    }

    public class MyExpressions
    {
        public static Expression<Func<T, bool>> FilterToExpression<T>(Filter f)
        {
            //p.Title == ""
            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");
            return FilterToExpressionAux<T>(parameter, f);

        }
        public static Expression<Func<T, bool>> FilterToExpressionAux<T>(ParameterExpression parameter, Filter f)
        {
            switch (f.Kind)
            {
                case 0:
                    {
                        // ==

                        //p.Attribute
                        var propertyReference = Expression.Property(parameter, f.Attribute);
                        //p.Attribute    Value
                        var constantReference = Expression.Constant(f.Value);
                        //p.Attribute == Value
                        var LambdaBody = Expression.Lambda<Func<T, bool>>(Expression.Equal(propertyReference, constantReference), parameter);
                        Console.WriteLine(LambdaBody);
                        return LambdaBody;
                    }
                case 1:
                    {
                        // &&

                        // p.Title == "James bond 007"
                        var expr1 = FilterToExpressionAux<T>(parameter, f.F1);
                        // p.ReleaseYear == 1980
                        var expr2 = FilterToExpressionAux<T>(parameter, f.F2);
                        // AND operation
                        var Body = Expression.And(expr1.Body, expr2.Body);
                        // Lambda to combine the body and parameter.
                        var LambdaBody = Expression.Lambda<Func<T, bool>>(Body, parameter);
                        Console.WriteLine(LambdaBody);
                        return LambdaBody;
                    }
                case 2:
                    {
                        // ||

                        // p.Title == "James bond 007"
                        var expr1 = FilterToExpressionAux<T>(parameter, f.F1);
                        // p.ReleaseYear == 1980
                        var expr2 = FilterToExpressionAux<T>(parameter, f.F2);
                        // OR operation
                        var Body = Expression.Or(expr1.Body, expr2.Body);
                        // Lambda to combine the body and parameter.
                        var LambdaBody = Expression.Lambda<Func<T, bool>>(Body, parameter);
                        Console.WriteLine(LambdaBody);
                        return LambdaBody;
                    }
                case 3: {

                        // >=
                        
                        //p.Attribute
                        var propertyReference = Expression.Property(parameter, f.Attribute);
                        //p.Attribute    Value
                        var constantReference = Expression.Constant(f.Value);
                        //p.Attribute >= Value
                        var LambdaBody = Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(propertyReference, constantReference), parameter);
                        Console.WriteLine(LambdaBody);
                        return LambdaBody;
                }
                case 4: {

                        // <=

                        //p.Attribute
                        var propertyReference = Expression.Property(parameter, f.Attribute);
                        //p.Attribute    Value
                        var constantReference = Expression.Constant(f.Value);
                        //p.Attribute <= Value
                        var LambdaBody = Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(propertyReference, constantReference), parameter);
                        Console.WriteLine(LambdaBody);
                        return LambdaBody;
                }
                case 5: {

                        // <

                        //p.Attribute
                        var propertyReference = Expression.Property(parameter, f.Attribute);
                        //p.Attribute    Value
                        var constantReference = Expression.Constant(f.Value);
                        //p.Attribute < Value
                        var LambdaBody = Expression.Lambda<Func<T, bool>>(Expression.LessThan(propertyReference, constantReference), parameter);
                        Console.WriteLine(LambdaBody);
                        return LambdaBody;

                }
                case 6: {

                        // >

                        //p.Attribute
                        var propertyReference = Expression.Property(parameter, f.Attribute);
                        //p.Attribute    Value
                        var constantReference = Expression.Constant(f.Value);
                        //p.Attribute > Value
                        var LambdaBody = Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(propertyReference, constantReference), parameter);
                        Console.WriteLine(LambdaBody);
                        return LambdaBody;

                }

                case 7: {

                        // !=

                        //p.Attribute
                        var propertyReference = Expression.Property(parameter, f.Attribute);
                        //p.Attribute    Value
                        var constantReference = Expression.Constant(f.Value);
                        //p.Attribute != Value
                        var LambdaBody = Expression.Lambda<Func<T, bool>>(Expression.NotEqual(propertyReference, constantReference), parameter);
                        Console.WriteLine(LambdaBody);
                        return LambdaBody;
                }
            }
            return null;
        }

    }

}