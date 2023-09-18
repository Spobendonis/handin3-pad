# handin3-pad

## PLC

### 3.3

    Write out the rightmost derivation of the string below from the expression grammar at the end of Sect. 3.6.5, corresponding to ExprPar.fsy. Take note
    of the sequence of grammar rules (A–I) used.
    ---
    Main ::= Expr EOF               rule A
    Expr ::= NAME                   rule B
    | CSTINT                        rule C
    | MINUS CSTINT                  rule D
    | LPAR Expr RPAR                rule E
    | LET NAME EQ Expr IN Expr END  rule F
    | Expr TIMES Expr               rule G
    | Expr PLUS Expr                rule H
    | Expr MINUS Expr               rule I

`let z = (17) in z + 2 * 3 end EOF`

#### Confusions

- I don't understand if the number after '#' matters, I will assume not for now

- Furthermore what is a 'rightmost derviation? the book talks about leftmost but not rightmost so I'm unsure

- When reducing let, there is no binding for let -> LET. Assumption is that it should all be done at once

- How is it that z + 2 \* 3 is parsed as z + (2 \* 3)?

Input | Parse Stack | Action
--- | --- | ---
let z = (17) in z + 2 * 3 end EOF | #0 | shift #1
z = (17) in z + 2 * 3 | #0 let #1 | shift #2
= (17) in z + 2 * 3 | #0 let #1 z #2 | reduce B
= (17) in z + 2 * 3 | #0 let NAME | goto #3
= (17) in z + 2 * 3 | #0 let NAME #3 | shift #4
(17) in z + 2 * 3 | #0 let NAME #3 = #4 | shift #5
17\) in z + 2 * 3 | #0 let NAME #3 = #4 \( #5 | shift #6
\) in z + 2 * 3 | #0 let NAME #3 = #4 \( #5 17 # 6 | reduce C
\) in z + 2 * 3 | #0 let NAME #3 = #4 \( #5 EXPR | goto #7
\) in z + 2 * 3 | #0 let NAME #3 = #4 \( #5 EXPR #7| shift #8
in z + 2 * 3 | #0 let NAME #3 = #4 \( #5 EXPR #7 \) #8| reduce E
in z + 2 * 3 | #0 let NAME #3 = EXPR | goto #9
in z + 2 * 3 | #0 let NAME #3 = EXPR #9 | shift #10
z + 2 * 3 | #0 let NAME #3 = EXPR #9 in #10 |

Main ->
Expr EOF ->
LET NAME EQ Expr IN Expr END EOF ->
LET NAME EQ Expr IN Expr TIMES Expr END EOF ->
LET NAME EQ Expr IN Expr TIMES CSTINT END EOF ->
LET NAME EQ Expr IN Expr PLUS Expr TIMES CSTINT END EOF ->
LET NAME EQ Expr IN Expr PLUS CSTINT TIMES CSTINT END EOF ->
LET NAME EQ Expr IN NAME PLUS CSTINT TIMES CSTINT END EOF ->
LET NAME EQ LPAR Expr RPAR IN NAME PLUS CSTINT TIMES CSTINT END EOF ->
LET NAME EQ LPAR CSTINT RPAR IN NAME PLUS CSTINT TIMES CSTINT END EOF

### 3.4

### 3.5

### 3.6

### 3.7
