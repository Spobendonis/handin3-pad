module Exercises

open Absyn
open Expr
open Parse

let compString s =
    s |> fromString
    |> scomp <| [];;