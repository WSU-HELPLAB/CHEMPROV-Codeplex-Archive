//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 3.4
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// $ANTLR 3.4 D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g 2012-02-08 11:12:49

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 219
// Unreachable code detected.
#pragma warning disable 162
// Missing XML comment for publicly visible type or member 'Type_or_Member'
#pragma warning disable 1591
// CLS compliance checking will not be performed on 'type' because it is not visible from outside this assembly.
#pragma warning disable 3019


	using System.Collections;
	using System;


using System.Collections.Generic;
using Antlr.Runtime;
using Antlr.Runtime.Misc;
using Antlr.Runtime.Tree;
using RewriteRuleITokenStream = Antlr.Runtime.Tree.RewriteRuleTokenStream;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "3.4")]
[System.CLSCompliant(false)]
public partial class Eval : Antlr.Runtime.Tree.TreeParser
{
	internal static readonly string[] tokenNames = new string[] {
		"<invalid>", "<EOR>", "<DOWN>", "<UP>", "ID", "INT", "NEWLINE", "WS", "'('", "')'", "'*'", "'+'", "'-'", "'='"
	};
	public const int EOF=-1;
	public const int T__8=8;
	public const int T__9=9;
	public const int T__10=10;
	public const int T__11=11;
	public const int T__12=12;
	public const int T__13=13;
	public const int ID=4;
	public const int INT=5;
	public const int NEWLINE=6;
	public const int WS=7;

	public Eval(ITreeNodeStream input)
		: this(input, new RecognizerSharedState())
	{
	}
	public Eval(ITreeNodeStream input, RecognizerSharedState state)
		: base(input, state)
	{
		OnCreated();
	}

	public override string[] TokenNames { get { return Eval.tokenNames; } }
	public override string GrammarFileName { get { return "D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g"; } }


		Hashtable memory = new Hashtable();


	partial void OnCreated();
	partial void EnterRule(string ruleName, int ruleIndex);
	partial void LeaveRule(string ruleName, int ruleIndex);

	#region Rules
	partial void EnterRule_expr();
	partial void LeaveRule_expr();

	// $ANTLR start "expr"
	// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:17:1: expr returns [int value] : ( ^( '+' a= expr b= expr ) | ^( '-' a= expr b= expr ) | ^( '*' a= expr b= expr ) | ID | INT );
	[GrammarRule("expr")]
	private int expr()
	{
		EnterRule_expr();
		EnterRule("expr", 1);
		TraceIn("expr", 1);
		int value = default(int);


		CommonTree ID1 = default(CommonTree);
		CommonTree INT2 = default(CommonTree);
		int a = default(int);
		int b = default(int);

		try { DebugEnterRule(GrammarFileName, "expr");
		DebugLocation(17, 1);
		try
		{
			// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:18:2: ( ^( '+' a= expr b= expr ) | ^( '-' a= expr b= expr ) | ^( '*' a= expr b= expr ) | ID | INT )
			int alt1=5;
			try { DebugEnterDecision(1, false);
			switch (input.LA(1))
			{
			case 11:
				{
				alt1 = 1;
				}
				break;
			case 12:
				{
				alt1 = 2;
				}
				break;
			case 10:
				{
				alt1 = 3;
				}
				break;
			case ID:
				{
				alt1 = 4;
				}
				break;
			case INT:
				{
				alt1 = 5;
				}
				break;
			default:
				{
					NoViableAltException nvae = new NoViableAltException("", 1, 0, input);
					DebugRecognitionException(nvae);
					throw nvae;
				}
			}

			} finally { DebugExitDecision(1); }
			switch (alt1)
			{
			case 1:
				DebugEnterAlt(1);
				// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:18:4: ^( '+' a= expr b= expr )
				{
				DebugLocation(18, 4);
				DebugLocation(18, 6);
				Match(input,11,Follow._11_in_expr49); 

				Match(input, TokenTypes.Down, null); 
				DebugLocation(18, 11);
				PushFollow(Follow._expr_in_expr53);
				a=expr();
				PopFollow();

				DebugLocation(18, 18);
				PushFollow(Follow._expr_in_expr57);
				b=expr();
				PopFollow();


				Match(input, TokenTypes.Up, null); 

				DebugLocation(18, 25);
				value = a+b;

				}
				break;
			case 2:
				DebugEnterAlt(2);
				// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:19:4: ^( '-' a= expr b= expr )
				{
				DebugLocation(19, 4);
				DebugLocation(19, 6);
				Match(input,12,Follow._12_in_expr66); 

				Match(input, TokenTypes.Down, null); 
				DebugLocation(19, 11);
				PushFollow(Follow._expr_in_expr70);
				a=expr();
				PopFollow();

				DebugLocation(19, 18);
				PushFollow(Follow._expr_in_expr74);
				b=expr();
				PopFollow();


				Match(input, TokenTypes.Up, null); 

				DebugLocation(19, 25);
				value = a-b;

				}
				break;
			case 3:
				DebugEnterAlt(3);
				// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:20:4: ^( '*' a= expr b= expr )
				{
				DebugLocation(20, 4);
				DebugLocation(20, 6);
				Match(input,10,Follow._10_in_expr83); 

				Match(input, TokenTypes.Down, null); 
				DebugLocation(20, 11);
				PushFollow(Follow._expr_in_expr87);
				a=expr();
				PopFollow();

				DebugLocation(20, 18);
				PushFollow(Follow._expr_in_expr91);
				b=expr();
				PopFollow();


				Match(input, TokenTypes.Up, null); 

				DebugLocation(20, 25);
				value = a*b;

				}
				break;
			case 4:
				DebugEnterAlt(4);
				// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:21:4: ID
				{
				DebugLocation(21, 4);
				ID1=(CommonTree)Match(input,ID,Follow._ID_in_expr99); 
				DebugLocation(22, 6);

						    int v = (int)memory[(ID1!=null?ID1.Text:null)];
						    if(v != null)
						    {
						    	value = v;
						    }
						    else
						    {
						    	Console.WriteLine("undefined variable " + (ID1!=null?ID1.Text:null));
						    }
					    

				}
				break;
			case 5:
				DebugEnterAlt(5);
				// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:33:4: INT
				{
				DebugLocation(33, 4);
				INT2=(CommonTree)Match(input,INT,Follow._INT_in_expr111); 
				DebugLocation(33, 8);
				value = Convert.ToInt32((INT2!=null?INT2.Text:null));

				}
				break;

			}
		}
		catch (RecognitionException re)
		{
			ReportError(re);
			Recover(input,re);
		}
		finally
		{
			TraceOut("expr", 1);
			LeaveRule("expr", 1);
			LeaveRule_expr();
		}
		DebugLocation(34, 1);
		} finally { DebugExitRule(GrammarFileName, "expr"); }
		return value;

	}
	// $ANTLR end "expr"

	partial void EnterRule_prog();
	partial void LeaveRule_prog();

	// $ANTLR start "prog"
	// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:36:8: public prog : ( stat )+ ;
	[GrammarRule("prog")]
	public void prog()
	{
		EnterRule_prog();
		EnterRule("prog", 2);
		TraceIn("prog", 2);
		try { DebugEnterRule(GrammarFileName, "prog");
		DebugLocation(36, 20);
		try
		{
			// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:36:13: ( ( stat )+ )
			DebugEnterAlt(1);
			// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:36:16: ( stat )+
			{
			DebugLocation(36, 16);
			// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:36:16: ( stat )+
			int cnt2=0;
			try { DebugEnterSubRule(2);
			while (true)
			{
				int alt2=2;
				try { DebugEnterDecision(2, false);
				int LA2_0 = input.LA(1);

				if (((LA2_0>=ID && LA2_0<=INT)||(LA2_0>=10 && LA2_0<=13)))
				{
					alt2 = 1;
				}


				} finally { DebugExitDecision(2); }
				switch (alt2)
				{
				case 1:
					DebugEnterAlt(1);
					// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:36:16: stat
					{
					DebugLocation(36, 16);
					PushFollow(Follow._stat_in_prog126);
					stat();
					PopFollow();


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


			}

		}
		catch (RecognitionException re)
		{
			ReportError(re);
			Recover(input,re);
		}
		finally
		{
			TraceOut("prog", 2);
			LeaveRule("prog", 2);
			LeaveRule_prog();
		}
		DebugLocation(36, 20);
		} finally { DebugExitRule(GrammarFileName, "prog"); }
		return;

	}
	// $ANTLR end "prog"

	partial void EnterRule_stat();
	partial void LeaveRule_stat();

	// $ANTLR start "stat"
	// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:37:1: stat : ( expr | ^( '=' ID expr ) );
	[GrammarRule("stat")]
	private void stat()
	{
		EnterRule_stat();
		EnterRule("stat", 3);
		TraceIn("stat", 3);
		CommonTree ID4 = default(CommonTree);
		int expr3 = default(int);
		int expr5 = default(int);

		try { DebugEnterRule(GrammarFileName, "stat");
		DebugLocation(37, 1);
		try
		{
			// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:37:6: ( expr | ^( '=' ID expr ) )
			int alt3=2;
			try { DebugEnterDecision(3, false);
			int LA3_0 = input.LA(1);

			if (((LA3_0>=ID && LA3_0<=INT)||(LA3_0>=10 && LA3_0<=12)))
			{
				alt3 = 1;
			}
			else if ((LA3_0==13))
			{
				alt3 = 2;
			}
			else
			{
				NoViableAltException nvae = new NoViableAltException("", 3, 0, input);
				DebugRecognitionException(nvae);
				throw nvae;
			}
			} finally { DebugExitDecision(3); }
			switch (alt3)
			{
			case 1:
				DebugEnterAlt(1);
				// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:37:8: expr
				{
				DebugLocation(37, 8);
				PushFollow(Follow._expr_in_stat134);
				expr3=expr();
				PopFollow();

				DebugLocation(37, 13);
				Console.WriteLine(expr3);

				}
				break;
			case 2:
				DebugEnterAlt(2);
				// D:\\code\\chemprov_codeplex\\branches\\antlr-sandbox\\antlr_sandbox\\Eval.g:38:4: ^( '=' ID expr )
				{
				DebugLocation(38, 4);
				DebugLocation(38, 6);
				Match(input,13,Follow._13_in_stat142); 

				Match(input, TokenTypes.Down, null); 
				DebugLocation(38, 10);
				ID4=(CommonTree)Match(input,ID,Follow._ID_in_stat144); 
				DebugLocation(38, 13);
				PushFollow(Follow._expr_in_stat146);
				expr5=expr();
				PopFollow();


				Match(input, TokenTypes.Up, null); 

				DebugLocation(38, 19);
				memory.Add((ID4!=null?ID4.Text:null), Convert.ToInt32(expr5));

				}
				break;

			}
		}
		catch (RecognitionException re)
		{
			ReportError(re);
			Recover(input,re);
		}
		finally
		{
			TraceOut("stat", 3);
			LeaveRule("stat", 3);
			LeaveRule_stat();
		}
		DebugLocation(39, 1);
		} finally { DebugExitRule(GrammarFileName, "stat"); }
		return;

	}
	// $ANTLR end "stat"
	#endregion Rules


	#region Follow sets
	private static class Follow
	{
		public static readonly BitSet _11_in_expr49 = new BitSet(new ulong[]{0x4UL});
		public static readonly BitSet _expr_in_expr53 = new BitSet(new ulong[]{0x1C30UL});
		public static readonly BitSet _expr_in_expr57 = new BitSet(new ulong[]{0x8UL});
		public static readonly BitSet _12_in_expr66 = new BitSet(new ulong[]{0x4UL});
		public static readonly BitSet _expr_in_expr70 = new BitSet(new ulong[]{0x1C30UL});
		public static readonly BitSet _expr_in_expr74 = new BitSet(new ulong[]{0x8UL});
		public static readonly BitSet _10_in_expr83 = new BitSet(new ulong[]{0x4UL});
		public static readonly BitSet _expr_in_expr87 = new BitSet(new ulong[]{0x1C30UL});
		public static readonly BitSet _expr_in_expr91 = new BitSet(new ulong[]{0x8UL});
		public static readonly BitSet _ID_in_expr99 = new BitSet(new ulong[]{0x2UL});
		public static readonly BitSet _INT_in_expr111 = new BitSet(new ulong[]{0x2UL});
		public static readonly BitSet _stat_in_prog126 = new BitSet(new ulong[]{0x3C32UL});
		public static readonly BitSet _expr_in_stat134 = new BitSet(new ulong[]{0x2UL});
		public static readonly BitSet _13_in_stat142 = new BitSet(new ulong[]{0x4UL});
		public static readonly BitSet _ID_in_stat144 = new BitSet(new ulong[]{0x1C30UL});
		public static readonly BitSet _expr_in_stat146 = new BitSet(new ulong[]{0x8UL});
	}
	#endregion Follow sets
}