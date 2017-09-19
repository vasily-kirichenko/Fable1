module App.Types

open Global

type Msg =
    | CounterMsg of Counter.Types.Msg
    | HomeMsg of Home.Types.Msg
    | TimerMsg of Timer.Types.Msg

type Model =
    { currentPage: Page
      counter: Counter.Types.Model
      timer: Timer.Types.Model
      home: Home.Types.Model }
