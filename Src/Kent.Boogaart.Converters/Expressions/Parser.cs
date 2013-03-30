namespace Kent.Boogaart.Converters.Expressions
{
    using Kent.Boogaart.Converters.Expressions.Nodes;
    using Kent.Boogaart.HelperTrinity;
    using System;
    using System.Diagnostics;

    internal sealed class Parser : IDisposable
    {
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(Parser));
        private readonly Tokenizer tokenizer;
        private Token currentToken;

        private bool AreMoreTokens
        {
            get { return this.currentToken != null; }
        }

        public Parser(Tokenizer tokenizer)
        {
            Debug.Assert(tokenizer != null);

            this.tokenizer = tokenizer;
            this.ReadNextToken();
        }

        public void Dispose()
        {
            this.tokenizer.Dispose();
        }

        public Node ParseExpression()
        {
            var expression = this.ParseNullCoalescing();

            if (this.AreMoreTokens)
            {
                throw exceptionHelper.Resolve("UnexpectedInput", this.currentToken.Value);
            }

            return expression;
        }

        #region Implementation of Parse Methods

        /* what follows below are the various parse methods in increasing order of precedence */

        private Node ParseNullCoalescing()
        {
            var lNode = this.ParseTernaryConditional();

            while (this.AreMoreTokens)
            {
                if (this.currentToken.Equals(TokenType.Symbol, "??"))
                {
                    this.ReadNextToken();
                    lNode = new NullCoalescingNode(lNode, this.ParseTernaryConditional());
                }
                else
                {
                    break;
                }
            }

            return lNode;
        }

        private Node ParseTernaryConditional()
        {
            var lNode = this.ParseConditionalOr();

            while (this.AreMoreTokens)
            {
                if (this.currentToken.Equals(TokenType.Symbol, "?"))
                {
                    this.ReadNextToken();
                    var secondNode = this.ParseConditionalOr();
                    this.SkipExpectedToken(TokenType.Symbol, ":");
                    var thirdNode = this.ParseConditionalOr();
                    lNode = new TernaryConditionalNode(lNode, secondNode, thirdNode);
                }
                else
                {
                    break;
                }
            }

            return lNode;
        }

        private Node ParseConditionalOr()
        {
            var lNode = this.ParseConditionalAnd();

            while (this.AreMoreTokens)
            {
                if (this.currentToken.Equals(TokenType.Symbol, "||"))
                {
                    this.ReadNextToken();
                    lNode = new ConditionalOrNode(lNode, this.ParseConditionalAnd());
                }
                else
                {
                    break;
                }
            }

            return lNode;
        }

        private Node ParseConditionalAnd()
        {
            var lNode = this.ParseLogicalOr();

            while (this.AreMoreTokens)
            {
                if (this.currentToken.Equals(TokenType.Symbol, "&&"))
                {
                    this.ReadNextToken();
                    lNode = new ConditionalAndNode(lNode, this.ParseLogicalOr());
                }
                else
                {
                    break;
                }
            }

            return lNode;
        }

        private Node ParseLogicalOr()
        {
            var lNode = this.ParseLogicalXor();

            while (this.AreMoreTokens)
            {
                if (this.currentToken.Equals(TokenType.Symbol, "|"))
                {
                    this.ReadNextToken();
                    lNode = new LogicalOrNode(lNode, this.ParseLogicalXor());
                }
                else
                {
                    break;
                }
            }

            return lNode;
        }

        private Node ParseLogicalXor()
        {
            var lNode = this.ParseLogicalAnd();

            while (this.AreMoreTokens)
            {
                if (this.currentToken.Equals(TokenType.Symbol, "^"))
                {
                    this.ReadNextToken();
                    lNode = new LogicalXorNode(lNode, this.ParseLogicalAnd());
                }
                else
                {
                    break;
                }
            }

            return lNode;
        }

        private Node ParseLogicalAnd()
        {
            var lNode = this.ParseEquality();

            while (this.AreMoreTokens)
            {
                if (this.currentToken.Equals(TokenType.Symbol, "&"))
                {
                    this.ReadNextToken();
                    lNode = new LogicalAndNode(lNode, this.ParseEquality());
                }
                else
                {
                    break;
                }
            }

            return lNode;
        }

        private Node ParseEquality()
        {
            var lNode = this.ParseRelational();

            while (this.AreMoreTokens)
            {
                if (this.currentToken.Equals(TokenType.Symbol, "=="))
                {
                    this.ReadNextToken();
                    lNode = new EqualityNode(lNode, this.ParseRelational());
                }
                else if (this.currentToken.Equals(TokenType.Symbol, "!="))
                {
                    this.ReadNextToken();
                    lNode = new InequalityNode(lNode, this.ParseRelational());
                }
                else
                {
                    break;
                }
            }

            return lNode;
        }

        private Node ParseRelational()
        {
            var lNode = this.ParseShift();

            while (this.AreMoreTokens)
            {
                if (this.currentToken.Equals(TokenType.Symbol, "<="))
                {
                    this.ReadNextToken();
                    return new LessThanOrEqualNode(lNode, this.ParseShift());
                }
                else if (this.currentToken.Equals(TokenType.Symbol, "<"))
                {
                    this.ReadNextToken();
                    return new LessThanNode(lNode, this.ParseShift());
                }
                else if (this.currentToken.Equals(TokenType.Symbol, ">="))
                {
                    this.ReadNextToken();
                    return new GreaterThanOrEqualNode(lNode, this.ParseShift());
                }
                else if (this.currentToken.Equals(TokenType.Symbol, ">"))
                {
                    this.ReadNextToken();
                    return new GreaterThanNode(lNode, this.ParseShift());
                }
                else
                {
                    break;
                }
            }

            return lNode;
        }

        private Node ParseShift()
        {
            var lNode = this.ParseAdditive();

            while (this.AreMoreTokens)
            {
                if (this.currentToken.Equals(TokenType.Symbol, "<<"))
                {
                    this.ReadNextToken();
                    lNode = new ShiftLeftNode(lNode, this.ParseAdditive());
                }
                else if (this.currentToken.Equals(TokenType.Symbol, ">>"))
                {
                    this.ReadNextToken();
                    lNode = new ShiftRightNode(lNode, this.ParseAdditive());
                }
                else
                {
                    break;
                }
            }

            return lNode;
        }

        private Node ParseAdditive()
        {
            var lNode = this.ParseMultiplicative();

            while (this.AreMoreTokens)
            {
                if (this.currentToken.Equals(TokenType.Symbol, "+"))
                {
                    this.ReadNextToken();
                    lNode = new AddNode(lNode, this.ParseMultiplicative());
                }
                else if (this.currentToken.Equals(TokenType.Symbol, "-"))
                {
                    this.ReadNextToken();
                    lNode = new SubtractNode(lNode, this.ParseMultiplicative());
                }
                else
                {
                    break;
                }
            }

            return lNode;
        }

        private Node ParseMultiplicative()
        {
            var lNode = this.ParseUnary();

            while (this.AreMoreTokens)
            {
                if (this.currentToken.Equals(TokenType.Symbol, "*"))
                {
                    this.ReadNextToken();
                    lNode = new MultiplyNode(lNode, this.ParseUnary());
                }
                else if (this.currentToken.Equals(TokenType.Symbol, "/"))
                {
                    this.ReadNextToken();
                    lNode = new DivideNode(lNode, this.ParseUnary());
                }
                else if (this.currentToken.Equals(TokenType.Symbol, "%"))
                {
                    this.ReadNextToken();
                    lNode = new ModulusNode(lNode, this.ParseUnary());
                }
                else
                {
                    break;
                }
            }

            return lNode;
        }

        private Node ParseUnary()
        {
            this.EnsureMoreTokens();

            if (this.currentToken.Equals(TokenType.Symbol, "-"))
            {
                this.ReadNextToken();
                return new NegateNode(this.ParseBase());
            }
            else if (this.currentToken.Equals(TokenType.Symbol, "+"))
            {
                // discard superfluous "+"
                this.ReadNextToken();
            }
            else if (this.currentToken.Equals(TokenType.Symbol, "!"))
            {
                this.ReadNextToken();
                return new NotNode(this.ParseUnary());
            }
            else if (this.currentToken.Equals(TokenType.Symbol, "~"))
            {
                this.ReadNextToken();
                return new ComplementNode(this.ParseUnary());
            }
            else if (this.currentToken.Equals(TokenType.Word, "true"))
            {
                this.ReadNextToken();
                return new ConstantNode<bool>(true);
            }
            else if (this.currentToken.Equals(TokenType.Word, "false"))
            {
                this.ReadNextToken();
                return new ConstantNode<bool>(false);
            }

            return this.ParseBase();
        }

        private Node ParseBase()
        {
            this.EnsureMoreTokens();

            switch (this.currentToken.Type)
            {
                case TokenType.Number:
                    return this.ParseNumber();
                case TokenType.String:
                    string str = this.currentToken.Value;
                    this.ReadNextToken();
                    return new ConstantNode<string>(str);
                case TokenType.Word:
                    return this.ParseKeyword();
                case TokenType.Symbol:
                    if (this.currentToken.Value == "(")
                    {
                        this.ReadNextToken();

                        if (this.currentToken.Type == TokenType.Word)
                        {
                            return this.ParseCast();
                        }
                        else
                        {
                            return this.ParseGroup();
                        }
                    }
                    else if (this.currentToken.Value == "{")
                    {
                        this.ReadNextToken();
                        ConstantNode<int> integerConstant = this.ParseInt32();
                        this.SkipExpectedToken(TokenType.Symbol, "}");
                        return new VariableNode(integerConstant.Value);
                    }

                    throw exceptionHelper.Resolve("ExpressionExpected");
            }

            Debug.Assert(false);
            return null;
        }

        private CastNode ParseCast()
        {
            var targetTypeStr = this.currentToken.Value;
            var targetType = NodeValueType.Unknown;

            if (EqualsAny(targetTypeStr, "bool", "Boolean", "System.Boolean"))
            {
                targetType = NodeValueType.Boolean;
            }
            else if (EqualsAny(targetTypeStr, "string", "String", "System.String"))
            {
                targetType = NodeValueType.String;
            }
            else if (EqualsAny(targetTypeStr, "byte", "Byte", "System.Byte"))
            {
                targetType = NodeValueType.Byte;
            }
            else if (EqualsAny(targetTypeStr, "short", "Int16", "System.Int16"))
            {
                targetType = NodeValueType.Int16;
            }
            else if (EqualsAny(targetTypeStr, "int", "Int32", "System.Int32"))
            {
                targetType = NodeValueType.Int32;
            }
            else if (EqualsAny(targetTypeStr, "long", "Int64", "System.Int64"))
            {
                targetType = NodeValueType.Int64;
            }
            else if (EqualsAny(targetTypeStr, "float", "Single", "System.Single"))
            {
                targetType = NodeValueType.Single;
            }
            else if (EqualsAny(targetTypeStr, "double", "Double", "System.Double"))
            {
                targetType = NodeValueType.Double;
            }
            else if (EqualsAny(targetTypeStr, "decimal", "Decimal", "System.Decimal"))
            {
                targetType = NodeValueType.Decimal;
            }
            else
            {
                throw exceptionHelper.Resolve("CannotCastToType", targetTypeStr);
            }

            this.ReadNextToken();
            this.SkipExpectedToken(TokenType.Symbol, ")");

            return new CastNode(this.ParseUnary(), targetType);
        }

        private Node ParseGroup()
        {
            var lNode = this.ParseNullCoalescing();

            // skip the ')' that closes the group expression
            this.SkipExpectedToken(TokenType.Symbol, ")");

            return lNode;
        }

        private Node ParseNumber()
        {
            /*
             * Suffixes:- l/L for long
             *          - f/F for float
             *          - d/D for double
             *          - m/M for decimal
             * 
             * Prefixes:- 0x for hex
             * 
             * Examples:- 0x384
             *          - 0x384L (long)
             *          - 34.3 (double)
             *          - 34.3F (float)
             *          - 93L (long)
             *          - 4.33E29 (double)
             *          - 4.433E29F (float)
             *          - 4M (decimal)
             */

            var numberStr = this.currentToken.Value;

            if (numberStr.StartsWith("0x", StringComparison.Ordinal))
            {
                return this.ParseHexadecimalConstant();
            }
            else if (numberStr.IndexOfAny(new char[] { '.', 'e', 'E', 'f', 'F', 'd', 'D', 'm', 'M' }) != -1)
            {
                return this.ParseReal();
            }
            else
            {
                return this.ParseIntegral();
            }
        }

        private Node ParseHexadecimalConstant()
        {
            var numberStr = this.currentToken.Value;
            var isLong = false;

            if (numberStr[numberStr.Length - 1] == 'l' || numberStr[numberStr.Length - 1] == 'L')
            {
                numberStr = numberStr.Substring(2, numberStr.Length - 3);
                isLong = true;
            }
            else
            {
                numberStr = numberStr.Substring(2);
            }

            exceptionHelper.ResolveAndThrowIf(numberStr.Length == 0, "InvalidNumber", this.currentToken.Value);

            foreach (var c in numberStr)
            {
                exceptionHelper.ResolveAndThrowIf(!Uri.IsHexDigit(c), "InvalidNumber", this.currentToken.Value);
            }

            this.ReadNextToken();

            if (isLong)
            {
                return new ConstantNode<long>(Convert.ToInt64(numberStr, 16));
            }
            else
            {
                return new ConstantNode<int>(Convert.ToInt32(numberStr, 16));
            }
        }

        private Node ParseIntegral()
        {
            var numberStr = this.currentToken.Value;
            var isLong = false;

            if (numberStr[numberStr.Length - 1] == 'l' || numberStr[numberStr.Length - 1] == 'L')
            {
                numberStr = numberStr.Substring(0, numberStr.Length - 1);
                isLong = true;
            }

            if (isLong)
            {
                long val;
                exceptionHelper.ResolveAndThrowIf(!long.TryParse(numberStr, out val), "InvalidNumber", this.currentToken.Value);
                this.ReadNextToken();
                return new ConstantNode<long>(val);
            }
            else
            {
                return this.ParseInt32();
            }
        }

        private ConstantNode<int> ParseInt32()
        {
            int val;
            exceptionHelper.ResolveAndThrowIf(!int.TryParse(this.currentToken.Value, out val), "InvalidNumber", this.currentToken.Value);
            this.ReadNextToken();
            return new ConstantNode<int>(val);
        }

        private Node ParseReal()
        {
            var numberStr = this.currentToken.Value;
            var realType = RealType.Double;

            if (numberStr[numberStr.Length - 1] == 'd' || numberStr[numberStr.Length - 1] == 'D')
            {
                numberStr = numberStr.Substring(0, numberStr.Length - 1);
            }
            else if (numberStr[numberStr.Length - 1] == 'f' || numberStr[numberStr.Length - 1] == 'F')
            {
                numberStr = numberStr.Substring(0, numberStr.Length - 1);
                realType = RealType.Single;
            }
            else if (numberStr[numberStr.Length - 1] == 'm' || numberStr[numberStr.Length - 1] == 'M')
            {
                numberStr = numberStr.Substring(0, numberStr.Length - 1);
                realType = RealType.Decimal;
            }

            switch (realType)
            {
                case RealType.Double:
                    {
                        double val;
                        exceptionHelper.ResolveAndThrowIf(!double.TryParse(numberStr, out val), "InvalidNumber", this.currentToken.Value);
                        this.ReadNextToken();
                        return new ConstantNode<double>(val);
                    }

                case RealType.Single:
                    {
                        float val;
                        exceptionHelper.ResolveAndThrowIf(!float.TryParse(numberStr, out val), "InvalidNumber", this.currentToken.Value);
                        this.ReadNextToken();
                        return new ConstantNode<float>(val);
                    }

                case RealType.Decimal:
                    {
                        decimal val;
                        exceptionHelper.ResolveAndThrowIf(!decimal.TryParse(numberStr, out val), "InvalidNumber", this.currentToken.Value);
                        this.ReadNextToken();
                        return new ConstantNode<decimal>(val);
                    }
            }

            Debug.Assert(false);
            return null;
        }

        private Node ParseKeyword()
        {
            if (this.currentToken.Equals(TokenType.Word, "null"))
            {
                this.ReadNextToken();
                return NullNode.Instance;
            }

            throw exceptionHelper.Resolve("UnexpectedInput", this.currentToken.Value);
        }

        #endregion

        private void ReadNextToken()
        {
            this.currentToken = this.tokenizer.ReadNextToken();
        }

        private void SkipExpectedToken(TokenType type, string value)
        {
            this.EnsureMoreTokens();
            exceptionHelper.ResolveAndThrowIf(!this.currentToken.Equals(type, value), "UnexpectedToken", this.currentToken.Value, value);
            this.ReadNextToken();
        }

        private void EnsureMoreTokens()
        {
            exceptionHelper.ResolveAndThrowIf(!this.AreMoreTokens, "UnexpectedEndOfInput");
        }

        private static bool EqualsAny(string value, params string[] acceptableValues)
        {
            foreach (var acceptableValue in acceptableValues)
            {
                if (string.Equals(acceptableValue, value, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        private enum RealType
        {
            Single,
            Double,
            Decimal
        }
    }
}
