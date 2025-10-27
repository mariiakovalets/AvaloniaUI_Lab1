// Generated from /Users/mariakovalec/Desktop/uni/CSharp/TableManager/TableManager.App/Controllers/TableExpression.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue"})
public class TableExpressionParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, NUMBER=17, 
		CELL_REF=18, WS=19;
	public static final int
		RULE_formula = 0, RULE_expression = 1, RULE_arithmeticExpr = 2, RULE_comparisonExpr = 3;
	private static String[] makeRuleNames() {
		return new String[] {
			"formula", "expression", "arithmeticExpr", "comparisonExpr"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'='", "'('", "')'", "'+'", "'-'", "'*'", "'/'", "' mod '", "' div '", 
			"'inc'", "'dec'", "'<='", "'>='", "'<>'", "'<'", "'>'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, "NUMBER", "CELL_REF", "WS"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "TableExpression.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public TableExpressionParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@SuppressWarnings("CheckReturnValue")
	public static class FormulaContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode EOF() { return getToken(TableExpressionParser.EOF, 0); }
		public FormulaContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_formula; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterFormula(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitFormula(this);
		}
	}

	public final FormulaContext formula() throws RecognitionException {
		FormulaContext _localctx = new FormulaContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_formula);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(8);
			match(T__0);
			setState(9);
			match(T__1);
			setState(10);
			expression();
			setState(11);
			match(T__2);
			setState(12);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ExpressionContext extends ParserRuleContext {
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	 
		public ExpressionContext() { }
		public void copyFrom(ExpressionContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ComparisonExpressionContext extends ExpressionContext {
		public ComparisonExprContext comparisonExpr() {
			return getRuleContext(ComparisonExprContext.class,0);
		}
		public ComparisonExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterComparisonExpression(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitComparisonExpression(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ArithmeticExpressionContext extends ExpressionContext {
		public ArithmeticExprContext arithmeticExpr() {
			return getRuleContext(ArithmeticExprContext.class,0);
		}
		public ArithmeticExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterArithmeticExpression(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitArithmeticExpression(this);
		}
	}

	public final ExpressionContext expression() throws RecognitionException {
		ExpressionContext _localctx = new ExpressionContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_expression);
		try {
			setState(16);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,0,_ctx) ) {
			case 1:
				_localctx = new ArithmeticExpressionContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(14);
				arithmeticExpr(0);
				}
				break;
			case 2:
				_localctx = new ComparisonExpressionContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(15);
				comparisonExpr();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ArithmeticExprContext extends ParserRuleContext {
		public ArithmeticExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arithmeticExpr; }
	 
		public ArithmeticExprContext() { }
		public void copyFrom(ArithmeticExprContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ModDivContext extends ArithmeticExprContext {
		public List<ArithmeticExprContext> arithmeticExpr() {
			return getRuleContexts(ArithmeticExprContext.class);
		}
		public ArithmeticExprContext arithmeticExpr(int i) {
			return getRuleContext(ArithmeticExprContext.class,i);
		}
		public ModDivContext(ArithmeticExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterModDiv(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitModDiv(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class DecContext extends ArithmeticExprContext {
		public ArithmeticExprContext arithmeticExpr() {
			return getRuleContext(ArithmeticExprContext.class,0);
		}
		public DecContext(ArithmeticExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterDec(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitDec(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class NumberContext extends ArithmeticExprContext {
		public TerminalNode NUMBER() { return getToken(TableExpressionParser.NUMBER, 0); }
		public NumberContext(ArithmeticExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterNumber(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitNumber(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class AddSubContext extends ArithmeticExprContext {
		public List<ArithmeticExprContext> arithmeticExpr() {
			return getRuleContexts(ArithmeticExprContext.class);
		}
		public ArithmeticExprContext arithmeticExpr(int i) {
			return getRuleContext(ArithmeticExprContext.class,i);
		}
		public AddSubContext(ArithmeticExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterAddSub(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitAddSub(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class MulDivContext extends ArithmeticExprContext {
		public List<ArithmeticExprContext> arithmeticExpr() {
			return getRuleContexts(ArithmeticExprContext.class);
		}
		public ArithmeticExprContext arithmeticExpr(int i) {
			return getRuleContext(ArithmeticExprContext.class,i);
		}
		public MulDivContext(ArithmeticExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterMulDiv(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitMulDiv(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ParenArithmeticContext extends ArithmeticExprContext {
		public ArithmeticExprContext arithmeticExpr() {
			return getRuleContext(ArithmeticExprContext.class,0);
		}
		public ParenArithmeticContext(ArithmeticExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterParenArithmetic(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitParenArithmetic(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class CellReferenceContext extends ArithmeticExprContext {
		public TerminalNode CELL_REF() { return getToken(TableExpressionParser.CELL_REF, 0); }
		public CellReferenceContext(ArithmeticExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterCellReference(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitCellReference(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class IncContext extends ArithmeticExprContext {
		public ArithmeticExprContext arithmeticExpr() {
			return getRuleContext(ArithmeticExprContext.class,0);
		}
		public IncContext(ArithmeticExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterInc(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitInc(this);
		}
	}

	public final ArithmeticExprContext arithmeticExpr() throws RecognitionException {
		return arithmeticExpr(0);
	}

	private ArithmeticExprContext arithmeticExpr(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ArithmeticExprContext _localctx = new ArithmeticExprContext(_ctx, _parentState);
		ArithmeticExprContext _prevctx = _localctx;
		int _startState = 4;
		enterRecursionRule(_localctx, 4, RULE_arithmeticExpr, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(35);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__9:
				{
				_localctx = new IncContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(19);
				match(T__9);
				setState(20);
				match(T__1);
				setState(21);
				arithmeticExpr(0);
				setState(22);
				match(T__2);
				}
				break;
			case T__10:
				{
				_localctx = new DecContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(24);
				match(T__10);
				setState(25);
				match(T__1);
				setState(26);
				arithmeticExpr(0);
				setState(27);
				match(T__2);
				}
				break;
			case T__1:
				{
				_localctx = new ParenArithmeticContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(29);
				match(T__1);
				setState(30);
				arithmeticExpr(0);
				setState(31);
				match(T__2);
				}
				break;
			case NUMBER:
				{
				_localctx = new NumberContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(33);
				match(NUMBER);
				}
				break;
			case CELL_REF:
				{
				_localctx = new CellReferenceContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(34);
				match(CELL_REF);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			_ctx.stop = _input.LT(-1);
			setState(48);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,3,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(46);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,2,_ctx) ) {
					case 1:
						{
						_localctx = new AddSubContext(new ArithmeticExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_arithmeticExpr);
						setState(37);
						if (!(precpred(_ctx, 8))) throw new FailedPredicateException(this, "precpred(_ctx, 8)");
						setState(38);
						_la = _input.LA(1);
						if ( !(_la==T__3 || _la==T__4) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(39);
						arithmeticExpr(9);
						}
						break;
					case 2:
						{
						_localctx = new MulDivContext(new ArithmeticExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_arithmeticExpr);
						setState(40);
						if (!(precpred(_ctx, 7))) throw new FailedPredicateException(this, "precpred(_ctx, 7)");
						setState(41);
						_la = _input.LA(1);
						if ( !(_la==T__5 || _la==T__6) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(42);
						arithmeticExpr(8);
						}
						break;
					case 3:
						{
						_localctx = new ModDivContext(new ArithmeticExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_arithmeticExpr);
						setState(43);
						if (!(precpred(_ctx, 6))) throw new FailedPredicateException(this, "precpred(_ctx, 6)");
						setState(44);
						_la = _input.LA(1);
						if ( !(_la==T__7 || _la==T__8) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(45);
						arithmeticExpr(7);
						}
						break;
					}
					} 
				}
				setState(50);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,3,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ComparisonExprContext extends ParserRuleContext {
		public ComparisonExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_comparisonExpr; }
	 
		public ComparisonExprContext() { }
		public void copyFrom(ComparisonExprContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class LessOrEqualContext extends ComparisonExprContext {
		public List<ArithmeticExprContext> arithmeticExpr() {
			return getRuleContexts(ArithmeticExprContext.class);
		}
		public ArithmeticExprContext arithmeticExpr(int i) {
			return getRuleContext(ArithmeticExprContext.class,i);
		}
		public LessOrEqualContext(ComparisonExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterLessOrEqual(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitLessOrEqual(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class LessThanContext extends ComparisonExprContext {
		public List<ArithmeticExprContext> arithmeticExpr() {
			return getRuleContexts(ArithmeticExprContext.class);
		}
		public ArithmeticExprContext arithmeticExpr(int i) {
			return getRuleContext(ArithmeticExprContext.class,i);
		}
		public LessThanContext(ComparisonExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterLessThan(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitLessThan(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class NotEqualContext extends ComparisonExprContext {
		public List<ArithmeticExprContext> arithmeticExpr() {
			return getRuleContexts(ArithmeticExprContext.class);
		}
		public ArithmeticExprContext arithmeticExpr(int i) {
			return getRuleContext(ArithmeticExprContext.class,i);
		}
		public NotEqualContext(ComparisonExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterNotEqual(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitNotEqual(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class EqualContext extends ComparisonExprContext {
		public List<ArithmeticExprContext> arithmeticExpr() {
			return getRuleContexts(ArithmeticExprContext.class);
		}
		public ArithmeticExprContext arithmeticExpr(int i) {
			return getRuleContext(ArithmeticExprContext.class,i);
		}
		public EqualContext(ComparisonExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterEqual(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitEqual(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class GreaterThanContext extends ComparisonExprContext {
		public List<ArithmeticExprContext> arithmeticExpr() {
			return getRuleContexts(ArithmeticExprContext.class);
		}
		public ArithmeticExprContext arithmeticExpr(int i) {
			return getRuleContext(ArithmeticExprContext.class,i);
		}
		public GreaterThanContext(ComparisonExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterGreaterThan(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitGreaterThan(this);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class GreaterOrEqualContext extends ComparisonExprContext {
		public List<ArithmeticExprContext> arithmeticExpr() {
			return getRuleContexts(ArithmeticExprContext.class);
		}
		public ArithmeticExprContext arithmeticExpr(int i) {
			return getRuleContext(ArithmeticExprContext.class,i);
		}
		public GreaterOrEqualContext(ComparisonExprContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).enterGreaterOrEqual(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof TableExpressionListener ) ((TableExpressionListener)listener).exitGreaterOrEqual(this);
		}
	}

	public final ComparisonExprContext comparisonExpr() throws RecognitionException {
		ComparisonExprContext _localctx = new ComparisonExprContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_comparisonExpr);
		try {
			setState(75);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,4,_ctx) ) {
			case 1:
				_localctx = new LessOrEqualContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(51);
				arithmeticExpr(0);
				setState(52);
				match(T__11);
				setState(53);
				arithmeticExpr(0);
				}
				break;
			case 2:
				_localctx = new GreaterOrEqualContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(55);
				arithmeticExpr(0);
				setState(56);
				match(T__12);
				setState(57);
				arithmeticExpr(0);
				}
				break;
			case 3:
				_localctx = new NotEqualContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(59);
				arithmeticExpr(0);
				setState(60);
				match(T__13);
				setState(61);
				arithmeticExpr(0);
				}
				break;
			case 4:
				_localctx = new EqualContext(_localctx);
				enterOuterAlt(_localctx, 4);
				{
				setState(63);
				arithmeticExpr(0);
				setState(64);
				match(T__0);
				setState(65);
				arithmeticExpr(0);
				}
				break;
			case 5:
				_localctx = new LessThanContext(_localctx);
				enterOuterAlt(_localctx, 5);
				{
				setState(67);
				arithmeticExpr(0);
				setState(68);
				match(T__14);
				setState(69);
				arithmeticExpr(0);
				}
				break;
			case 6:
				_localctx = new GreaterThanContext(_localctx);
				enterOuterAlt(_localctx, 6);
				{
				setState(71);
				arithmeticExpr(0);
				setState(72);
				match(T__15);
				setState(73);
				arithmeticExpr(0);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 2:
			return arithmeticExpr_sempred((ArithmeticExprContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean arithmeticExpr_sempred(ArithmeticExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 8);
		case 1:
			return precpred(_ctx, 7);
		case 2:
			return precpred(_ctx, 6);
		}
		return true;
	}

	public static final String _serializedATN =
		"\u0004\u0001\u0013N\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001\u0002"+
		"\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0001\u0000\u0001\u0000\u0001"+
		"\u0000\u0001\u0000\u0001\u0000\u0001\u0000\u0001\u0001\u0001\u0001\u0003"+
		"\u0001\u0011\b\u0001\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001"+
		"\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001"+
		"\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001"+
		"\u0002\u0003\u0002$\b\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001"+
		"\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0005"+
		"\u0002/\b\u0002\n\u0002\f\u00022\t\u0002\u0001\u0003\u0001\u0003\u0001"+
		"\u0003\u0001\u0003\u0001\u0003\u0001\u0003\u0001\u0003\u0001\u0003\u0001"+
		"\u0003\u0001\u0003\u0001\u0003\u0001\u0003\u0001\u0003\u0001\u0003\u0001"+
		"\u0003\u0001\u0003\u0001\u0003\u0001\u0003\u0001\u0003\u0001\u0003\u0001"+
		"\u0003\u0001\u0003\u0001\u0003\u0001\u0003\u0003\u0003L\b\u0003\u0001"+
		"\u0003\u0000\u0001\u0004\u0004\u0000\u0002\u0004\u0006\u0000\u0003\u0001"+
		"\u0000\u0004\u0005\u0001\u0000\u0006\u0007\u0001\u0000\b\tV\u0000\b\u0001"+
		"\u0000\u0000\u0000\u0002\u0010\u0001\u0000\u0000\u0000\u0004#\u0001\u0000"+
		"\u0000\u0000\u0006K\u0001\u0000\u0000\u0000\b\t\u0005\u0001\u0000\u0000"+
		"\t\n\u0005\u0002\u0000\u0000\n\u000b\u0003\u0002\u0001\u0000\u000b\f\u0005"+
		"\u0003\u0000\u0000\f\r\u0005\u0000\u0000\u0001\r\u0001\u0001\u0000\u0000"+
		"\u0000\u000e\u0011\u0003\u0004\u0002\u0000\u000f\u0011\u0003\u0006\u0003"+
		"\u0000\u0010\u000e\u0001\u0000\u0000\u0000\u0010\u000f\u0001\u0000\u0000"+
		"\u0000\u0011\u0003\u0001\u0000\u0000\u0000\u0012\u0013\u0006\u0002\uffff"+
		"\uffff\u0000\u0013\u0014\u0005\n\u0000\u0000\u0014\u0015\u0005\u0002\u0000"+
		"\u0000\u0015\u0016\u0003\u0004\u0002\u0000\u0016\u0017\u0005\u0003\u0000"+
		"\u0000\u0017$\u0001\u0000\u0000\u0000\u0018\u0019\u0005\u000b\u0000\u0000"+
		"\u0019\u001a\u0005\u0002\u0000\u0000\u001a\u001b\u0003\u0004\u0002\u0000"+
		"\u001b\u001c\u0005\u0003\u0000\u0000\u001c$\u0001\u0000\u0000\u0000\u001d"+
		"\u001e\u0005\u0002\u0000\u0000\u001e\u001f\u0003\u0004\u0002\u0000\u001f"+
		" \u0005\u0003\u0000\u0000 $\u0001\u0000\u0000\u0000!$\u0005\u0011\u0000"+
		"\u0000\"$\u0005\u0012\u0000\u0000#\u0012\u0001\u0000\u0000\u0000#\u0018"+
		"\u0001\u0000\u0000\u0000#\u001d\u0001\u0000\u0000\u0000#!\u0001\u0000"+
		"\u0000\u0000#\"\u0001\u0000\u0000\u0000$0\u0001\u0000\u0000\u0000%&\n"+
		"\b\u0000\u0000&\'\u0007\u0000\u0000\u0000\'/\u0003\u0004\u0002\t()\n\u0007"+
		"\u0000\u0000)*\u0007\u0001\u0000\u0000*/\u0003\u0004\u0002\b+,\n\u0006"+
		"\u0000\u0000,-\u0007\u0002\u0000\u0000-/\u0003\u0004\u0002\u0007.%\u0001"+
		"\u0000\u0000\u0000.(\u0001\u0000\u0000\u0000.+\u0001\u0000\u0000\u0000"+
		"/2\u0001\u0000\u0000\u00000.\u0001\u0000\u0000\u000001\u0001\u0000\u0000"+
		"\u00001\u0005\u0001\u0000\u0000\u000020\u0001\u0000\u0000\u000034\u0003"+
		"\u0004\u0002\u000045\u0005\f\u0000\u000056\u0003\u0004\u0002\u00006L\u0001"+
		"\u0000\u0000\u000078\u0003\u0004\u0002\u000089\u0005\r\u0000\u00009:\u0003"+
		"\u0004\u0002\u0000:L\u0001\u0000\u0000\u0000;<\u0003\u0004\u0002\u0000"+
		"<=\u0005\u000e\u0000\u0000=>\u0003\u0004\u0002\u0000>L\u0001\u0000\u0000"+
		"\u0000?@\u0003\u0004\u0002\u0000@A\u0005\u0001\u0000\u0000AB\u0003\u0004"+
		"\u0002\u0000BL\u0001\u0000\u0000\u0000CD\u0003\u0004\u0002\u0000DE\u0005"+
		"\u000f\u0000\u0000EF\u0003\u0004\u0002\u0000FL\u0001\u0000\u0000\u0000"+
		"GH\u0003\u0004\u0002\u0000HI\u0005\u0010\u0000\u0000IJ\u0003\u0004\u0002"+
		"\u0000JL\u0001\u0000\u0000\u0000K3\u0001\u0000\u0000\u0000K7\u0001\u0000"+
		"\u0000\u0000K;\u0001\u0000\u0000\u0000K?\u0001\u0000\u0000\u0000KC\u0001"+
		"\u0000\u0000\u0000KG\u0001\u0000\u0000\u0000L\u0007\u0001\u0000\u0000"+
		"\u0000\u0005\u0010#.0K";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}