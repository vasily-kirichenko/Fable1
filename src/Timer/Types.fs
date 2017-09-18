module Timer.Types

open System

type Msg = 
    | GotWeather of time: DateTime * content: string * elapsed: TimeSpan
    | FailedGotWeather of time: DateTime * error: string
    | UrlChanged of newUrl: string
    | GetWeather

[<RequireQualifiedAccess>]
type Content = 
    | Body of body: string * elapsed: TimeSpan
    | Error of string

type Model = 
    { Time: DateTime 
      Content: Content option
      Url: string }