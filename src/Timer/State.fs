module Timer.State

open System
open Elmish
open Types
open Fable.Import.Browser
open Fable.PowerPack.Fetch
open Fable.PowerPack

// module private Util =
//     let load<'T> key: 'T option =
//         try Some (Json.ofString (!^ localStorage.getItem(key)))
//         with _ -> None

//     let save key (data: 'T) =
//         Browser.localStorage.setItem(key, JS.JSON.stringify data)

let init () = 
    { Time = DateTime.Now; Content = None; Url = "http://ya.ru" }

let update msg (model: Model) : Model =
    let formatTime (time: DateTime) = time.ToString "dd.MM.yyyy HH:mm:ss"
    match msg with
    | GotWeather (time, value) -> 
        { model with Time = time; Content = Some (Content.Body value) }
    | FailedGotWeather (time, error) -> 
        { model with Time = time; Content = Some (Content.Error error) }
    | UrlChanged url -> 
        { model with Url = url }
    | GetWeather dispatch ->
        promise {
            let! res = 
                tryFetch 
                    model.Url
                    [ RequestProperties.Mode RequestMode.Nocors
                      requestHeaders [ContentType "text/html"] ]

            match res with 
            | Ok response ->
                let! text = response.text()
                dispatch (GotWeather (DateTime.Now, text))
            | Error (e: exn) ->
                dispatch (FailedGotWeather (DateTime.Now, e.Message))
        } |> Promise.start
        model

let subscribe (_initial: Model) : Cmd<Msg> =
    fun (dispatch: Msg -> unit) ->
        window.setInterval ((fun _ -> dispatch (GetWeather dispatch)), 1000) |> ignore
    |> Cmd.ofSub