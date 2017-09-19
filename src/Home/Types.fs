module Home.Types

[<Struct>]
type Model =
    | Model of string

type Msg =
    | ChangeStr of string
