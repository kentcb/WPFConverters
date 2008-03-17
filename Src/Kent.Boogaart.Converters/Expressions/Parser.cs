using System;
using System.Diagnostics;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Expressions
{
	internal sealed class Parser : IDisposable
	{
		private readonly Tokenizer _tokenizer;
		private Token _currentToken;

		private bool AreMoreTokens
		{
			get
			{
				return (_currentToken != null);
			}
		}

		public Parser(Tokenizer tokenizer)
		{
			Debug.Assert(tokenizer != null);
			_tokenizer = tokenizer;
			ReadNextToken();
		}

		public void Dispose()
		{
			_tokenizer.Dispose();
		}

		public Node ParseExpression()
		{
			Node expression = ParseConditionalOr();

			if (AreMoreTokens)
			{
				ExceptionHelper.Throw("UnexpectedInput", _currentToken.Value);
			}

			return expression;
		}

		#region Implementation of Parse Methods

		//what follows below are the various parse methods in increasing order of precedence

		private Node ParseConditionalOr()
		{
			Node lNode = ParseConditionalAnd();

			while (AreMoreTokens)
			{
				if (_currentToken.Equals(TokenType.Symbol, "||"))
				{
					ReadNextToken();
					lNode = new ConditionalOrNode(lNode, ParseConditionalAnd());
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
			Node lNode = ParseLogicalOr();

			while (AreMoreTokens)
			{
				if (_currentToken.Equals(TokenType.Symbol, "&&"))
				{
					ReadNextToken();
					lNode = new ConditionalAndNode(lNode, ParseLogicalOr());
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
			Node lNode = ParseLogicalXor();

			while (AreMoreTokens)
			{
				if (_currentToken.Equals(TokenType.Symbol, "|"))
				{
					ReadNextToken();
					lNode = new LogicalOrNode(lNode, ParseLogicalXor());
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
			Node lNode = ParseLogicalAnd();

			while (AreMoreTokens)
			{
				if (_currentToken.Equals(TokenType.Symbol, "^"))
				{
					ReadNextToken();
					lNode = new LogicalXorNode(lNode, ParseLogicalAnd());
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
			Node lNode = ParseEquality();

			while (AreMoreTokens)
			{
				if (_currentToken.Equals(TokenType.Symbol, "&"))
				{
					ReadNextToken();
					lNode = new LogicalAndNode(lNode, ParseEquality());
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
			Node lNode = ParseRelational();

			while (AreMoreTokens)
			{
				if (_currentToken.Equals(TokenType.Symbol, "=="))
				{
					ReadNextToken();
					lNode = new EqualityNode(lNode, ParseRelational());
				}
				else if (_currentToken.Equals(TokenType.Symbol, "!="))
				{
					ReadNextToken();
					lNode = new InequalityNode(lNode, ParseRelational());
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
			Node lNode = ParseShift();

			while (AreMoreTokens)
			{
				if (_currentToken.Equals(TokenType.Symbol, "<="))
				{
					ReadNextToken();
					return new LessThanOrEqualNode(lNode, ParseShift());
				}
				else if (_currentToken.Equals(TokenType.Symbol, "<"))
				{
					ReadNextToken();
					return new LessThanNode(lNode, ParseShift());
				}
				else if (_currentToken.Equals(TokenType.Symbol, ">="))
				{
					ReadNextToken();
					return new GreaterThanOrEqualNode(lNode, ParseShift());
				}
				else if (_currentToken.Equals(TokenType.Symbol, ">"))
				{
					ReadNextToken();
					return new GreaterThanNode(lNode, ParseShift());
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
			Node lNode = ParseAdditive();

			while (AreMoreTokens)
			{
				if (_currentToken.Equals(TokenType.Symbol, "<<"))
				{
					ReadNextToken();
					lNode = new ShiftLeftNode(lNode, ParseAdditive());
				}
				else if (_currentToken.Equals(TokenType.Symbol, ">>"))
				{
					ReadNextToken();
					lNode = new ShiftRightNode(lNode, ParseAdditive());
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
			Node lNode = ParseMultiplicative();

			while (AreMoreTokens)
			{
				if (_currentToken.Equals(TokenType.Symbol, "+"))
				{
					ReadNextToken();
					lNode = new AddNode(lNode, ParseMultiplicative());
				}
				else if (_currentToken.Equals(TokenType.Symbol, "-"))
				{
					ReadNextToken();
					lNode = new SubtractNode(lNode, ParseMultiplicative());
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
			Node lNode = ParseUnary();

			while (AreMoreTokens)
			{
				if (_currentToken.Equals(TokenType.Symbol, "*"))
				{
					ReadNextToken();
					lNode = new MultiplyNode(lNode, ParseUnary());
				}
				else if (_currentToken.Equals(TokenType.Symbol, "/"))
				{
					ReadNextToken();
					lNode = new DivideNode(lNode, ParseUnary());
				}
				else if (_currentToken.Equals(TokenType.Symbol, "%"))
				{
					ReadNextToken();
					lNode = new ModulusNode(lNode, ParseUnary());
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
			EnsureMoreTokens();

			if (_currentToken.Equals(TokenType.Symbol, "-"))
			{
				ReadNextToken();
				return new NegateNode(ParseBase());
			}
			else if (_currentToken.Equals(TokenType.Symbol, "+"))
			{
				//discard superfluous "+"
				ReadNextToken();
			}
			else if (_currentToken.Equals(TokenType.Symbol, "!"))
			{
				ReadNextToken();
				return new NotNode(ParseUnary());
			}
			else if (_currentToken.Equals(TokenType.Symbol, "~"))
			{
				ReadNextToken();
				return new ComplementNode(ParseUnary());
			}
			else if (_currentToken.Equals(TokenType.Word, "true"))
			{
				ReadNextToken();
				return new ConstantNode<bool>(true);
			}
			else if (_currentToken.Equals(TokenType.Word, "false"))
			{
				ReadNextToken();
				return new ConstantNode<bool>(false);
			}

			return ParseBase();
		}

		private Node ParseBase()
		{
			EnsureMoreTokens();

			switch (_currentToken.Type)
			{
				case TokenType.Number:
					return ParseNumber();
				case TokenType.String:
					string str = _currentToken.Value;
					ReadNextToken();
					return new ConstantNode<string>(str);
				case TokenType.Word:
					return ParseKeyword();
				case TokenType.Symbol:
					if (_currentToken.Value == "(")
					{
						ReadNextToken();

						if (_currentToken.Type == TokenType.Word)
						{
							return ParseCast();
						}
						else
						{
							return ParseGroup();
						}
					}
					else if (_currentToken.Value == "{")
					{
						ReadNextToken();
						ConstantNode<int> integerConstant = ParseInt32();
						SkipExpectedToken(TokenType.Symbol, "}");
						return new VariableNode(integerConstant.Value);
					}

					ExceptionHelper.Throw("ExpressionExpected");
					break;
			}

			Debug.Assert(false);
			return null;
		}

		private CastNode ParseCast()
		{
			string targetTypeStr = _currentToken.Value;
			NodeValueType targetType = NodeValueType.Unknown;

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
				ExceptionHelper.Throw("CannotCastToType", targetTypeStr);
			}

			ReadNextToken();
			SkipExpectedToken(TokenType.Symbol, ")");

			return new CastNode(ParseUnary(), targetType);
		}

		private Node ParseGroup()
		{
			Node lNode = ParseConditionalOr();
			//skip the ')' that closes the group expression
			SkipExpectedToken(TokenType.Symbol, ")");

			return lNode;
		}

		private Node ParseNumber()
		{
			//Suffixes:- l/L for long
			//         - f/F for float
			//         - d/D for double
			//         - m/M for decimal
			//
			//Prefixes:- 0x for hex
			//
			//Examples:- 0x384
			//         - 0x384L (long)
			//         - 34.3 (double)
			//         - 34.3F (float)
			//         - 93L (long)
			//         - 4.33E29 (double)
			//         - 4.433E29F (float)
			//         - 4M (decimal)

			string numberStr = _currentToken.Value;

			if (numberStr.StartsWith("0x"))
			{
				return ParseHexadecimalConstant();
			}
			else if (numberStr.IndexOfAny(new char[] { '.', 'e', 'E', 'f', 'F', 'd', 'D', 'm', 'M' }) != -1)
			{
				return ParseReal();
			}
			else
			{
				return ParseIntegral();
			}
		}

		private Node ParseHexadecimalConstant()
		{
			string numberStr = _currentToken.Value;
			bool isLong = false;

			if (numberStr[numberStr.Length - 1] == 'l' || numberStr[numberStr.Length - 1] == 'L')
			{
				numberStr = numberStr.Substring(2, numberStr.Length - 3);
				isLong = true;
			}
			else
			{
				numberStr = numberStr.Substring(2);
			}

			ExceptionHelper.ThrowIf(numberStr.Length == 0, "InvalidNumber", _currentToken.Value);

			foreach (char c in numberStr)
			{
				ExceptionHelper.ThrowIf(!Uri.IsHexDigit(c), "InvalidNumber", _currentToken.Value);
			}

			ReadNextToken();

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
			string numberStr = _currentToken.Value;
			bool isLong = false;

			if (numberStr[numberStr.Length - 1] == 'l' || numberStr[numberStr.Length - 1] == 'L')
			{
				numberStr = numberStr.Substring(0, numberStr.Length - 1);
				isLong = true;
			}

			if (isLong)
			{
				long val;
				ExceptionHelper.ThrowIf(!long.TryParse(numberStr, out val), "InvalidNumber", _currentToken.Value);
				ReadNextToken();
				return new ConstantNode<long>(val);
			}
			else
			{
				return ParseInt32();
			}
		}

		private ConstantNode<int> ParseInt32()
		{
			int val;
			ExceptionHelper.ThrowIf(!int.TryParse(_currentToken.Value, out val), "InvalidNumber", _currentToken.Value);
			ReadNextToken();
			return new ConstantNode<int>(val);
		}

		private Node ParseReal()
		{
			string numberStr = _currentToken.Value;
			RealType realType = RealType.Double;

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
						ExceptionHelper.ThrowIf(!double.TryParse(numberStr, out val), "InvalidNumber", _currentToken.Value);
						ReadNextToken();
						return new ConstantNode<double>(val);
					}
				case RealType.Single:
					{
						float val;
						ExceptionHelper.ThrowIf(!float.TryParse(numberStr, out val), "InvalidNumber", _currentToken.Value);
						ReadNextToken();
						return new ConstantNode<float>(val);
					}
				case RealType.Decimal:
					{
						decimal val;
						ExceptionHelper.ThrowIf(!decimal.TryParse(numberStr, out val), "InvalidNumber", _currentToken.Value);
						ReadNextToken();
						return new ConstantNode<decimal>(val);
					}
			}

			Debug.Assert(false);
			return null;
		}

		private Node ParseKeyword()
		{
			if (_currentToken.Equals(TokenType.Word, "null"))
			{
				ReadNextToken();
				return NullNode.Instance;
			}

			ExceptionHelper.Throw("UnexpectedInput", _currentToken.Value);
			return null;
		}

		#endregion

		private void ReadNextToken()
		{
			_currentToken = _tokenizer.ReadNextToken();
		}

		private void SkipExpectedToken(TokenType type, string value)
		{
			EnsureMoreTokens();
			ExceptionHelper.ThrowIf(!_currentToken.Equals(type, value), "UnexpectedToken", _currentToken.Value, value);
			ReadNextToken();
		}

		private void EnsureMoreTokens()
		{
			ExceptionHelper.ThrowIf(!AreMoreTokens, "UnexpectedEndOfInput");
		}

		private static bool EqualsAny(string value, params string[] acceptableValues)
		{
			foreach (string acceptableValue in acceptableValues)
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
