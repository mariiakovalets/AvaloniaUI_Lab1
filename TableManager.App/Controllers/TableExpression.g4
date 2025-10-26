grammar TableExpression;

formula: '=' '(' expression ')' EOF;

expression
    : arithmeticExpr     # ArithmeticExpression
    | comparisonExpr     # ComparisonExpression
    ;

arithmeticExpr
    : arithmeticExpr ('+' | '-') arithmeticExpr          # AddSub
    | arithmeticExpr ('*' | '/') arithmeticExpr          # MulDiv
    | arithmeticExpr (' mod ' | ' div ') arithmeticExpr  # ModDiv
    | 'inc' '(' arithmeticExpr ')'                       # Inc
    | 'dec' '(' arithmeticExpr ')'                       # Dec
    | '(' arithmeticExpr ')'                             # ParenArithmetic
    | NUMBER                                             # Number
    | CELL_REF                                           # CellReference
    ;

comparisonExpr
    : arithmeticExpr '<=' arithmeticExpr   # LessOrEqual
    | arithmeticExpr '>=' arithmeticExpr   # GreaterOrEqual
    | arithmeticExpr '<>' arithmeticExpr   # NotEqual
    | arithmeticExpr '=' arithmeticExpr    # Equal
    | arithmeticExpr '<' arithmeticExpr    # LessThan
    | arithmeticExpr '>' arithmeticExpr    # GreaterThan
    ;

NUMBER: [0-9]+ ('.' [0-9]+)?;
CELL_REF: [A-Z]+ [0-9]+;

WS: [ \t\r\n]+ -> skip;