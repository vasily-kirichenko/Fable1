module App.View

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Browser
open Types
open App.State
open Global
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Elmish.React
open Elmish.Debug

importAll "../sass/main.sass"

let menuItem label page currentPage =
    li
      []
      [ a
          [ classList [ "is-active", page = currentPage ]
            Href (toHash page) ]
          [ str label ] ]

let menu currentPage =
  aside
    [ ClassName "menu" ]
    [ p
        [ ClassName "menu-label" ]
        [ str "General" ]
      ul
        [ ClassName "menu-list" ]
        [ menuItem "Home" Home currentPage
          menuItem "Counter sample" Counter currentPage
          menuItem "Timer" Timer currentPage
          menuItem "About" Page.About currentPage ] ]

let root model dispatch =

  let pageHtml =
    function
    | Page.About -> Info.View.root
    | Counter -> Counter.View.root model.counter (CounterMsg >> dispatch)
    | Timer -> Timer.View.root model.timer (TimerMsg >> dispatch) 
    | Home -> Home.View.root model.home (HomeMsg >> dispatch)

  div
    []
    [ div
        [ ClassName "navbar-bg" ]
        [ div
            [ ClassName "container" ]
            [ Navbar.View.root ] 
        ]
      div
        [ ClassName "section" ]
        [ div
            [ ClassName "container" ]
            [ div
                [ ClassName "columns" ]
                [ div
                    [ ClassName "column is-2" ]
                    [ menu model.currentPage ]
                  div
                    [ ClassName "column" ]
                    [ pageHtml model.currentPage ] 
                ] 
            ] 
        ] 
    ]

//let subscription model =
  //Cmd.map TimerMsg (Timer.State.subscribe model.timer)

// App
Program.mkProgram init update root
//|> Program.withSubscription subscription
|> Program.toNavigable (parseHash pageParser) urlUpdate
|> Program.withReact "elmish-app"

#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
