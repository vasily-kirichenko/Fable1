module Timer.Types

open System

type Msg = 
    | GotWeather of DateTime * string
    | FailedGotWeather of DateTime * string

type Model = string