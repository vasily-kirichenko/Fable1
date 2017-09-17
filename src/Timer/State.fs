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
    { Time = DateTime.Now; Content = None; Url = "http://ya.ru" },
    GetWeather

let update msg (model: Model) : Model * Cmd<Msg> =
    let formatTime (time: DateTime) = time.ToString "dd.MM.yyyy HH:mm:ss"
    match msg with
    | GotWeather (time, value) -> 
        { model with Time = time; Content = Some (Content.Body value) }, []
    | FailedGotWeather (time, error) -> 
        { model with Time = time; Content = Some (Content.Error error) }, []
    | UrlChanged url -> 
        { model with Url = url }, []
    | GetWeather ->
        let f () =
            promise {
                do! Promise.sleep 1000
                let! response = 
                    fetch 
                        model.Url
                        [ RequestProperties.Mode RequestMode.Nocors
                          requestHeaders [ContentType "text/html"] ]

                return! response.text()
            }
        model, 
        Cmd.ofPromise 
            f () 
            (fun x -> Cmd.batch [ GotWeather (DateTime.Now, x); GetWeather ]) 
            (fun e -> Cmd.batch [ FailedGotWeather (DateTime.Now, e.Message); GetWeather ])
