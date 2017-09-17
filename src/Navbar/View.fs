module Navbar.View

open Fable.Helpers.React
open Fable.Helpers.React.Props

let root =
    nav
        [ ClassName "nav" ]
        [ div
            [ ClassName "nav-left" ]
            [ h1
                [ ClassName "nav-item is-brand title is-4" ]
                [ str "Elmish test" ] ]
        ]