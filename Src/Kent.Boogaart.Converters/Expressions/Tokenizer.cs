using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Expressions
{
	internal sealed class Tokenizer : IDisposable
	{
		private readonly TextReader _textReader;
		private readonly StringBuilder _buffer;
		private char _currentChar;

		private bool IsAtEndOfStream
		{
			get
			{
				return (_textReader.Peek() == -1);
			}
		}

		public Tokenizer(TextReader textReader)
		{
			Debug.Assert(textReader != null);
			_textReader = textReader;
			_buffer = new StringBuilder();
			DiscardWhiteSpace();
		}

		public void Dispose()
		{
			_textReader.Dispose();
		}

		public Token ReadNextToken()
		{
			if (IsAtEndOfStream)
			{
				return null;
			}

			Token token;
			ReadNextChar();
			_buffer.Length = 0;
			_buffer.Append(_currentChar);

			if (char.IsDigit(_currentChar) || _currentChar == '.')
			{
				token = ReadNumberToken();
			}
			else if (char.IsLetter(_currentChar))
			{
				token = ReadWordToken();
			}
			else if (_currentChar == '"')
			{
				token = ReadStringToken();
			}
			else
			{
				token = ReadSymbolToken();
			}

			DiscardWhiteSpace();
			return token;
		}

		private void ReadNextChar()
		{
			_currentChar = (char) _textReader.Read();
		}

		private void DiscardWhiteSpace()
		{
			while (!IsAtEndOfStream && char.IsWhiteSpace((char) _textReader.Peek()))
			{
				ReadNextChar();
			}
		}

		private Token ReadNumberToken()
		{
			char nextChar = (char) _textReader.Peek();

			while (!IsAtEndOfStream && (char.IsLetterOrDigit(nextChar) || nextChar == '.'))
			{
				ReadNextChar();
				_buffer.Append(_currentChar);
				nextChar = (char) _textReader.Peek();
			}

			return new Token(TokenType.Number, _buffer.ToString());
		}

		private Token ReadStringToken()
		{
			_buffer.Length = 0;
			ReadNextChar();

			while (!IsAtEndOfStream && _currentChar != '"')
			{
				if (_currentChar == '\\')
				{
					//handle escape sequence
					ReadNextChar();

					switch (_currentChar)
					{
						case '\'':
							_buffer.Append("'");
							break;
						case '"':
							_buffer.Append("\"");
							break;
						case '\\':
							_buffer.Append("\\");
							break;
						case '0':
							_buffer.Append("\0");
							break;
						case 'a':
							_buffer.Append("\a");
							break;
						case 'b':
							_buffer.Append("\b");
							break;
						case 'f':
							_buffer.Append("\f");
							break;
						case 'n':
							_buffer.Append("\n");
							break;
						case 'r':
							_buffer.Append("\r");
							break;
						case 't':
							_buffer.Append("\t");
							break;
						case 'v':
							_buffer.Append("\v");
							break;
						default:
							ExceptionHelper.Throw("UnrecognizedEscapeSequence");
							break;
					}
				}
				else
				{
					_buffer.Append(_currentChar);
				}

				ReadNextChar();
			}

			return new Token(TokenType.String, _buffer.ToString());
		}

		private Token ReadWordToken()
		{
			char nextChar = (char) _textReader.Peek();

			while (!IsAtEndOfStream && !char.IsWhiteSpace(nextChar) && !char.IsPunctuation(nextChar))
			{
				ReadNextChar();
				_buffer.Append(_currentChar);
				nextChar = (char) _textReader.Peek();
			}

			return new Token(TokenType.Word, _buffer.ToString());
		}

		private Token ReadSymbolToken()
		{
			char nextChar = (char) _textReader.Peek();

			while (!IsAtEndOfStream && !char.IsWhiteSpace(nextChar) && !char.IsLetterOrDigit(nextChar) && !IsBracket(nextChar) && nextChar != '"')
			{
				ReadNextChar();
				_buffer.Append(_currentChar);
				nextChar = (char) _textReader.Peek();
			}

			return new Token(TokenType.Symbol, _buffer.ToString());
		}

		private static bool IsBracket(char c)
		{
			return c == '(' || c == ')' || c == '{' || c == '}';
		}
	}
}