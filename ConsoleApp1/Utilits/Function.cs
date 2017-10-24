using ELW.Library.Math;
using ELW.Library.Math.Expressions;
using ELW.Library.Math.Tools;
using System;
using System.Collections.Generic;

namespace NumericMethods.Utilits
{
    class Function
    {
        private CompiledExpression compiledExpression;

        public Function(String expression)
        {
            PreparedExpression preparedExpression = ToolsHelper.Parser.Parse(expression);
            compiledExpression = ToolsHelper.Compiler.Compile(preparedExpression);
        }

        public double Calculate(double x)
        {
            List<VariableValue> variables = new List<VariableValue>();
            variables.Add(new VariableValue(x, "x"));
            return ToolsHelper.Calculator.Calculate(compiledExpression, variables);
        }
    }
}
