module Timer.State

open System
open System.Diagnostics
open Elmish
open Types
open Fable.Import
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
    { Time = DateTime.Now
      Content = None
      Url = "http://localhost:8080" },
    Cmd.ofMsg GetWeather

let getWeather url =
    let period = TimeSpan.FromSeconds 1.
    promise {
        let start = DateTime.UtcNow
        let! response = fetch url [ RequestProperties.Mode RequestMode.Nocors ]
        let! text = response.text()
        let elapsed = DateTime.UtcNow - start
        match period - elapsed with
        | x when x <= TimeSpan.Zero -> ()
        | x -> do! Promise.sleep (int x.TotalMilliseconds)
        return text, elapsed
    }

let update msg (model: Model) : Model * Cmd<Msg> =
    let formatTime (time: DateTime) = time.ToString "dd.MM.yyyy HH:mm:ss"
    match msg with
    | GotWeather (time, value, elapsed) -> 
        { model with Time = time; Content = Some (Content.Body (value, elapsed)) }, Cmd.ofMsg GetWeather
    | FailedGotWeather (time, error) -> 
        { model with Time = time; Content = Some (Content.Error error) }, Cmd.ofMsg GetWeather
    | UrlChanged url -> { model with Url = url }, []
    | GetWeather ->
        model, 
        Cmd.ofPromise 
            getWeather model.Url 
            (fun (x, elapsed) -> GotWeather (DateTime.Now, x, elapsed)) 
            (fun e -> FailedGotWeather (DateTime.Now, e.Message))
        
