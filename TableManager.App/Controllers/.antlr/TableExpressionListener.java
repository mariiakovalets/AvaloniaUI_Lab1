// Generated from /Users/mariakovalec/Desktop/uni/CSharp/TableManager/TableManager.App/Controllers/TableExpression.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link TableExpressionParser}.
 */
public interface TableExpressionListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link TableExpressionParser#formula}.
	 * @param ctx the parse tree
	 */
	void enterFormula(TableExpressionParser.FormulaContext ctx);
	/**
	 * Exit a parse tree produced by {@link TableExpressionParser#formula}.
	 * @param ctx the parse tree
	 */
	void exitFormula(TableExpressionParser.FormulaContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ArithmeticExpression}
	 * labeled alternative in {@link TableExpressionParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterArithmeticExpression(TableExpressionParser.ArithmeticExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ArithmeticExpression}
	 * labeled alternative in {@link TableExpressionParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitArithmeticExpression(TableExpressionParser.ArithmeticExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ComparisonExpression}
	 * labeled alternative in {@link TableExpressionParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterComparisonExpression(TableExpressionParser.ComparisonExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ComparisonExpression}
	 * labeled alternative in {@link TableExpressionParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitComparisonExpression(TableExpressionParser.ComparisonExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ModDiv}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void enterModDiv(TableExpressionParser.ModDivContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ModDiv}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void exitModDiv(TableExpressionParser.ModDivContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Dec}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void enterDec(TableExpressionParser.DecContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Dec}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void exitDec(TableExpressionParser.DecContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Number}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void enterNumber(TableExpressionParser.NumberContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Number}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void exitNumber(TableExpressionParser.NumberContext ctx);
	/**
	 * Enter a parse tree produced by the {@code AddSub}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void enterAddSub(TableExpressionParser.AddSubContext ctx);
	/**
	 * Exit a parse tree produced by the {@code AddSub}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void exitAddSub(TableExpressionParser.AddSubContext ctx);
	/**
	 * Enter a parse tree produced by the {@code MulDiv}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void enterMulDiv(TableExpressionParser.MulDivContext ctx);
	/**
	 * Exit a parse tree produced by the {@code MulDiv}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void exitMulDiv(TableExpressionParser.MulDivContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ParenArithmetic}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void enterParenArithmetic(TableExpressionParser.ParenArithmeticContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ParenArithmetic}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void exitParenArithmetic(TableExpressionParser.ParenArithmeticContext ctx);
	/**
	 * Enter a parse tree produced by the {@code CellReference}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void enterCellReference(TableExpressionParser.CellReferenceContext ctx);
	/**
	 * Exit a parse tree produced by the {@code CellReference}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void exitCellReference(TableExpressionParser.CellReferenceContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Inc}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void enterInc(TableExpressionParser.IncContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Inc}
	 * labeled alternative in {@link TableExpressionParser#arithmeticExpr}.
	 * @param ctx the parse tree
	 */
	void exitInc(TableExpressionParser.IncContext ctx);
	/**
	 * Enter a parse tree produced by the {@code LessOrEqual}
	 * labeled alternative in {@link TableExpressionParser#comparisonExpr}.
	 * @param ctx the parse tree
	 */
	void enterLessOrEqual(TableExpressionParser.LessOrEqualContext ctx);
	/**
	 * Exit a parse tree produced by the {@code LessOrEqual}
	 * labeled alternative in {@link TableExpressionParser#comparisonExpr}.
	 * @param ctx the parse tree
	 */
	void exitLessOrEqual(TableExpressionParser.LessOrEqualContext ctx);
	/**
	 * Enter a parse tree produced by the {@code GreaterOrEqual}
	 * labeled alternative in {@link TableExpressionParser#comparisonExpr}.
	 * @param ctx the parse tree
	 */
	void enterGreaterOrEqual(TableExpressionParser.GreaterOrEqualContext ctx);
	/**
	 * Exit a parse tree produced by the {@code GreaterOrEqual}
	 * labeled alternative in {@link TableExpressionParser#comparisonExpr}.
	 * @param ctx the parse tree
	 */
	void exitGreaterOrEqual(TableExpressionParser.GreaterOrEqualContext ctx);
	/**
	 * Enter a parse tree produced by the {@code NotEqual}
	 * labeled alternative in {@link TableExpressionParser#comparisonExpr}.
	 * @param ctx the parse tree
	 */
	void enterNotEqual(TableExpressionParser.NotEqualContext ctx);
	/**
	 * Exit a parse tree produced by the {@code NotEqual}
	 * labeled alternative in {@link TableExpressionParser#comparisonExpr}.
	 * @param ctx the parse tree
	 */
	void exitNotEqual(TableExpressionParser.NotEqualContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Equal}
	 * labeled alternative in {@link TableExpressionParser#comparisonExpr}.
	 * @param ctx the parse tree
	 */
	void enterEqual(TableExpressionParser.EqualContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Equal}
	 * labeled alternative in {@link TableExpressionParser#comparisonExpr}.
	 * @param ctx the parse tree
	 */
	void exitEqual(TableExpressionParser.EqualContext ctx);
	/**
	 * Enter a parse tree produced by the {@code LessThan}
	 * labeled alternative in {@link TableExpressionParser#comparisonExpr}.
	 * @param ctx the parse tree
	 */
	void enterLessThan(TableExpressionParser.LessThanContext ctx);
	/**
	 * Exit a parse tree produced by the {@code LessThan}
	 * labeled alternative in {@link TableExpressionParser#comparisonExpr}.
	 * @param ctx the parse tree
	 */
	void exitLessThan(TableExpressionParser.LessThanContext ctx);
	/**
	 * Enter a parse tree produced by the {@code GreaterThan}
	 * labeled alternative in {@link TableExpressionParser#comparisonExpr}.
	 * @param ctx the parse tree
	 */
	void enterGreaterThan(TableExpressionParser.GreaterThanContext ctx);
	/**
	 * Exit a parse tree produced by the {@code GreaterThan}
	 * labeled alternative in {@link TableExpressionParser#comparisonExpr}.
	 * @param ctx the parse tree
	 */
	void exitGreaterThan(TableExpressionParser.GreaterThanContext ctx);
}