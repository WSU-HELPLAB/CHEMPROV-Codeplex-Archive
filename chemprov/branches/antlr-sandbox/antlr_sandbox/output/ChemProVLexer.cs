//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 3.4
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// $ANTLR 3.4 D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g 2012-02-08 15:33:44

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 219
// Unreachable code detected.
#pragma warning disable 162
// Missing XML comment for publicly visible type or member 'Type_or_Member'
#pragma warning disable 1591
// CLS compliance checking will not be performed on 'type' because it is not visible from outside this assembly.
#pragma warning disable 3019


using System.Collections.Generic;
using Antlr.Runtime;
using Antlr.Runtime.Misc;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "3.4")]
[System.CLSCompliant(false)]
public partial class ChemProVLexer : Antlr.Runtime.Lexer
{
	public const int EOF=-1;
	public const int T__10=10;
	public const int T__11=11;
	public const int T__12=12;
	public const int T__13=13;
	public const int T__14=14;
	public const int T__15=15;
	public const int T__16=16;
	public const int T__17=17;
	public const int FLOAT=4;
	public const int IDENTIFIER=5;
	public const int INTEGER=6;
	public const int NEWLINE=7;
	public const int VAR=8;
	public const int WS=9;

    // delegates
    // delegators

	public ChemProVLexer()
	{
		OnCreated();
	}

	public ChemProVLexer(ICharStream input )
		: this(input, new RecognizerSharedState())
	{
	}

	public ChemProVLexer(ICharStream input, RecognizerSharedState state)
		: base(input, state)
	{

		OnCreated();
	}
	public override string GrammarFileName { get { return "D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g"; } }


	partial void OnCreated();
	partial void EnterRule(string ruleName, int ruleIndex);
	partial void LeaveRule(string ruleName, int ruleIndex);

	partial void EnterRule_T__10();
	partial void LeaveRule_T__10();

	// $ANTLR start "T__10"
	[GrammarRule("T__10")]
	private void mT__10()
	{
		EnterRule_T__10();
		EnterRule("T__10", 1);
		TraceIn("T__10", 1);
		try
		{
			int _type = T__10;
			int _channel = DefaultTokenChannel;
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:7:7: ( '(' )
			DebugEnterAlt(1);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:7:9: '('
			{
			DebugLocation(7, 9);
			Match('('); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("T__10", 1);
			LeaveRule("T__10", 1);
			LeaveRule_T__10();
		}
	}
	// $ANTLR end "T__10"

	partial void EnterRule_T__11();
	partial void LeaveRule_T__11();

	// $ANTLR start "T__11"
	[GrammarRule("T__11")]
	private void mT__11()
	{
		EnterRule_T__11();
		EnterRule("T__11", 2);
		TraceIn("T__11", 2);
		try
		{
			int _type = T__11;
			int _channel = DefaultTokenChannel;
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:8:7: ( ')' )
			DebugEnterAlt(1);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:8:9: ')'
			{
			DebugLocation(8, 9);
			Match(')'); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("T__11", 2);
			LeaveRule("T__11", 2);
			LeaveRule_T__11();
		}
	}
	// $ANTLR end "T__11"

	partial void EnterRule_T__12();
	partial void LeaveRule_T__12();

	// $ANTLR start "T__12"
	[GrammarRule("T__12")]
	private void mT__12()
	{
		EnterRule_T__12();
		EnterRule("T__12", 3);
		TraceIn("T__12", 3);
		try
		{
			int _type = T__12;
			int _channel = DefaultTokenChannel;
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:9:7: ( '*' )
			DebugEnterAlt(1);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:9:9: '*'
			{
			DebugLocation(9, 9);
			Match('*'); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("T__12", 3);
			LeaveRule("T__12", 3);
			LeaveRule_T__12();
		}
	}
	// $ANTLR end "T__12"

	partial void EnterRule_T__13();
	partial void LeaveRule_T__13();

	// $ANTLR start "T__13"
	[GrammarRule("T__13")]
	private void mT__13()
	{
		EnterRule_T__13();
		EnterRule("T__13", 4);
		TraceIn("T__13", 4);
		try
		{
			int _type = T__13;
			int _channel = DefaultTokenChannel;
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:10:7: ( '+' )
			DebugEnterAlt(1);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:10:9: '+'
			{
			DebugLocation(10, 9);
			Match('+'); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("T__13", 4);
			LeaveRule("T__13", 4);
			LeaveRule_T__13();
		}
	}
	// $ANTLR end "T__13"

	partial void EnterRule_T__14();
	partial void LeaveRule_T__14();

	// $ANTLR start "T__14"
	[GrammarRule("T__14")]
	private void mT__14()
	{
		EnterRule_T__14();
		EnterRule("T__14", 5);
		TraceIn("T__14", 5);
		try
		{
			int _type = T__14;
			int _channel = DefaultTokenChannel;
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:11:7: ( '-' )
			DebugEnterAlt(1);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:11:9: '-'
			{
			DebugLocation(11, 9);
			Match('-'); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("T__14", 5);
			LeaveRule("T__14", 5);
			LeaveRule_T__14();
		}
	}
	// $ANTLR end "T__14"

	partial void EnterRule_T__15();
	partial void LeaveRule_T__15();

	// $ANTLR start "T__15"
	[GrammarRule("T__15")]
	private void mT__15()
	{
		EnterRule_T__15();
		EnterRule("T__15", 6);
		TraceIn("T__15", 6);
		try
		{
			int _type = T__15;
			int _channel = DefaultTokenChannel;
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:12:7: ( '/' )
			DebugEnterAlt(1);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:12:9: '/'
			{
			DebugLocation(12, 9);
			Match('/'); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("T__15", 6);
			LeaveRule("T__15", 6);
			LeaveRule_T__15();
		}
	}
	// $ANTLR end "T__15"

	partial void EnterRule_T__16();
	partial void LeaveRule_T__16();

	// $ANTLR start "T__16"
	[GrammarRule("T__16")]
	private void mT__16()
	{
		EnterRule_T__16();
		EnterRule("T__16", 7);
		TraceIn("T__16", 7);
		try
		{
			int _type = T__16;
			int _channel = DefaultTokenChannel;
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:13:7: ( '=' )
			DebugEnterAlt(1);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:13:9: '='
			{
			DebugLocation(13, 9);
			Match('='); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("T__16", 7);
			LeaveRule("T__16", 7);
			LeaveRule_T__16();
		}
	}
	// $ANTLR end "T__16"

	partial void EnterRule_T__17();
	partial void LeaveRule_T__17();

	// $ANTLR start "T__17"
	[GrammarRule("T__17")]
	private void mT__17()
	{
		EnterRule_T__17();
		EnterRule("T__17", 8);
		TraceIn("T__17", 8);
		try
		{
			int _type = T__17;
			int _channel = DefaultTokenChannel;
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:14:7: ( 'let' )
			DebugEnterAlt(1);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:14:9: 'let'
			{
			DebugLocation(14, 9);
			Match("let"); 


			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("T__17", 8);
			LeaveRule("T__17", 8);
			LeaveRule_T__17();
		}
	}
	// $ANTLR end "T__17"

	partial void EnterRule_IDENTIFIER();
	partial void LeaveRule_IDENTIFIER();

	// $ANTLR start "IDENTIFIER"
	[GrammarRule("IDENTIFIER")]
	private void mIDENTIFIER()
	{
		EnterRule_IDENTIFIER();
		EnterRule("IDENTIFIER", 9);
		TraceIn("IDENTIFIER", 9);
		try
		{
			int _type = IDENTIFIER;
			int _channel = DefaultTokenChannel;
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:27:2: ( ( 'a' .. 'z' | 'A' .. 'Z' )+ | ( 'a' .. 'z' | 'A' .. 'Z' )+ '0' .. '9' ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' )* )
			int alt4=2;
			try { DebugEnterDecision(4, false);
			try
			{
				alt4 = dfa4.Predict(input);
			}
			catch (NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
				throw;
			}
			} finally { DebugExitDecision(4); }
			switch (alt4)
			{
			case 1:
				DebugEnterAlt(1);
				// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:27:4: ( 'a' .. 'z' | 'A' .. 'Z' )+
				{
				DebugLocation(27, 4);
				// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:27:4: ( 'a' .. 'z' | 'A' .. 'Z' )+
				int cnt1=0;
				try { DebugEnterSubRule(1);
				while (true)
				{
					int alt1=2;
					try { DebugEnterDecision(1, false);
					int LA1_0 = input.LA(1);

					if (((LA1_0>='A' && LA1_0<='Z')||(LA1_0>='a' && LA1_0<='z')))
					{
						alt1 = 1;
					}


					} finally { DebugExitDecision(1); }
					switch (alt1)
					{
					case 1:
						DebugEnterAlt(1);
						// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:
						{
						DebugLocation(27, 4);
						input.Consume();


						}
						break;

					default:
						if (cnt1 >= 1)
							goto loop1;

						EarlyExitException eee1 = new EarlyExitException( 1, input );
						DebugRecognitionException(eee1);
						throw eee1;
					}
					cnt1++;
				}
				loop1:
					;

				} finally { DebugExitSubRule(1); }


				}
				break;
			case 2:
				DebugEnterAlt(2);
				// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:27:27: ( 'a' .. 'z' | 'A' .. 'Z' )+ '0' .. '9' ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' )*
				{
				DebugLocation(27, 27);
				// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:27:27: ( 'a' .. 'z' | 'A' .. 'Z' )+
				int cnt2=0;
				try { DebugEnterSubRule(2);
				while (true)
				{
					int alt2=2;
					try { DebugEnterDecision(2, false);
					int LA2_0 = input.LA(1);

					if (((LA2_0>='A' && LA2_0<='Z')||(LA2_0>='a' && LA2_0<='z')))
					{
						alt2 = 1;
					}


					} finally { DebugExitDecision(2); }
					switch (alt2)
					{
					case 1:
						DebugEnterAlt(1);
						// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:
						{
						DebugLocation(27, 27);
						input.Consume();


						}
						break;

					default:
						if (cnt2 >= 1)
							goto loop2;

						EarlyExitException eee2 = new EarlyExitException( 2, input );
						DebugRecognitionException(eee2);
						throw eee2;
					}
					cnt2++;
				}
				loop2:
					;

				} finally { DebugExitSubRule(2); }

				DebugLocation(27, 48);
				MatchRange('0','9'); 
				DebugLocation(27, 57);
				// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:27:57: ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' )*
				try { DebugEnterSubRule(3);
				while (true)
				{
					int alt3=2;
					try { DebugEnterDecision(3, false);
					int LA3_0 = input.LA(1);

					if (((LA3_0>='0' && LA3_0<='9')||(LA3_0>='A' && LA3_0<='Z')||(LA3_0>='a' && LA3_0<='z')))
					{
						alt3 = 1;
					}


					} finally { DebugExitDecision(3); }
					switch ( alt3 )
					{
					case 1:
						DebugEnterAlt(1);
						// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:
						{
						DebugLocation(27, 57);
						input.Consume();


						}
						break;

					default:
						goto loop3;
					}
				}

				loop3:
					;

				} finally { DebugExitSubRule(3); }


				}
				break;

			}
			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("IDENTIFIER", 9);
			LeaveRule("IDENTIFIER", 9);
			LeaveRule_IDENTIFIER();
		}
	}
	// $ANTLR end "IDENTIFIER"

	partial void EnterRule_INTEGER();
	partial void LeaveRule_INTEGER();

	// $ANTLR start "INTEGER"
	[GrammarRule("INTEGER")]
	private void mINTEGER()
	{
		EnterRule_INTEGER();
		EnterRule("INTEGER", 10);
		TraceIn("INTEGER", 10);
		try
		{
			int _type = INTEGER;
			int _channel = DefaultTokenChannel;
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:31:2: ( ( '0' .. '9' )+ )
			DebugEnterAlt(1);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:31:4: ( '0' .. '9' )+
			{
			DebugLocation(31, 4);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:31:4: ( '0' .. '9' )+
			int cnt5=0;
			try { DebugEnterSubRule(5);
			while (true)
			{
				int alt5=2;
				try { DebugEnterDecision(5, false);
				int LA5_0 = input.LA(1);

				if (((LA5_0>='0' && LA5_0<='9')))
				{
					alt5 = 1;
				}


				} finally { DebugExitDecision(5); }
				switch (alt5)
				{
				case 1:
					DebugEnterAlt(1);
					// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:
					{
					DebugLocation(31, 4);
					input.Consume();


					}
					break;

				default:
					if (cnt5 >= 1)
						goto loop5;

					EarlyExitException eee5 = new EarlyExitException( 5, input );
					DebugRecognitionException(eee5);
					throw eee5;
				}
				cnt5++;
			}
			loop5:
				;

			} finally { DebugExitSubRule(5); }


			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("INTEGER", 10);
			LeaveRule("INTEGER", 10);
			LeaveRule_INTEGER();
		}
	}
	// $ANTLR end "INTEGER"

	partial void EnterRule_FLOAT();
	partial void LeaveRule_FLOAT();

	// $ANTLR start "FLOAT"
	[GrammarRule("FLOAT")]
	private void mFLOAT()
	{
		EnterRule_FLOAT();
		EnterRule("FLOAT", 11);
		TraceIn("FLOAT", 11);
		try
		{
			int _type = FLOAT;
			int _channel = DefaultTokenChannel;
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:33:2: ( ( '0' .. '9' )+ '.' ( '0' .. '9' )+ )
			DebugEnterAlt(1);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:33:4: ( '0' .. '9' )+ '.' ( '0' .. '9' )+
			{
			DebugLocation(33, 4);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:33:4: ( '0' .. '9' )+
			int cnt6=0;
			try { DebugEnterSubRule(6);
			while (true)
			{
				int alt6=2;
				try { DebugEnterDecision(6, false);
				int LA6_0 = input.LA(1);

				if (((LA6_0>='0' && LA6_0<='9')))
				{
					alt6 = 1;
				}


				} finally { DebugExitDecision(6); }
				switch (alt6)
				{
				case 1:
					DebugEnterAlt(1);
					// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:
					{
					DebugLocation(33, 4);
					input.Consume();


					}
					break;

				default:
					if (cnt6 >= 1)
						goto loop6;

					EarlyExitException eee6 = new EarlyExitException( 6, input );
					DebugRecognitionException(eee6);
					throw eee6;
				}
				cnt6++;
			}
			loop6:
				;

			} finally { DebugExitSubRule(6); }

			DebugLocation(33, 14);
			Match('.'); 
			DebugLocation(33, 18);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:33:18: ( '0' .. '9' )+
			int cnt7=0;
			try { DebugEnterSubRule(7);
			while (true)
			{
				int alt7=2;
				try { DebugEnterDecision(7, false);
				int LA7_0 = input.LA(1);

				if (((LA7_0>='0' && LA7_0<='9')))
				{
					alt7 = 1;
				}


				} finally { DebugExitDecision(7); }
				switch (alt7)
				{
				case 1:
					DebugEnterAlt(1);
					// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:
					{
					DebugLocation(33, 18);
					input.Consume();


					}
					break;

				default:
					if (cnt7 >= 1)
						goto loop7;

					EarlyExitException eee7 = new EarlyExitException( 7, input );
					DebugRecognitionException(eee7);
					throw eee7;
				}
				cnt7++;
			}
			loop7:
				;

			} finally { DebugExitSubRule(7); }


			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("FLOAT", 11);
			LeaveRule("FLOAT", 11);
			LeaveRule_FLOAT();
		}
	}
	// $ANTLR end "FLOAT"

	partial void EnterRule_NEWLINE();
	partial void LeaveRule_NEWLINE();

	// $ANTLR start "NEWLINE"
	[GrammarRule("NEWLINE")]
	private void mNEWLINE()
	{
		EnterRule_NEWLINE();
		EnterRule("NEWLINE", 12);
		TraceIn("NEWLINE", 12);
		try
		{
			int _type = NEWLINE;
			int _channel = DefaultTokenChannel;
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:35:9: ( ( '\\r' )? '\\n' )
			DebugEnterAlt(1);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:35:11: ( '\\r' )? '\\n'
			{
			DebugLocation(35, 11);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:35:11: ( '\\r' )?
			int alt8=2;
			try { DebugEnterSubRule(8);
			try { DebugEnterDecision(8, false);
			int LA8_0 = input.LA(1);

			if ((LA8_0=='\r'))
			{
				alt8 = 1;
			}
			} finally { DebugExitDecision(8); }
			switch (alt8)
			{
			case 1:
				DebugEnterAlt(1);
				// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:35:11: '\\r'
				{
				DebugLocation(35, 11);
				Match('\r'); 

				}
				break;

			}
			} finally { DebugExitSubRule(8); }

			DebugLocation(35, 17);
			Match('\n'); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("NEWLINE", 12);
			LeaveRule("NEWLINE", 12);
			LeaveRule_NEWLINE();
		}
	}
	// $ANTLR end "NEWLINE"

	partial void EnterRule_WS();
	partial void LeaveRule_WS();

	// $ANTLR start "WS"
	[GrammarRule("WS")]
	private void mWS()
	{
		EnterRule_WS();
		EnterRule("WS", 13);
		TraceIn("WS", 13);
		try
		{
			int _type = WS;
			int _channel = DefaultTokenChannel;
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:36:4: ( ( ' ' | '\\t' | '\\n' | '\\r' )+ )
			DebugEnterAlt(1);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:36:6: ( ' ' | '\\t' | '\\n' | '\\r' )+
			{
			DebugLocation(36, 6);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:36:6: ( ' ' | '\\t' | '\\n' | '\\r' )+
			int cnt9=0;
			try { DebugEnterSubRule(9);
			while (true)
			{
				int alt9=2;
				try { DebugEnterDecision(9, false);
				int LA9_0 = input.LA(1);

				if (((LA9_0>='\t' && LA9_0<='\n')||LA9_0=='\r'||LA9_0==' '))
				{
					alt9 = 1;
				}


				} finally { DebugExitDecision(9); }
				switch (alt9)
				{
				case 1:
					DebugEnterAlt(1);
					// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:
					{
					DebugLocation(36, 6);
					input.Consume();


					}
					break;

				default:
					if (cnt9 >= 1)
						goto loop9;

					EarlyExitException eee9 = new EarlyExitException( 9, input );
					DebugRecognitionException(eee9);
					throw eee9;
				}
				cnt9++;
			}
			loop9:
				;

			} finally { DebugExitSubRule(9); }

			DebugLocation(36, 28);
			Skip();

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("WS", 13);
			LeaveRule("WS", 13);
			LeaveRule_WS();
		}
	}
	// $ANTLR end "WS"

	public override void mTokens()
	{
		// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:8: ( T__10 | T__11 | T__12 | T__13 | T__14 | T__15 | T__16 | T__17 | IDENTIFIER | INTEGER | FLOAT | NEWLINE | WS )
		int alt10=13;
		try { DebugEnterDecision(10, false);
		try
		{
			alt10 = dfa10.Predict(input);
		}
		catch (NoViableAltException nvae)
		{
			DebugRecognitionException(nvae);
			throw;
		}
		} finally { DebugExitDecision(10); }
		switch (alt10)
		{
		case 1:
			DebugEnterAlt(1);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:10: T__10
			{
			DebugLocation(1, 10);
			mT__10(); 

			}
			break;
		case 2:
			DebugEnterAlt(2);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:16: T__11
			{
			DebugLocation(1, 16);
			mT__11(); 

			}
			break;
		case 3:
			DebugEnterAlt(3);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:22: T__12
			{
			DebugLocation(1, 22);
			mT__12(); 

			}
			break;
		case 4:
			DebugEnterAlt(4);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:28: T__13
			{
			DebugLocation(1, 28);
			mT__13(); 

			}
			break;
		case 5:
			DebugEnterAlt(5);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:34: T__14
			{
			DebugLocation(1, 34);
			mT__14(); 

			}
			break;
		case 6:
			DebugEnterAlt(6);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:40: T__15
			{
			DebugLocation(1, 40);
			mT__15(); 

			}
			break;
		case 7:
			DebugEnterAlt(7);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:46: T__16
			{
			DebugLocation(1, 46);
			mT__16(); 

			}
			break;
		case 8:
			DebugEnterAlt(8);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:52: T__17
			{
			DebugLocation(1, 52);
			mT__17(); 

			}
			break;
		case 9:
			DebugEnterAlt(9);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:58: IDENTIFIER
			{
			DebugLocation(1, 58);
			mIDENTIFIER(); 

			}
			break;
		case 10:
			DebugEnterAlt(10);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:69: INTEGER
			{
			DebugLocation(1, 69);
			mINTEGER(); 

			}
			break;
		case 11:
			DebugEnterAlt(11);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:77: FLOAT
			{
			DebugLocation(1, 77);
			mFLOAT(); 

			}
			break;
		case 12:
			DebugEnterAlt(12);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:83: NEWLINE
			{
			DebugLocation(1, 83);
			mNEWLINE(); 

			}
			break;
		case 13:
			DebugEnterAlt(13);
			// D:\\acarter\\code\\chemprov\\branches\\antlr-sandbox\\antlr_sandbox\\ChemProV.g:1:91: WS
			{
			DebugLocation(1, 91);
			mWS(); 

			}
			break;

		}

	}


	#region DFA
	DFA4 dfa4;
	DFA10 dfa10;

	protected override void InitDFAs()
	{
		base.InitDFAs();
		dfa4 = new DFA4(this);
		dfa10 = new DFA10(this);
	}

	private class DFA4 : DFA
	{
		private const string DFA4_eotS =
			"\x1\xFFFF\x1\x2\x2\xFFFF";
		private const string DFA4_eofS =
			"\x4\xFFFF";
		private const string DFA4_minS =
			"\x1\x41\x1\x30\x2\xFFFF";
		private const string DFA4_maxS =
			"\x2\x7A\x2\xFFFF";
		private const string DFA4_acceptS =
			"\x2\xFFFF\x1\x1\x1\x2";
		private const string DFA4_specialS =
			"\x4\xFFFF}>";
		private static readonly string[] DFA4_transitionS =
			{
				"\x1A\x1\x6\xFFFF\x1A\x1",
				"\xA\x3\x7\xFFFF\x1A\x1\x6\xFFFF\x1A\x1",
				"",
				""
			};

		private static readonly short[] DFA4_eot = DFA.UnpackEncodedString(DFA4_eotS);
		private static readonly short[] DFA4_eof = DFA.UnpackEncodedString(DFA4_eofS);
		private static readonly char[] DFA4_min = DFA.UnpackEncodedStringToUnsignedChars(DFA4_minS);
		private static readonly char[] DFA4_max = DFA.UnpackEncodedStringToUnsignedChars(DFA4_maxS);
		private static readonly short[] DFA4_accept = DFA.UnpackEncodedString(DFA4_acceptS);
		private static readonly short[] DFA4_special = DFA.UnpackEncodedString(DFA4_specialS);
		private static readonly short[][] DFA4_transition;

		static DFA4()
		{
			int numStates = DFA4_transitionS.Length;
			DFA4_transition = new short[numStates][];
			for ( int i=0; i < numStates; i++ )
			{
				DFA4_transition[i] = DFA.UnpackEncodedString(DFA4_transitionS[i]);
			}
		}

		public DFA4( BaseRecognizer recognizer )
		{
			this.recognizer = recognizer;
			this.decisionNumber = 4;
			this.eot = DFA4_eot;
			this.eof = DFA4_eof;
			this.min = DFA4_min;
			this.max = DFA4_max;
			this.accept = DFA4_accept;
			this.special = DFA4_special;
			this.transition = DFA4_transition;
		}

		public override string Description { get { return "26:1: IDENTIFIER : ( ( 'a' .. 'z' | 'A' .. 'Z' )+ | ( 'a' .. 'z' | 'A' .. 'Z' )+ '0' .. '9' ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' )* );"; } }

		public override void Error(NoViableAltException nvae)
		{
			DebugRecognitionException(nvae);
		}
	}

	private class DFA10 : DFA
	{
		private const string DFA10_eotS =
			"\x8\xFFFF\x1\x9\x1\xFFFF\x1\xF\x1\xD\x1\x11\x1\xFFFF\x1\x9\x3\xFFFF\x1"+
			"\x13\x1\xFFFF";
		private const string DFA10_eofS =
			"\x14\xFFFF";
		private const string DFA10_minS =
			"\x1\x9\x7\xFFFF\x1\x65\x1\xFFFF\x1\x2E\x1\xA\x1\x9\x1\xFFFF\x1\x74\x3"+
			"\xFFFF\x1\x30\x1\xFFFF";
		private const string DFA10_maxS =
			"\x1\x7A\x7\xFFFF\x1\x65\x1\xFFFF\x1\x39\x1\xA\x1\x20\x1\xFFFF\x1\x74"+
			"\x3\xFFFF\x1\x7A\x1\xFFFF";
		private const string DFA10_acceptS =
			"\x1\xFFFF\x1\x1\x1\x2\x1\x3\x1\x4\x1\x5\x1\x6\x1\x7\x1\xFFFF\x1\x9\x3"+
			"\xFFFF\x1\xD\x1\xFFFF\x1\xA\x1\xB\x1\xC\x1\xFFFF\x1\x8";
		private const string DFA10_specialS =
			"\x14\xFFFF}>";
		private static readonly string[] DFA10_transitionS =
			{
				"\x1\xD\x1\xC\x2\xFFFF\x1\xB\x12\xFFFF\x1\xD\x7\xFFFF\x1\x1\x1\x2\x1"+
				"\x3\x1\x4\x1\xFFFF\x1\x5\x1\xFFFF\x1\x6\xA\xA\x3\xFFFF\x1\x7\x3\xFFFF"+
				"\x1A\x9\x6\xFFFF\xB\x9\x1\x8\xE\x9",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x1\xE",
				"",
				"\x1\x10\x1\xFFFF\xA\xA",
				"\x1\xC",
				"\x2\xD\x2\xFFFF\x1\xD\x12\xFFFF\x1\xD",
				"",
				"\x1\x12",
				"",
				"",
				"",
				"\xA\x9\x7\xFFFF\x1A\x9\x6\xFFFF\x1A\x9",
				""
			};

		private static readonly short[] DFA10_eot = DFA.UnpackEncodedString(DFA10_eotS);
		private static readonly short[] DFA10_eof = DFA.UnpackEncodedString(DFA10_eofS);
		private static readonly char[] DFA10_min = DFA.UnpackEncodedStringToUnsignedChars(DFA10_minS);
		private static readonly char[] DFA10_max = DFA.UnpackEncodedStringToUnsignedChars(DFA10_maxS);
		private static readonly short[] DFA10_accept = DFA.UnpackEncodedString(DFA10_acceptS);
		private static readonly short[] DFA10_special = DFA.UnpackEncodedString(DFA10_specialS);
		private static readonly short[][] DFA10_transition;

		static DFA10()
		{
			int numStates = DFA10_transitionS.Length;
			DFA10_transition = new short[numStates][];
			for ( int i=0; i < numStates; i++ )
			{
				DFA10_transition[i] = DFA.UnpackEncodedString(DFA10_transitionS[i]);
			}
		}

		public DFA10( BaseRecognizer recognizer )
		{
			this.recognizer = recognizer;
			this.decisionNumber = 10;
			this.eot = DFA10_eot;
			this.eof = DFA10_eof;
			this.min = DFA10_min;
			this.max = DFA10_max;
			this.accept = DFA10_accept;
			this.special = DFA10_special;
			this.transition = DFA10_transition;
		}

		public override string Description { get { return "1:1: Tokens : ( T__10 | T__11 | T__12 | T__13 | T__14 | T__15 | T__16 | T__17 | IDENTIFIER | INTEGER | FLOAT | NEWLINE | WS );"; } }

		public override void Error(NoViableAltException nvae)
		{
			DebugRecognitionException(nvae);
		}
	}

 
	#endregion

}
