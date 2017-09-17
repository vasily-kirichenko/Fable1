module Timer.State

open System
open Elmish
open Types
open Fable.Import.Browser
open Fable.PowerPack.Fetch
open Fable.PowerPack

let init () = "no data yet"

let update msg model =
    match msg with
    | GotWeather (time, value) -> sprintf "[%O] - %s" time value
    | FailedGotWeather (time, error) -> sprintf "[%O] - ERROR (%s)" time error

let subscribe (initial: Model) : Cmd<Msg> =
    let sub (dispatch: Msg -> unit) : unit =
        window.setInterval ((fun _ ->
            promise {
                let! res = 
                    tryFetch 
                        "http://ya.ru:80" 
                        [ RequestProperties.Mode RequestMode.Nocors ]
                match res with 
                | Ok response ->
                    let! text = response.text()
                    dispatch (GotWeather (DateTime.Now, text))
                | Error (e: exn) ->
                    dispatch (FailedGotWeather (DateTime.Now, e.Message))
            } |> Promise.start), 1000
        ) |> ignore
    
    Cmd.ofSub sub