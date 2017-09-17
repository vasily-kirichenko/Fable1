module Timer.View

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Types

let root (model: Model) dispatch =
  div
    [ ]
    [ span
        [ ]
        [ str model ] 
    ]