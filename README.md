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

Main
→ **Expr** EOF
→ LET NAME EQ *Expr* IN **Expr** END EOF
→ LET NAME EQ *Expr* IN *Expr* TIMES **Expr** END EOF
→ LET NAME EQ *Expr* IN **Expr** TIMES CSTINT END EOF
→ LET NAME EQ *Expr* IN *Expr* PLUS **Expr** TIMES CSTINT END EOF
→ LET NAME EQ *Expr* IN **Expr** PLUS CSTINT TIMES CSTINT END EOF
→ LET NAME EQ **Expr** IN NAME PLUS CSTINT TIMES CSTINT END EOF
→ LET NAME EQ LPAR **Expr** RPAR IN NAME PLUS CSTINT TIMES CSTINT END EOF
→ LET NAME EQ LPAR CSTINT RPAR IN NAME PLUS CSTINT TIMES CSTINT END EOF

### 3.4

![](derivation.drawio.png)

### 3.5

Done...

### 3.6

```fsharp
let compString s =
    s |> fromString
    |> scomp <| [];;
```

### 3.7

Below is displayed all code that was modified...

**`Absyn.fs`**
```fsharp
type expr = 
  | CstI of int
  | Var of string
  | Let of string * expr * expr
  | If of expr * expr * expr
  | Prim of string * expr * expr
```

**`ExprLex.fsl`**
```fsharp
type expr = 
  | CstI of int
  | Var of string
  | Let of string * expr * expr
  | If of expr * expr * expr
  | Prim of string * expr * expr
```

**`ExprPar.fsy`**
```
Expr:
    NAME                                { Var $1            }
  | CSTINT                              { CstI $1           }
  | MINUS CSTINT                        { CstI (- $2)       }
  | LPAR Expr RPAR                      { $2                }
  | LET NAME EQ Expr IN Expr END        { Let($2, $4, $6)   }
  | IF Expr THEN Expr ELSE Expr         { If($2, $4, $6)    }
  | Expr TIMES Expr                     { Prim("*", $1, $3) }
  | Expr PLUS  Expr                     { Prim("+", $1, $3) }  
  | Expr MINUS Expr                     { Prim("-", $1, $3) } 
;
```
