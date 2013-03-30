namespace Kent.Boogaart.Converters.Expressions
{
    using Kent.Boogaart.HelperTrinity;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    internal sealed class Tokenizer : IDisposable
    {
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(Tokenizer));
        private readonly TextReader textReader;
        private readonly StringBuilder buffer;
        private char currentChar;

        private bool IsAtEndOfStream
        {
            get { return this.textReader.Peek() == -1; }
        }

        public Tokenizer(TextReader textReader)
        {
            Debug.Assert(textReader != null);

            this.textReader = textReader;
            this.buffer = new StringBuilder();
            this.DiscardWhiteSpace();
        }

        public void Dispose()
        {
            this.textReader.Dispose();
        }

        public Token ReadNextToken()
        {
            if (this.IsAtEndOfStream)
            {
                return null;
            }

            Token token;
            this.ReadNextChar();
            this.buffer.Length = 0;
            this.buffer.Append(this.currentChar);

            if (char.IsDigit(this.currentChar) || this.currentChar == '.')
            {
                token = this.ReadNumberToken();
            }
            else if (char.IsLetter(this.currentChar))
            {
                token = this.ReadWordToken();
            }
            else if (this.currentChar == '"')
            {
                token = this.ReadStringToken();
            }
            else
            {
                token = this.ReadSymbolToken();
            }

            this.DiscardWhiteSpace();
            return token;
        }

        private void ReadNextChar()
        {
            this.currentChar = (char)this.textReader.Read();
        }

        private void DiscardWhiteSpace()
        {
            while (!this.IsAtEndOfStream && char.IsWhiteSpace((char)this.textReader.Peek()))
            {
                this.ReadNextChar();
            }
        }

        private Token ReadNumberToken()
        {
            char nextChar = (char)this.textReader.Peek();

            while (!this.IsAtEndOfStream && (char.IsLetterOrDigit(nextChar) || nextChar == '.'))
            {
                this.ReadNextChar();
                this.buffer.Append(this.currentChar);
                nextChar = (char)this.textReader.Peek();
            }

            return new Token(TokenType.Number, this.buffer.ToString());
        }

        private Token ReadStringToken()
        {
            this.buffer.Length = 0;
            this.ReadNextChar();

            while (!this.IsAtEndOfStream && this.currentChar != '"')
            {
                if (this.currentChar == '\\')
                {
                    // handle escape sequence
                    this.ReadNextChar();

                    switch (this.currentChar)
                    {
                        case '\'':
                            this.buffer.Append("'");
                            break;
                        case '"':
                            this.buffer.Append("\"");
                            break;
                        case '\\':
                            this.buffer.Append("\\");
                            break;
                        case '0':
                            this.buffer.Append("\0");
                            break;
                        case 'a':
                            this.buffer.Append("\a");
                            break;
                        case 'b':
                            this.buffer.Append("\b");
                            break;
                        case 'f':
                            this.buffer.Append("\f");
                            break;
                        case 'n':
                            this.buffer.Append("\n");
                            break;
                        case 'r':
                            this.buffer.Append("\r");
                            break;
                        case 't':
                            this.buffer.Append("\t");
                            break;
                        case 'v':
                            this.buffer.Append("\v");
                            break;
                        default:
                            throw exceptionHelper.Resolve("UnrecognizedEscapeSequence");
                    }
                }
                else
                {
                    this.buffer.Append(this.currentChar);
                }

                this.ReadNextChar();
            }

            return new Token(TokenType.String, this.buffer.ToString());
        }

        private Token ReadWordToken()
        {
            char nextChar = (char)this.textReader.Peek();

            while (!this.IsAtEndOfStream && !char.IsWhiteSpace(nextChar) && !char.IsPunctuation(nextChar))
            {
                this.ReadNextChar();
                this.buffer.Append(this.currentChar);
                nextChar = (char)this.textReader.Peek();
            }

            return new Token(TokenType.Word, this.buffer.ToString());
        }

        private Token ReadSymbolToken()
        {
            char nextChar = (char)this.textReader.Peek();

            while (!this.IsAtEndOfStream && !char.IsWhiteSpace(nextChar) && !char.IsLetterOrDigit(nextChar) && !IsBracket(nextChar) && nextChar != '"')
            {
                this.ReadNextChar();
                this.buffer.Append(this.currentChar);
                nextChar = (char)this.textReader.Peek();
            }

            return new Token(TokenType.Symbol, this.buffer.ToString());
        }

        private static bool IsBracket(char c)
        {
            return c == '(' || c == ')' || c == '{' || c == '}';
        }
    }
}