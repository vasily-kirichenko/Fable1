module Timer.State

open System
open Elmish
open Types
open Fable.Import.Browser

let init () = "no data yet"

let update (Weather (time, value)) model =
    sprintf "[%O] - %s" time value

let subscribe initial =
    let sub dispatch =
        window.setInterval ((fun _ ->
            dispatch (Weather (DateTime.Now, "foo"))), 1000
        ) |> ignore
    Cmd.ofSub sub