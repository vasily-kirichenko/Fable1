module Timer.Types

open System

type Msg = 
    | GotWeather of DateTime * string
    | FailedGotWeather of DateTime * string
    | UrlChanged of string
    | GetWeather

[<RequireQualifiedAccess>]
type Content = 
    | Body of string 
    | Error of string

type Model = 
    { Time: DateTime 
      Content: Content option
      Url: string }